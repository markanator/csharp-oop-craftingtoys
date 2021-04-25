using System;
using System.Collections.Generic;
using System.Text;

namespace CraftToys1
{
    class Toy
    {
        public string ToyName;
        public Dictionary<string, int> ToyReqs;

        public Toy() { }
        public Toy (string _name, Dictionary<string, int> _reqs)
        {
            ToyName = _name;
            ToyReqs = _reqs;
        }

        public virtual void ConstructToys()
        {
            // Override in subclasses
        }
    }
}
