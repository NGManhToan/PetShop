using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PetShop.Action;
using PetShop.Action.Interface;
using PetShop.Database;
using PetShop.Query;
using PetShop.Query.Interface;
using PetShop.Service;
using PetShop.Service.Interface;
using PetShop.UtilsService;
using PetShop.UtilsService.Interface;
using WebApiTutorialHE.Service.Interface;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddSingleton<ISharingDapper, SharingDapper>();
builder.Services.AddScoped<ILoginQuery, LoginQuery>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRegisterAction, RegisterAction>();
builder.Services.AddScoped<IRegisterAccountService, RegisterAccountService>();
builder.Services.AddScoped<IProductQuery, ProductQuery>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<IPetQuery, PetQuery>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartQuery, CartQuery>();
builder.Services.AddScoped<ICartAction, CartAction>();
// Add services to the container.


builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();


builder.Services.AddDbContext<SharingContext>(options => options.UseMySql(configuration.GetConnectionString("SharingConnection"),
                ServerVersion.Parse("8.0.33-mysql")), ServiceLifetime.Scoped, ServiceLifetime.Scoped);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            // configure cookie options
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // adjust as needed
            options.LoginPath = "/Login/Login"; // adjust login path
            options.AccessDeniedPath = "/Home/AccessDenied"; // adjust access denied path
        });

builder.Services.AddCors();
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
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                    .AllowCredentials()); // allow credentials
app.Run();
