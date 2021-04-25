using System;
using System.Collections.Generic;
using System.Text;

namespace CraftToys1
{
    class Material
    {
        public string MaterialName;
        public double PurchaseCost;

        public Material (string _name, double _cost)
        {
            MaterialName = _name;
            PurchaseCost = _cost;
        }
        // TODO use enums for type?
    }
}
