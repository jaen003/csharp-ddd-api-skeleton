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

## ⚙️ Environment Variables

If you want to modify any environment variables from the `.env` file, you can.

## 🧪 Running Tests

To run tests, run the following command.

```bash
make test
```

## 📜 License

[GNU GPLv2](https://choosealicense.com/licenses/gpl-2.0/)