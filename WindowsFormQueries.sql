CREATE TABLE Form_EmployeeDetails (
[ID] INT IDENTITY NOT NULL,
[Emp_Id] VARCHAR(10) NOT NULL,
[FirstName] VARCHAR(30) NOT NULL,
[LastName] VARCHAR(30) NOT NULL,
[DOB] DATE NOT NULL,
[GENDER] VARCHAR(10) NOT NULL,
[Email] VARCHAR(40) NOT NULL,
[Password] VARCHAR(20) NOT NULL,
[City] VARCHAR(30) NOT NULL,
[State] VARCHAR(30) NOT NULL,
[PhoneNumber] VARCHAR(15) NOT NULL,
[isActive] INT DEFAULT 1,
CONSTRAINT PK_Form_EmployeeDetails PRIMARY KEY (Emp_Id));

CREATE TABLE Form_Attendance (
[ID] INT IDENTITY NOT NULL,
[Emp_Id] VARCHAR(10) NOT NULL,
[Date] DATE NOT NULL,
[Login_Time] VARCHAR(30) NOT NULL,
[Logout_Time] VARCHAR(30) NOT NULL,
CONSTRAINT PK_Form_Attendance PRIMARY KEY (ID),
CONSTRAINT FK_Form_Attendance FOREIGN KEY (Emp_Id) REFERENCES Form_EmployeeDetails (Emp_Id));



INSERT INTO Form_EmployeeDetails ([Emp_Id], [FirstName], [LastName], [DOB], [GENDER], [Email], [Password], [City], [State], [PhoneNumber])
VALUES ('0692', 'Jegadeeshwar', 'T', '2002-06-01', 'Male', 'jegadeeshoff@gmail.com', 'Jega@123', 'Rajapalayam', 'Tamilnadu', '8838315207');

INSERT INTO Form_Attendance ([Date], [Login_Time], [Logout_Time])
VALUES (GETDATE(), convert(varchar, getdate(), 8), '-');

UPDATE Form_Attendance SET Logout_Time = convert(varchar, getdate(), 8) WHERE Logout_Time = '-';

SELECT * FROM Form_EmployeeDetails;
SELECT * FROM Form_Attendance;

--GetLogin
ALTER PROCEDURE GetLogin (@Email NVARCHAR(50), @Password NVARCHAR(30))
AS
BEGIN
    SELECT Emp_Id, Email, Password
    FROM Form_EmployeeDetails
    WHERE Email = @Email AND Password = @Password
END

--GetEmployeeDetails
CREATE PROCEDURE GetEmployeeDetails
AS
BEGIN
SELECT * FROM Form_EmployeeDetails;
END

--GetAttendanceDetails
ALTER PROCEDURE GetAttendanceDetails
AS
BEGIN
SELECT * FROM Form_Attendance
ORDER BY [Date] DESC, [Login_Time] DESC;
END

--InsertAttendance
CREATE PROCEDURE InsertAttendance
AS
BEGIN
    INSERT INTO Form_Attendance ([Date], [Login_Time], [Logout_Time])
    VALUES (GETDATE(), CONVERT(VARCHAR, GETDATE(), 8), '-');
END

--UpdateAttendance
CREATE PROCEDURE UpdateAttendance
AS
BEGIN
UPDATE Form_Attendance SET Logout_Time = convert(varchar, getdate(), 8) WHERE Logout_Time = '-';
END


--
---
----
CREATE TABLE Form_EmployeeDetails_2 (
[ID] INT IDENTITY(1,1) NOT NULL,
[Emp_Id] AS RIGHT('0000' + CAST(ID AS VARCHAR(4)),4) PERSISTED,
[FirstName] VARCHAR(30) NOT NULL,
[LastName] VARCHAR(30) NOT NULL,
[DOB] DATE NOT NULL,
[GENDER] VARCHAR(10) NOT NULL,
[Email] VARCHAR(40) NOT NULL,
[Password] VARCHAR(20) NOT NULL,
[City] VARCHAR(30) NOT NULL,
[State] VARCHAR(30) NOT NULL,
[PhoneNumber] VARCHAR(15) NOT NULL,
[isActive] INT DEFAULT 1,
CONSTRAINT PK_Form_EmployeeDetails_2 PRIMARY KEY (Emp_Id));

CREATE TABLE Form_Attendance_2 (
[ID] INT IDENTITY NOT NULL,
[Emp_Id] VARCHAR(4) NOT NULL,
[Date] DATE NOT NULL,
[Login_Time] VARCHAR(30) NOT NULL,
[Logout_Time] VARCHAR(30) NOT NULL,
CONSTRAINT PK_Form_Attendance_2 PRIMARY KEY (ID),
CONSTRAINT FK_Form_Attendance_2 FOREIGN KEY (Emp_Id) REFERENCES Form_EmployeeDetails_2 (Emp_Id));
---
----
-----