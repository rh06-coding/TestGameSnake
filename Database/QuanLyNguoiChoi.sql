
USE master;
GO

-- TAIKHOAN
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
    PRINT 'Tạo Bảng Thành Công';
END
GO

-- SCORES
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
    PRINT 'Tạo Bảng Scores Thành Công';
END
GO

-- Tạo tài khoản mặc định
IF NOT EXISTS (SELECT * FROM TAIKHOAN WHERE username = 'admin')
BEGIN
    -- Password: admin (SHA256 hash)
    INSERT INTO TAIKHOAN (username, matkhau, email, HighestScore)
    VALUES ('admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 'admin@snakegame.com', 0);
    PRINT 'Tài khoản admin đã được tạo (username: admin, password: admin)';
END
GO

-- Xem Dữ liệu
SELECT * FROM TAIKHOAN;
SELECT * FROM SCORES;
GO
