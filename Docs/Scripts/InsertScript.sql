SET IDENTITY_INSERT LocationTable ON;

INSERT INTO LocationTable (Id, Title, City, State, Country, Zip)
VALUES (10030, 'US Head Office', 'Baltimore', 'MD', 'United States', 21202);

SET IDENTITY_INSERT LocationTable OFF;



SET IDENTITY_INSERT DepartmentTable ON;

INSERT INTO DepartmentTable (Id, Title)
VALUES (2085, 'Software Development');;

SET IDENTITY_INSERT DepartmentTable OFF;
