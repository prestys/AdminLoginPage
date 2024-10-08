using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ParagonID.InternalSystem.Helpers;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddRazorPages();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddSingleton<JWTHelper>();
builder.Services.AddSingleton<NavlinksHelper>();
builder.Services.AddSingleton<AuthorisationHelper>();
builder.Services.AddSingleton<Radzen.ThemeService>();

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
