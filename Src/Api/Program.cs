using Microsoft.EntityFrameworkCore;
using Src.Api.Middlewares;
using Src.Core.Products.Application;
using Src.Core.Products.Application.Validators;
using Src.Core.Products.Infrastructure;
using Src.Core.Products.Infrastructure.Mappers;
using Src.Core.Restaurants.Application;
using Src.Core.Restaurants.Application.UseCases;
using Src.Core.Restaurants.Application.Validators;
using Src.Core.Restaurants.Infrastructure;
using Src.Core.Restaurants.Infrastructure.Mappers;
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
builder.Services.AddSingleton<RabbitMQEventBusConnection>();
builder.Services.AddTransient<RabbitMQMessagePublisher>();
builder.Services.AddTransient<RabbitMQConsumptionErrorHandler>();
builder.Services.CollectDomainEventInformation();
builder.Services.AddTransient<RabbitMQEventBusConfigurer>();
builder.Services.AddSingleton<RabbitMQDomainEventConsumer>();
PostgresDatabaseConnectionData databaseConnectionData = new();
builder.Services.AddPooledDbContextFactory<PostgresDatabaseContext>(
    options => options.UseNpgsql(databaseConnectionData.ConnectionString),
    databaseConnectionData.PoolSize
);
builder.Services.AddTransient<PostgresDatabaseMigrator>();
builder.Services.AddScoped<IDomainEventPublisher, RabbitMQDomainEventPublisher>();
builder.Services.AddTransient<IRestaurantRepository, PostgresRestaurantRepository>();
builder.Services.AddTransient<RestaurantCreator>();
builder.Services.AddScoped<IRestaurantExistenceValidator, RestaurantExistenceValidator>();
builder.Services.AddScoped<IProductRepository, PostgresProductRepository>();
builder.Services.AddScoped<IProductNameAvailabilityValidator, ProductNameAvailabilityValidator>();
var app = builder.Build();

// Add middlewares

app.UseMiddleware<CustomExceptionMiddleware>();

// Init services

PostgresDatabaseMigrator databaseMigrator =
    app.Services.GetRequiredService<PostgresDatabaseMigrator>();
await databaseMigrator.Migrate();
RabbitMQEventBusConfigurer eventBusConfigurer =
    app.Services.GetRequiredService<RabbitMQEventBusConfigurer>();
await eventBusConfigurer.Configure();
RabbitMQDomainEventConsumer eventBusConsumer =
    app.Services.GetRequiredService<RabbitMQDomainEventConsumer>();
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
