# test-identityserver4

## Four main projects:
- Test.IdentityServer4.AspNetIdentity
- Test.MvcClient
- Test.WebApi
- Test.JavaScriptClient

## Added projects (building history)

### Initial Swagger
With this one, added is another client (Swagger) to the main project Test.IdentityServer4.AspNetIdentity's Config file.
It is created for easier handling of later projects, and as a learning project.

- Test.WebApi.Swagger 

### ASP.net Identity database
Three next projects come in batch. Their main purpose is to prove that is possible for IS4 to use our database. 
As a prerequisite, it was needed to create seeded database so we have actual users to test on. SENG.System database is not compatible since here we are using ASP.net Identity, and is created seeded database using Identity users and roles.
These are still in development, and are built on a preceding Swagger IS4 project.

- Test.WebApi.Swagger.RealData
- Test.IdentityServer4.Data
- Test.IdentityServer4.AspNetIdentity.RealLess

### Custom Identity Server 4 MVC login
With this project is proved that is possible to create customized MVC login page for Identity Server login.
This IS4 is not built upon a main IS4 model (not the Swagger one).

- Test.IdentityServer4.Login
