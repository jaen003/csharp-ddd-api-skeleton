using dotenv.net;
using Microsoft.EntityFrameworkCore;
using Src.Api.Middlewares;
using Src.Core.Products.Domain;
using Src.Core.Products.Infrastructure;
using Src.Core.Restaurants.Application.Services;
using Src.Core.Restaurants.Domain;
using Src.Core.Restaurants.Infrastructure;
using Src.Core.Shared.Domain.EventBus;
using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.Generators;
using Src.Core.Shared.Infrastructure.Database;
using Src.Core.Shared.Infrastructure.EventBus;
using Src.Core.Shared.Infrastructure.Events;
using Src.Core.Shared.Infrastructure.Generators;
using Src.Core.Shared.Infrastructure.Logging;
using Src.Core.Shared.Infrastructure.Mappers;
using ILogger = Src.Core.Shared.Domain.Logging.ILogger;

var builder = WebApplication.CreateBuilder(args);
DotEnv.Load();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<ProductMapper, ProductMapper>();
builder.Services.AddTransient<RestaurantMapper, RestaurantMapper>();
builder.Services.AddSingleton<ApplicationLoggerCreator, ApplicationLoggerCreator>();
builder.Services.AddScoped<ILogger>(
    serviceProvider => serviceProvider.GetRequiredService<ApplicationLoggerCreator>().Create()
);
builder.Services.AddTransient<DomainExceptionHandler, DomainExceptionHandler>();
builder.Services.AddSingleton<RabbitmqEventBusConnection, RabbitmqEventBusConnection>();
builder.Services.AddTransient<RabbitmqMessagePublisher, RabbitmqMessagePublisher>();
builder.Services.AddTransient<RabbitmqConsumptionErrorHandler, RabbitmqConsumptionErrorHandler>();
builder.Services.CollectDomainEventInformation();
builder.Services.AddTransient<RabbitmqEventBusConfigurer, RabbitmqEventBusConfigurer>();
builder.Services.AddSingleton<RabbitmqDomainEventConsumer, RabbitmqDomainEventConsumer>();
PostgresqlDatabaseConnectionData databaseConnectionData = new();
builder.Services.AddPooledDbContextFactory<PostgresqlDatabaseContext>(
    options => options.UseNpgsql(databaseConnectionData.ConnectionString),
    databaseConnectionData.PoolSize
);
builder.Services.AddSingleton<
    SnowflakeIdentifierGeneratorCreator,
    SnowflakeIdentifierGeneratorCreator
>();
builder.Services.AddScoped<IIdentifierGenerator>(
    serviceProvider =>
        serviceProvider.GetRequiredService<SnowflakeIdentifierGeneratorCreator>().Create()
);
builder.Services.AddScoped<PostgresqlDatabaseMigrator, PostgresqlDatabaseMigrator>();
builder.Services.AddScoped<IDomainEventPublisher, RabbitmqDomainEventPublisher>();
builder.Services.AddScoped<IRestaurantRepository, PostgresqlRestaurantRepository>();
builder.Services.AddScoped<RestaurantCreator, RestaurantCreator>();
builder.Services.AddScoped<IProductRepository, PostgresqlProductRepository>();
var app = builder.Build();

// Add middlewares

app.UseMiddleware<ExceptionMiddleware>();

// Init services

PostgresqlDatabaseMigrator databaseMigrator =
    app.Services.GetRequiredService<PostgresqlDatabaseMigrator>();
await databaseMigrator.Migrate();
RabbitmqEventBusConfigurer eventBusConfigurer =
    app.Services.GetRequiredService<RabbitmqEventBusConfigurer>();
await eventBusConfigurer.Configure();
RabbitmqDomainEventConsumer eventBusConsumer =
    app.Services.GetRequiredService<RabbitmqDomainEventConsumer>();
eventBusConsumer.Consume();

// Configure the HTTP request pipeline.

app.MapControllers();
app.Run();
