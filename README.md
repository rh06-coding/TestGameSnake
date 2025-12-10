# Snake Game ??

Game r?n s?n m?i c? ?i?n ???c vi?t b?ng C# Windows Forms v?i .NET Framework 4.8

## ? Tính n?ng

- ?? Gameplay r?n s?n m?i c? ?i?n
- ?? H? th?ng ??ng ký/??ng nh?p tài kho?n
- ?? L?u ?i?m cao nh?t
- ?? Nhi?u màu r?n và background map
- ?? Tính n?ng pause game
- ?? SQL Server LocalDB (t? ??ng t?o database)

## ?? H??ng d?n ch?y

### Yêu c?u h? th?ng
- Windows 7 tr? lên
- .NET Framework 4.8
- Visual Studio 2019 tr? lên (có s?n SQL Server LocalDB)

### Cách ch?y

1. **Clone repository:**
```bash
git clone https://github.com/rh06-coding/TestGameSnake.git
cd TestGameSnake
```

2. **M? solution trong Visual Studio:**
   - M? file `SnakeGame.sln`
   - Build solution (__Ctrl + Shift + B__)
   - Ch?y ?ng d?ng (__F5__)

3. **??ng nh?p:**
   - **Tài kho?n m?c ??nh:**
     - Username: `admin`
     - Password: `admin`
   - Ho?c ??ng ký tài kho?n m?i

**L?u ý:** 
- Database `QuanLyNguoiChoi.mdf` s? t? ??ng t?o trong th? m?c `bin\Debug` l?n ??u ch?y
- Visual Studio 2019+ ?ã bao g?m SQL Server LocalDB
- N?u g?p l?i LocalDB, xem ph?n **Kh?c ph?c l?i** bên d??i

## ?? Cách ch?i

- **Phím di chuy?n:** W/A/S/D ho?c phím m?i tên ?/?/?/?
- **Space:** B?t ??u game
- **ESC:** T?m d?ng game
- **R:** Ch?i l?i (khi game over)
- **E:** Quay v? menu (khi game over)

## ??? Database

Project s? d?ng **SQL Server LocalDB**:
- File database: `QuanLyNguoiChoi.mdf` (t? ??ng t?o trong `bin\Debug`)
- Tài kho?n m?c ??nh: `admin/admin`
- Có th? xóa file `.mdf` và `.ldf` ?? reset database

### C?u trúc Database

**B?ng TAIKHOAN:**
- `player_ID` (INT, Primary Key, Identity)
- `username` (NVARCHAR(50), Unique)
- `matkhau` (NVARCHAR(256)) - Mã hóa SHA256
- `email` (NVARCHAR(255), Unique)
- `JoinDate` (DATETIME)
- `HighestScore` (INT)

**B?ng SCORES:**
- `score_ID` (INT, Primary Key, Identity)
- `player_ID` (INT, Foreign Key)
- `score` (INT)
- `AchievedAt` (DATETIME)

### Kh?c ph?c l?i LocalDB

N?u g?p l?i không k?t n?i ???c LocalDB:

**B??c 1: Ki?m tra LocalDB ?ã cài ??t:**
```cmd
sqllocaldb info
```

**B??c 2: T?o và kh?i ??ng instance (n?u ch?a có):**
```cmd
# M? Command Prompt v?i quy?n Administrator
sqllocaldb create MSSQLLocalDB
sqllocaldb start MSSQLLocalDB
```

**B??c 3: Ki?m tra tr?ng thái:**
```cmd
sqllocaldb info MSSQLLocalDB
```

**N?u v?n l?i, cài ??t l?i LocalDB:**
1. T?i SQL Server Express LocalDB: https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb
2. Ho?c cài qua Visual Studio Installer: __Individual Components > SQL Server Express 2019 LocalDB__

## ?? C?u trúc Project

```
SnakeGame/
??? Forms/              # Các form giao di?n
?   ??? StartForm.cs    # Màn hình ??ng nh?p
?   ??? SignInForm.cs   # Màn hình ??ng ký
?   ??? MenuForm.cs     # Menu chính
?   ??? ForgotPasswordForm.cs
??? Database/           # L?p x? lý database
?   ??? DatabaseHelper.cs
?   ??? TaiKhoanRepository.cs
?   ??? ScoreRepository.cs
?   ??? SessionManager.cs
??? Models/             # Các model d? li?u
?   ??? Snake.cs
?   ??? Food.cs
?   ??? GameState.cs
?   ??? TaiKhoan.cs
?   ??? Score.cs
??? Services/           # Game engine
?   ??? NewGameEngine.cs
??? Properties/         # Resources (hình ?nh, icons)
??? App.config          # C?u hình connection string
```

## ??? Công ngh? s? d?ng

- **Framework:** .NET Framework 4.8
- **UI:** Windows Forms
- **Database:** SQL Server LocalDB
- **ORM:** ADO.NET (SqlConnection, SqlCommand)
- **Security:** SHA256 Password Hashing

## ?? Tính n?ng n?i b?t

### Game Engine
- Input buffering - cho phép l?u 2 l?nh ?i?u khi?n
- Collision detection t?i ?u v?i HashSet
- Frame-independent movement
- Pause/Resume functionality

### Database
- T? ??ng t?o database và b?ng khi ch?a t?n t?i
- H? tr? LocalDB - không c?n SQL Server
- Password hashing v?i SHA256
- Session management

### UI/UX
- Multiple snake colors (Green, Red, Blue)
- Multiple background maps
- Pause menu
- Smooth animations

## ?? ?óng góp

M?i ?óng góp ??u ???c chào ?ón! Hãy t?o Pull Request ho?c Issue n?u b?n có ý t??ng c?i thi?n.

### Cách ?óng góp:
1. Fork repository
2. T?o branch m?i (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to branch (`git push origin feature/AmazingFeature`)
5. M? Pull Request

## ?? License

MIT License - T? do s? d?ng cho m?c ?ích cá nhân và h?c t?p.

## ????? Tác gi?

GitHub: [@rh06-coding](https://github.com/rh06-coding)

## ?? Liên h?

N?u có v?n ?? ho?c câu h?i, vui lòng t?o [Issue](https://github.com/rh06-coding/TestGameSnake/issues) trên GitHub.

---

**Phiên b?n:** 1.0.0  
**Ngày c?p nh?t:** 2025
