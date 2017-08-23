CREATE TABLE ideas(
	ideaId INT IDENTITY(1,1),
	ideaHeading VARCHAR(1024) NOT NULL,
	ideaBody TEXT NOT NULL,
	likes INT NOT NULL,
	dislikes INT NOT NULL,
	ideaStatus INT NOT NULL,
	rowDeleted BIT NOT NULL DEFAULT 'FALSE',
	dateAdded DATETIME DEFAULT CURRENT_TIMESTAMP,
	addedBy VARCHAR(100) NOT NULL,
	CONSTRAINT [PK_Ideas_IdeasId] PRIMARY KEY CLUSTERED ([ideaId] ASC)
)
GO

CREATE FULLTEXT CATALOG IdeasFTS 
WITH ACCENT_SENSITIVITY = OFF
GO

CREATE FULLTEXT INDEX ON ideas  
(ideaHeading, ideaBody LANGUAGE 1033)  
KEY INDEX PK_Ideas_IdeasId  
ON IdeasFTS
WITH STOPLIST = SYSTEM
GO

CREATE PROCEDURE listIdeas   
    @pageNumber int  
AS
    SELECT *
    FROM ideas
	ORDER BY dateAdded ASC
	OFFSET (@pageNumber * 10) ROWS
    FETCH NEXT 10 ROWS ONLY
GO

CREATE PROCEDURE searchIdeas
	@search varchar(1024),
	@pageNUmber int
AS
	SELECT *
	FROM ideas
	WHERE FREETEXT (ideaHeading, @search) OR FREETEXT (ideaBody, @search)
	ORDER BY dateAdded ASC
	OFFSET (@pageNumber * 10) ROWS
	FETCH NEXT 10 ROWS ONLY
GO

-- SEED

INSERT INTO ideas(ideaHeading, ideaBody, likes, dislikes, ideaStatus, addedBy)
VALUES ('First Idea', 'First Idea Explanation', 0, 0, 1, 'sabeer'),
		('Second Idea', 'Second Idea Explanation', 0, 0, 1, 'sabeer'),
		('Third idea', 'Third Idea Explanation', 0, 0, 1, 'athul')
GO