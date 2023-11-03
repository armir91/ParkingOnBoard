using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingOnBoard.ApplicationMenu;

public static class StreetManagementMenu
{
    public static void StreetManagement()
    {
        Console.Clear();
        Console.WriteLine("Option 1 selected - Street Management.");
        Console.WriteLine("If you want to manage the streets, please select one of the options below(or type Exit to exit the program):\n");
        Console.WriteLine(" a. Add a new street.");
        Console.WriteLine(" b. Close a street(make it InActive).");
        Console.WriteLine(" c. Validate a street(make it Active)\n");
        Console.WriteLine("\n\rPress the Enter key to continue");
    }
}
