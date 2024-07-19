IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Courses] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Students] (
    [Id] int NOT NULL IDENTITY,
    [Forename] nvarchar(max) NULL,
    [Surname] nvarchar(max) NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [EmailAddress] nvarchar(max) NULL,
    [Gender] nvarchar(max) NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CourseStudent] (
    [CoursesId] int NOT NULL,
    [StudentsId] int NOT NULL,
    CONSTRAINT [PK_CourseStudent] PRIMARY KEY ([CoursesId], [StudentsId]),
    CONSTRAINT [FK_CourseStudent_Courses_CoursesId] FOREIGN KEY ([CoursesId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CourseStudent_Students_StudentsId] FOREIGN KEY ([StudentsId]) REFERENCES [Students] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_CourseStudent_StudentsId] ON [CourseStudent] ([StudentsId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240718211816_InitialCreate', N'8.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Courses]'))
    SET IDENTITY_INSERT [Courses] ON;
INSERT INTO [Courses] ([Id], [Name])
VALUES (1, N'Course 1'),
(2, N'Course 2');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Courses]'))
    SET IDENTITY_INSERT [Courses] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DateOfBirth', N'EmailAddress', N'Forename', N'Gender', N'Surname') AND [object_id] = OBJECT_ID(N'[Students]'))
    SET IDENTITY_INSERT [Students] ON;
INSERT INTO [Students] ([Id], [DateOfBirth], [EmailAddress], [Forename], [Gender], [Surname])
VALUES (1, '2000-01-01T00:00:00.0000000', N'john.doe@example.com', N'John', N'Male', N'Doe'),
(2, '2001-02-02T00:00:00.0000000', N'jane.doe@example.com', N'Jane', N'Female', N'Doe');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DateOfBirth', N'EmailAddress', N'Forename', N'Gender', N'Surname') AND [object_id] = OBJECT_ID(N'[Students]'))
    SET IDENTITY_INSERT [Students] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240719060007_Two', N'8.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CoursesId', N'StudentsId') AND [object_id] = OBJECT_ID(N'[CourseStudent]'))
    SET IDENTITY_INSERT [CourseStudent] ON;
INSERT INTO [CourseStudent] ([CoursesId], [StudentsId])
VALUES (1, 1),
(1, 2),
(2, 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CoursesId', N'StudentsId') AND [object_id] = OBJECT_ID(N'[CourseStudent]'))
    SET IDENTITY_INSERT [CourseStudent] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240719064835_Three', N'8.0.7');
GO

COMMIT;
GO

