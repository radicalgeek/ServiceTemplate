namespace Service.Messaging.Filter
{
    public interface IMessageFilter
    {
        bool ShouldTryProcessingMessage(dynamic message);
    }
}