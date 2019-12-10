using System;

namespace Dromedary
{
    public interface IConsumer
    {
        IProcessor CreateProcessor(IServiceProvider provider);
    }
}
