using ParkingOnBoard.Context;

namespace ParkingOnBoard.Operation.ParkingOperation;

public static class ParkingOperationPark
{
    public static void Park()
    {
        string readResult = Convert.ToString(Console.ReadLine());

        using (DataContext context = new())
        {
            try
            {
                var result = (from slots in context.Slots
                              join street in context.Streets
                              on slots.StreetId equals street.Id
                              select new
                              {
                                  slots.Id,
                                  street.Name,
                                  slots.IsDeleted,
                                  slots.IsActive,
                                  slots.IsOccupied

                              }).Where(x => x.IsDeleted == false && x.IsActive == true && x.IsOccupied != true);


                if (readResult != "*")
                    result = result.Where(x => x.Name.Contains(readResult));



                Console.WriteLine("Slot ID:\tOccupied:\tStreet Name:");

                foreach (var item in result)
                {
                    Console.WriteLine($"{item.Id}\t\t{item.IsOccupied}\t\t{item.Name}");
                }
                Console.WriteLine();
                Console.WriteLine("Please specify in which of the listed streets you wish to park by specifying the ID of the slot: ");

                int selection = ValidateSelection.ValidateUserInput();

                context.Slots.Where(s => selection == s.Id).ToList().ForEach(x => x.IsOccupied = true);
                context.SaveChanges();

                Console.WriteLine($"The parking slot with the ID: {selection} occupied successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        };
    }
}
