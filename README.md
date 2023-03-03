<h1 align="center">
    DDD API Skeleton In C#
</h1>

## 📋 Project Description

The main idea of ​​this project is to share a template that can be used as a code base for many back-end community projects incorporating DDD. It is a restaurant backoffice microservice that allows you to manage product information and extends its functionalities through communication with other microservices through a messaging broker.

It is built with clean architecture, SOLID principles and above all with ❤️.

## 🎢 Features

- Identifier generator based on Twitter's snowflake algorithm.
- Log events by file and console with Serilog logger.
- Domain event information collector using reflection.
- Domain event Json serializer and deserializer.
- Asynchronous consumer and publisher of domain events using RabbitMQ.
- Retry queue to reprocess failed event bus messages, you can modify the delivery time from `EVENT_BUS_MESSAGE_REDELIVERY_DELAY` environment variable in the `.env` file.
- Dead letter queue to publish failed domain event messages after exceeding the retry limit, you can modify the message delivery limit from `EVENT_BUS_MESSAGE_DELIVERY_LIMIT` environment variable in the `.env` file.
- Connection pool in Entity Framework, you can modify the pool size from `DATABASE_CONNECTION_POOL_SIZE` environment variable in the `.env` file.
- Database migrations with Entity Framework.
- Key pagination and sorting by field receiving parameters through path.
- Restaurant creation from the `restaurant.created` domain event.
- Middleware to catch exceptions and log events.

<a name="setup"></a>
## 🔧 Setup

1. [Install Docker](https://www.docker.com/get-started).
2. If you are using Linux [Install Docker Compose](https://docs.docker.com/).
3. Create a docker network. 
```bash
docker network create -d bridge csharp_ddd_api_skeleton_network
```
4. Create a docker container for the RabbitMQ event bus.
```bash
docker run -d -h rabbitmq --restart unless-stopped -v rabbitmq_data:/var/lib/rabbitmq/:delegated -e "RABBITMQ_DEFAULT_USER=guest" -e "RABBITMQ_DEFAULT_PASS=guest" --network csharp_ddd_api_skeleton_network heidiks/rabbitmq-delayed-message-exchange:3.10.2-management
```
5. Create a docker container for the PostgreSQL database.
```bash
docker run -d -h postgresql --restart unless-stopped -v postgresql_data:/var/lib/postgresql/data:delegated -e "POSTGRES_USER=root" -e "POSTGRES_PASSWORD=root" --network csharp_ddd_api_skeleton_network postgres:15.1-alpine
```
6. Clone this project.
```bash 
git clone https://github.com/jaen003/csharp-ddd-api-skeleton csharp-ddd-api-skeleton
```
7. Move to the project folder. 
```bash
cd csharp-ddd-api-skeleton
```

## 🚀 Deployment

Make sure you have completed the [setup](#setup) section and then run the following command.

```bash
docker-compose up -d
```

**NOTE:** This project runs inside a private network and does not expose a public port to access the API, it is recommended to do so through an API gateway.

## ⚙️ Environment Variables

If you want to modify any environment variables from the `.env` file, you can.

## 🧪 Running Tests

To run tests, run the following command.

```bash
make test
```

## 📜 License

[GNU GPLv2](https://choosealicense.com/licenses/gpl-2.0/)