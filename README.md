# Luke_Kim_MitchellCodingChallenge
Coding challenge for Mitchell interview

README.txt

Mitchell claims manager web service

Author: Luke Kim
Languages: ASP, C#, JSON, PostgreSQL
Programs: Visual Studio 2015, PostgreSQL

-= SETTING UP POSTGRESQL =-
- To set up the database, install PostgreSQL, and initialize a new PostgreSQL 9.5 database.
- The password used in my code is "dc5ao522" without quotes, but if you want to use a different password,
make sure to edit the connection string in the "RestServiceImpl.svc.cs" file in the project.
- Database server should be local or "127.0.0.1", Port 5432, User ID "postgres", and database "postgres".
- Then, run the included "MitchellClaimDB.sql" query file in the PostgreSQL query executor to create
the necessary tables.

-= SETTING UP VISUAL STUDIO =-
- In Visual Studio 2015, open the "RestService.sln" file.
- Rebuild to compile.
- Run the program, and the web service and client should automatically be hosted by IIS Express.

-= USING THE APPLICATION =-
- To add a claim, enter in the fields using the textboxes in the grid footer and click the "Insert"
button to the right.
- To find a single claim by claim number, enter it into the textbox next to "Get claim number",
and click "Get claim number".
- To get all claims, click the "Get all claims" button.
- To get claims in a date range, select the dates in the corresponding "Open calendar" buttons at the
top of the page, and click the "Get claims in date range" button.
- To delete a claim, click on the "Delete" button on the right-most column of the grid.
- To delete all claims, click the "Delete all claims" button at the top of the page.

-= TO BE ADDED =-
- "Edit" button
- Reading vehicle information
