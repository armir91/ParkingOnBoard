using ParkingOnBoard.Context;

namespace ParkingOnBoard.Operation.ParkingOperation;

public static class ParkingOperationUnpark
{
    public static void Unpark()
    {
        Console.Clear();
        Console.WriteLine("Option 'b' selected - UnPark");
        Console.WriteLine("Below you can view a list of the occupied slots:\n");

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

                              }).Where(x => x.IsOccupied == true);


                Console.WriteLine("Slot ID:\tOccupied:\tStreet Name:");
                foreach (var item in result)
                {
                    Console.WriteLine($"{item.Id}\t\t{item.IsOccupied}\t\t{item.Name}");
                }

                Console.WriteLine("Please specifi in which of the listed streets you wish to park by specifying the ID of the slot: ");

                int selection = ValidateSelection.ValidateUserInput();

                context.Slots.Where(s => selection == s.Id).ToList().ForEach(x => x.IsOccupied = false);
                context.SaveChanges();

                Console.WriteLine($"The parking slot with the ID: {selection} freed successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        };
    }
}
