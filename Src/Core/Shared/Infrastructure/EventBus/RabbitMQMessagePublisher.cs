using RabbitMQ.Client;
using Src.Core.Shared.Infrastructure.Exceptions;

namespace Src.Core.Shared.Infrastructure.EventBus;

public class RabbitMQMessagePublisher
{
    private readonly RabbitMQEventBusConnection eventBusConnection;
    private readonly int messageDeliveryMode;

    public RabbitMQMessagePublisher(RabbitMQEventBusConnection eventBusConnection)
    {
        this.eventBusConnection = eventBusConnection;
        messageDeliveryMode = int.Parse(
            Environment.GetEnvironmentVariable("EVENT_BUS_MESSAGE_DELIVERY_MODE")!
        );
    }

    public void Publish(string exchangeName, byte[] body, IBasicProperties? properties = null)
    {
        try
        {
            using IModel channel = eventBusConnection.GetChannel()!;
            if (properties == null)
            {
                properties = channel.CreateBasicProperties();
                properties.DeliveryMode = (byte)messageDeliveryMode;
            }
            channel.BasicPublish(exchangeName, "", properties, body);
        }
        catch (Exception exception)
        {
            throw new EventBusMessagePublishingFailedException(exception.ToString());
        }
    }
}
