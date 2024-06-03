API Url : https://localhost:7067/swagger/index.html

Database Creation:
1. Create Table as table name CMS
2. Run following comments in Package ManagerConsole
	Add-Migration initialDb
	Update-Database

Application: CMS
Install - Following Packages from NuGet =>
CMS.Core =>
	MediatR - Version="12.2.0,
    Microsoft.AspNetCore.Authentication.JwtBearer - V6.0.27,
    Microsoft.AspNetCore.Mvc.NewtonsoftJson - V6.0.31,
    Microsoft.EntityFrameworkCore.Design - V7.0.16,
    Newtonsoft.Json - V13.0.3,
    Swashbuckle.AspNetCore - V6.5.0,
	
CMS.Infrastructure => 
	Microsoft.EntityFrameworkCore,
	Microsoft.EntityFrameworkCore.Design,
	Microsoft.EntityFrameworkCore.SqlServer,
	Microsoft.EntityFrameworkCore.Tools

CMS.Appliction => 
	AutoMapper.Extensions.Microsoft.DependencyInjection - V12.0.1,
    MediatR - V12.2.0,
    Microsoft.Extensions.Configuration.Abstractions - V7.0.0,
    Microsoft.IdentityModel.Tokens - V6.35.0,
    System.IdentityModel.Tokens.Jwt - V6.35.0,

CMS.API => 
	Microsoft.EntityFrameworkCore - V7.0.16,
    Microsoft.EntityFrameworkCore.Design - V7.0.16,
    Microsoft.EntityFrameworkCore.SqlServer - V7.0.16,
    Microsoft.EntityFrameworkCore.Tools - V7.0.16">


	
