using Microsoft.EntityFrameworkCore;
using Src.Api.Middlewares;
using Src.Core.Products.Application;
using Src.Core.Products.Application.Services;
using Src.Core.Products.Infrastructure.Mappers;
using Src.Core.Products.Infrastructure.Repositories;
using Src.Core.Restaurants.Application;
using Src.Core.Restaurants.Application.Services;
using Src.Core.Restaurants.Infrastructure.Mappers;
using Src.Core.Restaurants.Infrastructure.Repositories;
using Src.Core.Shared.Application.EventBus;
using Src.Core.Shared.Application.Exceptions;
using Src.Core.Shared.Infrastructure.Database;
using Src.Core.Shared.Infrastructure.EventBus;
using Src.Core.Shared.Infrastructure.Events;
using Src.Core.Shared.Infrastructure.Logging;
using LoggerFactory = Src.Core.Shared.Infrastructure.Logging.LoggerFactory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ProductMapper>();
builder.Services.AddTransient<RestaurantMapper>();
builder.Services.AddSingleton<LoggerFactory>(
    builder.Environment.IsDevelopment() ? new ConsoleLoggerFactory() : new FileLoggerFactory()
);
builder.Services.AddTransient(serviceProvider =>
    serviceProvider.GetRequiredService<LoggerFactory>().Create()
);
builder.Services.AddTransient<CustomExceptionHandler>();
builder.Services.AddSingleton<RabbitmqEventBusConnection>();
builder.Services.AddTransient<RabbitmqMessagePublisher>();
builder.Services.AddTransient<RabbitmqConsumptionErrorHandler>();
builder.Services.CollectDomainEventInformation();
builder.Services.AddTransient<RabbitmqEventBusConfigurer>();
builder.Services.AddSingleton<RabbitmqDomainEventConsumer>();
PostgresqlDatabaseConnectionData databaseConnectionData = new();
builder.Services.AddPooledDbContextFactory<PostgresqlDatabaseContext>(
    options => options.UseNpgsql(databaseConnectionData.ConnectionString),
    databaseConnectionData.PoolSize
);
builder.Services.AddTransient<PostgresqlDatabaseMigrator>();
builder.Services.AddScoped<IDomainEventPublisher, RabbitmqDomainEventPublisher>();
builder.Services.AddTransient<IRestaurantRepository, PostgresqlRestaurantRepository>();
builder.Services.AddTransient<RestaurantCreator>();
builder.Services.AddScoped<RestaurantExistenceValidator>();
builder.Services.AddScoped<IProductRepository, PostgresqlProductRepository>();
builder.Services.AddScoped<ProductNameAvailabilityValidator>();
var app = builder.Build();

// Add middlewares

app.UseMiddleware<CustomExceptionMiddleware>();

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
app.MapControllers();
app.Run();
