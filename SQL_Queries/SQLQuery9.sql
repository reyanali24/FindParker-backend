CREATE TABLE Alerts
(
    AlertId BIGINT IDENTITY(1,1) PRIMARY KEY,

    UserId BIGINT NOT NULL,

    VehicleId BIGINT NULL,

    AlertType VARCHAR(20) NOT NULL DEFAULT 'system'
        CHECK (AlertType IN
        ('security', 'masked_call', 'qr_scan', 'system', 'towing')),

    Title VARCHAR(150) NOT NULL,

    Description VARCHAR(500) NULL,

    IsRead BIT NOT NULL DEFAULT 0,

    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Alerts_Users
        FOREIGN KEY (UserId)
        REFERENCES Users(UserId),

    CONSTRAINT FK_Alerts_Vehicles
        FOREIGN KEY (VehicleId)
        REFERENCES Vehicles(VehicleId)
);
CREATE PROCEDURE sp_CreateAlert
    @UserId BIGINT,
    @VehicleId BIGINT = NULL,
    @AlertType VARCHAR(20),
    @Title VARCHAR(150),
    @Description VARCHAR(500) = NULL,
    @IsRead BIT = 0
AS
BEGIN
    INSERT INTO Alerts
    (
        UserId,
        VehicleId,
        AlertType,
        Title,
        Description,
        IsRead
    )
    VALUES
    (
        @UserId,
        @VehicleId,
        @AlertType,
        @Title,
        @Description,
        @IsRead
    );
END
GO

CREATE PROCEDURE sp_GetAlerts
AS
BEGIN
    SELECT
        AlertId,
        UserId,
        VehicleId,
        AlertType,
        Title,
        Description,
        IsRead,
        CreatedAt
    FROM Alerts
    ORDER BY CreatedAt DESC;
END
GO

CREATE PROCEDURE sp_GetAlertsByUserId
    @UserId BIGINT
AS
BEGIN
    SELECT
        AlertId,
        UserId,
        VehicleId,
        AlertType,
        Title,
        Description,
        IsRead,
        CreatedAt
    FROM Alerts
    WHERE UserId = @UserId
    ORDER BY CreatedAt DESC;
END
GO

CREATE PROCEDURE sp_UpdateAlert
    @AlertId BIGINT,
    @VehicleId BIGINT = NULL,
    @AlertType VARCHAR(20),
    @Title VARCHAR(150),
    @Description VARCHAR(500) = NULL,
    @IsRead BIT
AS
BEGIN
    UPDATE Alerts
    SET
        VehicleId = @VehicleId,
        AlertType = @AlertType,
        Title = @Title,
        Description = @Description,
        IsRead = @IsRead
    WHERE AlertId = @AlertId;
END
GO


CREATE PROCEDURE sp_DeleteAlert
    @AlertId BIGINT
AS
BEGIN
    DELETE FROM Alerts
    WHERE AlertId = @AlertId;
END
GO