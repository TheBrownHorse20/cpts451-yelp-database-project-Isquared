/*
    Team: Isquared
    File: Isqured_relations_v1.sql
    Description: DDL statements from relational schema. 
*/

CREATE TABLE Business(
    
    business_id VARCHAR(50) PRIMARY KEY UNIQUE,
    name TEXT,
    address TEXT,
    postal_code TEXT,
    stars FLOAT,
    review_count INTEGER,
    is_open INTEGER
);

CREATE TABLE Users(

    user_id VARCHAR(50) PRIMARY KEY UNIQUE,
    name TEXT,
    review_count INTEGER,
    average_stars FLOAT,
    yelping_since DATE
);

CREATE TABLE Review(

    review_id VARCHAR(50) PRIMARY KEY UNIQUE,
    user_id VARCHAR(50) NOT NULL,
    business_id VARCHAR(50) NOT NULL,
    stars INTEGER,
    text TEXT,
    date DATE,
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (business_id) REFERENCES Business(business_id)
);

CREATE TABLE CheckIn(

    business_id VARCHAR(50),
    day VARCHAR(20),
    hour VARCHAR(10),
    checkin_count INTEGER,
    PRIMARY KEY (business_id, day, hour),
    FOREIGN KEY (business_id) REFERENCES Business(business_id)
);

CREATE TABLE Category(

    category_name TEXT PRIMARY KEY
);

CREATE TABLE BelongsTo(

    business_id VARCHAR(50),
    category_name TEXT,
    PRIMARY KEY (business_id, category_name),
    FOREIGN KEY (business_id) REFERENCES Business(business_id),
    FOREIGN KEY (category_name) REFERENCES Category(category_name)
);