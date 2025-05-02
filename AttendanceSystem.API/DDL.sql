use attendance_schema;
SET FOREIGN_KEY_CHECKS = 0;

-- drop table Course;
Create table Course (
	COURSE_ID varchar(25) NOT NULL,	-- Course#.Section#
    COURSE_NAME	varchar(25) NOT NULL,
    START_TIME Time	NOT NULL,
    END_TIME Time NOT NULL,
    
    Constraint COURSE_PK 
		Primary Key (COURSE_ID)
);

-- drop table Student;
Create table Student (
	UTD_ID varchar(10) NOT NULL,
    FIRST_NAME varchar(25) NOT NULL,
    LAST_NAME varchar(25) NOT NULL,
    NET_ID varchar(10),
    
    Constraint STUDENT_PK
		Primary Key (UTD_ID)
);

-- drop table Quiz;
Create table Quiz (
	QUIZ_ID int NOT NULL AUTO_INCREMENT,
    due_date date,
    POOL_ID int NOT NULL,
    
    Constraint QUIZ_PK
		Primary Key (QUIZ_ID),
	constraint POOL_FK2
		foreign key (POOL_ID) references Question_Pool(POOL_ID)
			ON DELETE Restrict ON UPDATE Cascade
);

-- drop table class_session;
Create table class_session (
	SESSION_DATE Date NOT NULL,
    COURSE_ID varchar(25) NOT NULL,
    PASSWORD varchar(100) NOT NULL,
    QUIZ_ID int,
    
    Constraint CLASS_SESSION_PK
		Primary Key (SESSION_DATE, COURSE_ID),
	Constraint COURSE_FK
		Foreign Key (COURSE_ID) References Course(COURSE_ID)
			ON DELETE Cascade ON UPDATE Cascade,
	Constraint QUIZ_FK
		Foreign Key (QUIZ_ID) References Quiz(QUIZ_ID)
			ON DELETE Set null ON UPDATE Cascade
);

-- drop table Attended_By;
Create table Attended_By (
	ATTENDANCE_ID int NOT NULL AUTO_INCREMENT,
	SESSION_DATE Date NOT NULL,
    COURSE_ID varchar(25) NOT NULL,
    UTD_ID varchar(10) NOT NULL,
    
    UNIQUE(SESSION_DATE, COURSE_ID, UTD_ID),
    
    Constraint ATTENDED_BY_PK
		Primary Key (ATTENDANCE_ID),
	Constraint CLASS_SESSION_FK
		Foreign Key (SESSION_DATE, COURSE_ID) References class_session(SESSION_DATE, COURSE_ID)
			ON DELETE Cascade On Update Cascade,
	Constraint STUDENT_FK
		Foreign Key (UTD_ID) References Student(UTD_ID)
			ON DELETE Cascade On UPDATE Cascade
);

-- drop table Submissions;
Create table Submissions (
	SUBMISSION_ID int NOT NULL AUTO_INCREMENT,
	COURSE_ID varchar(25) NOT NULL,
    SESSION_DATE Date NOT NULL,
    UTD_ID varchar(10) NOT NULL,
    QUIZ_ID int NOT NULL,
    IP_ADDRESS varchar(15) NOT NULL,
    SUBMISSION_TIME datetime NOT NULL,
    ANSWER1 char(1),
    ANSWER2 char(1),
    ANSWER3 char(1),
    STATUS varchar(20),
    
    UNIQUE(COURSE_ID, SESSION_DATE, UTD_ID, QUIZ_ID),
    
    Constraint SUBMISSIONS_PK
		Primary Key (SUBMISSION_ID),
	Constraint CLASS_SESSION_FK2
		Foreign Key (SESSION_DATE, COURSE_ID) References class_session(SESSION_DATE, COURSE_ID)
			ON DELETE Cascade ON UPDATE Cascade,
	Constraint STUDENT_FK2
		Foreign Key (UTD_ID) References Student(UTD_ID)
			ON DELETE Cascade ON UPDATE Cascade,
	Constraint QUIZ_FK2
		Foreign Key (QUIZ_ID) References Quiz(QUIZ_ID)
			ON DELETE Cascade ON UPDATE Cascade
);

-- drop table Question_Pool;
create table Question_Pool (
	POOL_ID int NOT NULL AUTO_INCREMENT,
    POOL_NAME varchar(25) NOT NULL,
    COURSE_ID varchar(25) NOT NULL,
    
    constraint POOL_PK
		primary key (POOL_ID),
	constraint COURSE_ID
		foreign key (COURSE_ID) references Course(COURSE_ID)
);

-- drop table Questions;
create table Questions (
	QUESTIONS_ID int NOT NULL AUTO_INCREMENT,
    TEXT TEXT NOT NULL,
    OPTION_A varchar(50) NOT NULL,
    OPTION_B varchar(50) NOT NULL,
    OPTION_C varchar(50),
    OPTION_D varchar(50),
    CORRECT_ANSWER char(1) NOT NULL,
    QUIZ_ID int,
    POOL_ID int NOT NULL,
    
    Constraint QUESTIONS_PK
		primary key (QUESTIONS_ID),
	constraint QUIZ_FK3
		foreign key (QUIZ_ID) references Quiz(QUIZ_ID)
			ON DELETE Set null ON UPDATE Cascade,
	constraint POOL_FK
		foreign key (POOL_ID) references Question_Pool(POOL_ID)
			ON DELETE Restrict ON UPDATE Cascade
);
    
        


