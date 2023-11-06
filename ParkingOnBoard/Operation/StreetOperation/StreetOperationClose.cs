using ParkingOnBoard.Context;
using ParkingOnBoard.Operation;
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

                Console.WriteLine("Please specify a few characters of the street you want to close in order to first find it: ");
                name = Console.ReadLine();

                while (!ValidateRegEx.ValidateName(name))
                {
                    Console.WriteLine("Input contains special characters or numbers.\nPlease try again.\n");
                    name = Console.ReadLine();
                }

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

                    int selection = ValidateSelection.ValidateUserInput();

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
