CREATE TABLE ideas(
	ideaId INT IDENTITY(1,1) PRIMARY KEY,
	ideaHeading VARCHAR(1024) NOT NULL,
	ideaBody TEXT NOT NULL,
	likes INT NOT NULL,
	dislikes INT NOT NULL,
	ideaStatus INT NOT NULL
);

-- SEED

INSERT INTO ideas(ideaHeading, ideaBody, likes, dislikes, ideaStatus)
VALUES ('First Idea', 'First Idea Explanation', 0, 0, 1);