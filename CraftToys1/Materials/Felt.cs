using System;
using System.Collections.Generic;
using System.Text;

namespace CraftToys1
{
    class Felt : Material
    {
        public Felt(string _name, double _cost) : base(_name, _cost)
        {
            MaterialName = "Felt";
        }
    }
}
