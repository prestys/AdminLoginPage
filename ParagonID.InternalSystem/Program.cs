using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using ParagonID.Data.Context;
using ParagonID.Data.Repository;
using ParagonID.Data.Service;
using ParagonID.InternalSystem.Helpers;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddRazorPages();

// [Singleton]
builder.Services.AddSingleton<JWTHelper>();
builder.Services.AddSingleton<NavlinksHelper>();
builder.Services.AddSingleton<AuthorisationHelper>();
builder.Services.AddSingleton<Radzen.ThemeService>();

// [Scoped]
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<DataRetriever>();

builder.Services.AddRadzenComponents();

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(Environment.GetEnvironmentVariable("InternalConnectionString")!));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
