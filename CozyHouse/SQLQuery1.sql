CREATE TABLE [Users] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [documentType] varchar(3) NOT NULL,
  [documentNumber] varchar(9) NOT NULL,
  [name] varchar(36) NOT NULL,
  [lastName] varchar(36) NOT NULL,
  [email] varchar(100) NOT NULL,
  [password] varchar(24) NOT NULL,
  [rol] varchar(6) NOT NULL,
  [createdAt] datetime NOT NULL,
  [updatedAt] datetime
)
GO

CREATE TABLE [Buildings] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [codeBuilding] varchar(30) NOT NULL,
  [codeStreet] varchar(24) NOT NULL,
  [name] varchar(255),
  [doorway] varchar(36) NOT NULL,
  [createdAt] datetime NOT NULL,
  [updatedAt] datetime
)
GO

CREATE TABLE [Apartments] (
  [id] int PRIMARY KEY,
  [code] varchar(50) NOT NULL,
  [door] varchar(10) NOT NULL,
  [floor] int NOT NULL,
  [price] float NOT NULL,
  [area] int NOT NULL,
  [numberOfRooms] int,
  [numberOfBathrooms] int,
  [isAvailable] bit,
  [buildingId] int NOT NULL,
  [hasLift] bit,
  [hasGarage] bit,
  [createdAt] datetime NOT NULL,
  [updatedAt] datetime
)
GO

CREATE TABLE [Rentals] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int NOT NULL,
  [apartmentId] int NOT NULL,
  [startDate] datetime NOT NULL,
  [endDate] datetime NOT NULL,
  [statusId] varchar(1) NOT NULL,
  [createdAt] datetime NOT NULL,
  [updatedAt] datetime
)
GO

CREATE TABLE [Incidences] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [spokesperson] varchar(100) NOT NULL,
  [description] varchar(500) NOT NULL,
  [issueType] int NOT NULL,
  [assignedCompany] varchar(100),
  [createdAt] datetime NOT NULL,
  [updatedAt] datetime,
  [apartmentId] int NOT NULL,
  [rentalId] int NOT NULL,
  [tenantId] int NOT NULL,
  [statusId] varchar(1) NOT NULL
)
GO

CREATE TABLE [Log] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [createdAt] datetime NOT NULL,
  [actionPerformed] varchar(50) NOT NULL,
  [usersId] int NOT NULL,
  [tableAffected] varchar(50) NOT NULL,
  [description] varchar(100) NOT NULL
)
GO

CREATE TABLE [District] (
  [id] int PRIMARY KEY NOT NULL,
  [name] varchar(255) NOT NULL,
  [zipcode] varchar(255) NOT NULL,
  [country] varchar(255) NOT NULL,
  [city] varchar(255) NOT NULL,
  [createdAt] datetime NOT NULL,
  [updatedAt] datetime
)
GO

CREATE TABLE [Street] (
  [id] int PRIMARY KEY NOT NULL,
  [code] varchar(24) NOT NULL,
  [name] varchar(255) NOT NULL,
  [createdAt] datetime NOT NULL,
  [updatedAt] datetime
)
GO

CREATE TABLE [DistrictStreet] (
  [districtId] int NOT NULL,
  [streetId] int NOT NULL,
  PRIMARY KEY ([districtId], [streetId])
)
GO

CREATE TABLE [Status] (
  [id] varchar(1) PRIMARY KEY NOT NULL,
  [name] varchar(10) NOT NULL,
  [createdAt] datetime NOT NULL,
  [updatedAt] datetime
)
GO

CREATE TABLE [Payments] (
  [id] int PRIMARY KEY,
  [statusId] varchar(1) NOT NULL,
  [amount] float NOT NULL,
  [rentalId] int NOT NULL,
  [paymentDate] datetime NOT NULL,
  [bankAccount] varchar(24) NOT NULL
)
GO

CREATE INDEX [Incidences_index_0] ON [Incidences] ("apartmentId")
GO

CREATE INDEX [Incidences_index_1] ON [Incidences] ("rentalId")
GO

