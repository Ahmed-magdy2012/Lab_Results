
ASP.NET Core Web API
Entity Framework Core
SQL Server



Admin
Creates a new patient result session.
Generate Secure Access Link
Returns Session Id (Sid)
Get Patients (Pagination + Filtering)
Add Lab Results
Normal / Abnormal classification



1. git clone repo   git clone https://github.com/YOUR_USERNAME/LabResults.git

2. Configure Database in appsettings.Development.json "Default": "Server=.;Database=Lab;Trusted_Connection=True;"
3. 3. Run Migrations dotnet ef database update
dotnet run
