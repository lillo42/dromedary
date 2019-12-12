using System.Collections.Generic;

namespace Dromedary
{
    public class Output : IWriteOutput, IReadOutput
    {
        private readonly LinkedList<object> _outputs = new LinkedList<object>();
        
        public void WriteOutput(object result)
        {
            _outputs.AddLast(result);
        }

        object IReadOutput.Output => _outputs.Last.Value;
        public IEnumerable<object> Outputs => _outputs;
    }
}
