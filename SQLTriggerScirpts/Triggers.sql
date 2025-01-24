CREATE TRIGGER trg_AfterInsert_Events
ON [21180022].[Events]
AFTER INSERT
AS
BEGIN
    INSERT INTO log_21180022 (TableName, OperationType, OperationDateTime)
    VALUES ('Events', 'INSERT', GETDATE());
END
Go
CREATE TRIGGER trg_AfterUpdate_Events
ON [21180022].[Events]
AFTER UPDATE
AS
BEGIN
    INSERT INTO log_21180022 (TableName, OperationType, OperationDateTime)
    VALUES ('Events', 'UPDATE', GETDATE());
END
GO
CREATE TRIGGER trg_AfterInsert_EngineTypes
ON [21180022].[EngineTypes]
AFTER INSERT
AS
BEGIN
    INSERT INTO log_21180022 (TableName, OperationType, OperationDateTime)
    VALUES ('EngineTypes', 'INSERT', GETDATE());
END
GO
CREATE TRIGGER trg_AfterUpdate_EngineTypes
ON [21180022].[EngineTypes]
AFTER UPDATE
AS
BEGIN
    INSERT INTO log_21180022 (TableName, OperationType, OperationDateTime)
    VALUES ('EngineTypes', 'UPDATE', GETDATE());
END
GO
CREATE TRIGGER trg_AfterInsert_Locations
ON [21180022].[Locations]
AFTER INSERT
AS
BEGIN
    INSERT INTO log_21180022 (TableName, OperationType, OperationDateTime)
    VALUES ('Locations', 'INSERT', GETDATE());
END
GO
CREATE TRIGGER trg_AfterUpdate_Locations
ON [21180022].[Locations]
AFTER UPDATE
AS
BEGIN
    INSERT INTO log_21180022 (TableName, OperationType, OperationDateTime)
    VALUES ('Locations', 'UPDATE', GETDATE());
END
GO
CREATE TRIGGER trg_AfterInsert_Vehicles
ON [21180022].[Vehicles]
AFTER INSERT
AS
BEGIN
    INSERT INTO log_21180022 (TableName, OperationType, OperationDateTime)
    VALUES ('Vehicles', 'INSERT', GETDATE());
END
GO
CREATE TRIGGER trg_AfterUpdate_Vehicles
ON [21180022].[Vehicles]
AFTER UPDATE
AS
BEGIN
    INSERT INTO log_21180022 (TableName, OperationType, OperationDateTime)
    VALUES ('Vehicles', 'UPDATE', GETDATE());
END
GO
CREATE TRIGGER trg_AfterInsert_VehicleCategories
ON [21180022].[VehicleCategories]
AFTER INSERT
AS
BEGIN
    INSERT INTO log_21180022 (TableName, OperationType, OperationDateTime)
    VALUES ('VehicleCategories', 'INSERT', GETDATE());
END
GO
CREATE TRIGGER trg_AfterUpdate_VehicleCategories
ON [21180022].[VehicleCategories]
AFTER UPDATE
AS
BEGIN
    INSERT INTO log_21180022 (TableName, OperationType, OperationDateTime)
    VALUES ('VehicleCategories', 'UPDATE', GETDATE());
END
GO