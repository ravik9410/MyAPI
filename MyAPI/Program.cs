using Microsoft.EntityFrameworkCore;
using MyAPI.Db;
using System;
using static Microsoft.AspNetCore.Http.StatusCodes;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var dbHost = Environment.GetEnvironmentVariable("demoappdb");
var dbName = Environment.GetEnvironmentVariable("UserManagement_ServicesDb");
var dbPassword = Environment.GetEnvironmentVariable("password@12345#");
var connectionString = $"Data Source={dbHost};Initial Catalog={dbName}; Persist Security Info=True; User ID=sa; Password={dbPassword};";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(connectionString));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*//builder.Services.AddHsts(options =>
//{
//    options.Preload = true;
//    options.IncludeSubDomains = true;
//    options.MaxAge = TimeSpan.FromDays(60);
//    options.ExcludedHosts.Add("example.com");
//    options.ExcludedHosts.Add("www.example.com");
//});
//builder.Services.AddHttpsRedirection(options =>
//{
//    options.RedirectStatusCode = Status307TemporaryRedirect;
//    options.HttpsPort = 7016;
//});
//if (!builder.Environment.IsDevelopment())
//{
//    builder.Services.AddHttpsRedirection(options =>
//    {
//        options.RedirectStatusCode = Status308PermanentRedirect;
//        options.HttpsPort = 443;
//    });
//}*/

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
ApplyMigration();
app.Run();
void ApplyMigration()
{

    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}