using ParkingOnBoard.Context;
using ParkingOnBoard.Entities;

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
                string name = Console.ReadLine();

                while (string.IsNullOrEmpty(name))
                {
                    Console.Clear();
                    Console.WriteLine("Please try again by inputing the correct format!");
                    name = Console.ReadLine();
                }



                Console.WriteLine("Number of sides(select 1 or 2):");
                int sides;
                while (!int.TryParse(Console.ReadLine(), out sides) || sides < 1 || sides > 2)
                {
                    Console.Clear();
                    Console.WriteLine("You entered an invalid number(options 1 or 2 only).");
                    Console.WriteLine("Retry again!");
                }

                bool isActive = true;


                Console.WriteLine("Please input the total slot number:");
                int totalSlots;
                while (!int.TryParse(Console.ReadLine(), out totalSlots) || totalSlots < 1 || totalSlots > 20)
                {
                    Console.Clear();
                    Console.WriteLine("You entered an invalid number(please input only numbers from 1 to 20).");
                    Console.WriteLine("Retry again!");
                }

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
