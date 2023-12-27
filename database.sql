--Create tables
CREATE TABLE Gender (Id SERIAL NOT NULL, Gender varchar(255) NOT NULL, PRIMARY KEY (Id));
CREATE TABLE "USER" (
    Id SERIAL NOT NULL,
    FirstName varchar(255) NOT NULL,
    LastName varchar(255) NOT NULL,
    Email varchar(255) NOT NULL UNIQUE,
    Phone varchar(255),
    Address varchar(255),
    GenderId int4,
    PRIMARY KEY (Id),
    CONSTRAINT FKUSER309022 FOREIGN KEY (GenderId) REFERENCES Gender (Id)
);

--Drop tables
ALTER TABLE "USER" DROP CONSTRAINT FKUSER309022;
DROP TABLE IF EXISTS Gender CASCADE;
DROP TABLE IF EXISTS "USER" CASCADE;

--Select
SELECT Id, Gender FROM Gender;
SELECT Id, FirstName, LastName, Email, Phone, Address, GenderId FROM "USER";

--Insert
INSERT INTO Gender(Id, Gender) VALUES (?, ?);
INSERT INTO "USER"(Id, FirstName, LastName, Email, Phone, Address, GenderId) VALUES (?, ?, ?, ?, ?, ?, ?);

--Update
UPDATE Gender SET Gender = ? WHERE Id = ?;
UPDATE "USER" SET FirstName = ?, LastName = ?, Email = ?, Phone = ?, Address = ?, GenderId = ? WHERE Id = ?;

--Delete
DELETE FROM Gender WHERE Id = ?;
DELETE FROM "USER" WHERE Id = ?;

--Stored Procedures / Functions (PostgreSQL)--

--User
--AddUser
CREATE OR REPLACE PROCEDURE addUser(
    p_id INT,
    p_firstName VARCHAR(255),
    p_lastName VARCHAR(255),
    p_email VARCHAR(255),
    p_phone VARCHAR(255),
    p_address VARCHAR(255),
    p_genderId INT
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO "USER"(Id, FirstName, LastName, Email, Phone, Address, GenderId) 
    VALUES (p_id, p_firstName, p_lastName, p_email, p_phone, p_address, p_genderId);
END;
$$;

--GetAllUsers
CREATE OR REPLACE FUNCTION getAllUsers()
RETURNS TABLE (
    Id int,
    FirstName varchar,
    LastName varchar,
    Email varchar,
    Phone varchar,
    Address varchar,
    GenderId int,
    Gender varchar
)
AS $$
BEGIN
    RETURN QUERY 
    SELECT U.*, G.Gender FROM "USER" U
    LEFT JOIN Gender G ON U.GenderId = G.Id;
END; $$
LANGUAGE plpgsql;

--Select user by id
CREATE OR REPLACE FUNCTION getUserById(p_id int)
RETURNS TABLE (
    Id int,
    FirstName varchar,
    LastName varchar,
    Email varchar,
    Phone varchar,
    Address varchar,
    GenderId int,
    Gender varchar
)
AS $$
BEGIN
    RETURN QUERY 
    SELECT U.*, G.Gender FROM "USER" U
    LEFT JOIN Gender G ON U.GenderId = G.Id
    WHERE U.Id = p_id;
END; $$
LANGUAGE plpgsql;

--AddUser
CREATE OR REPLACE PROCEDURE addUser(
    p_firstName VARCHAR(255),
    p_lastName VARCHAR(255),
    p_email VARCHAR(255),
    p_phone VARCHAR(255),
    p_address VARCHAR(255),
    p_genderId INT
)
LANGUAGE plpgsql
AS $$
BEGIN
    IF p_genderId IS NULL THEN
        INSERT INTO "USER"(FirstName, LastName, Email, Phone, Address) 
        VALUES (p_firstName, p_lastName, p_email, p_phone, p_address);
    ELSE
        INSERT INTO "USER"(FirstName, LastName, Email, Phone, Address, GenderId) 
        VALUES (p_firstName, p_lastName, p_email, p_phone, p_address, p_genderId);
    END IF;
END;
$$;


--Update User
CREATE OR REPLACE PROCEDURE updateUser(
    p_id INT,
    p_firstName VARCHAR(255),
    p_lastName VARCHAR(255),
    p_email VARCHAR(255),
    p_phone VARCHAR(255),
    p_address VARCHAR(255),
    p_genderId INT
)
LANGUAGE plpgsql
AS $$
BEGIN
    UPDATE "USER" 
    SET FirstName = p_firstName, 
        LastName = p_lastName, 
        Email = p_email, 
        Phone = p_phone, 
        Address = p_address, 
        GenderId = p_genderId
    WHERE Id = p_id;
END;
$$;

--Delete user
CREATE OR REPLACE PROCEDURE deleteUser(p_id INT)
LANGUAGE plpgsql
AS $$
BEGIN
    DELETE FROM "USER" WHERE Id = p_id;
END;
$$;

-- Gender
-- GetGenderById
CREATE OR REPLACE FUNCTION getGenderById(p_id INT)
RETURNS TABLE (
    Id INT,
    Gender VARCHAR(255)
)
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY SELECT * FROM Gender WHERE Id = p_id;
END;
$$;