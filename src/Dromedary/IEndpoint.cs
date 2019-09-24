using System.Collections.Generic;

namespace Dromedary
{
    public interface IEndpoint
    {
        IDromedaryContext Context { get; }
        
        IProducer CreateProducer();

        IConsumer CreateConsumer();

        void ConfigureProperties(IDictionary<string, object> option);
    }
}
