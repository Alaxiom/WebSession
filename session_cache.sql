IF NOT EXISTS (
	SELECT TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'ILDBCache'
)
BEGIN
	CREATE TABLE [dbo].[ILDBCache](Id nvarchar(449) COLLATE SQL_Latin1_General_CP1_CS_AS NOT NULL, Value varbinary(MAX) NOT NULL, ExpiresAtTime datetimeoffset NOT NULL, SlidingExpirationInSeconds bigint NULL,AbsoluteExpiration datetimeoffset NULL, PRIMARY KEY (Id))
	CREATE NONCLUSTERED INDEX Index_ExpiresAtTime ON [dbo].[ILDBCache](ExpiresAtTime)
END
