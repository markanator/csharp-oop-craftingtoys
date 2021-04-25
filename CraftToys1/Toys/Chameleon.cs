using System;
using System.Collections.Generic;
using System.Text;

namespace CraftToys1
{
    class Chameleon : Toy
    {
        public Chameleon() : base()
        {
            ToyName = "Chameleon";
            ToyReqs = new Dictionary<string, int>()
            {
                { "PipeCleaner", 1 },
                { "GooglyEyes", 2 },
            };
        }
    }
}
