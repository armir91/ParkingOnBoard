using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingOnBoard.ApplicationMenu;

public static class ParkingMenuOption
{
    public static void ParkingMenu()
    {
        Console.Clear();
        Console.WriteLine("Option 3 selected - Parking.");
        Console.WriteLine("Your options for parking a car are listed below:\n");
        Console.WriteLine(" a. Park");
        Console.WriteLine(" b. Unpark\n");
        Console.WriteLine("Enter your selection letter (or type Exit to exit the application)");
    }
}
