
-- ============================================================
--  ApartmentExpense — Local Database Setup
--  Run this once to get started
--  SQL Server Express (localhost\SQLEXPRESS)
-- ============================================================

-- CREATE DATABASE ApartmentExpenseDB;
-- GO

-- USE ApartmentExpenseDB;
-- GO

-- ── Tables ────────────────────────────────────────────────────

-- CREATE TABLE Users (
--     UserID   UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
--     Name     NVARCHAR(100) NOT NULL,
--     Email    NVARCHAR(150) NOT NULL UNIQUE,
--     Password NVARCHAR(255) NOT NULL,
--     Role     NVARCHAR(10)  NOT NULL DEFAULT 'User'
--              CHECK (Role IN ('Admin', 'User')),
--     Status   NVARCHAR(10)  NOT NULL DEFAULT 'Pending'
--              CHECK (Status IN ('Pending', 'Approved', 'Blocked')),
--     JoinedAt DATETIME      NOT NULL DEFAULT GETDATE()
-- );
-- GO

-- CREATE TABLE Categories (
--     CategoryID   UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
--     CategoryName NVARCHAR(100) NOT NULL UNIQUE,
--     Icon         NVARCHAR(10)  NOT NULL DEFAULT '🏷',
--     Description  NVARCHAR(255) NULL
-- );
-- GO

-- CREATE TABLE Expenses (
--     ExpenseID   UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
--     Title       NVARCHAR(200)  NOT NULL,
--     Amount      DECIMAL(18,2)  NOT NULL CHECK (Amount > 0),
--     ExpenseDate DATE           NOT NULL,
--     CategoryID  UNIQUEIDENTIFIER NOT NULL
--                 FOREIGN KEY REFERENCES Categories(CategoryID),
--     UserID      UNIQUEIDENTIFIER NOT NULL
--                 FOREIGN KEY REFERENCES Users(UserID),
--     CreatedAt   DATETIME NOT NULL DEFAULT GETDATE()
-- );
-- GO

-- ── Seed Data ─────────────────────────────────────────────────

-- -- 1 Admin
-- INSERT INTO Users (Name, Email, Password, Role, Status)
-- VALUES ('Admin', 'admin@example.com', 'admin123', 'Admin', 'Approved');
-- GO

-- -- 1 Regular User
-- INSERT INTO Users (Name, Email, Password, Role, Status)
-- VALUES ('Test User', 'user@example.com', 'user123', 'User', 'Approved');
-- GO

-- -- Categories
-- INSERT INTO Categories (CategoryName, Icon, Description) VALUES
-- ('Materials',  '🧱', 'Building materials'),
-- ('Labour',     '👷', 'Workforce costs'),
-- ('Equipment',  '🔧', 'Machinery rental'),
-- ('Electrical', '⚡', 'Electrical work'),
-- ('Fixtures',   '🪟', 'Doors and windows'),
-- ('Plumbing',   '🚿', 'Plumbing systems'),
-- ('Transport',  '🚛', 'Transportation'),
-- ('Other',      '📋', 'Miscellaneous');
-- GO

-- -- Sample Expenses
-- INSERT INTO Expenses (Title, Amount, ExpenseDate, CategoryID, UserID)
-- SELECT 'Cement bags - Block A', 45000.00, '2025-01-15',
--     (SELECT CategoryID FROM Categories WHERE CategoryName = 'Materials'),
--     (SELECT UserID FROM Users WHERE Email = 'user@example.com');

-- INSERT INTO Expenses (Title, Amount, ExpenseDate, CategoryID, UserID)
-- SELECT 'Labour - Foundation', 80000.00, '2025-01-16',
--     (SELECT CategoryID FROM Categories WHERE CategoryName = 'Labour'),
--     (SELECT UserID FROM Users WHERE Email = 'user@example.com');

-- INSERT INTO Expenses (Title, Amount, ExpenseDate, CategoryID, UserID)
-- SELECT 'Crane Rental', 120000.00, '2025-01-18',
--     (SELECT CategoryID FROM Categories WHERE CategoryName = 'Equipment'),
--     (SELECT UserID FROM Users WHERE Email = 'admin@example.com');
-- GO

-- ── Verify ────────────────────────────────────────────────────
-- SELECT 'Users'      AS TableName, COUNT(*) AS Rows FROM Users
-- UNION ALL
-- SELECT 'Categories' AS TableName, COUNT(*) AS Rows FROM Categories
-- UNION ALL
-- SELECT 'Expenses'   AS TableName, COUNT(*) AS Rows FROM Expenses;
-- GO

UPDATE Users SET Status = 'Pending' where Email = 'rahima@corp.bd';
-- select * from users;
-- delete from Users where Email like 'fahmudul@';
