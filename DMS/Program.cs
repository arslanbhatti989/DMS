using DMS.Data;
using DMS.Models;
using DMS.Repositories;
using DMS.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using OrmaProject.Data;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<Users>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddScoped<IProjectRepository, ProjectService>();
builder.Services.AddScoped<IUnitRepository, UnitService>();
builder.Services.AddScoped<IUnitTypeRepository, UnitTypeService>();
builder.Services.AddScoped<IPaymentPlansRepository, PaymentPlansService>();
builder.Services.AddScoped<IInstallmentsRepository, InstallmentsServices>();
builder.Services.AddScoped<IPersonRepository, PersonService>();
builder.Services.AddScoped<ICountryRepository, CountryService>();
builder.Services.AddScoped<ICityRepository, CityService>();
builder.Services.AddScoped<IRolePermissions, RolePermissionsService>();
builder.Services.AddScoped<IUsers, UsersService>();

//Email Service
builder.Services.AddTransient<IMailSender, MailSender>();


// ✅ Set U.S. culture globally
var usCulture = new CultureInfo("en-US");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(usCulture);
    options.SupportedCultures = new List<CultureInfo> { usCulture };
    options.SupportedUICultures = new List<CultureInfo> { usCulture };
});
var app = builder.Build();
// ✅ Apply U.S. Culture globally
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(usCulture),
    SupportedCultures = new List<CultureInfo> { usCulture },
    SupportedUICultures = new List<CultureInfo> { usCulture }
};
app.UseRequestLocalization(localizationOptions);
using (var scope = app.Services.CreateScope())
{
    await SeedData.EnsureSeedData(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.MapRazorPages();
// ✅ Force redirect to login if user is not authenticated
//app.Use(async (context, next) =>
//{
//    var path = context.Request.Path;

//    var isStatic = path.StartsWithSegments("/assets") ||
//                   path.StartsWithSegments("/css") ||
//                   path.StartsWithSegments("/js") ||
//                   path.StartsWithSegments("/lib") ||
//                   path.StartsWithSegments("/images");

//    if (!context.User.Identity.IsAuthenticated &&
//        !path.StartsWithSegments("/Identity/Account/Login") &&
//        !path.StartsWithSegments("/Identity/Account/Register") &&
//        !path.StartsWithSegments("/Identity/Account/ForgotPassword") &&
//        !path.StartsWithSegments("/Identity/Account/ResetPassword") &&
//        !path.StartsWithSegments("/Identity/Account/AfterForgotPassword") &&
//        !isStatic)
//    {
//        context.Response.Redirect("/Identity/Account/Login");
//        return;
//    }

//    await next();
//});



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
