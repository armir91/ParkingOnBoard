namespace ParkingOnBoard.ApplicationMenu;

public static class ApplicationMenu
{
    public static void MainMenu()
    {
        Console.Clear();
        Console.WriteLine("WELCOME TO PARKING ON BOARD APP.\n\nYour main menu options are:\n");
        Console.WriteLine(" 1. Manage information on streets.");
        Console.WriteLine(" 2. Manage parking slots.");
        Console.WriteLine(" 3. Parking.\n");
        Console.WriteLine("Enter your selection number (or type Exit to exit the program)");
    }
}
