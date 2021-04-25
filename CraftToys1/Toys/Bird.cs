using System;
using System.Collections.Generic;
using System.Text;

namespace CraftToys1
{
    class Bird : Toy
    {
        public Bird() : base()
        {
            ToyName = "Bird";
            ToyReqs = new Dictionary<string, int>()
            {
                { "PipeCleaner", 1 },
                { "GooglyEyes", 2 },
                { "Felt", 1 },
            };
        }
    }
}
