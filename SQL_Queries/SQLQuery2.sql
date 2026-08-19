CREATE TABLE UserProfiles (
    ProfileId            BIGINT IDENTITY(1,1) PRIMARY KEY,
    UserId               BIGINT NOT NULL UNIQUE,  -- one-to-one with Users
    FullName             VARCHAR(150) NOT NULL,
    PhoneNumber          VARCHAR(20) NOT NULL,
    ResidentialAddress   VARCHAR(255) NOT NULL,
    City                 VARCHAR(100) NOT NULL,
    ProfilePhotoUrl      VARCHAR(500) NULL,
    CreatedAt            DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt            DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_UserProfiles_Users FOREIGN KEY (UserId)
        REFERENCES Users(UserId)
        ON DELETE CASCADE
);

CREATE PROCEDURE sp_CreateUserProfile
    @UserId BIGINT,
    @FullName VARCHAR(150),
    @PhoneNumber VARCHAR(20),
    @ResidentialAddress VARCHAR(255),
    @City VARCHAR(100),
    @ProfilePhotoUrl VARCHAR(500) = NULL
AS
BEGIN
    INSERT INTO UserProfiles
    (
        UserId,
        FullName,
        PhoneNumber,
        ResidentialAddress,
        City,
        ProfilePhotoUrl
    )
    VALUES
    (
        @UserId,
        @FullName,
        @PhoneNumber,
        @ResidentialAddress,
        @City,
        @ProfilePhotoUrl
    );
END
GO




CREATE PROCEDURE sp_DeleteUserProfile
    @ProfileId BIGINT
AS
BEGIN
    DELETE FROM UserProfiles
    WHERE ProfileId = @ProfileId;
END
GO

CREATE PROCEDURE sp_GetUserProfiles
AS
BEGIN
    SELECT
       ProfileId,            
    UserId   ,           -- one-to-one with Users
    FullName,            
    PhoneNumber,         
    ResidentialAddress  ,
    City      ,           
    ProfilePhotoUrl   ,  
    CreatedAt     ,     
    UpdatedAt  
    FROM UserProfiles
	end
GO


CREATE PROCEDURE sp_UpdateUserProfile
    @ProfileId BIGINT,
    @UserId BIGINT,
    @FullName VARCHAR(150),
    @PhoneNumber VARCHAR(20),
    @ResidentialAddress VARCHAR(255),
    @City VARCHAR(100),
    @ProfilePhotoUrl VARCHAR(500) = NULL,
    @UpdatedAt DATETIME
AS
BEGIN
    UPDATE user_profiles
    SET
        user_id = @UserId,
        full_name = @FullName,
        phone_number = @PhoneNumber,
        residential_address = @ResidentialAddress,
        city = @City,
        profile_photo_url = @ProfilePhotoUrl,
        updated_at = @UpdatedAt
    WHERE profile_id = @ProfileId;
END
GO

SELECT
    UserId,
    FirebaseUid,
    Email
FROM dbo.Users;

SELECT *
FROM UserProfiles

EXEC sp_helptext 'sp_CreateUser';

ALTER PROCEDURE sp_CreateUser
    @FirebaseUid NVARCHAR(255),
    @Email NVARCHAR(255),
    @AuthProvider NVARCHAR(50),
    @IsActive BIT,
    @CreatedAt DATETIME2,
    @UpdatedAt DATETIME2,
    @LastLoginAt DATETIME2 = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Users
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

    SELECT CAST(SCOPE_IDENTITY() AS BIGINT) AS UserId;
END



