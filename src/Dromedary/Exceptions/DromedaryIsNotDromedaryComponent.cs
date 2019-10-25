using System;

namespace Dromedary.Exceptions
{
    public class DromedaryIsNotDromedaryComponent : DromedaryException
    {
        public Type Component { get; }
        
        public DromedaryIsNotDromedaryComponent(Type component)
        {
            Component = component;
        }
    }
}
