var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddDistributedMemoryCache();

builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString(
        "CacheDatabase");
    options.SchemaName = "dbo";
    options.TableName = "ILDBCache";
});

// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-8.0

builder.Services.AddSession(options =>
{    
    options.IdleTimeout = TimeSpan.FromSeconds(180);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Server farm
/*
* The session cookie is encrypted via IDataProtector. Data Protection must be properly configured to read session cookies 
* on each machine. For more information, see ASP.NET Core Data Protection Overview and Key storage providers.
*/
https://learn.microsoft.com/en-us/aspnet/core/performance/caching/distributed?view=aspnetcore-8.0

/*
 * When SQL Server is used as a distributed cache backing store, use of the same database for the cache and the app's 
 * ordinary data storage and retrieval can negatively impact the performance of both. We recommend using a dedicated SQL Server 
 * instance for the distributed cache backing store.
 */

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
