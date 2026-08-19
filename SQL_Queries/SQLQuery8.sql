CREATE TABLE QrScans
(
    ScanId BIGINT IDENTITY(1,1) PRIMARY KEY,

    VehicleId BIGINT NOT NULL,

    ScanLocation VARCHAR(255) NULL,

    ScanResult VARCHAR(10) NOT NULL DEFAULT 'valid'
        CHECK (ScanResult IN ('valid', 'invalid', 'expired')),

    ScannedAt DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_QrScans_Vehicles
        FOREIGN KEY (VehicleId)
        REFERENCES Vehicles(VehicleId)
);

CREATE PROCEDURE sp_CreateQrScan
    @VehicleId BIGINT,
    @ScanLocation VARCHAR(255) = NULL,
    @ScanResult VARCHAR(10)
AS
BEGIN
    INSERT INTO QrScans
    (
        VehicleId,
        ScanLocation,
        ScanResult
    )
    VALUES
    (
        @VehicleId,
        @ScanLocation,
        @ScanResult
    );
END
GO

CREATE PROCEDURE sp_GetQrScans
AS
BEGIN
    SELECT
        ScanId,
        VehicleId,
        ScanLocation,
        ScanResult,
        ScannedAt
    FROM QrScans
    ORDER BY ScannedAt DESC;
END
GO

CREATE PROCEDURE sp_UpdateQrScan
    @ScanId BIGINT,
    @VehicleId BIGINT,
    @ScanLocation VARCHAR(255) = NULL,
    @ScanResult VARCHAR(10)
AS
BEGIN
    UPDATE QrScans
    SET
        VehicleId = @VehicleId,
        ScanLocation = @ScanLocation,
        ScanResult = @ScanResult
    WHERE ScanId = @ScanId;
END
GO

CREATE PROCEDURE sp_DeleteQrScan
    @ScanId BIGINT
AS
BEGIN
    DELETE FROM QrScans
    WHERE ScanId = @ScanId;
END
GO

CREATE PROCEDURE sp_GetQrScansByVehicleId
    @VehicleId BIGINT
AS
BEGIN
    SELECT
        ScanId,
        VehicleId,
        ScanLocation,
        ScanResult,
        ScannedAt
    FROM QrScans
    WHERE VehicleId = @VehicleId
    ORDER BY ScannedAt DESC;
END
GO