# RoomManagement

# Command

```
dotnet ef dbcontext scaffold "Server=.\;Database=NTLAGENTDB;trusted_connection=true;encrypt=false;" Microsoft.EntityFrameworkCore.SqlServer --project src\Infrastructure --startup-project src\WebApi --output-dir src\Domain\Entities
```


```
dotnet ef dbcontext scaffold "Server=.\;Database=NTLAGENTDB;trusted_connection=true;encrypt=false;" Microsoft.EntityFrameworkCore.SqlServer -o ..\Domain\Entities\ --namespace Domain.Entities
```

from root directory

```
dotnet ef migrations add "initialDatabase" --project .\src\Infrastructure\ --startup-project .\src\WebApi\ --output-dir Persistence\Migrations
```

```
dotnet ef migrations add "AddCategory" --project .\src\Infrastructure\ --startup-project .\src\WebApi\ --output-dir Persistence\Migrations
```

```
dotnet ef migrations remove --project .\src\Infrastructure\ --startup-project .\src\WebApi\
```