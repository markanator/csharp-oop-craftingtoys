using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using static CraftToys1.UTILS;

namespace CraftToys1
{
    class GameEntity
    {
        public string Name;
        public double Wallet;
        // type : cost
        public Dictionary<string, double> MaterialPriceList;
        // type : cost
        public Dictionary<string, double> ToyPriceList;
        // user stuff here
        // type : amount
        public Dictionary<string, int> MaterialInventory;
        // type : amount
        public Dictionary<string, int> ToyInventory;


        public GameEntity(string _name, double _wallet)
        {
            Name = _name;
            Wallet = _wallet;
            MaterialPriceList = new Dictionary<string, double>();
            ToyPriceList = new Dictionary<string, double>();

            MaterialInventory = new Dictionary<string, int>();
            ToyInventory = new Dictionary<string, int>();
        }


        public void PrintMaterialPriceList()
        {
            foreach(var item in MaterialPriceList)
            {
                WriteLine($"{item.Value} : ${item.Value}");
            }

            WaitForInput();
        }
    }
}
