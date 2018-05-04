The code is based from the following blog post:

https://mcguirev10.com/2018/01/02/identityserver4-without-entity-framework.html

STEPS: 

* new ASP.NET Core web app (empty), localhost:5000
* install: IdentityServer4
* powershell script in the root dir:

iex ((New-Object System.Net.WebClient).DownloadString('https://raw.githubusercontent.com/IdentityServer/IdentityServer4.Quickstart.UI/release/get.ps1'))

* new database (McGuire), execute init.sql script
* appsettings.json with AppDB connection string

* Config.cs
* Startup.cs
* persistence folder with files
