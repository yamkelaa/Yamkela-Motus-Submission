UPDATE [dbo].[Vehicles]
SET ModelYear = 2015 + ABS(CHECKSUM(NEWID())) % 10;


ALTER TABLE [dbo].[Vehicles]
ADD CONSTRAINT CHK_ModelYear CHECK (ModelYear >= 2015 AND ModelYear <= 2024);
