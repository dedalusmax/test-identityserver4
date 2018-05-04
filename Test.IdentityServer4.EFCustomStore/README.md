The code is based from the following blog posts:

https://mcguirev10.com/2018/01/02/identityserver4-without-entity-framework.html
http://docs.identityserver.io/en/release/quickstarts/8_entity_framework.html
http://hamidmosalla.com/2017/12/07/policy-based-authorization-using-asp-net-core-2-identityserver4/

NOTES:

The IdentityServer4.EntityFramework package contains entity classes that map from IdentityServer’s models. 
As IdentityServer’s models change, so will the entity classes in IdentityServer4.EntityFramework. 
As you use IdentityServer4.EntityFramework and upgrade over time, 
you are responsible for your own database schema and changes necessary to that schema as the entity classes change. 

STEPS: 

* new ASP.NET Core web app (empty), localhost:5000
* install: IdentityServer4, IdentityServer4.EntityFramework
* powershell script in the root dir:

iex ((New-Object System.Net.WebClient).DownloadString('https://raw.githubusercontent.com/IdentityServer/IdentityServer4.Quickstart.UI/release/get.ps1'))

* new database
* appsettings.json with AppDB connection string

* Config.cs
* Startup.cs
* persistence folder with ProfileService.cs

