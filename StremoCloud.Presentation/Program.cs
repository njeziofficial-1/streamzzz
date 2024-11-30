using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;
using StremoCloud.Application.Extensions;
using StremoCloud.Infrastructure.Options;
using StremoCloud.Presentation.Middleware;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Add the XML comments to Swagger
    var xmlFile = Path.Combine(AppContext.BaseDirectory, "StremoCloud.xml");
    options.IncludeXmlComments(xmlFile);
});

var configuration = builder.Configuration;
builder.Services.AddApplicationLayer(configuration);

var cloudinaryConfig = configuration.GetSection("Cloudinary").Get<CloudinarySettings>();

var cloudinary = new Cloudinary(new Account(
    cloudinaryConfig.CloudName,
    cloudinaryConfig.ApiKey,
    cloudinaryConfig.ApiSecret));

builder.Services.AddSingleton(cloudinary);

//login to serilog
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseCors(x => x
              .AllowAnyMethod()
              .AllowAnyHeader()
              .SetIsOriginAllowed(origin => true)
              .AllowCredentials());

app.MapControllers();
app.DatabaseSeederExtension(configuration);
app.Run();
