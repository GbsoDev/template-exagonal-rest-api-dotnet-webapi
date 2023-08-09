# exagonal-rest-api-template
plantilla para desarrollar api rest con arquitectura exagonal

## Comandos de desarrollo
### Crear migraciones
```
dotnet ef migrations add [migration-name] --startup-project GbsoDevExagonalTemplate.Data.EfCore.MSSQL --project GbsoDevExagonalTemplate.Data.EfCore.MSSQL
```

### Actualizar base de datos
```
dotnet ef database update [new-migration-name] --startup-project GbsoDevExagonalTemplate.Data.EfCore.MSSQL --project GbsoDevExagonalTemplate.Data.EfCore.MSSQL
```
