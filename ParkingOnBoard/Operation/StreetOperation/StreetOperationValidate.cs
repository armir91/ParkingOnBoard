using ParkingOnBoard.Context;

namespace ParkingOnBoard.Operations.StreetOperation;

public static class StreetOperationValidate
{
    public static void ValidateStreet()
    {
        Console.Clear();
        Console.WriteLine("Option 'c'- Street validation selected.");

        using (DataContext context = new())
        {
            try
            {
                Console.WriteLine("Below you can view the list of streets that are closed: ");

                var searchResult = context.Streets.Where(x => x.IsActive == false).ToList();

                Console.WriteLine("ID:\t Name:");
                foreach (var street in searchResult)
                {
                    Console.WriteLine($"{street.Id}\t {street.Name}");
                }

                Console.WriteLine("Please select which of the above results you want to validate(input ID)?");
                int selection;
                while (!int.TryParse(Console.ReadLine(), out selection))
                {

                    Console.WriteLine("You entered an invalid value.");
                    Console.WriteLine("Retry again!");
                }

                context.Streets.Where(s => selection == s.Id).ToList().ForEach(x => x.IsActive = true);
                context.SaveChanges();

                Console.WriteLine($"The street with ID: {selection} validated successfully.");

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        };
    }
}
