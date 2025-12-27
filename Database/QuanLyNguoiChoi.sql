-- =============================================
-- DATABASE: QuanLyNguoiChoi
-- Mục đích: Quản lý người chơi và điểm số cho Snake Game
-- Server: (LocalDB)\MSSQLLocalDB
-- =============================================

USE master;
GO

-- Tạo database nếu chưa tồn tại
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'QuanLyNguoiChoi')
BEGIN
    CREATE DATABASE QuanLyNguoiChoi;
    PRINT '✓ Database QuanLyNguoiChoi đã được tạo!';
END
ELSE
BEGIN
    PRINT '- Database QuanLyNguoiChoi đã tồn tại.';
END
GO

USE QuanLyNguoiChoi;
GO

-- =============================================
-- BẢNG TAIKHOAN
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TAIKHOAN')
BEGIN
    CREATE TABLE TAIKHOAN (
        player_ID INT PRIMARY KEY IDENTITY(1,1),
        username NVARCHAR(50) UNIQUE NOT NULL,
        matkhau NVARCHAR(256) NOT NULL,
        email NVARCHAR(255) UNIQUE NOT NULL,
        JoinDate DATETIME DEFAULT GETDATE(),
        HighestScore INT DEFAULT 0,
        CONSTRAINT CK_HighestScore CHECK (HighestScore >= 0)
    );
    PRINT '✓ Bảng TAIKHOAN đã được tạo';
END
ELSE
BEGIN
    PRINT '- Bảng TAIKHOAN đã tồn tại';
END
GO

-- =============================================
-- BẢNG SCORES
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SCORES')
BEGIN
    CREATE TABLE SCORES (
        score_ID INT PRIMARY KEY IDENTITY(1,1),
        player_ID INT NOT NULL,
        score INT NOT NULL,
        AchievedAt DATETIME DEFAULT GETDATE(),
        CONSTRAINT FK_SCORES_TAIKHOAN FOREIGN KEY (player_ID) 
            REFERENCES TAIKHOAN(player_ID) ON DELETE CASCADE,
        CONSTRAINT CK_Score CHECK (score >= 0)
    );
    PRINT '✓ Bảng SCORES đã được tạo';
END
ELSE
BEGIN
    PRINT '- Bảng SCORES đã tồn tại';
END
GO

-- =============================================
-- TẠO INDEX
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SCORES_PlayerID' AND object_id = OBJECT_ID('SCORES'))
BEGIN
    CREATE INDEX IX_SCORES_PlayerID ON SCORES(player_ID);
    PRINT '✓ Index IX_SCORES_PlayerID đã được tạo';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_TAIKHOAN_Username' AND object_id = OBJECT_ID('TAIKHOAN'))
BEGIN
    CREATE INDEX IX_TAIKHOAN_Username ON TAIKHOAN(username);
    PRINT '✓ Index IX_TAIKHOAN_Username đã được tạo';
END
GO

-- =============================================
-- DỮ LIỆU MẪU
-- =============================================

