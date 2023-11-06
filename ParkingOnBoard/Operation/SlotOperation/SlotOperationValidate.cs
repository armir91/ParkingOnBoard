using ParkingOnBoard.Context;

namespace ParkingOnBoard.Operation.SlotOperation;

public static class SlotOperationValidate
{
    public static void ValidateSlot()
    {
        Console.Clear();
        Console.WriteLine("Option 'd' selected - Validate a parking slot");
        Console.WriteLine("Below you can view all of the Slot ID-s and it's corresponding street that can be validated: \n");
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
                                          }).Where(x => x.IsDeleted == false && x.IsActive == false);

                Console.WriteLine("Slot ID:\tStreet Name:");
                foreach (var item in printStreetSlotsId)
                {
                    Console.WriteLine($"{item.Id}\t\t{item.Name}");
                }

                Console.WriteLine("Please specify the slot ID you wish to validate(set to Active): ");

                int selection = ValidateSelection.ValidateUserInput();

                context.Slots.Where(s => selection == s.Id).ToList().ForEach(x => x.IsActive = true);
                context.SaveChanges();

                Console.WriteLine($"The slot with ID: {selection} activated successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        };
    }
}
