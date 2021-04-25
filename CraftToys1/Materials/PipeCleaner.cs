using System;
using System.Collections.Generic;
using System.Text;

namespace CraftToys1
{
    class PipeCleaner : Material
    {
        public PipeCleaner(string _name, double _cost) : base(_name, _cost)
        {
            MaterialName = "PipeCleaner";
        }
    }
}
