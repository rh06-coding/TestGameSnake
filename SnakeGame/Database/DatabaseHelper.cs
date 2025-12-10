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

        static DatabaseHelper()
        {
            try
            {
                // Set DataDirectory to app folder
                string appPath = AppDomain.CurrentDomain.BaseDirectory;
                AppDomain.CurrentDomain.SetData("DataDirectory", appPath);

                connectionString = ConfigurationManager.ConnectionStrings["SnakeGameDB"]?.ConnectionString;
                
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException(
                        "Connection string 'SnakeGameDB' không ???c tìm th?y trong App.config.");
                }

                // Connection string to master database for creating new database
                masterConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30";

                // T? ??ng t?o database n?u ch?a có
                InitializeDatabase();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Database initialization error: {ex.Message}");
                throw new InvalidOperationException(
                    "L?i kh?i t?o database: " + ex.Message, ex);
            }
        }

        private static void InitializeDatabase()
        {
            try
            {
                string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "QuanLyNguoiChoi.mdf");
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "QuanLyNguoiChoi_log.ldf");
                
                // N?u database ch?a t?n t?i, t?o m?i
                if (!File.Exists(dbPath))
                {
                    System.Diagnostics.Debug.WriteLine("Database không t?n t?i. ?ang t?o database m?i...");
                    CreateDatabase(dbPath, logPath);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Database ?ã t?n t?i t?i: {dbPath}");
                    // Ki?m tra và t?o b?ng n?u ch?a có
                    EnsureTablesExist();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Initialize database error: {ex.Message}");
            }
        }

        private static void CreateDatabase(string dbPath, string logPath)
        {
            try
            {
                // B??c 1: T?o database file b?ng cách k?t n?i master
                using (SqlConnection conn = new SqlConnection(masterConnectionString))
                {
                    conn.Open();
                    
                    // T?o database v?i file path c? th?
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

                // B??c 2: T?o b?ng trong database m?i
                CreateTables();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Create database error: {ex.Message}");
                // N?u database ?ã t?n t?i, b? qua l?i
                if (!ex.Message.Contains("already exists"))
                {
                    throw;
                }
                else
                {
                    // Database ?ã t?n t?i, ch? c?n t?o b?ng
                    CreateTables();
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

                    // T?o b?ng TAIKHOAN
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

                            -- T?o tài kho?n m?c ??nh (username: admin, password: admin - ?ã hash SHA256)
                            INSERT INTO TAIKHOAN (username, matkhau, email, HighestScore)
                            VALUES ('admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 'admin@snakegame.com', 0);
                        END
                    ";

                    using (SqlCommand cmd = new SqlCommand(createTaiKhoanTable, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // T?o b?ng SCORES
                    string createScoresTable = @"
                        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SCORES')
                        BEGIN
                            CREATE TABLE SCORES (
                                score_ID INT PRIMARY KEY IDENTITY(1,1),
                                player_ID INT NOT NULL,
                                score INT NOT NULL,
                                AchievedAt DATETIME DEFAULT GETDATE(),
                                CONSTRAINT FK_SCORES_TAIKHOAN FOREIGN KEY (player_ID) 
                                    REFERENCES TAIKHOAN(player_ID)
                            );
                        END
                    ";

                    using (SqlCommand cmd = new SqlCommand(createScoresTable, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    System.Diagnostics.Debug.WriteLine("Database tables created successfully with default account (username: admin, password: admin)");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Create tables error: {ex.Message}");
                throw;
            }
        }

        private static void EnsureTablesExist()
        {
            try
            {
                CreateTables();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ensure tables exist error: {ex.Message}");
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
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Database connection error: {ex.Message}");
                System.Windows.Forms.MessageBox.Show(
                    $"Không th? k?t n?i database!\n\nL?i: {ex.Message}\n\n" +
                    "Vui lòng ki?m tra:\n" +
                    "1. Visual Studio ?ã cài ??t SQL Server LocalDB\n" +
                    "2. Có quy?n ghi file trong th? m?c ?ng d?ng\n\n" +
                    "?? kh?c ph?c, m? Command Prompt (Run as Admin) và ch?y:\n" +
                    "sqllocaldb create MSSQLLocalDB\n" +
                    "sqllocaldb start MSSQLLocalDB",
                    "L?i k?t n?i Database",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
