using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;

namespace Dromedary
{
    public class DefaultChannel : IChannel
    {
        public IProcessor Current { get; protected set; }

        object IEnumerator.Current => Current;
        
        public IEnumerator<IProcessor> GetEnumerator() 
            => this;

        IEnumerator IEnumerable.GetEnumerator() 
            => GetEnumerator();

        public bool MoveNext()
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}
