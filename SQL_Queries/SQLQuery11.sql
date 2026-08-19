CREATE PROCEDURE sp_GetUserProfileByUserId
    @UserId BIGINT
AS
BEGIN
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
    FROM UserProfiles
    WHERE UserId = @UserId;
END
GO

CREATE PROCEDURE sp_GetVehiclesByUserId
    @UserId BIGINT
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
    FROM Vehicles
    WHERE UserId = @UserId
    ORDER BY CreatedAt DESC;
END
GO

CREATE PROCEDURE sp_GetEmergencyContactsByUserId
    @UserId BIGINT
AS
BEGIN
    SELECT
        ContactId,
        UserId,
        ContactName,
        ContactPhone,
        Relationship,
        IsPrimary,
        CreatedAt,
        UpdatedAt
    FROM EmergencyContacts
    WHERE UserId = @UserId
    ORDER BY IsPrimary DESC, CreatedAt DESC;
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
        AssignedAt,
        CreatedAt
    FROM QrCodes
    WHERE VehicleId = @VehicleId;
END
GO

CREATE PROCEDURE sp_GetPrivacySettingsByUserId
    @UserId BIGINT
AS
BEGIN
    SELECT
        SettingId,
        UserId,
        PrivacyMode,
        MaskedLineEnabled,
        AutoReplyEnabled,
        UpdatedAt
    FROM PrivacySettings
    WHERE UserId = @UserId;
END
GO

CREATE PROCEDURE sp_GetMaskedCallsByUserId
    @UserId BIGINT
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
    WHERE UserId = @UserId
    ORDER BY CalledAt DESC;
END
GO

select * from Users