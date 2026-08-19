CREATE TABLE PrivacySettings
(
    SettingId BIGINT IDENTITY(1,1) PRIMARY KEY,

    UserId BIGINT NOT NULL UNIQUE,

    PrivacyMode VARCHAR(10) NOT NULL DEFAULT 'private'
        CHECK (PrivacyMode IN ('public', 'private')),

    MaskedLineEnabled BIT NOT NULL DEFAULT 1,

    AutoReplyEnabled BIT NOT NULL DEFAULT 0,

    UpdatedAt DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_PrivacySettings_Users
        FOREIGN KEY (UserId)
        REFERENCES Users(UserId)
        ON DELETE CASCADE
);

CREATE PROCEDURE sp_CreatePrivacySettings
    @UserId BIGINT,
    @PrivacyMode VARCHAR(10),
    @MaskedLineEnabled BIT,
    @AutoReplyEnabled BIT
AS
BEGIN
    INSERT INTO PrivacySettings
    (
        UserId,
        PrivacyMode,
        MaskedLineEnabled,
        AutoReplyEnabled
    )
    VALUES
    (
        @UserId,
        @PrivacyMode,
        @MaskedLineEnabled,
        @AutoReplyEnabled
    );
END
GO

CREATE PROCEDURE sp_GetPrivacySettings
AS
BEGIN
    SELECT
        SettingId,
        UserId,
        PrivacyMode,
        MaskedLineEnabled,
        AutoReplyEnabled,
        UpdatedAt
    FROM PrivacySettings;
END
GO

CREATE PROCEDURE sp_UpdatePrivacySettings
    @SettingId BIGINT,
    @UserId BIGINT,
    @PrivacyMode VARCHAR(10),
    @MaskedLineEnabled BIT,
    @AutoReplyEnabled BIT
AS
BEGIN
    UPDATE PrivacySettings
    SET
        UserId = @UserId,
        PrivacyMode = @PrivacyMode,
        MaskedLineEnabled = @MaskedLineEnabled,
        AutoReplyEnabled = @AutoReplyEnabled,
        UpdatedAt = GETDATE()
    WHERE SettingId = @SettingId;
END
GO

CREATE PROCEDURE sp_DeletePrivacySettings
    @SettingId BIGINT
AS
BEGIN
    DELETE FROM PrivacySettings
    WHERE SettingId = @SettingId;
END
GO