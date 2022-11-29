## Service scope
This service owns/defines: 
- `employee-management-api`
- `employee-management` scheme in database

## Service dependencies
This service relies on:

## Useful commands
### Managing Database (EF Core 6)

```
dotnet ef migrations add <<migration-name>> --project .\src\ZPI.Persistance.ZPIDb\ZPI.Persistance.ZPIDb.csproj --context ZPIDbContext --startup-project .\src\ZPI\ZPI.csproj
```

```
npx @openapitools/openapi-generator-cli generate -i ./swagger.json -o ./client-typescript -g typescript-fetch
```
