/* Ian: Run these commands in PostgreSQL using psql. 
        This sets up the simple version of the milestone1db database and business table that matches
        your WPF app setup.

        I will be pushing the full .sql version later today. 
*/

CREATE DATABASE milestone1db;

\c milestone1db

CREATE TABLE business (
	name VARCHAR(100),
	state CHAR(2),
	city VARCHAR(50),
	PRIMARY KEY (name,state,city)
)

--Run the following command in psql to import the CSV file
--Just replace the <path>/milestone1DB.csv portion to include the actual path in you machine :)
copy business (name, state, city) FROM '<path>/milestone1DB.csv' DELIMITER ',' CSV;
