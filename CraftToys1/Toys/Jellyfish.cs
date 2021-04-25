using System;
using System.Collections.Generic;
using System.Text;

namespace CraftToys1
{
    class Jellyfish : Toy
    {
        public Jellyfish() : base()
        {
            ToyName = "Jellyfish";
            ToyReqs = new Dictionary<string, int>() {
                { "PomPom", 1 },
                { "PipeCleaner", 4 },
            };
        }
    }
}
