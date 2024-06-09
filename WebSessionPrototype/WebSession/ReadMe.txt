dotnet sql-cache create "Data Source=(localdb)/MSSQLLocalDB;Initial Catalog=DistCache;Integrated Security=True;" dbo TestCache


https://github.com/dotnet/aspnetcore/tree/main/src/Tools/dotnet-sql-cache


Usage: dotnet sql-cache script [arguments] [options]

Arguments:
  [schemaName]  Name of the table schema.
  [tableName]   Name of the table to be created.

Options:
  -o|--output      The file to write the result to.
  -i|--idempotent  Generates a script that can be used on a database that already has the table.
  -?|-h|--help     Show help information
  -v|--verbose     Show verbose output

dotnet sql-cache script dbo ILDBCache --output "session_cache.sql" -i

username distcacheuser theDistCacheUser
password distcacheuser Password1