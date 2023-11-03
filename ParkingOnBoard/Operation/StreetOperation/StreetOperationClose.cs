using ParkingOnBoard.Context;
using System.Text.RegularExpressions;

namespace ParkingOnBoard.Operations.StreetOperation;

public static class StreetOperationClose
{
    public static void CloseStreet()
    {
        Console.Clear();
        Console.WriteLine("Option 'b' - Close a street selected:");

        using (DataContext context = new())
        {
            try
            {
                string name;
                bool hasSpecialCharacters = false;
                do
                {

                    Console.WriteLine("Please specify a few characters of the street you want to close in order to first find it(or click enter to list all the streets): ");
                    name = Console.ReadLine();

                    // Define the regular expression pattern to allow only alphanumeric characters
                    Regex regex = new("^[a-zA-Z0-9 ]*$");

                    if (regex.IsMatch(name))
                    {
                        // Your logic if the input is valid
                        Console.WriteLine("Searching for matches: " + name + "\n");
                        hasSpecialCharacters = false;
                    }
                    else
                    {
                        // Your logic if the input contains special characters
                        Console.WriteLine("Input contains special characters. Please try again.");
                        hasSpecialCharacters = true;
                    }
                } while (hasSpecialCharacters);

                var searchResult = context.Streets.Where(x => x.Name.Contains(name) && x.IsActive == true).ToList();


                if (searchResult.Count > 0)
                {
                    Console.WriteLine("Found the below streets: ");
                    Console.WriteLine("ID:\t Name:");
                    foreach (var street in searchResult)
                    {
                        Console.WriteLine($"{street.Id}\t {street.Name}");
                    }

                    Console.WriteLine("Please select which of the above results you want to close(input ID)?");
                    int selection;
                    while (!int.TryParse(Console.ReadLine(), out selection))
                    {

                        Console.WriteLine("You entered an invalid value.");
                        Console.WriteLine("Retry again!");
                    }

                    context.Streets.Where(s => selection == s.Id).ToList().ForEach(x => x.IsActive = false);
                    context.SaveChanges();

                    Console.WriteLine($"The street with ID: {selection} closed successfully.");
                }

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        };
    }
}
