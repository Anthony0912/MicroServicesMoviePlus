ALTER TABLE Movies DROP CONSTRAINT PK_Movies;
ALTER TABLE Movies DROP COLUMN Id;
ALTER TABLE Movies ADD Id int IDENTITY(1, 1) NOT NULL;
ALTER TABLE Movies ADD CONSTRAINT PK_id_unique_autoincrement PRIMARY KEY CLUSTERED (Id);