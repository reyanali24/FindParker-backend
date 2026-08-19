CREATE TABLE QrCodes
(
    QrId BIGINT IDENTITY(1,1) PRIMARY KEY,

    SerialNo VARCHAR(50) NOT NULL UNIQUE,

    QrCodeValue VARCHAR(50) NOT NULL UNIQUE,

    QrLink VARCHAR(500) NOT NULL,

    IsAssigned BIT NOT NULL DEFAULT 0,

    VehicleId BIGINT NULL,

    Status VARCHAR(20) NOT NULL DEFAULT 'available',

    AssignedAt DATETIME NULL,

    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_QrCodes_Vehicles
        FOREIGN KEY (VehicleId)
        REFERENCES Vehicles(VehicleId),

    CONSTRAINT CK_QrCodes_Status
        CHECK (Status IN ('available', 'assigned', 'damaged', 'replaced'))
);

CREATE UNIQUE INDEX UX_QrCodes_ActiveVehicle
ON QrCodes(VehicleId)
WHERE VehicleId IS NOT NULL
  AND IsAssigned = 1;


  CREATE PROCEDURE sp_CreateQrCode
    @SerialNo VARCHAR(50),
    @QrCodeValue VARCHAR(50),
    @QrLink VARCHAR(500)
AS
BEGIN
    INSERT INTO QrCodes
    (
        SerialNo,
        QrCodeValue,
        QrLink
    )
    VALUES
    (
        @SerialNo,
        @QrCodeValue,
        @QrLink
    );
END
GO


CREATE PROCEDURE sp_GetQrCodes
AS
BEGIN
    SELECT
        QrId,
        SerialNo,
        QrCodeValue,
        QrLink,
        IsAssigned,
        VehicleId,
        Status,
        AssignedAt,
        CreatedAt
    FROM QrCodes;
END
GO

CREATE PROCEDURE sp_GetQrCodeByVehicleId
    @VehicleId BIGINT
AS
BEGIN
    SELECT
        QrId,
        SerialNo,
        QrCodeValue,
        QrLink,
        IsAssigned,
        VehicleId,
        Status,
        AssignedAt,
        CreatedAt
    FROM QrCodes
    WHERE VehicleId = @VehicleId;
END
GO


CREATE PROCEDURE sp_UpdateQrCode
    @QrId BIGINT,
    @SerialNo VARCHAR(50),
    @QrCodeValue VARCHAR(50),
    @QrLink VARCHAR(500),
    @IsAssigned BIT,
    @VehicleId BIGINT = NULL,
    @Status VARCHAR(20),
    @AssignedAt DATETIME = NULL
AS
BEGIN
    UPDATE QrCodes
    SET
        SerialNo = @SerialNo,
        QrCodeValue = @QrCodeValue,
        QrLink = @QrLink,
        IsAssigned = @IsAssigned,
        VehicleId = @VehicleId,
        Status = @Status,
        AssignedAt = @AssignedAt
    WHERE QrId = @QrId;
END
GO


CREATE PROCEDURE sp_DeleteQrCode
    @QrId BIGINT
AS
BEGIN
    DELETE FROM QrCodes
    WHERE QrId = @QrId;
END
GO

TRUNCATE TABLE QrCodes;

SELECT
    QrId,
    SerialNo,
    QrCodeValue,
    IsAssigned,
    VehicleId,
    Status,
    AssignedAt,
    CreatedAt
FROM QrCodes
where VehicleId = 7;

ALTER PROCEDURE sp_GetQrCodeByVehicleId
    @VehicleId BIGINT
AS
BEGIN
    SELECT
        QrId,
        SerialNo,
        QrCodeValue,
        QrLink,
        IsAssigned,
        VehicleId,
        Status,
        AssignedAt,
        CreatedAt
    FROM QrCodes
    WHERE VehicleId = @VehicleId;
END
GO

select * from QrCodes

select * from Vehicles

EXEC sp_GetQrCodeByVehicleId @VehicleId = 7;