-- Tài khoản admin (password: admin - SHA256)
IF NOT EXISTS (SELECT * FROM TAIKHOAN WHERE username = 'admin')
BEGIN
    INSERT INTO TAIKHOAN (username, matkhau, email, HighestScore)
    VALUES ('admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 'admin@snakegame.com', 0);
    PRINT '✓ Tài khoản admin đã được tạo (password: admin)';
END
GO

-- Tài khoản test (password: 123456 - SHA256)
IF NOT EXISTS (SELECT * FROM TAIKHOAN WHERE username = 'player1')
BEGIN
    INSERT INTO TAIKHOAN (username, matkhau, email, HighestScore)
    VALUES 
        ('player1', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'player1@test.com', 150),
        ('player2', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'player2@test.com', 200),
        ('player3', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'player3@test.com', 100);
    PRINT '✓ Tài khoản test đã được tạo (password: 123456)';
END
GO

-- Điểm mẫu
IF NOT EXISTS (SELECT * FROM SCORES WHERE player_ID = 2)
BEGIN
    INSERT INTO SCORES (player_ID, score, AchievedAt)
    VALUES 
        (2, 120, DATEADD(day, -5, GETDATE())),
        (2, 150, DATEADD(day, -3, GETDATE())),
        (3, 180, DATEADD(day, -4, GETDATE())),
        (3, 200, DATEADD(day, -2, GETDATE())),
        (4, 90, DATEADD(day, -6, GETDATE())),
        (4, 100, DATEADD(day, -1, GETDATE()));
    PRINT '✓ Dữ liệu điểm mẫu đã được thêm';
END
GO

-- =============================================
-- STORED PROCEDURES
-- =============================================

-- SP: Lấy thống kê người chơi
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_GetPlayerStatistics')
    DROP PROCEDURE sp_GetPlayerStatistics;
GO

CREATE PROCEDURE sp_GetPlayerStatistics
    @playerID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        t.username, t.email, t.JoinDate, t.HighestScore,
        COUNT(s.score_ID) AS TotalGames,
        ISNULL(AVG(CAST(s.score AS FLOAT)), 0) AS AverageScore,
        ISNULL(MIN(s.score), 0) AS LowestScore,
        DATEDIFF(day, t.JoinDate, GETDATE()) AS DaysSinceJoined
    FROM TAIKHOAN t
    LEFT JOIN SCORES s ON t.player_ID = s.player_ID
    WHERE t.player_ID = @playerID
    GROUP BY t.username, t.email, t.JoinDate, t.HighestScore;
END
GO

-- SP: Lấy bảng xếp hạng
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_GetLeaderboard')
    DROP PROCEDURE sp_GetLeaderboard;
GO

CREATE PROCEDURE sp_GetLeaderboard
    @topN INT = 10
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP (@topN)
        ROW_NUMBER() OVER (ORDER BY HighestScore DESC, JoinDate ASC) AS Rank,
        username, HighestScore, JoinDate,
        (SELECT COUNT(*) FROM SCORES WHERE player_ID = t.player_ID) AS TotalGames
    FROM TAIKHOAN t
    WHERE HighestScore > 0
    ORDER BY HighestScore DESC, JoinDate ASC;
END
GO

-- SP: Lưu điểm và kiểm tra kỷ lục
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_SaveScore')
    DROP PROCEDURE sp_SaveScore;
GO

CREATE PROCEDURE sp_SaveScore
    @playerID INT,
    @score INT,
    @isNewRecord BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @currentHighest INT;
    
    SELECT @currentHighest = HighestScore FROM TAIKHOAN WHERE player_ID = @playerID;
    INSERT INTO SCORES (player_ID, score) VALUES (@playerID, @score);
    
    IF @score > @currentHighest
    BEGIN
        UPDATE TAIKHOAN SET HighestScore = @score WHERE player_ID = @playerID;
        SET @isNewRecord = 1;
    END
    ELSE
        SET @isNewRecord = 0;
END
GO

-- =============================================
-- VIEWS
-- =============================================

-- View: Thống kê người chơi
IF EXISTS (SELECT * FROM sys.views WHERE name = 'vw_PlayerStatistics')
    DROP VIEW vw_PlayerStatistics;
GO

CREATE VIEW vw_PlayerStatistics AS
SELECT 
    t.player_ID, t.username, t.email, t.JoinDate, t.HighestScore,
    COUNT(s.score_ID) AS TotalGames,
    ISNULL(AVG(CAST(s.score AS FLOAT)), 0) AS AverageScore,
    ISNULL(MIN(s.score), 0) AS LowestScore,
    DATEDIFF(day, t.JoinDate, GETDATE()) AS DaysSinceJoined
FROM TAIKHOAN t
LEFT JOIN SCORES s ON t.player_ID = s.player_ID
GROUP BY t.player_ID, t.username, t.email, t.JoinDate, t.HighestScore;
GO

-- View: Bảng xếp hạng
IF EXISTS (SELECT * FROM sys.views WHERE name = 'vw_Leaderboard')
    DROP VIEW vw_Leaderboard;
GO

CREATE VIEW vw_Leaderboard AS
SELECT 
    ROW_NUMBER() OVER (ORDER BY HighestScore DESC, JoinDate ASC) AS Rank,
    username, HighestScore, JoinDate,
    (SELECT COUNT(*) FROM SCORES WHERE player_ID = t.player_ID) AS TotalGames
FROM TAIKHOAN t
WHERE HighestScore > 0;
GO

-- =============================================
-- KIỂM TRA KẾT QUẢ
-- =============================================
PRINT '';
PRINT '========================================';
PRINT 'CÀI ĐẶT DATABASE HOÀN TẤT!';
PRINT '========================================';
PRINT '';

-- Xem dữ liệu
SELECT * FROM TAIKHOAN;
SELECT * FROM SCORES;
SELECT * FROM vw_Leaderboard;

PRINT '';
PRINT 'TÀI KHOẢN MẶC ĐỊNH:';
PRINT '  admin    / admin';
PRINT '  player1  / 123456';
PRINT '  player2  / 123456';
PRINT '  player3  / 123456';
GO

-- ===============================
-- Cập nhật cấu trúc bảng SCORES
-- ===============================
-- Thêm cột mapType vào bảng SCORES
ALTER TABLE SCORES
ADD mapType INT NOT NULL DEFAULT 1;
GO

-- Thêm constraint để đảm bảo mapType hợp lệ (1 hoặc 2)
ALTER TABLE SCORES
ADD CONSTRAINT CK_MapType CHECK (mapType IN (1, 2));
GO

-- Tạo index để tăng tốc query theo map
CREATE INDEX IX_SCORES_MapType ON SCORES(mapType, score DESC);
GO
