using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Demo;
using Web.Demo.Data;
using Web.Demo.Models;
using Serilog;
using Serilog.Events;
using Web.Infru.DBContexts;
using System.Reflection;
using Microsoft.Extensions.Options;
using Web.Infru;
using Web.Infru.Enitites;
using Web.Infru.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Web.Infru.Securities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var assemblyName = Assembly.GetExecutingAssembly().FullName;

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => {
    containerBuilder.RegisterModule(new Webmodule());
    containerBuilder.RegisterModule(new Infru(connectionString,assemblyName));
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(connectionString, m => m.MigrationsAssembly(assemblyName)));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration));

try
{
    builder.Services
   .AddIdentity<ApplicationUser, ApplicationRole>()
   .AddEntityFrameworkStores<ApplicationDbContext>()
   .AddUserManager<ApplicationUserManager>()
   .AddRoleManager<ApplicationRoleManager>()
   .AddSignInManager<ApplicationSignInManager>()
   .AddDefaultTokenProviders();
    builder.Services.AddAuthentication()
   .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
   {
       options.LoginPath = new PathString("/Accout/Login");
       options.AccessDeniedPath = new PathString("/Accout/Login");
       options.LogoutPath = new PathString("/Accout/Logout");
       options.Cookie.Name = "WebDemoPortal.Identity";
       options.SlidingExpiration = true;
       options.ExpireTimeSpan = TimeSpan.FromHours(1);
   });

    builder.Services.Configure<IdentityOptions>(options =>
    {
        // Password settings.
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 0;

        // Lockout settings.
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        // User settings.
        options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = true;
    });
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("CourseManagementPolicy", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireRole("Teacher");
        });
        options.AddPolicy("CourseViewPolicy", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("ViewCourse", "true");
        });
        options.AddPolicy("CourseCreatePolicy", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("CreateCourse", "true");
        });
        options.AddPolicy("CourseViewRequirementPolicy", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.Requirements.Add(new CourseViewRequirement());
        });

    });
    builder.Services.AddSingleton<IAuthorizationHandler, CourseViewRequirementHandler>();
    builder.Services.AddControllersWithViews();
 

    var app = builder.Build();
    Log.Information("the app is starting");
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
    name: "areas",
          pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}