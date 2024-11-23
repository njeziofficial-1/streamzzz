using Serilog;
using StremoCloud.Application.Extensions;
using StremoCloud.Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationLayer();


//login to serilog
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

//Cors
//builder.Services.AddCors(options => {
//    options.AddPolicy("EnabledCQRS",
//        options => { options
//            .AllowAnyHeader()
//            .AllowAnyMethod();
//        });
//});

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

//app.UseCors("EnabledCORS");
app.UseCors(x => x
              .AllowAnyMethod()
              .AllowAnyHeader()
              .SetIsOriginAllowed(origin => true)
              .AllowCredentials());

app.MapControllers();

app.Run();
