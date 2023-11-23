<h1 align="center">
    DDD API Skeleton In C#
</h1>

## 📋 Project Description

This project provides a template that can be used as a code base for many back-end community projects incorporating Domain-Driven Design (DDD). It is a restaurant backoffice microservice that allows you to manage product information and extends its functionalities through communication with other microservices through a messaging broker.

It is built with clean architecture, SOLID principles, and love ❤️.

## 🎢 Features

* Logging to file and console with Serilog.
* Domain event information collection using reflection.
* Domain event JSON serialization and deserialization.
* Asynchronous consumption and publishing of domain events using RabbitMQ.
* Retry queue to reprocess failed event bus messages (delivery time can be modified from the `EVENT_BUS_MESSAGE_REDELIVERY_DELAY` environment variable in the `.env` file).
* Dead letter queue to publish failed domain event messages after exceeding the retry limit (message delivery limit can be modified from the `EVENT_BUS_MESSAGE_DELIVERY_LIMIT` environment variable in the `.env` file).
* Connection pool in Entity Framework (pool size can be modified. from the `DATABASE_CONNECTION_POOL_SIZE` environment variable in the `.env` file).
* Database migrations with Entity Framework.
* Key pagination and sorting by field receiving parameters through path.
* Restaurant creation from the `restaurant.created` domain event.
* Middleware to catch exceptions and log events.
* Product creation.
* Search for products by ID and restaurant.
* Product update (price, description, name)
* Product deletion.
* Automatic object mapping with Mapperly.
* Swagger integration for interactive API exploration and testing.
* Download OpenAPI specification in JSON format using Swagger.

<a name="setup"></a>
## 🔧 Setup

1. [Install Docker](https://www.docker.com/get-started).
2. If you are using Linux, [install Docker Compose](https://docs.docker.com/).
3. Create a Docker network:
```bash
docker network create -d bridge csharp_ddd_api_skeleton_network
```
4. Create a Docker container for the RabbitMQ event bus:
```bash
docker run -d -h rabbitmq --restart unless-stopped -v rabbitmq_data:/var/lib/rabbitmq/ -e "RABBITMQ_DEFAULT_USER=guest" -e "RABBITMQ_DEFAULT_PASS=guest" --network csharp_ddd_api_skeleton_network heidiks/rabbitmq-delayed-message-exchange:3.10.2-management
```
5. Create a Docker container for the PostgreSQL database:
```bash
docker run -d -h postgresql --restart unless-stopped -v postgresql_data:/var/lib/postgresql/data -e "POSTGRES_USER=root" -e "POSTGRES_PASSWORD=root" --network csharp_ddd_api_skeleton_network postgres:15.1-alpine
```
6. Clone this project:
```bash 
git clone https://github.com/jaen003/csharp-ddd-api-skeleton csharp-ddd-api-skeleton
```
7. Move to the project folder:
```bash
cd csharp-ddd-api-skeleton
```

## 🚀 Deployment

Once you have completed the [setup steps](#setup), run the following command to deploy the project:

```bash
docker-compose up -d
```

**NOTE:** This project runs inside a private network and does not expose a public port to access the API. It is recommended to access the API through an API gateway.

## 📚 API Documentation

Explore the API using Swagger UI. The API documentation is generated dynamically and can be accessed through the Swagger UI interface. Additionally, you can download the OpenAPI specification in JSON format.

### Swagger UI

Access the Swagger UI to interactively explore and test the API endpoints.

* **Swagger UI URL:** [http://localhost:5004/swagger](http://localhost:5004/swagger)

### OpenAPI Specification (JSON)

Download the OpenAPI specification in JSON format to use with other tools.

* **Swagger JSON URL:** [http://localhost:5004/swagger/v1/swagger.json](http://localhost:5004/swagger/v1/swagger.json)

**NOTE:** Access to documentation is limited to the development environment only. If you also want to extend it to the production environment, it is advisable to implement an identity server.

## 🗃️ Migrations

To create a new migration, run the following command:

```bash
dotnet ef migrations add <migration-name> -p Src/Core -o Shared/Infrastructure/Database/Migrations
```

For example, to create a migration that adds a new column to the `Products` table, you would run the following command:

```bash
dotnet ef migrations add AddProductNameColumn -p Src/Core -o Shared/Infrastructure/Database/Migrations
```

## ⚙️ Environment Variables

You can modify any environment variables in the `.env` file.

**NOTE:** If you are considering using this application in production you should include the '.env' file in your '.gitignore' so as not to expose the security of the application.

## 🧪 Running Tests

To run the tests, run the following command:

```bash
make test
```

## 📜 License

This project is licensed under the [GNU GPLv2](https://choosealicense.com/licenses/gpl-2.0/).