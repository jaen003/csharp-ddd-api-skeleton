using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace Src.Core.Shared.Infrastructure.EventBus;

public class RabbitmqEventBusConnection
{
    private readonly int reconnectionTime;
    private readonly int reconnectLimit;
    private IConnection? connection;
    private readonly ConnectionFactory connectionFactory;

    public RabbitmqEventBusConnection()
    {
        reconnectionTime = int.Parse(
            Environment.GetEnvironmentVariable("EVENT_BUS_RECONNECTION_TIME")!
        );
        reconnectLimit = int.Parse(
            Environment.GetEnvironmentVariable("EVENT_BUS_RECONNECT_LIMIT")!
        );
        connectionFactory = CreateConnectionFactory();
        connection = CreateConnection();
    }

    private static ConnectionFactory CreateConnectionFactory()
    {
        string password = Environment.GetEnvironmentVariable("EVENT_BUS_PASSWORD")!;
        string user = Environment.GetEnvironmentVariable("EVENT_BUS_USER")!;
        int port = int.Parse(Environment.GetEnvironmentVariable("EVENT_BUS_PORT")!);
        string host = Environment.GetEnvironmentVariable("EVENT_BUS_HOST")!;
        return new()
        {
            UserName = user,
            Password = password,
            HostName = host,
            Port = port,
            DispatchConsumersAsync = true
        };
    }

    private IConnection? CreateConnection()
    {
        int reconnectAttempts = 0;
        IConnection? connection = null;
        while (connection == null && reconnectAttempts < reconnectLimit)
        {
            try
            {
                connection = connectionFactory.CreateConnection();
            }
            catch (BrokerUnreachableException)
            {
                reconnectAttempts++;
                Thread.Sleep(reconnectionTime);
            }
        }
        return connection;
    }

    public IModel? GetChannel()
    {
        connection ??= CreateConnection();
        if (connection != null)
        {
            return connection.CreateModel();
        }
        return null;
    }
}
