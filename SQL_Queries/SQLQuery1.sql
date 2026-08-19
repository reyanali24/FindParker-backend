CREATE TABLE Users (
    UserId        BIGINT IDENTITY(1,1) PRIMARY KEY,
    FirebaseUid   VARCHAR(128) NOT NULL UNIQUE,
    Email         VARCHAR(255) NOT NULL UNIQUE,
    AuthProvider  VARCHAR(10) NOT NULL CHECK (AuthProvider IN ('email','google')),
    IsActive      BIT NOT NULL DEFAULT 1,
    CreatedAt     DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt     DATETIME NOT NULL DEFAULT GETDATE(),
    LastLoginAt   DATETIME NULL
);
USE FindParker;
GO

CREATE PROCEDURE sp_CreateUser
    @FirebaseUid VARCHAR(128),
    @Email VARCHAR(255),
    @AuthProvider VARCHAR(10),
    @IsActive BIT,
    @CreatedAt DATETIME,
    @UpdatedAt DATETIME,
    @LastLoginAt DATETIME = NULL
AS
BEGIN

    INSERT INTO Users
    (
        FirebaseUid,
        Email,
        AuthProvider,
        IsActive,
        CreatedAt,
        UpdatedAt,
        LastLoginAt
    )
    VALUES
    (
        @FirebaseUid,
        @Email,
        @AuthProvider,
        @IsActive,
        @CreatedAt,
        @UpdatedAt,
        @LastLoginAt
    );

END
GO
USE FindParker;
GO

CREATE PROCEDURE sp_GetUsers
AS
BEGIN
    SELECT
        UserId,
        FirebaseUid,
        Email,
        AuthProvider,
        IsActive,
        CreatedAt,
        UpdatedAt,
        LastLoginAt
    FROM Users;
END
GO
sp_GetUsers

CREATE PROCEDURE sp_UpdateUser
    @UserId BIGINT,
    @Email VARCHAR(255),
    @AuthProvider VARCHAR(10),
    @IsActive BIT,
    @UpdatedAt DATETIME,
    @LastLoginAt DATETIME = NULL
AS
BEGIN
    UPDATE Users
    SET
        Email = @Email,
        AuthProvider = @AuthProvider,
        IsActive = @IsActive,
        UpdatedAt = @UpdatedAt,
        LastLoginAt = @LastLoginAt
    WHERE UserId = @UserId;
END
GO

CREATE PROCEDURE sp_DeleteUser
    @UserId BIGINT
AS
BEGIN
    DELETE FROM Users
    WHERE UserId = @UserId;
END
GO

SELECT *
FROM Users
WHERE UserId = 2;


ALTER PROCEDURE sp_UpdateUser
    @UserId BIGINT,
    @FirebaseUid VARCHAR(128),
    @Email VARCHAR(255),
    @AuthProvider VARCHAR(10),
    @IsActive BIT,
    @UpdatedAt DATETIME,
    @LastLoginAt DATETIME = NULL
AS
BEGIN
    UPDATE Users
    SET
        FirebaseUid = @FirebaseUid,
        Email = @Email,
        AuthProvider = @AuthProvider,
        IsActive = @IsActive,
        UpdatedAt = @UpdatedAt,
        LastLoginAt = @LastLoginAt
    WHERE UserId = @UserId;
END
GO

select * from UserProfiles

CREATE OR ALTER PROCEDURE sp_GetUserProfileByUserId
    @UserId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ProfileId,
        UserId,
        FullName,
        PhoneNumber,
        ResidentialAddress,
        City,
        ProfilePhotoUrl,
        CreatedAt,
        UpdatedAt
    FROM dbo.UserProfiles
    WHERE UserId = @UserId;
END
GO

EXEC sp_GetUserProfileByUserId @UserId = 7

SELECT TABLE_SCHEMA, TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_NAME = 'UserProfiles';

DROP PROCEDURE IF EXISTS dbo.sp_GetUserProfileByUserId;
GO

CREATE PROCEDURE dbo.sp_GetUserProfileByUserId
    @UserId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ProfileId,
        UserId,
        FullName,
        PhoneNumber,
        ResidentialAddress,
        City,
        ProfilePhotoUrl,
        CreatedAt,
        UpdatedAt
    FROM dbo.UserProfiles
    WHERE UserId = @UserId;
END
GO

EXEC dbo.sp_GetUserProfileByUserId @UserId = 7;

select * from Users

SELECT *
FROM UserProfiles
WHERE UserId = 11;

DELETE FROM UserProfiles
WHERE UserId = 11;

DELETE FROM Users
WHERE UserId = 11;