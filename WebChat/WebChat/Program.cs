using Microsoft.EntityFrameworkCore;
using WebChat;
using WebChat.Hubs;
using WebChat.Services;
using AutoMapper;
using Hangfire;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSignalR();

builder.Services.AddSingleton<ChatService>();

//builder.Services.AddHangfire(config => config
//    .UseSimpleAssemblyNameTypeSerializer()
//    .UseRecommendedSerializerSettings()
//    .UseSQLiteStorage(builder.Configuration.GetConnectionString("ChatDBConnection"))
//);

var connectionString = builder.Configuration.GetConnectionString("ChatDBConnection");

builder.Services.AddDbContext<ApplicationContext>(options =>
options.UseSqlite(connectionString));

//builder.Services.AddAutoMapper(typeof(AppMappingProfile));

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatHub>("/chat");

app.Run();
