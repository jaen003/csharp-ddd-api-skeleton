using Src.Core.Shared.Domain.Generators;
using Src.Core.Shared.Infrastructure.Generators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<
    SnowflakeIdentifierGeneratorCreator,
    SnowflakeIdentifierGeneratorCreator
>();
builder.Services.AddScoped<IIdentifierGenerator>(
    serviceProvider =>
        serviceProvider.GetRequiredService<SnowflakeIdentifierGeneratorCreator>().Create()
);
var app = builder.Build();

// Configure the HTTP request pipeline.

app.Run();
