using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingOnBoard.ApplicationMenu
{
    public static class EndOfApplicationMessage
    {
        public static void EndMessage()
        {
            Console.WriteLine("\n\rPress the Enter key to continue");
            Console.ReadLine();
        }
    }
}
