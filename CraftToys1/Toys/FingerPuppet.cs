using System;
using System.Collections.Generic;
using System.Text;

namespace CraftToys1
{
    class FingerPuppet : Toy
    {
        public FingerPuppet() : base()
        {
            ToyName = "Finger Puppet";
            ToyReqs = new Dictionary<string, int>()
            {
                { "PomPom", 1 },
                { "PipeCleaner", 1 },
                { "GooglyEyes", 2 },
            };
        }
    }
}
