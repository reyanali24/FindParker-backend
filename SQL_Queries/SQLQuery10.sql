CREATE TABLE LoginHistory
(
    HistoryId BIGINT IDENTITY(1,1) PRIMARY KEY,

    UserId BIGINT NOT NULL,

    DeviceInfo VARCHAR(255) NULL,

    IpAddress VARCHAR(45) NULL,

    LoginAt DATETIME NOT NULL DEFAULT GETDATE(),

    LoggedOutAt DATETIME NULL,

    CONSTRAINT FK_LoginHistory_Users
        FOREIGN KEY (UserId)
        REFERENCES Users(UserId)
);

CREATE PROCEDURE sp_CreateLoginHistory
    @UserId BIGINT,
    @DeviceInfo VARCHAR(255) = NULL,
    @IpAddress VARCHAR(45) = NULL
AS
BEGIN
    INSERT INTO LoginHistory
    (
        UserId,
        DeviceInfo,
        IpAddress
    )
    VALUES
    (
        @UserId,
        @DeviceInfo,
        @IpAddress
    );
END
GO


CREATE PROCEDURE sp_GetLoginHistory
AS
BEGIN
    SELECT
        HistoryId,
        UserId,
        DeviceInfo,
        IpAddress,
        LoginAt,
        LoggedOutAt
    FROM LoginHistory
    ORDER BY LoginAt DESC;
END
GO

CREATE PROCEDURE sp_GetLoginHistoryByUserId
    @UserId BIGINT
AS
BEGIN
    SELECT
        HistoryId,
        UserId,
        DeviceInfo,
        IpAddress,
        LoginAt,
        LoggedOutAt
    FROM LoginHistory
    WHERE UserId = @UserId
    ORDER BY LoginAt DESC;
END
GO

CREATE PROCEDURE sp_UpdateLoginHistory
    @HistoryId BIGINT,
    @LoggedOutAt DATETIME = NULL
AS
BEGIN
    UPDATE LoginHistory
    SET
        LoggedOutAt = @LoggedOutAt
    WHERE HistoryId = @HistoryId;
END
GO

CREATE PROCEDURE sp_DeleteLoginHistory
    @HistoryId BIGINT
AS
BEGIN
    DELETE FROM LoginHistory
    WHERE HistoryId = @HistoryId;
END
GO