using RabbitMQ.Client;
using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Infrastructure.EventBus;

public class RabbitmqMessagePublisher
{
    private readonly RabbitmqEventBusConnection eventBusConnection;
    private readonly int messageDeliveryMode;

    public RabbitmqMessagePublisher(RabbitmqEventBusConnection eventBusConnection)
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
            channel.Close();
        }
        catch (Exception exception)
        {
            throw new EventBusError(exception.ToString());
        }
    }
}
