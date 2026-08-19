CREATE TABLE Vehicles (
    VehicleId      BIGINT IDENTITY(1,1) PRIMARY KEY,
    UserId         BIGINT NOT NULL,
    Name           VARCHAR(100) NOT NULL,
    PlateNumber    VARCHAR(20) NOT NULL,
    Color          VARCHAR(50) NOT NULL,
    VehicleType    VARCHAR(10) NOT NULL DEFAULT 'car'
                   CHECK (VehicleType IN ('car','bike','scooter')),
    Status         VARCHAR(10) NOT NULL DEFAULT 'active'
                   CHECK (Status IN ('active','inactive')),
    CreatedAt      DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt      DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Vehicles_Users FOREIGN KEY (UserId)
        REFERENCES Users(UserId)
);


CREATE PROCEDURE sp_CreateVehicle
    @UserId BIGINT,
    @Name VARCHAR(100),
    @PlateNumber VARCHAR(20),
    @Color VARCHAR(50),
    @VehicleType VARCHAR(20),
    @Status VARCHAR(20)
AS
BEGIN
    INSERT INTO Vehicles
    (
        UserId,
        Name,
        PlateNumber,
        Color,
        VehicleType,
        Status
    )
    VALUES
    (
        @UserId,
        @Name,
        @PlateNumber,
        @Color,
        @VehicleType,
        @Status
    );
END
GO

CREATE PROCEDURE sp_GetVehicles
AS
BEGIN
    SELECT
        VehicleId,
        UserId,
        Name,
        PlateNumber,
        Color,
        VehicleType,
        Status,
        CreatedAt,
        UpdatedAt
    FROM Vehicles;
END
GO


CREATE PROCEDURE sp_UpdateVehicle
    @VehicleId BIGINT,
    @UserId BIGINT,
    @Name VARCHAR(100),
    @PlateNumber VARCHAR(20),
    @Color VARCHAR(50),
    @VehicleType VARCHAR(20),
    @Status VARCHAR(20),
    @UpdatedAt DATETIME
AS
BEGIN
    UPDATE Vehicles
    SET
        UserId = @UserId,
        Name = @Name,
        PlateNumber = @PlateNumber,
        Color = @Color,
        VehicleType = @VehicleType,
        Status = @Status,
        UpdatedAt = @UpdatedAt
    WHERE VehicleId = @VehicleId;
END
GO


CREATE PROCEDURE sp_DeleteVehicle
    @VehicleId BIGINT
AS
BEGIN
    DELETE FROM Vehicles
    WHERE VehicleId = @VehicleId;
END
GO

CREATE PROCEDURE sp_GetVehiclesByUserId
    @UserId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        VehicleId,
        UserId,
        Name,
        PlateNumber,
        Color,
        VehicleType,
        Status,
        CreatedAt,
        UpdatedAt
    FROM Vehicles
    WHERE UserId = @UserId;
END
GO

EXEC sp_GetVehiclesByUserId @UserId = 12;

select * from UserProfiles
select * from Vehicles
SELECT UserId, FirebaseUid, Email
FROM dbo.Users
ORDER BY UserId;