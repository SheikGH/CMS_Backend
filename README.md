✅ Steps to Setup and Run .NET Core Project (CMS)
🔽 Step 1: Download or Clone the Project from GitHub
git clone <https://github.com/SheikGH/CMS_Backend.git>
Example: git clone https://github.com/SheikGH/CMS_Backend.git
________________________________________
📁 Step 2: Open the Solution in Visual Studio
1.	Open Visual Studio 2022+
2.	Click "Open a project or solution"
3.	Navigate to the downloaded folder
4.	Open CMS.sln
________________________________________
🛠️ Step 3: Restore NuGet Packages
In Visual Studio:
•	Right-click the Solution → Restore NuGet Packages
•	Or use the terminal:
dotnet restore
________________________________________
⚙️ Step 4: Update appsettings.json (CMS.API/appsettings.json)
Example:
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=CMS;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Jwt": {
    "Key": "Your_Secret_Key_Here",
    "Issuer": "CMSApp",
    "Audience": "CMSAppUsers",
    "DurationInMinutes": 60
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
🔐 Replace YOUR_SERVER_NAME with your actual SQL Server name (e.g., localhost\\SQLEXPRESS)
Replace "Your_Secret_Key_Here" with a strong secret key
________________________________________
🗃️ Step 5: Create Database
1.	Open SQL Server Management Studio (SSMS)
2.	Run:
CREATE DATABASE CMS;
________________________________________
📦 Step 6: Run Migrations and Update Database
In Package Manager Console:
1.	Select CMS.Infrastructure as the default project
2.	Run:
Add-Migration initialDb
Update-Database
Make sure Startup Project is set to CMS.API
________________________________________
📦 Step 7: Install Required NuGet Packages
🔷 CMS.Core
Install:
Install-Package MediatR -Version 12.2.0
Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -Version 6.0.27
Install-Package Microsoft.AspNetCore.Mvc.NewtonsoftJson -Version 6.0.31
Install-Package Microsoft.EntityFrameworkCore.Design -Version 7.0.16
Install-Package Newtonsoft.Json -Version 13.0.3
Install-Package Swashbuckle.AspNetCore -Version 6.5.0
🔷 CMS.Infrastructure
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.Design
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
🔷 CMS.Application
Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection -Version 12.0.1
Install-Package MediatR -Version 12.2.0
Install-Package Microsoft.Extensions.Configuration.Abstractions -Version 7.0.0
Install-Package Microsoft.IdentityModel.Tokens -Version 6.35.0
Install-Package System.IdentityModel.Tokens.Jwt -Version 6.35.0
🔷 CMS.API
Install-Package Microsoft.EntityFrameworkCore -Version 7.0.16
Install-Package Microsoft.EntityFrameworkCore.Design -Version 7.0.16
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 7.0.16
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 7.0.16
________________________________________
▶️ Step 8: Run the Application
1.	Set CMS.API as the Startup Project
2.	Press F5 or click Run
3.	Swagger UI should open:
https://localhost:7067/swagger/index.html
________________________________________
🔁 Step 9: Test Endpoints via Swagger or Postman
•	✅ POST /api/Auth/login
•	✅ GET /api/Customers
•	✅ POST /api/Customers
•	✅ PUT /api/Customers/{id}
•	✅ DELETE /api/Customers/{id}
________________________________________
✅ Project Structure Recap (Clean Architecture)
CMS/
├── CMS.Core/           → Domain Models, Interfaces
├── CMS.Infrastructure/ → EF Core, SQL, Repositories
├── CMS.Application/    → Services, DTOs, Mapping, Use Cases
├── CMS.API/            → Controllers, Middleware, Auth, Swagger
________________________________________
