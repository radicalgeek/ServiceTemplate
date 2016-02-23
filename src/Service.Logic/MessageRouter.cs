using System;
using Serilog;
using Service.Messaging.Filter;
using Service.Messaging.Routing;

namespace Service.Logic
{
    /// <summary>
    /// This class proccesses incoming messages, and persists them to the data store
    /// </summary>
    public class MessageRouter : IMessageRouter
    {
        private readonly IServiceOperations _dataOperations;
        private readonly IMessageFilter _filter;

        public MessageRouter(IServiceOperations dataOperations, IMessageFilter filter)
        {
            _dataOperations = dataOperations;
            _filter = filter;
        }

        /// <summary>
        /// Route the message based on the "method" field in the incoming message.
        /// </summary>
        /// <param name="message">dynamic message object from the bus keeps contracts loosly coupled</param>
        public void RouteSampleMessage(dynamic message)
        {
            if (_filter.ShouldTryProcessingMessage(message))
            {
                switch ((string) message.Method.ToString())
                {
                    case "GET":
                        Log.Information("Event=\"Message Routed\" Operation=\"GET\" ");
                        _dataOperations.GetSampleEntities(message);
                        break;
                    case "POST":
                        Log.Information("Event=\"Message Routed\" Operation=\"POST\" ");
                        _dataOperations.CreateSampleEntities(message);
                        break;
                    case "PUT":
                        Log.Information("Event=\"Message Routed\" Operation=\"PUT\" ");
                        _dataOperations.UpdateSampleEntities(message);
                        break;
                    case "DELETE":
                       Log.Information("Event=\"Message Routed\" Operation=\"DELETE\" ");
                        _dataOperations.DeleteSampleEntities(message);
                        break;
                }
            }
        }

        

      
    }
}
