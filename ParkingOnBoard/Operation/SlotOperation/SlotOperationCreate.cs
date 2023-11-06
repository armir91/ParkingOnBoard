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

                Console.WriteLine("Please specify a few characters of the street in order to first find it: ");
                name = Console.ReadLine();

                while (!ValidateRegEx.ValidateName(name))
                {
                    Console.WriteLine("Input contains special characters or numbers.\nPlease try again.\n");
                    name = Console.ReadLine();
                }

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

                    int selection = ValidateSelection.ValidateUserInput();

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
