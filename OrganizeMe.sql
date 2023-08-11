--DROP Scripts
USE [master]
GO
IF (EXISTS (SELECT name
FROM master.dbo.sysdatabases
WHERE ('[' + name + ']' = N'OrganizeMeFinalDB' OR name = N'OrganizeMeFinalDB')))
DROP DATABASE OrganizeMeFinalDB

CREATE DATABASE OrganizeMeFinalDB
GO
Use OrganizeMeFinalDB
GO

IF OBJECT_ID('Users')  IS NOT NULL
DROP TABLE Users
GO

IF OBJECT_ID('TaskCategory')  IS NOT NULL
DROP TABLE TaskCategory
GO

IF OBJECT_ID('Task')  IS NOT NULL
DROP TABLE Task
GO

IF OBJECT_ID('TaskList')  IS NOT NULL
DROP TABLE TaskList
GO

IF OBJECT_ID('ufn_CheckEmailId')  IS NOT NULL
DROP FUNCTION ufn_CheckEmailId
GO

IF OBJECT_ID('ufn_ValidateUserCredentials')  IS NOT NULL
DROP FUNCTION ufn_ValidateUserCredentials
GO

IF OBJECT_ID('ufn_GenerateNewTaskCategoryId')  IS NOT NULL
DROP FUNCTION ufn_GenerateNewTaskCategoryId
GO

IF OBJECT_ID('ufn_GenerateNewTaskListId')  IS NOT NULL
DROP FUNCTION ufn_GenerateNewTaskListId
GO

IF OBJECT_ID('ufn_GenerateNewTaskId')  IS NOT NULL
DROP FUNCTION ufn_GenerateNewTaskId
GO


CREATE TABLE Users
(
	[EmpId] NUMERIC(7) CONSTRAINT pk_EmpId PRIMARY KEY,--Product/Price
	[UserName] VARCHAR(20) NOT NULL,
	[UserPassword] VARCHAR(15) NOT NULL,
	[DateOfBirth] DATE CONSTRAINT chk_DateOfBirth CHECK(DateOfBirth<GETDATE()) NOT NULL,
	[UserAddress] VARCHAR(200) NOT NULL
)
GO

CREATE TABLE TaskList
(
	[TaskListId] TINYINT CONSTRAINT pk_TaskListId PRIMARY KEY,
	[TaskListName] VARCHAR(20) NOT NULL,
	[DeleteFlagTl] TINYINT CONSTRAINT chk_tlDeleteFlag CHECK(DeleteFlagTl BETWEEN 0 AND 1) DEFAULT 0 NOT NULL
);
GO

CREATE TABLE TaskCategory
(
	[TaskCategoryId] TINYINT CONSTRAINT pk_TaskCategoryId PRIMARY KEY,
	[TaskCategoryName] VARCHAR(20) NOT NULL,
	[DeleteFlagTc] TINYINT CONSTRAINT chk_tcDeleteFlag CHECK(DeleteFlagTc BETWEEN 0 AND 1) DEFAULT 0 NOT NULL
);
GO

CREATE TABLE Task
(
	[EmpId] NUMERIC(7) CONSTRAINT fk_EmpId REFERENCES Users(EmpId),
	[TaskId] CHAR(4) CONSTRAINT pk_TaskId PRIMARY KEY CONSTRAINT chk_TaskId CHECK(TaskId LIKE 'T%') NOT NULL,
	[TaskName] VARCHAR(50) NOT NULL,
	[TaskNote] VARCHAR(100),
	[TaskCategoryId] TINYINT CONSTRAINT fk_TaskCategoryId REFERENCES TaskCategory(TaskCategoryId),
	[TaskListId] TINYINT CONSTRAINT fk_TaskListId REFERENCES TaskList(TaskListId),
	[DateCreated] DATETIME CONSTRAINT chk_DateCreated DEFAULT GETDATE() NOT NULL,
	[DueDate] DATETIME CONSTRAINT chk_DueDate CHECK(DueDate>=GETDATE()),
	[AssignedTo] NUMERIC(7) CONSTRAINT fk_AssignedTo REFERENCES Users(EmpId),
	[Priority] TINYINT CONSTRAINT chk_Priority CHECK(Priority BETWEEN 0 AND 5) DEFAULT 0 NOT NULL,
	[TaskStatus] TINYINT CONSTRAINT chk_TaskStatus CHECK(TaskStatus BETWEEN 0 AND 1) DEFAULT 0 NOT NULL,
	[DeleteFlagT] TINYINT CONSTRAINT chk_tDeleteFlag CHECK(DeleteFlagT BETWEEN 0 AND 1) DEFAULT 0 NOT NULL
);
GO

