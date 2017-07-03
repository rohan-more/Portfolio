using System.Collections.Generic;

namespace AISandbox
{
    public abstract class Node
    {
        public string[] keys;
        public virtual void Init() { }
        public abstract BehaviorTree.Result Process();
    }
}
