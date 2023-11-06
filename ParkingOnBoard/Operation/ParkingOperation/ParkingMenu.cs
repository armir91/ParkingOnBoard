namespace ParkingOnBoard.Operation.ParkingOperation;

public static class ParkingMenu
{
    public static void ParkMenu()
    {
        Console.Clear();
        Console.WriteLine("Option 'a' selected - Park");
        Console.WriteLine("Please let us know the street in which you want to park:\n");
        Console.WriteLine("If you don't have an option in mind, please press '*' for a list with all available free slots:\n");
    }
}
