using ParkingOnBoard.Context;
using ParkingOnBoard.Entities;
using System.Text.RegularExpressions;

namespace ParkingOnBoard.Operation.SlotOperation;

public static class SlotOperationCreate
{
    public static void CreateSlot()
    {
        Console.Clear();
        Console.WriteLine("Option 'a' selected - Add a new parking slot to a specific street\n");
        Console.WriteLine("In order to add a parking slot to a street, first you need to find the desired street: ");

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
                        Console.WriteLine("Input contains special characters. Please try again.\n");
                        hasSpecialCharacters = true;
                    }
                } while (hasSpecialCharacters);

                var searchResult = context.Streets.Where(x => x.Name.Contains(name) && x.IsActive == true).ToList();

                if (searchResult.Count > 0)
                {
                    Console.WriteLine("Found the below streets: \n");
                    Console.WriteLine("ID:\t Name:");
                    foreach (var street in searchResult)
                    {
                        Console.WriteLine($"{street.Id}\t {street.Name}");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Please select in which street you want to add a slot(please input ID)?");
                    int selection;
                    while (!int.TryParse(Console.ReadLine(), out selection))
                    {

                        Console.WriteLine("You entered an invalid value.");
                        Console.WriteLine("Retry again!");
                    }

                    var slot = new Slot
                    {
                        IsActive = true,
                        StreetId = selection
                    };

                    context.Add(slot);
                    context.SaveChanges();

                    Console.WriteLine($"The new slot has been added successfully to the street with ID: {selection}.");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        };
    }
}
