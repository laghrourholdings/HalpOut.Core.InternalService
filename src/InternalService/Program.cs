using CommonLibrary.AspNetCore;
using CommonLibrary.Core;
using InternalService.EFCore;
using InternalService.Implementations;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var logger = new LoggerConfiguration().WriteTo.Console();
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