CREATE FUNCTION [dbo].[ufn_GenerateNewTaskId]()
RETURNS CHAR(4)
AS
BEGIN

	DECLARE @TaskId CHAR(4)

	IF NOT EXISTS(SELECT TaskId
	FROM Task)
		SET @TaskId='T100'
		
	ELSE
		SELECT @TaskId='T'+CAST(CAST(SUBSTRING(MAX(TaskId),2,3) AS INT)+1 AS CHAR)
	FROM Task

	RETURN @TaskId

END
GO


CREATE FUNCTION [dbo].[ufn_GenerateNewTaskCategoryId]()
RETURNS TINYINT
AS
BEGIN

	DECLARE @TaskCategoryId TINYINT

	IF NOT EXISTS(SELECT TaskCategoryId
	FROM TaskCategory)
		SET @TaskCategoryId ='1'
		
	ELSE
		SELECT @TaskCategoryId =MAX(TaskCategoryId)+1
	FROM TaskCategory

	RETURN @TaskCategoryId

END
GO


CREATE FUNCTION [dbo].[ufn_GenerateNewTaskListId]()
RETURNS TINYINT
AS
BEGIN

	DECLARE @TaskListId TINYINT

	IF NOT EXISTS(SELECT TaskListId
	FROM TaskList)
		SET @TaskListId ='1'
		
	ELSE
		SELECT @TaskListId =MAX(TaskListId)+1
	FROM TaskList

	RETURN @TaskListId

END
GO


--insertion scripts


--Users
INSERT INTO Users
	(EmpId, UserName, UserPassword, DateOfBirth, UserAddress)
VALUES
	(1300111, 'Nidhi', 'QWER@1234', '1999-03-15', 'Surathkal')
INSERT INTO Users
	(EmpId, UserName, UserPassword, DateOfBirth, UserAddress)
VALUES
	(1300112, 'Vaibhav', 'QWER@1234', '2001-02-03', 'Surathkal')
INSERT INTO Users
	(EmpId, UserName, UserPassword, DateOfBirth, UserAddress)
VALUES
	(1300113, 'Adarsh', 'QWER@1234', '2003-12-23', 'Surathkal')
INSERT INTO Users
	(EmpId, UserName, UserPassword, DateOfBirth, UserAddress)
VALUES
	(1300114, 'Abhijeeth', 'QWER@1234', '1996-04-17', 'Surathkal')

--TaskCategory
INSERT INTO TaskCategory
	(TaskCategoryId, TaskCategoryName, DeleteFlagTc)
VALUES
	(dbo.ufn_GenerateNewTaskCategoryId(), 'Category-A', DEFAULT)
INSERT INTO TaskCategory
	(TaskCategoryId, TaskCategoryName, DeleteFlagTc)
VALUES
	(dbo.ufn_GenerateNewTaskCategoryId(), 'Category-B', DEFAULT)
INSERT INTO TaskCategory
	(TaskCategoryId, TaskCategoryName, DeleteFlagTc)
VALUES
	(dbo.ufn_GenerateNewTaskCategoryId(), 'Category-C', DEFAULT)
INSERT INTO TaskCategory
	(TaskCategoryId, TaskCategoryName, DeleteFlagTc)
VALUES
	(dbo.ufn_GenerateNewTaskCategoryId(), 'Category-D', DEFAULT)
INSERT INTO TaskCategory
	(TaskCategoryId, TaskCategoryName, DeleteFlagTc)
VALUES
	(dbo.ufn_GenerateNewTaskCategoryId(), 'Category-E', DEFAULT)
INSERT INTO TaskCategory
	(TaskCategoryId, TaskCategoryName, DeleteFlagTc)
VALUES
	(dbo.ufn_GenerateNewTaskCategoryId(), 'Category-F', DEFAULT)
GO


--TaskList
INSERT INTO TaskList
	(TaskListId, TaskListName, DeleteFlagTl)
VALUES
	(dbo.ufn_GenerateNewTaskListId(), 'List 1', DEFAULT)
INSERT INTO TaskList
	(TaskListId, TaskListName, DeleteFlagTl)
VALUES
	(dbo.ufn_GenerateNewTaskListId(), 'List 2', DEFAULT)
INSERT INTO TaskList
	(TaskListId, TaskListName, DeleteFlagTl)
VALUES
	(dbo.ufn_GenerateNewTaskListId(), 'List 3', DEFAULT)
INSERT INTO TaskList
	(TaskListId, TaskListName, DeleteFlagTl)
VALUES
	(dbo.ufn_GenerateNewTaskListId(), 'List 4', DEFAULT)
INSERT INTO TaskList
	(TaskListId, TaskListName, DeleteFlagTl)
VALUES
	(dbo.ufn_GenerateNewTaskListId(), 'List 5', DEFAULT)
