using ParkingOnBoard.Context;

namespace ParkingOnBoard.Operation.SlotOperation;

public static class SlotOperationRemove
{
    public static void RemoveSlot()
    {
        Console.Clear();
        Console.WriteLine("Option 'b' selected - Remove a parking slot from a specific street\n");
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
                                              slots.IsDeleted
                                          }).Where(x => x.IsDeleted == false);

                Console.WriteLine("Slot ID:\tStreet Name:");
                foreach (var item in printStreetSlotsId)
                {
                    Console.WriteLine($"{item.Id}\t\t{item.Name}");
                }

                Console.WriteLine("Please specify the slot ID you wish to remove(delete): ");

                int selection = ValidateSelection.ValidateUserInput();


                context.Slots.Where(s => selection == s.Id).ToList().ForEach(x => x.IsDeleted = true);
                context.Slots.Where(s => selection == s.Id).ToList().ForEach(x => x.IsActive = false);
                context.SaveChanges();

                Console.WriteLine($"The slot with ID: {selection} deleted successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        };
    }
}
