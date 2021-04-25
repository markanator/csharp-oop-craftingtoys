using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Diagnostics;
using static System.Console;
using static CraftToys1.UTILS;

namespace CraftToys1
{
    class Pipboy
    {
        public GameEntity Player;
        public GameEntity Supplier;
        public GameEntity Buyer;

        public Pipboy()
        {
            Player = new GameEntity("Player", 0.0);
            Supplier = new GameEntity("Supplier", 0.0);
            Buyer = new GameEntity("Buyer", 7.07);
            // load inventory from TXT file
            string[] materialsPriceList = File.ReadAllLines("./TextData/MaterialsList.txt");
            string[] toysListWithPrices = File.ReadAllLines("./TextData/ToysList.txt");

            for (int i = 0; i < materialsPriceList.Length; i++)
            {
                string[] materialNameAndCost = materialsPriceList[i].Split(",");
                string[] toyNameAndRvenue = toysListWithPrices[i].Split(",");
                // materialName / cost / playerInventory
                Supplier.MaterialPriceList.Add(materialNameAndCost[0], Convert.ToDouble(materialNameAndCost[1]));
                Supplier.MaterialInventory.Add(materialNameAndCost[0], Convert.ToInt32(materialNameAndCost[2]));
                // PLAYER CONTENT
                // name and askingPrice
                Player.ToyPriceList.Add(toyNameAndRvenue[0], Convert.ToDouble(toyNameAndRvenue[1]));
                // player inventory
                Player.MaterialInventory.Add(materialNameAndCost[0], Convert.ToInt32(materialNameAndCost[2]));
            }

            MainMenu();
        }

