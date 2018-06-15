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


prendo l'elemento e il suo parent facendo la left join -> in pratica seleziono solo gli id da Tree
e cerco nella tabella chi ha è il parent del child che mi interessa

sqlite> select t1.Id, t2.Id from Tree as t1 left join Tree as t2 on t2.Parent = t1.Id where t2.Id = 109;