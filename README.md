# CGIBusinessCards
CGI Business Cards Project

## Requirements:
1. Local MSSQL Server
2. Visual Studio

## Used Components/Templates/Code:
1. No External components are used
2. Standard .NET 7.0 Templates was used as a base for the project
3. Standard project structure was used (from my code library)
4. BaseDataAccess code class is used (from my code library)
5. SqlDataReader Extension is used (from my code library)
6. Swagger Authorization CodeSnippet is used (from my code library)

## How to Install:

### Initialize Secret Store:
1. cd .\CGIBusinessCards.Web.Api\
2. dotnet user-secrets init
3. dotnet user-secrets set "CGIBusinessCards:SwaggerAuthenticationUser" "CGI"
4. dotnet user-secrets set "CGIBusinessCards:SwaggerAuthenticationPassword" "!CGI"

### Create MSSQL Database:
1. Create Database
Name: CGIBusinessCardsDB

2. Create Table
```
USE [CGIBusinessCardsDB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblBusinessCards] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]   VARCHAR (50)  NULL,
    [LastName]    VARCHAR (50)  NULL,
    [PhoneNumber] VARCHAR (20)  NULL,
    [Email]       VARCHAR (100) NULL,
    [Image]       VARCHAR (255) NULL
);
```

3. Seed Test Data
```
INSERT INTO [dbo].[tblBusinessCards] ([FirstName], [LastName], [PhoneNumber], [Email], [Image]) VALUES (N'Kalle', N'Karlsson', N'054-303030', N'kalle.karlsson@karlsson.se', N'https://images.cgi.se/images/8768687/')
INSERT INTO [dbo].[tblBusinessCards] ([FirstName], [LastName], [PhoneNumber], [Email], [Image]) VALUES (N'Lars', N'Larsson', N'054-202020', N'lars.larssom@larsson.com', N'https://images.com/image/3948347/')
INSERT INTO [dbo].[tblBusinessCards] ([FirstName], [LastName], [PhoneNumber], [Email], [Image]) VALUES (N'Pelle', N'Persson', N'054-404040', N'pelle.persson@persson.se', N'https://images.cgi.se/images/7546/')
```

### Update Project Settings:
1. Update ConnectionString in: appsettings.json 
```
Data Source=.\SQLEXPRESS;Initial Catalog=CGIBusinessCardsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;
```
