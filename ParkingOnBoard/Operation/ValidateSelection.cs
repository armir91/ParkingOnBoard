namespace ParkingOnBoard;

public static class ValidateSelection
{
    public static int ValidateUserInput()
    {
        int selection;
        while (!int.TryParse(Console.ReadLine(), out selection))
        {

            Console.WriteLine("You entered an invalid value.");
            Console.WriteLine("Retry again!");
        }
        return selection;
    }
}
