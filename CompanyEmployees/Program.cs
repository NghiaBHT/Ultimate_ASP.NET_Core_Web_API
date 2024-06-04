using CompanyEmployees.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.Xml;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();

var app = builder.Build();

if (app.Environment.IsDevelopment()) 
    app.UseDeveloperExceptionPage();
else 
    app.UseHsts(); //will add middleware for using HSTS, which adds the Strict - Transport - Security header.

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles();// enables using static files for the request. If we don’t set a path to the static files directory, it will use a wwwroot folder in our project by default.

app.UseForwardedHeaders(new ForwardedHeadersOptions //forward proxy headers to the current request. This will help us during application deployment
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.Use(async (context, next) =>
{
    Console.WriteLine($"Logic before executing the next delegate in the Use method");
    await next.Invoke();
    Console.WriteLine($"Logic after executing the next delegate in the Use method");
});

app.Run(async context =>
{
    Console.WriteLine($"Writing the response to the client in the Run method");
    await context.Response.WriteAsync("Hello from the middleware component.");
});

app.MapControllers();

app.Run();


