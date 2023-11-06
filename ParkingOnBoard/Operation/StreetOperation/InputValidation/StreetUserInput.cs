namespace ParkingOnBoard.Operation.StreetOperation.InputValidation;

public static class StreetUserInput
{
    public static string NameInput()
    {
        string name = Console.ReadLine();

        while (string.IsNullOrEmpty(name) || !ValidateRegEx.ValidateName(name))
        {
            Console.WriteLine("Please try again by inputing the correct format of a name(no special characters allowed)!");
            name = Console.ReadLine();
        }
        return name;
    }

    public static int SidesInput()
    {
        int sides;
        while (!int.TryParse(Console.ReadLine(), out sides) || sides < 1 || sides > 2)
        {
            Console.WriteLine("You entered an invalid number(options 1 or 2 only).");
            Console.WriteLine("Retry again!");
        }
        return sides;
    }

    public static int TotalSlotsInput()
    {
        int totalSlots;
        while (!int.TryParse(Console.ReadLine(), out totalSlots) || totalSlots < 1 || totalSlots > 20)
        {
            Console.WriteLine("You entered an invalid number(please input only numbers from 1 to 20).");
            Console.WriteLine("Retry again!");
        }
        return totalSlots;
    }
}
