using Microsoft.EntityFrameworkCore;
using WebChat;
using WebChat.Hubs;
using WebChat.Services;
using AutoMapper;
using Hangfire;
using Hangfire.Storage.SQLite;
using HangfireBasicAuthenticationFilter;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ChatDBConnection");



//var chatSync = (ChatService)provider.GetService(typeof(ChatService));


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSignalR();

builder.Services.AddScoped<ChatService>();

builder.Services.AddHangfire(config => config
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSQLiteStorage(connectionString)
);


builder.Services.AddDbContext<ApplicationContext>(options =>
options.UseSqlite(connectionString));

    builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Services.AddHangfireServer();

var app = builder.Build();

using var serviceScope = app.Services.CreateScope();
var chatSync = serviceScope.ServiceProvider.GetRequiredService<ChatService>();

//var chatSync = app.Services..GetService<ChatService>();


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

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    DashboardTitle = "My WebChat",
    Authorization = new[]
        {
                new HangfireCustomBasicAuthenticationFilter{
                    User = builder.Configuration.GetSection("HangfireSettings:UserName").Value,
                    Pass = builder.Configuration.GetSection("HangfireSettings:Password").Value
                }
            }
});

RecurringJob.AddOrUpdate(() => chatSync.ClearChat(), Cron.Minutely);


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHangfireDashboard();
});

app.MapHub<ChatHub>("/chat");




app.Run();
