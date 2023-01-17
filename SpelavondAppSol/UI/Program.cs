using Domain.Services;
using Infrastructure.Data;
using Infrastructure.Data.Security;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services
    .AddScoped<IUserRepository, UserRepo>()
    .AddScoped<IGameRepository, GameRepo>()
    .AddScoped<IGamenightRepository, GamenightRepo>()
    .AddScoped<Seeder>()

    // EF DB stuff -->
    .AddDbContext<DatabaseContext>(opts =>
    {
        opts
            .UseSqlServer(builder.Configuration["ConnectionStrings:MvcContext"])
            .EnableSensitiveDataLogging(true);
    })
    .AddDbContext<SecurityDBContext>(opts =>
    {
        opts
            .UseSqlServer(builder.Configuration["ConnectionStrings:SecurityContext"])
            .EnableSensitiveDataLogging(true);
    })
    .AddIdentity<IdentityUser, IdentityRole>(config =>
    {
        config.Password.RequiredLength = 2;
        config.Password.RequireDigit = false;
        config.Password.RequireNonAlphanumeric = false;
        config.Password.RequireUppercase = false;

    })
    .AddEntityFrameworkStores<SecurityDBContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(configure =>
{
    configure.LoginPath = "/Home/Login";
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();


builder.Services.AddAuthorization(config =>
{
    config.AddPolicy("EmailReq", policy =>
    {
        policy.RequireClaim("EmailCheck", "email");
    });
    
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(30);
    options.Cookie.IsEssential = false;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
else
{
    // Only in dev env seed with dummy data -->
    await SeedDatabase();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy();
app.UseSession();

app.UseRouting();

// who are you
app.UseAuthentication();

// are you allowed
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});




app.MapDefaultControllerRoute();

app.Run();

// Functionality to seed the db with dummy data -->
async Task SeedDatabase()
{
    using var scope = app.Services.CreateScope();
    var dbSeeder = scope.ServiceProvider.GetRequiredService<Seeder>();
    await dbSeeder.EnsurePopulated(true);

}
