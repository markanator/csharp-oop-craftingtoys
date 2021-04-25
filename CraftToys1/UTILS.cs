using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace CraftToys1
{
    static class UTILS
    {

        public static void WaitForInput(string msg = "Press any key to continue...")
        {
            WriteLine(msg);
            ReadKey();
        }
    }
}
