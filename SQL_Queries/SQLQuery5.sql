CREATE TABLE EmergencyContacts
(
    ContactId BIGINT IDENTITY(1,1) PRIMARY KEY,

    UserId BIGINT NOT NULL,

    ContactName VARCHAR(150) NOT NULL,

    ContactPhone VARCHAR(20) NOT NULL,

    Relationship VARCHAR(50) NULL,

    IsPrimary BIT NOT NULL DEFAULT 1,

    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),

    UpdatedAt DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_EmergencyContacts_Users
        FOREIGN KEY (UserId)
        REFERENCES Users(UserId)
        ON DELETE CASCADE
);

CREATE UNIQUE INDEX UX_EmergencyContacts_Primary
ON EmergencyContacts(UserId)
WHERE IsPrimary = 1;


CREATE PROCEDURE sp_CreateEmergencyContact
    @UserId BIGINT,
    @ContactName VARCHAR(150),
    @ContactPhone VARCHAR(20),
    @Relationship VARCHAR(50) = NULL,
    @IsPrimary BIT
AS
BEGIN
    INSERT INTO EmergencyContacts
    (
        UserId,
        ContactName,
        ContactPhone,
        Relationship,
        IsPrimary
    )
    VALUES
    (
        @UserId,
        @ContactName,
        @ContactPhone,
        @Relationship,
        @IsPrimary
    );
END
GO

ALTER PROCEDURE sp_CreateEmergencyContact
    @UserId BIGINT,
    @ContactName VARCHAR(150),
    @ContactPhone VARCHAR(20),
    @Relationship VARCHAR(50) = NULL,
    @IsPrimary BIT
AS
BEGIN

    -- If the new contact is going to be primary,
    -- remove primary status from the user's existing contact
    IF @IsPrimary = 1
    BEGIN
        UPDATE EmergencyContacts
        SET
            IsPrimary = 0,
            UpdatedAt = GETDATE()
        WHERE UserId = @UserId
          AND IsPrimary = 1;
    END

    -- Create the new contact
    INSERT INTO EmergencyContacts
    (
        UserId,
        ContactName,
        ContactPhone,
        Relationship,
        IsPrimary
    )
    VALUES
    (
        @UserId,
        @ContactName,
        @ContactPhone,
        @Relationship,
        @IsPrimary
    );

END
GO


CREATE PROCEDURE sp_GetEmergencyContacts
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
    FROM EmergencyContacts;
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
    ORDER BY IsPrimary DESC, CreatedAt ASC;
END
GO


CREATE PROCEDURE sp_UpdateEmergencyContact
    @ContactId BIGINT,
    @UserId BIGINT,
    @ContactName VARCHAR(150),
    @ContactPhone VARCHAR(20),
    @Relationship VARCHAR(50) = NULL,
    @IsPrimary BIT
AS
BEGIN
    UPDATE EmergencyContacts
    SET
        UserId = @UserId,
        ContactName = @ContactName,
        ContactPhone = @ContactPhone,
        Relationship = @Relationship,
        IsPrimary = @IsPrimary,
        UpdatedAt = GETDATE()
    WHERE ContactId = @ContactId;
END
GO

ALTER PROCEDURE sp_UpdateEmergencyContact
    @ContactId BIGINT,
    @UserId BIGINT,
    @ContactName VARCHAR(150),
    @ContactPhone VARCHAR(20),
    @Relationship VARCHAR(50) = NULL,
    @IsPrimary BIT
AS
BEGIN

    IF @IsPrimary = 1
    BEGIN
        UPDATE EmergencyContacts
        SET
            IsPrimary = 0,
            UpdatedAt = GETDATE()
        WHERE UserId = @UserId
          AND ContactId <> @ContactId
          AND IsPrimary = 1;
    END

    UPDATE EmergencyContacts
    SET
        UserId = @UserId,
        ContactName = @ContactName,
        ContactPhone = @ContactPhone,
        Relationship = @Relationship,
        IsPrimary = @IsPrimary,
        UpdatedAt = GETDATE()
    WHERE ContactId = @ContactId;

END
GO


CREATE PROCEDURE sp_DeleteEmergencyContact
    @ContactId BIGINT
AS
BEGIN
    DELETE FROM EmergencyContacts
    WHERE ContactId = @ContactId;
END
GO

select * from EmergencyContacts

	SELECT *
	FROM dbo.UserProfiles;

	[dbo].[sp_GetUserProfiles]

	SELECT 
    name,
    create_date,
    modify_date
FROM sys.procedures
WHERE name = 'sp_GetEmergencyContactsByUserId';

EXEC sp_helptext 'sp_GetEmergencyContactsByUserId';

EXEC sp_GetEmergencyContactsByUserId @UserId = 12;