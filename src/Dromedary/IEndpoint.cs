using System.Collections.Generic;

namespace Dromedary
{
    public interface IEndpoint
    {
        IProducer CreateProducer();

        IConsumer CreateConsumer();

        void ConfigureProperties(IDictionary<string, object> option);
    }
}
