using EasyNetQ;


namespace Service.Messaging.Consume
{
    public interface IMessageConsumer
    {
        /// <summary>
        /// Consumes incoming messages, first converting the message JSON into a dynamic object, #
        /// them passing it to a business logic layer
        /// </summary>
        /// <param name="message">Sample message from bus with JSON string</param>
        void Consume(dynamic message);
    }
}