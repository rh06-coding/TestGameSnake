using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace SnakeGame.Database
{
    public class DatabaseHelper
    {
        private static string connectionString;
        private static string masterConnectionString;
        private static bool useLocalDb = true;

        static DatabaseHelper()
        {
            try
            {
                string appPath = AppDomain.CurrentDomain.BaseDirectory;
                AppDomain.CurrentDomain.SetData("DataDirectory", appPath);

                connectionString = ConfigurationManager.ConnectionStrings["SnakeGameDB"]?.ConnectionString;
                
                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = ConfigurationManager.ConnectionStrings["SnakeGame.Properties.Settings.QuanLyNguoiChoiConnectionString"]?.ConnectionString;
                }

                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|QuanLyNguoiChoi.mdf;Integrated Security=True;Connect Timeout=30";
                    System.Diagnostics.Debug.WriteLine("Using default LocalDB connection string");
                }

                connectionString = connectionString.Replace("|DataDirectory|", appPath).Replace("\\\\", "\\");

                useLocalDb = connectionString.Contains("LocalDB") || connectionString.Contains("AttachDbFilename");
                
                if (useLocalDb)
                {
                    masterConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30";
                }
                else
                {
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
                    builder.InitialCatalog = "master";
                    masterConnectionString = builder.ConnectionString;
                }

                InitializeDatabase();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Database initialization error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }

        private static void InitializeDatabase()
        {
            try
            {
                if (useLocalDb)
                {
                    string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "QuanLyNguoiChoi.mdf");
                    string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "QuanLyNguoiChoi_log.ldf");
                    
                    if (!File.Exists(dbPath))
                    {
                        System.Diagnostics.Debug.WriteLine("Database không tồn tại. Đang tạo database mới...");
                        CreateLocalDatabase(dbPath, logPath);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Database đã tồn tại tại: {dbPath}");
                        DetachExistingDatabase();
                        EnsureTablesExist();
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Using SQL Server instance, checking tables...");
                    EnsureTablesExist();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Initialize database error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }

        private static void DetachExistingDatabase()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(masterConnectionString))
                {
                    conn.Open();
                    
                    string checkDbQuery = "SELECT COUNT(*) FROM sys.databases WHERE name = 'QuanLyNguoiChoi'";
                    using (SqlCommand checkCmd = new SqlCommand(checkDbQuery, conn))
                    {
                        int dbExists = (int)checkCmd.ExecuteScalar();
                        
                        if (dbExists > 0)
                        {
                            System.Diagnostics.Debug.WriteLine("Detaching existing database...");
                            
                            string detachQuery = @"
                                ALTER DATABASE QuanLyNguoiChoi SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                                EXEC sp_detach_db 'QuanLyNguoiChoi', 'true';
                            ";
                            
                            using (SqlCommand detachCmd = new SqlCommand(detachQuery, conn))
                            {
                                detachCmd.ExecuteNonQuery();
                            }
                            
                            System.Diagnostics.Debug.WriteLine("Database detached successfully");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Detach database warning: {ex.Message}");
            }
        }

        private static void CreateLocalDatabase(string dbPath, string logPath)
        {
            try
            {
                DetachExistingDatabase();
                
                using (SqlConnection conn = new SqlConnection(masterConnectionString))
                {
                    conn.Open();
                    
                    string createDbQuery = $@"
                        CREATE DATABASE QuanLyNguoiChoi
                        ON PRIMARY (
                            NAME = QuanLyNguoiChoi_Data,
                            FILENAME = '{dbPath}',
                            SIZE = 10MB,
                            FILEGROWTH = 5MB
                        )
                        LOG ON (
                            NAME = QuanLyNguoiChoi_Log,
                            FILENAME = '{logPath}',
                            SIZE = 5MB,
                            FILEGROWTH = 5MB
                        )
                    ";

                    using (SqlCommand cmd = new SqlCommand(createDbQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    System.Diagnostics.Debug.WriteLine("Database file created successfully");
                }

                System.Threading.Thread.Sleep(1000);
                
                CreateTables();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Create database error: {ex.Message}");
                
                if (ex.Message.Contains("already exists") || File.Exists(dbPath))
                {
                    System.Diagnostics.Debug.WriteLine("Database already exists, ensuring tables...");
                    try
                    {
                        CreateTables();
                    }
                    catch (Exception innerEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"Failed to create tables after database exists: {innerEx.Message}");
                    }
                }
                else
                {
                    throw;
                }
            }
        }

        private static void CreateTables()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();

                    string createTaiKhoanTable = @"
                        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TAIKHOAN')
                        BEGIN
                            CREATE TABLE TAIKHOAN (
                                player_ID INT PRIMARY KEY IDENTITY(1,1),
                                username NVARCHAR(50) UNIQUE NOT NULL,
                                matkhau NVARCHAR(256) NOT NULL,
                                email NVARCHAR(255) UNIQUE NOT NULL,
                                JoinDate DATETIME DEFAULT GETDATE(),
                                HighestScore INT DEFAULT 0
                            );

                            INSERT INTO TAIKHOAN (username, matkhau, email, HighestScore)
                            VALUES ('admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 'admin@snakegame.com', 0);
                            
                            PRINT 'Bảng TAIKHOAN đã được tạo với tài khoản admin mặc định';
                        END
                    ";

                    using (SqlCommand cmd = new SqlCommand(createTaiKhoanTable, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    string createScoresTable = @"
                        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SCORES')
                        BEGIN
                            CREATE TABLE SCORES (
                                score_ID INT PRIMARY KEY IDENTITY(1,1),
                                player_ID INT NOT NULL,
                                score INT NOT NULL,
                                AchievedAt DATETIME DEFAULT GETDATE(),
                                CONSTRAINT FK_SCORES_TAIKHOAN FOREIGN KEY (player_ID) 
                                    REFERENCES TAIKHOAN(player_ID) ON DELETE CASCADE
                            );
                            
                            PRINT 'Bảng SCORES đã được tạo';
                        END
                    ";

                    using (SqlCommand cmd = new SqlCommand(createScoresTable, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    System.Diagnostics.Debug.WriteLine("Database tables created successfully");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Create tables error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        private static void EnsureTablesExist()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();

                    string checkTables = @"
                        SELECT COUNT(*) FROM sys.tables 
                        WHERE name IN ('TAIKHOAN', 'SCORES')
                    ";

                    using (SqlCommand cmd = new SqlCommand(checkTables, conn))
                    {
                        int tableCount = (int)cmd.ExecuteScalar();
                        
                        if (tableCount < 2)
                        {
                            System.Diagnostics.Debug.WriteLine($"Chỉ tìm thấy {tableCount} bảng, đang tạo bảng còn thiếu...");
                            CreateTables();
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Tất cả bảng đã tồn tại");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ensure tables exist error: {ex.Message}");
                
                try
                {
                    CreateTables();
                }
                catch (Exception innerEx)
                {
                    System.Diagnostics.Debug.WriteLine($"Failed to create tables: {innerEx.Message}");
                }
            }
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    
                    string testQuery = "SELECT COUNT(*) FROM sys.tables WHERE name = 'TAIKHOAN'";
                    using (SqlCommand cmd = new SqlCommand(testQuery, conn))
                    {
                        int count = (int)cmd.ExecuteScalar();
                        if (count == 0)
                        {
                            System.Diagnostics.Debug.WriteLine("Bảng TAIKHOAN chưa tồn tại, đang tạo...");
                            conn.Close();
                            CreateTables();
                        }
                    }
                    
                    System.Diagnostics.Debug.WriteLine("Kết nối database thành công!");
                    return true;
                }
            }
            catch (SqlException sqlEx)
            {
                System.Diagnostics.Debug.WriteLine($"SQL Error: {sqlEx.Message}");
                System.Diagnostics.Debug.WriteLine($"Error Number: {sqlEx.Number}");
                
                string errorMessage = "Không thể kết nối database!\n\n";
                errorMessage += $"Lỗi: {sqlEx.Message}\n\n";
                
                if (useLocalDb)
                {
                    errorMessage += "Vui lòng kiểm tra:\n";
                    errorMessage += "1. Visual Studio đã cài đặt SQL Server LocalDB\n";
                    errorMessage += "2. Có quyền ghi file trong thư mục ứng dụng\n\n";
                    errorMessage += "Để khắc phục, mở Command Prompt (Run as Admin) và chạy:\n";
                    errorMessage += "sqllocaldb create MSSQLLocalDB\n";
                    errorMessage += "sqllocaldb start MSSQLLocalDB";
                }
                else
                {
                    errorMessage += "Vui lòng kiểm tra:\n";
                    errorMessage += "1. SQL Server đang chạy\n";
                    errorMessage += "2. Connection string trong App.config đúng\n";
                    errorMessage += "3. Database 'QuanLyNguoiChoi' đã được tạo\n";
                    errorMessage += "4. Tài khoản Windows có quyền truy cập database";
                }
                
                System.Windows.Forms.MessageBox.Show(
                    errorMessage,
                    "Lỗi kết nối Database",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Database connection error: {ex.Message}");
                System.Windows.Forms.MessageBox.Show(
                    $"Lỗi không xác định khi kết nối database!\n\n{ex.Message}",
                    "Lỗi Database",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
        }

        public static string GetConnectionInfo()
        {
            return $"Connection String: {connectionString}\nMode: {(useLocalDb ? "LocalDB" : "SQL Server")}";
        }
    }
}
