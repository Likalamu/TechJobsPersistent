--Part 1
--int Id
--string name
--int Employer_Id

SELECT column_name, data_type
FROM information_schema.columns
WHERE table_name='jobs';

--Part 2
SELECT name
FROM techjobs.employers
WHERE location='St. Louis City';

--Part 3
SELECT Skills.name, description
FROM skills
INNER JOIN jobs ON skills.id = jobs.id
WHERE jobs.Id is not null
ORDER BY skills.Name
