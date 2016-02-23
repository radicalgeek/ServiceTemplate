using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Messaging.Subscribe
{
   public interface IMessageSubscriber
    {
        void Start();
        void Stop();
    }
}

