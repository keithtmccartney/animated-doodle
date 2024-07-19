# animated-doodle
[C#] Development task / Demonstration

# Notes
A .NET Core API project showing components (Models, DTOs, Controllers, Services, etc.)

# Unit Tests
* Repository tests for Course and Student repositories under MSTest project "animated-doodle.Tests"

# Postman Collection
* CRUD operations (POST/GET/PUT/DELETE HTTP verbs) collection under Class Library project "animated-doodle.Data" in 'Scripts'

# Migrations
* Add-Migration InitialCreate -Context SchoolContext -Project animated-doodle.Data -StartupProject animated-doodle.Api
* Update-Database
* Script-Migration
