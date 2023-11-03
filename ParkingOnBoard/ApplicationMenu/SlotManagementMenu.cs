using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingOnBoard.ApplicationMenu;

public static class SlotManagementMenu
{
    public static void SlotManagement()
    {
        Console.Clear();
        Console.WriteLine("Option 2 selected - Parking Slots Management.");
        Console.WriteLine("Your options for slots management are listed below:\n");
        Console.WriteLine(" a. Add a parking slot to a street.");
        Console.WriteLine(" b. Remove a parking slot from a street.");
        Console.WriteLine(" c. Close a parking slot(make it InActive).");
        Console.WriteLine(" d. Validate a parking slot(make it Active).\n");
        Console.WriteLine("Enter your selection letter (or type Exit to exit the application)");
    }
}
