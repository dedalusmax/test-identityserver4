# test-identityserver4

## Four main projects:
- Test.IdentityServer4.AspNetIdentity
- Test.MvcClient
- Test.WebApi
- Test.JavaScriptClient

## Added projects (building history)

With this one, added is another client (Swagger) to the main project Test.IdentityServer4.AspNetIdentity's Config file.
It is created for easier handling of later projects, and as a learning project.
- Test.WebApi.Swagger 

Three next projects come in batch. Their main purpose is to prove that is possible for IS4 to use our database. 
As a prerequisite, it was needed to create seeded database so we have actual users to test on. SENG.System database is not compatible since here we are using ASP.net Identity, and is created seeded database using Identity users and roles.

- Test.WebApi.Swagger.DataLess
- Test.IdentityServer4.Data
- Test.IdentityServer4.AspNetIdentity.DataLess

These are still in development
