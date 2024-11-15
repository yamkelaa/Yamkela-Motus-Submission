CREATE TABLE Colours (
    ColourId INT IDENTITY(1,1) PRIMARY KEY,          
    ColourName NVARCHAR(255) NOT NULL,               
    ColourHex VARCHAR(7) NOT NULL,                   
    CONSTRAINT CHK_ColourName CHECK (LTRIM(RTRIM(ColourName)) <> N''),
    CONSTRAINT CHK_ColourHexFormat CHECK (ColourHex LIKE '#[0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f]')
);

INSERT INTO Colours (ColourName, ColourHex)
VALUES
    (N'Black', '#000000'),
    (N'Café', '#D7BBA0'),
    (N'Red', '#FF0000'),
    (N'Green', '#00FF00'),
    (N'Blue', '#0000FF'),
    (N'White', '#FFFFFF');


ALTER TABLE Vehicles
ADD ColourId INT NULL;  


ALTER TABLE Vehicles
ADD CONSTRAINT FK_Vehicles_Colours
    FOREIGN KEY (ColourId)
    REFERENCES Colours(ColourId)
    ON DELETE SET NULL  


CREATE NONCLUSTERED INDEX IDX_Vehicles_ColourId
    ON Vehicles (ColourId);