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

## ⚙️ Environment Variables

If you want to modify any environment variables from the `.env` file, you can.

## 🧪 Running Tests

To run tests, run the following command.

```bash
make test
```

## 📜 License

[GNU GPLv2](https://choosealicense.com/licenses/gpl-2.0/)