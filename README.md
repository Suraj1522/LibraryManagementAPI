Database Updates / Migrations
If you make changes to your models, create a new migration and update the database:

dotnet ef migrations add <MigrationName>
dotnet ef database update
