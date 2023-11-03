using ParkingOnBoard.Context;

namespace ParkingOnBoard.Operation.SlotOperation;

public static class SlotOperationClose
{
    public static void CloseSlot()
    {
        Console.Clear();
        Console.WriteLine("Option 'c' selected - Close a parking Slot.");
        Console.WriteLine("Below you can view all of the Slot ID-s and the streets that belong to: \n");
        using (DataContext context = new())
        {
            try
            {
                var printStreetSlotsId = (from slots in context.Slots
                                          join street in context.Streets
                                          on slots.StreetId equals street.Id
                                          select new
                                          {
                                              slots.Id,
                                              street.Name,
                                              slots.IsDeleted,
                                              slots.IsActive
                                          }).Where(x => x.IsDeleted != true && x.IsActive != false);

                Console.WriteLine("Slot ID:\tStreet Name:");
                foreach (var item in printStreetSlotsId)
                {
                    Console.WriteLine($"{item.Id}\t\t{item.Name}");
                }

                Console.WriteLine();
                Console.WriteLine("Please specify the slot ID you wish to close(make it InActive): ");

                int selection;
                while (!int.TryParse(Console.ReadLine(), out selection))
                {

                    Console.WriteLine("You entered an invalid value.");
                    Console.WriteLine("Retry again!");
                }

                context.Slots.Where(s => selection == s.Id).ToList().ForEach(x => x.IsActive = false);
                context.SaveChanges();

                Console.WriteLine($"The slot with ID: {selection} closed successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        };
    }
}
