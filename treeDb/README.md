# Example

```bash
dotnet ef dbcontext scaffold DataSource=app.db Microsoft.EntityFrameworkCore.Sqlite -o models
```
$env:Path += ";D:\tools\sqlite"

sqlite>

.open app.db
.tables

PRAGMA table_info('Closure');
Column number | Column name | Data type | Can be null | Default value | Primary key
0|Parent|INTEGER|0||1