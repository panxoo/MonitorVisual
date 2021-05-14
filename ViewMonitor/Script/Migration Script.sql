IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Monitors] (
    [id_monitor] int NOT NULL IDENTITY,
    [nom_monitor] nvarchar(500) NULL,
    [n_monitor] smallint NOT NULL,
    [procedimiento] nvarchar(1000) NULL,
    CONSTRAINT [PK_Monitors] PRIMARY KEY ([id_monitor])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200921193137_inicial', N'3.1.8');

GO

DROP TABLE [Monitors];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200921193910_ssa', N'3.1.8');

GO

CREATE TABLE [Agrupacions] (
    [AgrupacionID] int NOT NULL IDENTITY,
    [Nombre] nvarchar(1000) NULL,
    CONSTRAINT [PK_Agrupacions] PRIMARY KEY ([AgrupacionID])
);

GO

CREATE TABLE [Job_Monitors] (
    [Job_MonitorID] int NOT NULL IDENTITY,
    [Nombre] nvarchar(1000) NULL,
    CONSTRAINT [PK_Job_Monitors] PRIMARY KEY ([Job_MonitorID])
);

GO

CREATE TABLE [Monitors] (
    [MonitorID] int NOT NULL IDENTITY,
    [Nombre] nvarchar(500) NULL,
    [Procedimiento] nvarchar(1000) NULL,
    [Activo] bit NOT NULL,
    [Job_MonitorID] int NOT NULL,
    [AgrupacionID] int NOT NULL,
    CONSTRAINT [PK_Monitors] PRIMARY KEY ([MonitorID]),
    CONSTRAINT [FK_Monitors_Agrupacions_AgrupacionID] FOREIGN KEY ([AgrupacionID]) REFERENCES [Agrupacions] ([AgrupacionID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Monitors_Job_Monitors_Job_MonitorID] FOREIGN KEY ([Job_MonitorID]) REFERENCES [Job_Monitors] ([Job_MonitorID]) ON DELETE CASCADE
);

GO

CREATE TABLE [Monitor_Estado_Hists] (
    [Monitor_Estado_HistID] int NOT NULL IDENTITY,
    [estado] bit NOT NULL,
    [Fecha] datetime2 NOT NULL,
    [MonitorID] int NOT NULL,
    CONSTRAINT [PK_Monitor_Estado_Hists] PRIMARY KEY ([Monitor_Estado_HistID]),
    CONSTRAINT [FK_Monitor_Estado_Hists_Monitors_MonitorID] FOREIGN KEY ([MonitorID]) REFERENCES [Monitors] ([MonitorID]) ON DELETE CASCADE
);

GO

CREATE TABLE [Monitor_Estados] (
    [Monitor_EstadoID] int NOT NULL IDENTITY,
    [Estado] bit NOT NULL,
    [Fecha] datetime2 NOT NULL,
    [MonitorID] int NOT NULL,
    CONSTRAINT [PK_Monitor_Estados] PRIMARY KEY ([Monitor_EstadoID]),
    CONSTRAINT [FK_Monitor_Estados_Monitors_MonitorID] FOREIGN KEY ([MonitorID]) REFERENCES [Monitors] ([MonitorID]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Monitor_Estado_Hists_MonitorID] ON [Monitor_Estado_Hists] ([MonitorID]);

GO

CREATE UNIQUE INDEX [IX_Monitor_Estados_MonitorID] ON [Monitor_Estados] ([MonitorID]);

GO

CREATE INDEX [IX_Monitors_AgrupacionID] ON [Monitors] ([AgrupacionID]);

GO

CREATE INDEX [IX_Monitors_Job_MonitorID] ON [Monitors] ([Job_MonitorID]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200921223030_initial', N'3.1.8');

GO

ALTER TABLE [Monitors] ADD [Alerta] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200922172150_alerta', N'3.1.8');

GO

ALTER TABLE [Monitors] ADD [Descripcion] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200924202519_monitordescr', N'3.1.8');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Monitors]') AND [c].[name] = N'Procedimiento');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Monitors] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Monitors] ALTER COLUMN [Procedimiento] nvarchar(1000) NOT NULL;

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Monitors]') AND [c].[name] = N'Nombre');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Monitors] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Monitors] ALTER COLUMN [Nombre] nvarchar(500) NOT NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200925225138_monitorrequired', N'3.1.8');

GO

ALTER TABLE [Monitor_Estado_Hists] ADD [FalsoPositivo] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

ALTER TABLE [Monitor_Estado_Hists] ADD [Nota] nvarchar(max) NULL;

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Job_Monitors]') AND [c].[name] = N'Nombre');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Job_Monitors] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Job_Monitors] ALTER COLUMN [Nombre] nvarchar(1000) NOT NULL;

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Agrupacions]') AND [c].[name] = N'Nombre');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Agrupacions] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Agrupacions] ALTER COLUMN [Nombre] nvarchar(1000) NOT NULL;

GO

ALTER TABLE [Agrupacions] ADD [Activo] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201028190022_AgrupacionActive', N'3.1.8');

GO

ALTER TABLE [Job_Monitors] ADD [Activo] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201028194121_JobActivo', N'3.1.8');

GO

ALTER TABLE [Monitor_Estado_Hists] ADD [ProcesoId] int NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201105194638_addhistproce', N'3.1.8');

GO