        public void MainMenu()
        {
            Clear();
            ForegroundColor = ConsoleColor.White;
            ConsoleColor prevColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine(@"
  ______               ______           ______               _____ ____  ____ _  __
 /_  __/___  __  __   / ____/________ _/ __/ /____  _____   |__  // __ \/ __ \ |/ /
  / / / __ \/ / / /  / /   / ___/ __ `/ /_/ __/ _ \/ ___/    /_ </ / / / / / /   / 
 / / / /_/ / /_/ /  / /___/ /  / /_/ / __/ /_/  __/ /      ___/ / /_/ / /_/ /   |  
/_/  \____/\__, /   \____/_/   \__,_/_/  \__/\___/_/      /____/\____/\____/_/|_|  
          /____/                                                                   
");
            ForegroundColor = prevColor;
            WriteLine("\nHowdy, What would you like to do?\n");
            WriteLine("1) View and Make Toys from Recipes");
            WriteLine("2) Purchase Supplies");
            WriteLine("3) Sell Toys to Buyer");
            WriteLine("4) View Your Inventory");

            WriteLine("Q) Exit Application");
            string playerInput = ReadLine().Trim();

            switch (playerInput.ToLower())
            {
                case "1":
                    ViewAvailableRecipes();
                    break;
                case "2":
                    InteractWithSupplier();
                    break;
                case "3":
                    SellGoods();
                    break;
                case "4":
                    ViewingAllPlayerInventories();
                    break;
                case "q":
                    ForegroundColor = ConsoleColor.DarkYellow;
                    WriteLine("");
                    WriteLine("Thank you for dropping by...");
                    Thread.Sleep(1200);
                    Environment.Exit(0);
                    break;
                default:
                    Clear();
                    MainMenu();
                    break;
            }
        }

        private void ViewingAllPlayerInventories()
        {
            ResetColor();
            Clear();
            // iterate and add to the screen in color
            WriteLine("::: Viewing All Inventory ::: \n");
            WriteLine("~~~ Material Inventory ~~~");
            foreach (var item in Player.MaterialInventory)
            {
                WriteLine($"(x{item.Value}) - {item.Key}");
            }

            WriteLine("");
            WriteLine("~~~ Toy Inventory ~~~");
            if (Player.ToyInventory.Count == 0)
            {
                WriteLine("Nothing in Toy inventory");
            }
            else
            {
                foreach (var item in Player.ToyInventory)
                {
                    WriteLine($"(x{item.Value}) - {item.Key}");
                }
            }
            DisplayPlayerWallet();

            WriteLine("\n::: END REPORT :::");
            WriteLine("Press any key to return to Main Menu.");
            ReadKey();
            MainMenu();
        }

        private void ViewAvailableRecipes()
        {
            ResetColor();
            Clear();
            ConsoleColor prevColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Magenta;
            // iterate and add to the screen in color
            WriteLine("Please Select a Recipe to View");
            int indexCount = 1;
            foreach (var item in Player.ToyPriceList)
            {
                WriteLine($"{indexCount++}) {item.Key}");
            }
            WriteLine("R) Return to Main Menu\n");
            ForegroundColor = prevColor;
            string playerInput = ReadLine().Trim();

            switch (playerInput.ToLower())
            {
                case "1":
                    RenderToyRecipeToScreen(new Jellyfish());
                    break;
                case "2":
                    RenderToyRecipeToScreen(new Chameleon());
                    break;
                case "3":
                    RenderToyRecipeToScreen(new Bird());
                    break;
                case "4":
                    RenderToyRecipeToScreen(new FingerPuppet());
                    break;
                case "r":
                    Clear();
                    MainMenu();
                    break;
                default:
                    Clear();
                    ViewAvailableRecipes();
                    break;
            }


        }

        private void RenderToyRecipeToScreen(Toy _toy)
        {
            Clear();
            WriteLine("::: Viewing Recipe :::");
            WriteLine(_toy.ToyName + "\n");
            WriteLine("Requires::");
            foreach (var item in _toy.ToyReqs)
            {
                WriteLine($"(x{item.Value}) - {item.Key}");
            };

            WriteLine("\n");
            PlayerCanConstructToyUI(_toy);


            WaitForInput("Press any key to return to All Recipes...");
            ViewAvailableRecipes();
        }

        private void PlayerCanConstructToyUI(Toy _toy)
        {
            bool canMake = true;
            foreach (var item in _toy.ToyReqs)
            {
                int MatQuantity = Player.MaterialInventory[item.Key];
                if (MatQuantity - item.Value < 0)
                {
                    canMake = false;
                }
            }

            ConsoleColor prevColor = ForegroundColor;
            if (canMake)
            {
                ForegroundColor = ConsoleColor.Green;
                WriteLine("You can make this!");
                ForegroundColor = prevColor;

                // let user decide if they want to make it
                WriteLine($"Do you want to make (x1) {_toy.ToyName}? \n(Y)es || (N)o");
                string playerInput = ReadLine().Trim();

                if (playerInput.ToLower() == "y")
                {
                    ConstructToy(_toy);
                }
                else
                {
                    // user can make toy but chose not to.
                    return;
                }
            }
            else
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine("Sorry, not enough materials.");
                ForegroundColor = prevColor;
            }
        }

        private void ConstructToy(Toy _toy)
        {
            foreach (var item in _toy.ToyReqs)
            {
                Player.MaterialInventory[item.Key] -= item.Value;
            }

            if (Player.ToyInventory.TryGetValue(_toy.ToyName, out int value))
            {
                if (value > 0)
                {
                    Player.ToyInventory[_toy.ToyName] += 1;
                    WriteLine("Added to Inventory as a duplicate.");
                }
            }
            else
            {
                Player.ToyInventory.Add(_toy.ToyName, 1);
                WriteLine("Added to Inventory as a new Entry.");
            }

            //WaitForInput("Press any key to return to Toy Recipe.");
        }

        private void InteractWithSupplier()
        {
            ResetColor();
            Clear();
            ConsoleColor ogColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine("===== Supplier =====");
            WriteLine("What would you like to buy?");

            int count = 1;
            List<string> matNames = new List<string>() { "nothing" };

            foreach (var item in Supplier.MaterialPriceList)
            {
                int amountLeft = Supplier.MaterialInventory[item.Key];
                WriteLine($"{count++}) [x{amountLeft}] {item.Key} - {item.Value:C} / ea.");
                // add to matName so we can reference later
                matNames.Add(item.Key);
            }
            WriteLine("R) Return to Main Menu");
            // print player's wallet
            DisplayPlayerWallet();

            // take player input
            //Write("I'll take a number... \n");
            string playerInput = ReadLine().Trim();
            Debug.Write(playerInput);

            // return to Main Menu
            if (playerInput.ToLower() == "r") MainMenu();

            // now we check for input
            int convertedInput = Convert.ToInt32(playerInput);
            if (convertedInput > 0 && convertedInput <= Supplier.MaterialPriceList.Count)
            {
                // something found
                string matName = matNames[convertedInput];
                Supplier.MaterialPriceList.TryGetValue(matName, out double matCost);
                // if player has enough money
                if (Player.Wallet - matCost >= 0.0)
                {
                    Player.Wallet -= matCost;
                    Supplier.MaterialInventory[matName] -= 1;
                    Player.MaterialInventory[matName] += 1;

                    WriteLine($"Purchased: {matName} for {matCost:C}. Current Wallet: {Player.Wallet:C}");
                    WaitForInput();
                    InteractWithSupplier();
                }
                else
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("You do not have enough for that! Try selling a toy!");
                    ForegroundColor = ogColor;
                    WaitForInput();
                    InteractWithSupplier();
                }
            }
            else
            {
                // outOfBounds, reprint this fn
                InteractWithSupplier();
            }

            ForegroundColor = ogColor;
        }

        private void SellGoods()
        {
            Clear();
            ConsoleColor ogColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("~~~~ Buyer ~~~~");
            WriteLine("Whatd'ya got for me?");
            ForegroundColor = ogColor;

            // is there anything to show?
            if (Player.ToyInventory.Count <= 0)
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine("Sorry, nothing in inventory.");
                ForegroundColor = ogColor;
                WaitForInput("Press any key to return to Main Menu");
                MainMenu();
            }

            // list out the user's stuff
            List<string> toyNames = new List<string>() { "nothing" };
            int index = 1;
            foreach(var item in Player.ToyInventory)
            {
                // ref the pricelist
                Player.ToyPriceList.TryGetValue(item.Key, out double toyRev);
                // back to normal toy inventory
                WriteLine($"{index++}) (x{item.Value}) {item.Key}");
                WriteLine($"    └─ Sell Price: {toyRev:C} ea.");
                toyNames.Add(item.Key);
            }
            WriteLine("R) Return to Main Menu");

            // print player's wallet
            DisplayPlayerWallet();

            // take input
            string playerInput = ReadLine().Trim();
            // return to main menu
            if (playerInput.ToLower() == "r") MainMenu();

            int convertedInput = Convert.ToInt32(playerInput);
            // within bounds
            if (convertedInput > 0 && convertedInput <= Player.ToyInventory.Count)
            {
                string toyName = toyNames[convertedInput];
                // something found
                Player.ToyInventory.TryGetValue(toyName, out int toyOutValue);
                if (toyOutValue - 1 >= 0)
                {
                    // if player has more than 1 in inventory
                    double toyPrice = Player.ToyPriceList[toyName];
                    Player.Wallet += toyPrice;
                    Player.ToyInventory[toyName] -= 1;
                    //Buyer.ToyInventory.Add(toyName, 1);
                    if (Player.ToyInventory[toyName] == 0)
                    {
                        Player.ToyInventory.Remove(toyName);
                    }
                    WriteLine($"Sold (x1) {toyName}! Current Wallet Amount: {Player.Wallet:C}");
                    WaitForInput("Press any key to Sell more.");
                    SellGoods();
                }
                else
                {
                    WriteLine("Currently Out of stock.");
                    WaitForInput("Press any key to Sell something else.");
                    SellGoods();
                }
            }

            WaitForInput("Press any key to Sell more.");
            SellGoods();
        }

        private void DisplayPlayerWallet()
        {
            // print player's wallet
            ConsoleColor ogColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Green;
            WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            WriteLine($"Current Wallet Amount: {Player.Wallet:C}");
            WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
            ForegroundColor = ogColor;
        }
    }

}
