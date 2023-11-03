using ParkingOnBoard.ApplicationMenu;
using ParkingOnBoard.Context;
using ParkingOnBoard.Operation.ParkingOperation;
using ParkingOnBoard.Operation.SlotOperation;
using ParkingOnBoard.Operations.StreetOperation;



//automating migrations + seed initial data in the DB
AutoMigrateConfiguration.AutoMigrateAndSeed();

// variables that support data entry

string menuSelection = "";
string? readResult = "";

do
{
    ApplicationMenu.MainMenu();

    readResult = Console.ReadLine();
    menuSelection = readResult.ToLower();

    switch (menuSelection)
    {
        case "1":
            do
            {
                StreetManagementMenu.StreetManagement();
                readResult = Console.ReadLine();
                menuSelection = readResult.ToLower();

                switch (menuSelection)
                {
                    case "a":

                        StreetOperationCreate.CreateStreet();
                        EndOfApplicationMessage.EndMessage();
                        readResult = Console.ReadLine();

                        break;

                    case "b":

                        StreetOperationClose.CloseStreet();
                        EndOfApplicationMessage.EndMessage();
                        readResult = Console.ReadLine();

                        break;

                    case "c":

                        StreetOperationValidate.ValidateStreet();
                        EndOfApplicationMessage.EndMessage();
                        readResult = Console.ReadLine();

                        break;
                }

            } while (menuSelection != "exit");

            break;
        case "2":

            do
            {
                SlotManagementMenu.SlotManagement();
                readResult = Console.ReadLine();
                menuSelection = readResult.ToLower();

                switch (menuSelection)
                {
                    case "a":

                        SlotOperationCreate.CreateSlot();
                        EndOfApplicationMessage.EndMessage();
                        readResult = Console.ReadLine();

                        break;
                    case "b":

                        SlotOperationRemove.RemoveSlot();
                        EndOfApplicationMessage.EndMessage();
                        readResult = Console.ReadLine();

                        break;
                    case "c":

                        SlotOperationClose.CloseSlot();
                        EndOfApplicationMessage.EndMessage();
                        readResult = Console.ReadLine();

                        break;
                    case "d":

                        SlotOperationValidate.ValidateSlot();
                        EndOfApplicationMessage.EndMessage();
                        readResult = Console.ReadLine();

                        break;
                }

            } while (menuSelection != "exit");

            EndOfApplicationMessage.EndMessage();
            readResult = Console.ReadLine();

            break;
        case "3":

            do
            {
                ParkingMenuOption.ParkingMenu();
                readResult = Console.ReadLine();
                menuSelection = readResult.ToLower();

                switch (menuSelection)
                {
                    case "a":
                        Console.Clear();
                        Console.WriteLine("Option 'a' selected - Park");
                        Console.WriteLine("Please let us know the street in which you want to park in order to see if there ia any free slot available:\n");
                        Console.WriteLine("If you don't have an option in mind, please press '*' in order for us to print a list with all available free spots:\n");
                        readResult = Convert.ToString(Console.ReadLine());

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


                                if (readResult == "*")
                                {
                                    result = result;
                                }else
                                {
                                    result = result.Where(x => x.Name.Contains(readResult));
                                }



                                Console.WriteLine("Slot ID:\tOccupied:\tStreet Name:");
                                foreach (var item in result)
                                {
                                    Console.WriteLine($"{item.Id}\t\t{item.IsOccupied}\t\t{item.Name}");
                                }

                                Console.WriteLine("Please specify in which of the listed streets you wish to park by specifying the ID of the slot: ");
                                int selection;
                                while (!int.TryParse(Console.ReadLine(), out selection))
                                {

                                    Console.WriteLine("You entered an invalid value.");
                                    Console.WriteLine("Retry again!");
                                }

                                context.Slots.Where(s => selection == s.Id).ToList().ForEach(x => x.IsOccupied = true);
                                context.SaveChanges();

                                Console.WriteLine($"The parking slot with the ID: {selection} occupied successfully.");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        };

                        EndOfApplicationMessage.EndMessage();
                        readResult = Console.ReadLine();

                        break;
                    case "b":

                        ParkingOperationUnpark.Unpark();
                        EndOfApplicationMessage.EndMessage();
                        readResult = Console.ReadLine();

                        break;
                }

            } while (menuSelection != "exit");

            EndOfApplicationMessage.EndMessage();
            readResult = Console.ReadLine();

            break;
    }
} while (menuSelection != "exit");