CREATE INDEX [Incidences_index_2] ON [Incidences] ("createdAt")
GO

CREATE INDEX [Log_index_3] ON [Log] ("createdAt")
GO

CREATE INDEX [Log_index_4] ON [Log] ("tableAffected")
GO

CREATE INDEX [Log_index_5] ON [Log] ("usersId")
GO

CREATE UNIQUE INDEX [District_Zipcode] ON [District] ("zipcode")
GO

CREATE UNIQUE INDEX [Street_Code] ON [Street] ("code")
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'UNIQUE en script',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'Buildings',
@level2type = N'Column', @level2name = 'codeBuilding';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'UNIQUE en script',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'Buildings',
@level2type = N'Column', @level2name = 'codeStreet';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'UNIQUE en script',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'Apartments',
@level2type = N'Column', @level2name = 'code';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Letra de la A a la Z',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'Apartments',
@level2type = N'Column', @level2name = 'door';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Persona de contacto (2-100 chars)',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'Incidences',
@level2type = N'Column', @level2name = 'spokesperson';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Descripción de la incidencia (máx 1000 chars)',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'Incidences',
@level2type = N'Column', @level2name = 'description';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = '0..6',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'Incidences',
@level2type = N'Column', @level2name = 'issueType';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'UNIQUE',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'District',
@level2type = N'Column', @level2name = 'name';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'UNIQUE en script',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'District',
@level2type = N'Column', @level2name = 'zipcode';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'DEFAULT GETDATE()',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'District',
@level2type = N'Column', @level2name = 'createdAt';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'DEFAULT GETDATE()',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'District',
@level2type = N'Column', @level2name = 'updatedAt';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'UNIQUE en script',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'Street',
@level2type = N'Column', @level2name = 'code';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'DEFAULT GETDATE()',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'Street',
@level2type = N'Column', @level2name = 'createdAt';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'DEFAULT GETDATE()',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'Street',
@level2type = N'Column', @level2name = 'updatedAt';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'UNIQUE',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'Status',
@level2type = N'Column', @level2name = 'name';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'DEFAULT GETDATE()',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'Status',
@level2type = N'Column', @level2name = 'createdAt';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'DEFAULT GETDATE()',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'Status',
@level2type = N'Column', @level2name = 'updatedAt';
GO

ALTER TABLE [Buildings] ADD FOREIGN KEY ([codeStreet]) REFERENCES [Street] ([code])
GO

ALTER TABLE [Apartments] ADD FOREIGN KEY ([buildingId]) REFERENCES [Buildings] ([id])
GO

ALTER TABLE [Rentals] ADD FOREIGN KEY ([userId]) REFERENCES [Users] ([id])
GO

ALTER TABLE [Rentals] ADD FOREIGN KEY ([apartmentId]) REFERENCES [Apartments] ([id])
GO

ALTER TABLE [Rentals] ADD FOREIGN KEY ([statusId]) REFERENCES [Status] ([id])
GO

ALTER TABLE [Incidences] ADD FOREIGN KEY ([apartmentId]) REFERENCES [Apartments] ([id])
GO

ALTER TABLE [Incidences] ADD FOREIGN KEY ([rentalId]) REFERENCES [Rentals] ([id])
GO

ALTER TABLE [Incidences] ADD FOREIGN KEY ([tenantId]) REFERENCES [Users] ([id])
GO

ALTER TABLE [Incidences] ADD FOREIGN KEY ([statusId]) REFERENCES [Status] ([id])
GO

ALTER TABLE [Log] ADD FOREIGN KEY ([usersId]) REFERENCES [Users] ([id])
GO

ALTER TABLE [DistrictStreet] ADD FOREIGN KEY ([districtId]) REFERENCES [District] ([id])
GO

ALTER TABLE [DistrictStreet] ADD FOREIGN KEY ([streetId]) REFERENCES [Street] ([id])
GO

ALTER TABLE [Payments] ADD FOREIGN KEY ([statusId]) REFERENCES [Status] ([id])
GO

ALTER TABLE [Payments] ADD FOREIGN KEY ([rentalId]) REFERENCES [Rentals] ([id])
GO
