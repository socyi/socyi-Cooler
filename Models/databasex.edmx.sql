
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/01/2020 19:31:29
-- Generated from EDMX file: C:\Users\socra\Desktop\Projects\Cooler\Models\database.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [VassilikoFilters];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Bag_Replacement_Bag_Types]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bag_Replacement] DROP CONSTRAINT [FK_Bag_Replacement_Bag_Types];
GO
IF OBJECT_ID(N'[dbo].[FK_Bag_Replacement_Filters]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bag_Replacement] DROP CONSTRAINT [FK_Bag_Replacement_Filters];
GO
IF OBJECT_ID(N'[dbo].[FK_Bag_Replacement_Replacement_Reasons]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bag_Replacement] DROP CONSTRAINT [FK_Bag_Replacement_Replacement_Reasons];
GO
IF OBJECT_ID(N'[dbo].[FK_Bag_Suppliers_Colors]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bag_Suppliers] DROP CONSTRAINT [FK_Bag_Suppliers_Colors];
GO
IF OBJECT_ID(N'[dbo].[FK_Bag_Types_Bag_Types]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bag_Types] DROP CONSTRAINT [FK_Bag_Types_Bag_Types];
GO
IF OBJECT_ID(N'[dbo].[FK_Bag_Types_Fiber_Brand]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bag_Types] DROP CONSTRAINT [FK_Bag_Types_Fiber_Brand];
GO
IF OBJECT_ID(N'[dbo].[FK_Bag_Types_Material_Types]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bag_Types] DROP CONSTRAINT [FK_Bag_Types_Material_Types];
GO
IF OBJECT_ID(N'[dbo].[FK_Filters_Cage_maintenace]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cages_Maintenance] DROP CONSTRAINT [FK_Filters_Cage_maintenace];
GO
IF OBJECT_ID(N'[dbo].[FK_Filters_Departments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Filters] DROP CONSTRAINT [FK_Filters_Departments];
GO
IF OBJECT_ID(N'[dbo].[FK_Filters_Manufacturers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Filters] DROP CONSTRAINT [FK_Filters_Manufacturers];
GO
IF OBJECT_ID(N'[dbo].[FK_Moveable_Beams_Compartments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Moveable_Beams] DROP CONSTRAINT [FK_Moveable_Beams_Compartments];
GO
IF OBJECT_ID(N'[dbo].[FK_Nozle_Spares_Nozle_Types]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Nozle_Spares] DROP CONSTRAINT [FK_Nozle_Spares_Nozle_Types];
GO
IF OBJECT_ID(N'[dbo].[FK_Nozle_Suppliers_Nozle_Colors]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Nozle_Suppliers] DROP CONSTRAINT [FK_Nozle_Suppliers_Nozle_Colors];
GO
IF OBJECT_ID(N'[dbo].[FK_Reasons_Cage_maintenace]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cages_Maintenance] DROP CONSTRAINT [FK_Reasons_Cage_maintenace];
GO
IF OBJECT_ID(N'[dbo].[FK_Replacements_Compartments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Replacements] DROP CONSTRAINT [FK_Replacements_Compartments];
GO
IF OBJECT_ID(N'[dbo].[FK_Replacements_Nozle_Spares]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Replacements] DROP CONSTRAINT [FK_Replacements_Nozle_Spares];
GO
IF OBJECT_ID(N'[dbo].[FK_Replacements_Nozle_Types]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Replacements] DROP CONSTRAINT [FK_Replacements_Nozle_Types];
GO
IF OBJECT_ID(N'[dbo].[FK_Replacements_Reasons]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Replacements] DROP CONSTRAINT [FK_Replacements_Reasons];
GO
IF OBJECT_ID(N'[dbo].[FK_Valves_Maintenance_Filters]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Valves_Maintenance] DROP CONSTRAINT [FK_Valves_Maintenance_Filters];
GO
IF OBJECT_ID(N'[dbo].[FK_Vibrators_Filters]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Vibrators] DROP CONSTRAINT [FK_Vibrators_Filters];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Bag_Replacement]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bag_Replacement];
GO
IF OBJECT_ID(N'[dbo].[Bag_Suppliers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bag_Suppliers];
GO
IF OBJECT_ID(N'[dbo].[Bag_Types]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bag_Types];
GO
IF OBJECT_ID(N'[dbo].[Cages_Maintenance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cages_Maintenance];
GO
IF OBJECT_ID(N'[dbo].[Colors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Colors];
GO
IF OBJECT_ID(N'[dbo].[Compartments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Compartments];
GO
IF OBJECT_ID(N'[dbo].[Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Departments];
GO
IF OBJECT_ID(N'[dbo].[Fiber_Brand]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Fiber_Brand];
GO
IF OBJECT_ID(N'[dbo].[Filters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Filters];
GO
IF OBJECT_ID(N'[dbo].[Manufacturers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Manufacturers];
GO
IF OBJECT_ID(N'[dbo].[Material_Types]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Material_Types];
GO
IF OBJECT_ID(N'[dbo].[Moveable_Beams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Moveable_Beams];
GO
IF OBJECT_ID(N'[dbo].[Nozle_Colors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nozle_Colors];
GO
IF OBJECT_ID(N'[dbo].[Nozle_Spares]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nozle_Spares];
GO
IF OBJECT_ID(N'[dbo].[Nozle_Suppliers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nozle_Suppliers];
GO
IF OBJECT_ID(N'[dbo].[Nozle_Types]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nozle_Types];
GO
IF OBJECT_ID(N'[dbo].[Replacement_Reasons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Replacement_Reasons];
GO
IF OBJECT_ID(N'[dbo].[Replacements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Replacements];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Valves_Maintenance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Valves_Maintenance];
GO
IF OBJECT_ID(N'[dbo].[Vibrators]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Vibrators];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Bag_Replacement'
CREATE TABLE [dbo].[Bag_Replacement] (
    [Replacement_Code] int IDENTITY(1,1) NOT NULL,
    [Repl_Date] datetime  NOT NULL,
    [Filter_Code] nvarchar(50)  NOT NULL,
    [Sector_No] int  NOT NULL,
    [Valve_No] int  NOT NULL,
    [Bag_No] int  NOT NULL,
    [Bag_Code] int  NOT NULL,
    [Replacement_Reason_Code] int  NOT NULL,
    [Create_Date] datetime  NOT NULL
);
GO

-- Creating table 'Bag_Suppliers'
CREATE TABLE [dbo].[Bag_Suppliers] (
    [Supplier_Code] int  NOT NULL,
    [Supplier_Name] nvarchar(30)  NOT NULL,
    [Contact_Person] nvarchar(20)  NOT NULL,
    [Email_Address] nvarchar(30)  NOT NULL,
    [Telephone_Number] nvarchar(15)  NOT NULL,
    [Color] nchar(15)  NOT NULL
);
GO

-- Creating table 'Bag_Types'
CREATE TABLE [dbo].[Bag_Types] (
    [Bag_Code] int IDENTITY(1,1) NOT NULL,
    [Bag_Dimensions] nvarchar(50)  NOT NULL,
    [Bag_Description] nvarchar(20)  NOT NULL,
    [Material_Code] int  NOT NULL,
    [Supplier_Code] int  NOT NULL,
    [Bag_Cost] decimal(10,4)  NOT NULL,
    [Fiber_Code] int  NOT NULL
);
GO

-- Creating table 'Cages_Maintenance'
CREATE TABLE [dbo].[Cages_Maintenance] (
    [Cage_Code] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Filter_Code] nvarchar(50)  NOT NULL,
    [Replacement_Reason_Code] int  NOT NULL,
    [Sector_No] int  NOT NULL,
    [Valve_No] int  NOT NULL,
    [Bag_No] int  NOT NULL
);
GO

-- Creating table 'Compartments'
CREATE TABLE [dbo].[Compartments] (
    [Compartment_ID] int IDENTITY(1,1) NOT NULL,
    [No_Of_Beams] int  NOT NULL,
    [No_Of_Nozles] int  NOT NULL,
    [fans_No] nchar(20)  NOT NULL
);
GO

-- Creating table 'Departments'
CREATE TABLE [dbo].[Departments] (
    [Department_Code] nvarchar(20)  NOT NULL,
    [Department_Description] nvarchar(100)  NOT NULL,
    [No_Of_Filters] int  NOT NULL
);
GO

-- Creating table 'Fiber_Brand'
CREATE TABLE [dbo].[Fiber_Brand] (
    [Fiber_Code] int IDENTITY(1,1) NOT NULL,
    [Fiber_Supplier] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'Filters'
CREATE TABLE [dbo].[Filters] (
    [Filter_Code] nvarchar(50)  NOT NULL,
    [Filter_Description] nvarchar(50)  NOT NULL,
    [Department_Code] nvarchar(20)  NOT NULL,
    [Manufacturer_Code] int  NOT NULL,
    [No_Of_Sectors] int  NOT NULL,
    [Sector_No_Valves] int  NOT NULL,
    [Bags_Per_Valve] int  NOT NULL,
    [No_Of_Vibrators] int  NOT NULL,
    [X] int  NOT NULL,
    [Y] int  NOT NULL
);
GO

-- Creating table 'Manufacturers'
CREATE TABLE [dbo].[Manufacturers] (
    [Manufacturer_Code] int IDENTITY(1,1) NOT NULL,
    [Manufacturer_Name] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'Material_Types'
CREATE TABLE [dbo].[Material_Types] (
    [Material_Code] int IDENTITY(1,1) NOT NULL,
    [Material_Description] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Moveable_Beams'
CREATE TABLE [dbo].[Moveable_Beams] (
    [Compartment_ID] int  NOT NULL,
    [Beam_no] int  NOT NULL,
    [Moveable] bit  NOT NULL
);
GO

-- Creating table 'Nozle_Spares'
CREATE TABLE [dbo].[Nozle_Spares] (
    [Spare_ID] int IDENTITY(1,1) NOT NULL,
    [Nozle_ID] int  NOT NULL,
    [Spare_Store_Id] int  NOT NULL,
    [Description] nchar(50)  NOT NULL,
    [Supplier] nchar(20)  NOT NULL,
    [Cost] decimal(10,4)  NOT NULL,
    [Supplier_ID] int  NOT NULL
);
GO

-- Creating table 'Nozle_Types'
CREATE TABLE [dbo].[Nozle_Types] (
    [Nozle_ID] int  NOT NULL,
    [Nozle_Type] nchar(10)  NOT NULL,
    [Nozle_Description] nchar(50)  NOT NULL,
    [Nozle_no_of_Spare_Parts] int  NOT NULL
);
GO

-- Creating table 'Replacement_Reasons'
CREATE TABLE [dbo].[Replacement_Reasons] (
    [Replacement_Reason_Code] int IDENTITY(1,1) NOT NULL,
    [Replacement_Description] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Replacements'
CREATE TABLE [dbo].[Replacements] (
    [Replacement_ID] int IDENTITY(1,1) NOT NULL,
    [Beam_no] int  NOT NULL,
    [Nozle_no] int  NOT NULL,
    [Spare_ID] int  NOT NULL,
    [Compartment_ID] int  NOT NULL,
    [Nozle_ID] int  NOT NULL,
    [Date] datetime  NOT NULL,
    [Replacement_Reason_Code] int  NOT NULL,
    [Create_Date] datetime  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(10)  NOT NULL,
    [Password] nvarchar(10)  NOT NULL,
    [Role] nvarchar(15)  NOT NULL
);
GO

-- Creating table 'Valves_Maintenance'
CREATE TABLE [dbo].[Valves_Maintenance] (
    [Maintenance_Code] int IDENTITY(1,1) NOT NULL,
    [Maint_Date] datetime  NOT NULL,
    [Filter_Code] nvarchar(50)  NOT NULL,
    [Sector_No] int  NOT NULL,
    [Valve_No] int  NOT NULL
);
GO

-- Creating table 'Vibrators'
CREATE TABLE [dbo].[Vibrators] (
    [Vibrator_Code] int IDENTITY(1,1) NOT NULL,
    [Vibrator_Type] nvarchar(20)  NOT NULL,
    [Filter_Code] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Colors'
CREATE TABLE [dbo].[Colors] (
    [Color] nchar(15)  NOT NULL,
    [Available] bit  NOT NULL
);
GO

-- Creating table 'Nozle_Colors'
CREATE TABLE [dbo].[Nozle_Colors] (
    [Color] nchar(15)  NOT NULL,
    [Available] bit  NOT NULL
);
GO

-- Creating table 'Nozle_Suppliers'
CREATE TABLE [dbo].[Nozle_Suppliers] (
    [Supplier_ID] int IDENTITY(1,1) NOT NULL,
    [Supplier] nchar(20)  NOT NULL,
    [Color] nchar(15)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Replacement_Code] in table 'Bag_Replacement'
ALTER TABLE [dbo].[Bag_Replacement]
ADD CONSTRAINT [PK_Bag_Replacement]
    PRIMARY KEY CLUSTERED ([Replacement_Code] ASC);
GO

-- Creating primary key on [Supplier_Code] in table 'Bag_Suppliers'
ALTER TABLE [dbo].[Bag_Suppliers]
ADD CONSTRAINT [PK_Bag_Suppliers]
    PRIMARY KEY CLUSTERED ([Supplier_Code] ASC);
GO

-- Creating primary key on [Bag_Code] in table 'Bag_Types'
ALTER TABLE [dbo].[Bag_Types]
ADD CONSTRAINT [PK_Bag_Types]
    PRIMARY KEY CLUSTERED ([Bag_Code] ASC);
GO

-- Creating primary key on [Cage_Code] in table 'Cages_Maintenance'
ALTER TABLE [dbo].[Cages_Maintenance]
ADD CONSTRAINT [PK_Cages_Maintenance]
    PRIMARY KEY CLUSTERED ([Cage_Code] ASC);
GO

-- Creating primary key on [Compartment_ID] in table 'Compartments'
ALTER TABLE [dbo].[Compartments]
ADD CONSTRAINT [PK_Compartments]
    PRIMARY KEY CLUSTERED ([Compartment_ID] ASC);
GO

-- Creating primary key on [Department_Code] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [PK_Departments]
    PRIMARY KEY CLUSTERED ([Department_Code] ASC);
GO

-- Creating primary key on [Fiber_Code] in table 'Fiber_Brand'
ALTER TABLE [dbo].[Fiber_Brand]
ADD CONSTRAINT [PK_Fiber_Brand]
    PRIMARY KEY CLUSTERED ([Fiber_Code] ASC);
GO

-- Creating primary key on [Filter_Code] in table 'Filters'
ALTER TABLE [dbo].[Filters]
ADD CONSTRAINT [PK_Filters]
    PRIMARY KEY CLUSTERED ([Filter_Code] ASC);
GO

-- Creating primary key on [Manufacturer_Code] in table 'Manufacturers'
ALTER TABLE [dbo].[Manufacturers]
ADD CONSTRAINT [PK_Manufacturers]
    PRIMARY KEY CLUSTERED ([Manufacturer_Code] ASC);
GO

-- Creating primary key on [Material_Code] in table 'Material_Types'
ALTER TABLE [dbo].[Material_Types]
ADD CONSTRAINT [PK_Material_Types]
    PRIMARY KEY CLUSTERED ([Material_Code] ASC);
GO

-- Creating primary key on [Compartment_ID], [Beam_no] in table 'Moveable_Beams'
ALTER TABLE [dbo].[Moveable_Beams]
ADD CONSTRAINT [PK_Moveable_Beams]
    PRIMARY KEY CLUSTERED ([Compartment_ID], [Beam_no] ASC);
GO

-- Creating primary key on [Spare_ID] in table 'Nozle_Spares'
ALTER TABLE [dbo].[Nozle_Spares]
ADD CONSTRAINT [PK_Nozle_Spares]
    PRIMARY KEY CLUSTERED ([Spare_ID] ASC);
GO

-- Creating primary key on [Nozle_ID] in table 'Nozle_Types'
ALTER TABLE [dbo].[Nozle_Types]
ADD CONSTRAINT [PK_Nozle_Types]
    PRIMARY KEY CLUSTERED ([Nozle_ID] ASC);
GO

-- Creating primary key on [Replacement_Reason_Code] in table 'Replacement_Reasons'
ALTER TABLE [dbo].[Replacement_Reasons]
ADD CONSTRAINT [PK_Replacement_Reasons]
    PRIMARY KEY CLUSTERED ([Replacement_Reason_Code] ASC);
GO

-- Creating primary key on [Replacement_ID] in table 'Replacements'
ALTER TABLE [dbo].[Replacements]
ADD CONSTRAINT [PK_Replacements]
    PRIMARY KEY CLUSTERED ([Replacement_ID] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [UserID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [Maintenance_Code] in table 'Valves_Maintenance'
ALTER TABLE [dbo].[Valves_Maintenance]
ADD CONSTRAINT [PK_Valves_Maintenance]
    PRIMARY KEY CLUSTERED ([Maintenance_Code] ASC);
GO

-- Creating primary key on [Vibrator_Code] in table 'Vibrators'
ALTER TABLE [dbo].[Vibrators]
ADD CONSTRAINT [PK_Vibrators]
    PRIMARY KEY CLUSTERED ([Vibrator_Code] ASC);
GO

-- Creating primary key on [Color] in table 'Colors'
ALTER TABLE [dbo].[Colors]
ADD CONSTRAINT [PK_Colors]
    PRIMARY KEY CLUSTERED ([Color] ASC);
GO

-- Creating primary key on [Color] in table 'Nozle_Colors'
ALTER TABLE [dbo].[Nozle_Colors]
ADD CONSTRAINT [PK_Nozle_Colors]
    PRIMARY KEY CLUSTERED ([Color] ASC);
GO

-- Creating primary key on [Supplier_ID] in table 'Nozle_Suppliers'
ALTER TABLE [dbo].[Nozle_Suppliers]
ADD CONSTRAINT [PK_Nozle_Suppliers]
    PRIMARY KEY CLUSTERED ([Supplier_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Bag_Code] in table 'Bag_Replacement'
ALTER TABLE [dbo].[Bag_Replacement]
ADD CONSTRAINT [FK_Bag_Replacement_Bag_Types]
    FOREIGN KEY ([Bag_Code])
    REFERENCES [dbo].[Bag_Types]
        ([Bag_Code])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Bag_Replacement_Bag_Types'
CREATE INDEX [IX_FK_Bag_Replacement_Bag_Types]
ON [dbo].[Bag_Replacement]
    ([Bag_Code]);
GO

-- Creating foreign key on [Filter_Code] in table 'Bag_Replacement'
ALTER TABLE [dbo].[Bag_Replacement]
ADD CONSTRAINT [FK_Bag_Replacement_Filters]
    FOREIGN KEY ([Filter_Code])
    REFERENCES [dbo].[Filters]
        ([Filter_Code])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Bag_Replacement_Filters'
CREATE INDEX [IX_FK_Bag_Replacement_Filters]
ON [dbo].[Bag_Replacement]
    ([Filter_Code]);
GO

-- Creating foreign key on [Replacement_Reason_Code] in table 'Bag_Replacement'
ALTER TABLE [dbo].[Bag_Replacement]
ADD CONSTRAINT [FK_Bag_Replacement_Replacement_Reasons]
    FOREIGN KEY ([Replacement_Reason_Code])
    REFERENCES [dbo].[Replacement_Reasons]
        ([Replacement_Reason_Code])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Bag_Replacement_Replacement_Reasons'
CREATE INDEX [IX_FK_Bag_Replacement_Replacement_Reasons]
ON [dbo].[Bag_Replacement]
    ([Replacement_Reason_Code]);
GO

-- Creating foreign key on [Supplier_Code] in table 'Bag_Types'
ALTER TABLE [dbo].[Bag_Types]
ADD CONSTRAINT [FK_Bag_Types_Bag_Types]
    FOREIGN KEY ([Supplier_Code])
    REFERENCES [dbo].[Bag_Suppliers]
        ([Supplier_Code])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Bag_Types_Bag_Types'
CREATE INDEX [IX_FK_Bag_Types_Bag_Types]
ON [dbo].[Bag_Types]
    ([Supplier_Code]);
GO

-- Creating foreign key on [Fiber_Code] in table 'Bag_Types'
ALTER TABLE [dbo].[Bag_Types]
ADD CONSTRAINT [FK_Bag_Types_Fiber_Brand]
    FOREIGN KEY ([Fiber_Code])
    REFERENCES [dbo].[Fiber_Brand]
        ([Fiber_Code])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Bag_Types_Fiber_Brand'
CREATE INDEX [IX_FK_Bag_Types_Fiber_Brand]
ON [dbo].[Bag_Types]
    ([Fiber_Code]);
GO

-- Creating foreign key on [Material_Code] in table 'Bag_Types'
ALTER TABLE [dbo].[Bag_Types]
ADD CONSTRAINT [FK_Bag_Types_Material_Types]
    FOREIGN KEY ([Material_Code])
    REFERENCES [dbo].[Material_Types]
        ([Material_Code])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Bag_Types_Material_Types'
CREATE INDEX [IX_FK_Bag_Types_Material_Types]
ON [dbo].[Bag_Types]
    ([Material_Code]);
GO

-- Creating foreign key on [Filter_Code] in table 'Cages_Maintenance'
ALTER TABLE [dbo].[Cages_Maintenance]
ADD CONSTRAINT [FK_Filters_Cage_maintenace]
    FOREIGN KEY ([Filter_Code])
    REFERENCES [dbo].[Filters]
        ([Filter_Code])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Filters_Cage_maintenace'
CREATE INDEX [IX_FK_Filters_Cage_maintenace]
ON [dbo].[Cages_Maintenance]
    ([Filter_Code]);
GO

-- Creating foreign key on [Compartment_ID] in table 'Moveable_Beams'
ALTER TABLE [dbo].[Moveable_Beams]
ADD CONSTRAINT [FK_Moveable_Beams_Compartments]
    FOREIGN KEY ([Compartment_ID])
    REFERENCES [dbo].[Compartments]
        ([Compartment_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Compartment_ID] in table 'Replacements'
ALTER TABLE [dbo].[Replacements]
ADD CONSTRAINT [FK_Replacements_Compartments]
    FOREIGN KEY ([Compartment_ID])
    REFERENCES [dbo].[Compartments]
        ([Compartment_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Replacements_Compartments'
CREATE INDEX [IX_FK_Replacements_Compartments]
ON [dbo].[Replacements]
    ([Compartment_ID]);
GO

-- Creating foreign key on [Department_Code] in table 'Filters'
ALTER TABLE [dbo].[Filters]
ADD CONSTRAINT [FK_Filters_Departments]
    FOREIGN KEY ([Department_Code])
    REFERENCES [dbo].[Departments]
        ([Department_Code])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Filters_Departments'
CREATE INDEX [IX_FK_Filters_Departments]
ON [dbo].[Filters]
    ([Department_Code]);
GO

-- Creating foreign key on [Manufacturer_Code] in table 'Filters'
ALTER TABLE [dbo].[Filters]
ADD CONSTRAINT [FK_Filters_Manufacturers]
    FOREIGN KEY ([Manufacturer_Code])
    REFERENCES [dbo].[Manufacturers]
        ([Manufacturer_Code])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Filters_Manufacturers'
CREATE INDEX [IX_FK_Filters_Manufacturers]
ON [dbo].[Filters]
    ([Manufacturer_Code]);
GO

-- Creating foreign key on [Filter_Code] in table 'Valves_Maintenance'
ALTER TABLE [dbo].[Valves_Maintenance]
ADD CONSTRAINT [FK_Valves_Maintenance_Filters]
    FOREIGN KEY ([Filter_Code])
    REFERENCES [dbo].[Filters]
        ([Filter_Code])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Valves_Maintenance_Filters'
CREATE INDEX [IX_FK_Valves_Maintenance_Filters]
ON [dbo].[Valves_Maintenance]
    ([Filter_Code]);
GO

-- Creating foreign key on [Filter_Code] in table 'Vibrators'
ALTER TABLE [dbo].[Vibrators]
ADD CONSTRAINT [FK_Vibrators_Filters]
    FOREIGN KEY ([Filter_Code])
    REFERENCES [dbo].[Filters]
        ([Filter_Code])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Vibrators_Filters'
CREATE INDEX [IX_FK_Vibrators_Filters]
ON [dbo].[Vibrators]
    ([Filter_Code]);
GO

-- Creating foreign key on [Nozle_ID] in table 'Nozle_Spares'
ALTER TABLE [dbo].[Nozle_Spares]
ADD CONSTRAINT [FK_Nozle_Spares_Nozle_Types]
    FOREIGN KEY ([Nozle_ID])
    REFERENCES [dbo].[Nozle_Types]
        ([Nozle_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Nozle_Spares_Nozle_Types'
CREATE INDEX [IX_FK_Nozle_Spares_Nozle_Types]
ON [dbo].[Nozle_Spares]
    ([Nozle_ID]);
GO

-- Creating foreign key on [Spare_ID] in table 'Replacements'
ALTER TABLE [dbo].[Replacements]
ADD CONSTRAINT [FK_Replacements_Nozle_Spares]
    FOREIGN KEY ([Spare_ID])
    REFERENCES [dbo].[Nozle_Spares]
        ([Spare_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Replacements_Nozle_Spares'
CREATE INDEX [IX_FK_Replacements_Nozle_Spares]
ON [dbo].[Replacements]
    ([Spare_ID]);
GO

-- Creating foreign key on [Nozle_ID] in table 'Replacements'
ALTER TABLE [dbo].[Replacements]
ADD CONSTRAINT [FK_Replacements_Nozle_Types]
    FOREIGN KEY ([Nozle_ID])
    REFERENCES [dbo].[Nozle_Types]
        ([Nozle_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Replacements_Nozle_Types'
CREATE INDEX [IX_FK_Replacements_Nozle_Types]
ON [dbo].[Replacements]
    ([Nozle_ID]);
GO

-- Creating foreign key on [Replacement_Reason_Code] in table 'Cages_Maintenance'
ALTER TABLE [dbo].[Cages_Maintenance]
ADD CONSTRAINT [FK_Reasons_Cage_maintenace]
    FOREIGN KEY ([Replacement_Reason_Code])
    REFERENCES [dbo].[Replacement_Reasons]
        ([Replacement_Reason_Code])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Reasons_Cage_maintenace'
CREATE INDEX [IX_FK_Reasons_Cage_maintenace]
ON [dbo].[Cages_Maintenance]
    ([Replacement_Reason_Code]);
GO

-- Creating foreign key on [Color] in table 'Bag_Suppliers'
ALTER TABLE [dbo].[Bag_Suppliers]
ADD CONSTRAINT [FK_Bag_Suppliers_Colors]
    FOREIGN KEY ([Color])
    REFERENCES [dbo].[Colors]
        ([Color])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Bag_Suppliers_Colors'
CREATE INDEX [IX_FK_Bag_Suppliers_Colors]
ON [dbo].[Bag_Suppliers]
    ([Color]);
GO

-- Creating foreign key on [Supplier_ID] in table 'Nozle_Spares'
ALTER TABLE [dbo].[Nozle_Spares]
ADD CONSTRAINT [FK_Nozle_Spares_Nozle_Suppliers]
    FOREIGN KEY ([Supplier_ID])
    REFERENCES [dbo].[Nozle_Suppliers]
        ([Supplier_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Nozle_Spares_Nozle_Suppliers'
CREATE INDEX [IX_FK_Nozle_Spares_Nozle_Suppliers]
ON [dbo].[Nozle_Spares]
    ([Supplier_ID]);
GO

-- Creating foreign key on [Replacement_Reason_Code] in table 'Replacements'
ALTER TABLE [dbo].[Replacements]
ADD CONSTRAINT [FK_Replacements_Reasons]
    FOREIGN KEY ([Replacement_Reason_Code])
    REFERENCES [dbo].[Replacement_Reasons]
        ([Replacement_Reason_Code])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Replacements_Reasons'
CREATE INDEX [IX_FK_Replacements_Reasons]
ON [dbo].[Replacements]
    ([Replacement_Reason_Code]);
GO

-- Creating foreign key on [Color] in table 'Nozle_Suppliers'
ALTER TABLE [dbo].[Nozle_Suppliers]
ADD CONSTRAINT [FK_Nozle_Suppliers_Nozle_Colors]
    FOREIGN KEY ([Color])
    REFERENCES [dbo].[Nozle_Colors]
        ([Color])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Nozle_Suppliers_Nozle_Colors'
CREATE INDEX [IX_FK_Nozle_Suppliers_Nozle_Colors]
ON [dbo].[Nozle_Suppliers]
    ([Color]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------