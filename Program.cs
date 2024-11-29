using Microsoft.EntityFrameworkCore;
using LoginApp.Data;
using LoginApp.Services;
using LoginApp.Interceptor;
using LoginApp.Repositories; // Add this using directive for repository
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, services, configuration) => configuration
    .WriteTo.Console()
    .WriteTo.File("logs/logfile.log", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(context.Configuration));

try
{
    // Add services to the container.
    builder.Services.AddControllersWithViews();

    // Configure Entity Framework to use SQL Server
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    // Register the repositories and services
    builder.Services.AddScoped<IIngredientRepository, IngredientRepository>(); // Register Ingredient repository
    builder.Services.AddScoped<IngredientService>(); // Register IngredientService
    builder.Services.AddScoped<INutrientRepository, NutrientRepository>(); // Register Nutrient repository
    builder.Services.AddScoped<NutrientService>(); // Register NutrientService

 builder.Services.AddScoped<IUserRepository, UserRepository>(); // Register User repository
    builder.Services.AddScoped<UserService>();
    builder.Services.AddScoped<MeasurementService>(); // Register MeasurementService

    // Register the ApiExceptionInterceptor
    builder.Services.AddScoped<ApiExceptionInterceptor>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
    throw;
}
finally
{
    Log.CloseAndFlush();
}