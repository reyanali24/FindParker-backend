CREATE TABLE MaskedCalls
(
    CallId BIGINT IDENTITY(1,1) PRIMARY KEY,

    UserId BIGINT NOT NULL,

    VehicleId BIGINT NULL,

    CallerMaskedNumber VARCHAR(20) NULL,

    CallStatus VARCHAR(10) NOT NULL DEFAULT 'missed'
        CHECK (CallStatus IN ('missed', 'answered', 'declined')),

    CallDurationSeconds INT NULL,

    CalledAt DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_MaskedCalls_Users
        FOREIGN KEY (UserId)
        REFERENCES Users(UserId),

    CONSTRAINT FK_MaskedCalls_Vehicles
        FOREIGN KEY (VehicleId)
        REFERENCES Vehicles(VehicleId)
);

CREATE PROCEDURE sp_CreateMaskedCall
    @UserId BIGINT,
    @VehicleId BIGINT = NULL,
    @CallerMaskedNumber VARCHAR(20) = NULL,
    @CallStatus VARCHAR(10),
    @CallDurationSeconds INT = NULL
AS
BEGIN
    INSERT INTO MaskedCalls
    (
        UserId,
        VehicleId,
        CallerMaskedNumber,
        CallStatus,
        CallDurationSeconds
    )
    VALUES
    (
        @UserId,
        @VehicleId,
        @CallerMaskedNumber,
        @CallStatus,
        @CallDurationSeconds
    );
END
GO

CREATE PROCEDURE sp_GetMaskedCalls
AS
BEGIN
    SELECT
        CallId,
        UserId,
        VehicleId,
        CallerMaskedNumber,
        CallStatus,
        CallDurationSeconds,
        CalledAt
    FROM MaskedCalls
    ORDER BY CalledAt DESC;
END
GO

CREATE PROCEDURE sp_UpdateMaskedCall
    @CallId BIGINT,
    @UserId BIGINT,
    @VehicleId BIGINT = NULL,
    @CallerMaskedNumber VARCHAR(20) = NULL,
    @CallStatus VARCHAR(10),
    @CallDurationSeconds INT = NULL
AS
BEGIN
    UPDATE MaskedCalls
    SET
        UserId = @UserId,
        VehicleId = @VehicleId,
        CallerMaskedNumber = @CallerMaskedNumber,
        CallStatus = @CallStatus,
        CallDurationSeconds = @CallDurationSeconds
    WHERE CallId = @CallId;
END
GO

CREATE PROCEDURE sp_DeleteMaskedCall
    @CallId BIGINT
AS
BEGIN
    DELETE FROM MaskedCalls
    WHERE CallId = @CallId;
END
GO

EXEC sp_CreateMaskedCall
    @UserId = 1,
    @VehicleId =1 ,
    @CallerMaskedNumber = '03001234567',
    @CallStatus = 'answered',
    @CallDurationSeconds = 120;
    EXEC sp_GetMaskedCalls;