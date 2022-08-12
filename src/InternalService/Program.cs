using CommonLibrary.AspNetCore;
using CommonLibrary.Core;
using CommonLibrary.Repositories;
using InternalService.Implementations;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ILogger = Serilog.ILogger;

var builder = WebApplication.CreateBuilder(args);

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

ILogger logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
builder.Services.AddCommonLibrary(builder.Configuration, builder.Logging, logger , MyAllowSpecificOrigins);
builder.Services.AddScoped<IObjectRepository<IObject>, ObjectRepository>();
builder.Services.AddDbContext<ServiceDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCommonLibrary(MyAllowSpecificOrigins);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Run();
