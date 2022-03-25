using Maksgogo;
using Maksgogo.Interfaces;
using Maksgogo.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddMvc();


builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();
builder.Services.AddDbContext<MaksgogoContext>(option => option.UseSqlServer(
       builder.Configuration.GetConnectionString("DefaultConnection")
));

builder.Services.AddTransient<IFilms, FilmRepository>();
builder.Services.AddTransient<IGenres, GenreRepository>();
builder.Services.AddTransient<IStudios, StudioRepository>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sp => OrderCart.GetCart(sp));

builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStatusCodePages();   
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseMvcWithDefaultRoute();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");

app.Run();
