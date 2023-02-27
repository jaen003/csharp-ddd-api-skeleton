using dotenv.net;
using Src.Core.Shared.Domain.Generators;
using Src.Core.Shared.Infrastructure.Events;
using Src.Core.Shared.Infrastructure.Generators;
using Src.Core.Shared.Infrastructure.Logging;
using ILogger = Src.Core.Shared.Domain.Logging.ILogger;

var builder = WebApplication.CreateBuilder(args);
DotEnv.Load();

// Add services to the container.

builder.Services.AddSingleton<ApplicationLoggerCreator, ApplicationLoggerCreator>();
builder.Services.AddScoped<ILogger>(
    serviceProvider => serviceProvider.GetRequiredService<ApplicationLoggerCreator>().Create()
);
builder.Services.CollectDomainEventInformation();
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