INSERT INTO TaskList
	(TaskListId, TaskListName, DeleteFlagTl)
VALUES
	(dbo.ufn_GenerateNewTaskListId(), 'List 6', DEFAULT)


--Task
INSERT INTO Task
	(EmpId, TaskId, TaskName, TaskNote, TaskCategoryId, TaskListId, DateCreated, DueDate, AssignedTo, Priority, TaskStatus, DeleteFlagT)
VALUES('1300111', dbo.ufn_GenerateNewTaskId(), 'Finish Project1', 'Finish by 12PM today', NULL, NULL, DEFAULT, NULL, NULL, DEFAULT, DEFAULT, DEFAULT)
INSERT INTO Task
	(EmpId, TaskId, TaskName, TaskNote, TaskCategoryId, TaskListId, DateCreated, DueDate, AssignedTo, Priority, TaskStatus, DeleteFlagT)
VALUES('1300111', dbo.ufn_GenerateNewTaskId(), 'Finish Project1', 'Finish by 12PM today', NULL, NULL, DEFAULT, NULL, NULL, DEFAULT, DEFAULT, DEFAULT)
INSERT INTO Task
	(EmpId, TaskId, TaskName, TaskNote, TaskCategoryId, TaskListId, DateCreated, DueDate, AssignedTo, Priority, TaskStatus, DeleteFlagT)
VALUES('1300112', dbo.ufn_GenerateNewTaskId(), 'Finish Project2', 'Finish by 12PM today', NULL, NULL, DEFAULT, NULL, NULL, DEFAULT, DEFAULT, DEFAULT)
INSERT INTO Task
	(EmpId, TaskId, TaskName, TaskNote, TaskCategoryId, TaskListId, DateCreated, DueDate, AssignedTo, Priority, TaskStatus, DeleteFlagT)
VALUES('1300112', dbo.ufn_GenerateNewTaskId(), 'Finish Project2', 'Finish by 12PM today', NULL, NULL, DEFAULT, NULL, NULL, DEFAULT, DEFAULT, DEFAULT)
INSERT INTO Task
	(EmpId, TaskId, TaskName, TaskNote, TaskCategoryId, TaskListId, DateCreated, DueDate, AssignedTo, Priority, TaskStatus, DeleteFlagT)
VALUES('1300112', dbo.ufn_GenerateNewTaskId(), 'Finish Project2', 'Finish by 12PM today', NULL, NULL, DEFAULT, NULL, NULL, DEFAULT, DEFAULT, DEFAULT)
INSERT INTO Task
	(EmpId, TaskId, TaskName, TaskNote, TaskCategoryId, TaskListId, DateCreated, DueDate, AssignedTo, Priority, TaskStatus, DeleteFlagT)
VALUES('1300113', dbo.ufn_GenerateNewTaskId(), 'Finish Project3', 'Finish by 12PM today', NULL, NULL, DEFAULT, NULL, NULL, DEFAULT, DEFAULT, DEFAULT)
INSERT INTO Task
	(EmpId, TaskId, TaskName, TaskNote, TaskCategoryId, TaskListId, DateCreated, DueDate, AssignedTo, Priority, TaskStatus, DeleteFlagT)
VALUES('1300113', dbo.ufn_GenerateNewTaskId(), 'Finish Project3', 'Finish by 12PM today', NULL, NULL, DEFAULT, NULL, NULL, DEFAULT, DEFAULT, DEFAULT)
INSERT INTO Task
	(EmpId, TaskId, TaskName, TaskNote, TaskCategoryId, TaskListId, DateCreated, DueDate, AssignedTo, Priority, TaskStatus, DeleteFlagT)
VALUES('1300113', dbo.ufn_GenerateNewTaskId(), 'Finish Project3', 'Finish by 12PM today', NULL, NULL, DEFAULT, NULL, NULL, DEFAULT, DEFAULT, DEFAULT)
INSERT INTO Task
	(EmpId, TaskId, TaskName, TaskNote, TaskCategoryId, TaskListId, DateCreated, DueDate, AssignedTo, Priority, TaskStatus, DeleteFlagT)
VALUES('1300114', dbo.ufn_GenerateNewTaskId(), 'Finish Project4', 'Finish by 12PM today', NULL, NULL, DEFAULT, NULL, NULL, DEFAULT, DEFAULT, DEFAULT)
INSERT INTO Task
	(EmpId, TaskId, TaskName, TaskNote, TaskCategoryId, TaskListId, DateCreated, DueDate, AssignedTo, Priority, TaskStatus, DeleteFlagT)
VALUES('1300114', dbo.ufn_GenerateNewTaskId(), 'Finish Project4', 'Finish by 12PM today', NULL, NULL, DEFAULT, NULL, NULL, DEFAULT, DEFAULT, DEFAULT)