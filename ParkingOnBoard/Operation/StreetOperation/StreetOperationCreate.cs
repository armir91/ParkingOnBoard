using ParkingOnBoard.Context;
using ParkingOnBoard.Entities;
using ParkingOnBoard.Operation.StreetOperation.InputValidation;

namespace ParkingOnBoard.Operations.StreetOperation;

public static class StreetOperationCreate
{
    public static void CreateStreet()
    {
        Console.Clear();
        Console.WriteLine("Option 'a' - Add a new street selected:\n");

        using (DataContext context = new())
        {
            try
            {
                Console.WriteLine("Please specify a name for the street: ");
                string name = StreetUserInput.NameInput();

                Console.WriteLine("Number of sides(select 1 or 2):");
                int sides = StreetUserInput.SidesInput();

                bool isActive = true;

                Console.WriteLine("Please input the total slot number:");
                int totalSlots = StreetUserInput.TotalSlotsInput();

                var street = new Street
                {
                    Name = name,
                    Sides = sides,
                    TotalSlots = totalSlots,
                    IsActive = isActive
                };

                context.Add(street);
                context.SaveChanges();

                Console.WriteLine("Street created successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        };
    }
}
