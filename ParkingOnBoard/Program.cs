
using Microsoft.EntityFrameworkCore;
using ParkingOnBoard.Context;
using ParkingOnBoard.Entities;

using (DataContext context = new DataContext())
{
    context.Database.Migrate();
}

// seed initial data in the database
try
{
    using DataContext context = new DataContext();
    await Seed.SeedData(context);
}
catch (Exception)
{
    Console.WriteLine("An error occurred during the migration.");
}

// variables that support data entry

string menuSelection = "";
string? readResult = "";

do
{
    Console.Clear();

    Console.WriteLine("WELCOME TO PARKING ON BOARD APP.\n\nYour main menu options are:\n");
    
    Console.WriteLine(" 1. Manage information on streets.");
    Console.WriteLine(" 2. Manage parking slots.");
    Console.WriteLine(" 3. Parking.\n");
    Console.WriteLine("Enter your selection number (or type Exit to exit the program)");

    readResult = Console.ReadLine();
    if (readResult != null)
        menuSelection = readResult.ToLower();

    switch (menuSelection)
    {
        case "1":
            do
            {
                Console.Clear();
                Console.WriteLine("Option 1 selected - Street Management.");
                Console.WriteLine("If you want to manage the streets, please select one of the options below(or type Exit to exit the program):\n");

                Console.WriteLine(" a. Add a new street.");
                Console.WriteLine(" b. Close a street(make it InActive).");
                Console.WriteLine(" c. Validate a street(make it Active)\n");

                Console.WriteLine("\n\rPress the Enter key to continue");

                readResult = Console.ReadLine();
                if (readResult != null)
                    menuSelection = readResult.ToLower();

                switch (menuSelection)
                {
                    case "a":
                        Console.Clear();
                        Console.WriteLine("Option 'a' - Add a new street selected:");

                        using (DataContext context = new DataContext())
                        {
                            try
                            {
                                Console.WriteLine("Street Name:");
                                string name = Console.ReadLine();
                                int sides;
                                do
                                {
                                    Console.WriteLine("Number of sides(select 1 or 2):");
                                    sides = Convert.ToInt32(Console.ReadLine());

                                } while (sides < 1 || sides > 2);
                                

                                bool isActive = true;

                                Console.WriteLine("Total slots number:");
                                int totalSlots = Convert.ToInt32(Console.ReadLine());

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

                        Console.WriteLine("\n\rPress the Enter key to continue");
                        readResult = Console.ReadLine();
                        break;

                    case "b":
                        Console.Clear();
                        Console.WriteLine("Option 'b' - Close a street selected:");
                        
                        using (DataContext context = new DataContext())
                        {
                            try
                            {
                                Console.WriteLine("Please specify a few characters of the street you want to close: ");
                                string name = Console.ReadLine();

                                var searchResult = context.Streets.Where(x => x.Name.Contains(name) && x.IsActive == true).ToList();

                                
                                if (searchResult.Count() > 0)
                                {
                                    Console.WriteLine("Found the below streets: ");
                                    Console.WriteLine("ID:\t Name:");
                                    foreach (var street in searchResult)
                                    {
                                        Console.WriteLine($"{street.Id}\t {street.Name}");
                                    }

                                    Console.WriteLine("Please select which of the above results you want to close(input ID)?");
                                    var selection = Convert.ToInt32(Console.ReadLine());

                                    context.Streets.Where(s => selection == s.Id).ToList().ForEach(x => x.IsActive = false);
                                    context.SaveChanges();

                                    Console.WriteLine($"The street with ID: {selection} closed successfully.");
                                }
                            }
                            catch (Exception e)
                            {

                                Console.WriteLine(e.Message);
                            }
                        };
                        Console.WriteLine("\n\rPress the Enter key to continue");
                        readResult = Console.ReadLine();
                        break;

                    case "c":
                        Console.Clear();
                        Console.WriteLine("Option 'c'- Street validation selected.");

                        using (DataContext context = new DataContext())
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
                                var selection = Convert.ToInt32(Console.ReadLine());

                                context.Streets.Where(s => selection == s.Id).ToList().ForEach(x => x.IsActive = true);
                                context.SaveChanges();

                                Console.WriteLine($"The street with ID: {selection} validated successfully.");
                                
                            }
                            catch (Exception e)
                            {

                                Console.WriteLine(e.Message);
                            }
                        };

                        Console.WriteLine("\n\rPress the Enter key to continue");
                        readResult = Console.ReadLine();
                        break;
                }

            } while (menuSelection != "exit");

            break;
        case "2":

            do
            {
                Console.Clear();
                Console.WriteLine("Option 2 selected - Parking Slots Management.");
                Console.WriteLine("Your options for slots management are listed below:\n");

                Console.WriteLine(" a. Add a parking slot to a street.");
                Console.WriteLine(" b. Remove a parking slot from a street.");
                Console.WriteLine(" c. Close a parking slot(make it InActive).");
                Console.WriteLine(" d. Validate a parking slot(make it Active).\n");

                Console.WriteLine("Enter your selection letter (or type Exit to exit the application)");

                readResult = Console.ReadLine();
                if (readResult != null)
                    menuSelection = readResult.ToLower();

                switch (menuSelection)
                {
                    case "a":
                        Console.Clear();
                        Console.WriteLine("Option 'a' selected - Add a new parking slot to a specific street\n");
                        Console.WriteLine("In order to add a parking slot to a street, first you need to find the desired street: ");

                        using (DataContext context = new DataContext())
                        {
                            try
                            {
                                Console.WriteLine("Please specify a few characters of the street name you want to add the slot to: ");
                                string name = Console.ReadLine();

                                var searchResult = context.Streets.Where(x => x.Name.Contains(name) && x.IsActive == true).ToList();

                                if (searchResult.Count() > 0)
                                {
                                    Console.WriteLine("Found the below streets: \n");
                                    Console.WriteLine("ID:\t Name:");
                                    foreach (var street in searchResult)
                                    {
                                        Console.WriteLine($"{street.Id}\t {street.Name}");
                                    }

                                    Console.WriteLine("Please select in which street you want to add a slot(please input ID)?");
                                    var selection = Convert.ToInt32(Console.ReadLine());

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

                            Console.WriteLine("\n\rPress the Enter key to continue");
                        readResult = Console.ReadLine();
                        break;
                    case "b":
                        Console.Clear();
                        Console.WriteLine("Option 'b' selected - Remove a parking slot from a specific street\n");
                        Console.WriteLine("Below you can view all of the Slot ID-s and the streets that belong to: \n");
                        using (DataContext context = new DataContext())
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
                                var selection = Convert.ToInt32(Console.ReadLine());

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

                            Console.WriteLine("\n\rPress the Enter key to continue");
                        readResult = Console.ReadLine();
                        break;
                    case "c":
                        Console.Clear();
                        Console.WriteLine("Option 'c' selected - Close a parking Slot.");
                        Console.WriteLine("Below you can view all of the Slot ID-s and the streets that belong to: \n");
                        using (DataContext context = new DataContext())
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

                                Console.WriteLine("Please specify the slot ID you wish to close(make it InActive): ");
                                var selection = Convert.ToInt32(Console.ReadLine());

                                context.Slots.Where(s => selection == s.Id).ToList().ForEach(x => x.IsActive = false);
                                context.SaveChanges();

                                Console.WriteLine($"The slot with ID: {selection} closed successfully.");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        };

                        Console.WriteLine("\n\rPress the Enter key to continue");
                        readResult = Console.ReadLine();
                        break;
                    case "d":
                        Console.Clear();
                        Console.WriteLine("Option 'd' selected - Validate a parking slot");
                        Console.WriteLine("Below you can view all of the Slot ID-s and it's corresponding street that can be validated: \n");
                        using (DataContext context = new DataContext())
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
                                var selection = Convert.ToInt32(Console.ReadLine());

                                context.Slots.Where(s => selection == s.Id).ToList().ForEach(x => x.IsActive = true);
                                context.SaveChanges();

                                Console.WriteLine($"The slot with ID: {selection} activated successfully.");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        };

                        Console.WriteLine("\n\rPress the Enter key to continue");
                        readResult = Console.ReadLine();
                        break;
                }

            } while (menuSelection != "exit");

            Console.WriteLine("\n\rPress the Enter key to continue");
            readResult = Console.ReadLine();
            break;
        case "3":

            do
            {
                Console.Clear();
                Console.WriteLine("Option 3 selected - Parking.");
                Console.WriteLine("Your options for parking a car are listed below:\n");
                Console.WriteLine(" a. Park");
                Console.WriteLine(" b. Unpark\n");
                Console.WriteLine("Enter your selection letter (or type Exit to exit the application)");

                readResult = Console.ReadLine();
                if (readResult != null)
                    menuSelection = readResult.ToLower();

                switch (menuSelection)
                {
                    case "a":
                        Console.Clear();
                        Console.WriteLine("Option 'a' selected - Park");
                        Console.WriteLine("Below you can view a list of streets with available parking slots: ");
                        readResult = Convert.ToString(Console.ReadLine());

                        using (DataContext context = new DataContext())
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


                                Console.WriteLine("Slot ID:\tOccupied:\tStreet Name:");
                                foreach (var item in result)
                                {
                                    Console.WriteLine($"{item.Id}\t\t{item.IsOccupied}\t\t{item.Name}");
                                }

                                Console.WriteLine("Please specifi in which of the listed streets you wish to park by specifying the ID of the slot: ");
                                var selection = Convert.ToInt32(Console.ReadLine());

                                context.Slots.Where(s => selection == s.Id).ToList().ForEach(x => x.IsOccupied = true);
                                context.SaveChanges();

                                Console.WriteLine($"The parking slot with the ID: {selection} occupied successfully.");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        };

                        Console.WriteLine("\n\rPress the Enter key to continue");
                        readResult = Console.ReadLine();
                        break;
                    case "b":
                        Console.Clear();
                        Console.WriteLine("Option 'b' selected - UnPark");
                        Console.WriteLine("Below you can view a list of the occupied slots:\n");

                        using (DataContext context = new DataContext())
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
                                var selection = Convert.ToInt32(Console.ReadLine());

                                context.Slots.Where(s => selection == s.Id).ToList().ForEach(x => x.IsOccupied = false);
                                context.SaveChanges();

                                Console.WriteLine($"The parking slot with the ID: {selection} freed successfully.");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        };

                        Console.WriteLine("\n\rPress the Enter key to continue");
                        readResult = Console.ReadLine();
                        break;
                }

            } while (menuSelection != "exit");

            Console.WriteLine("\n\rPress the Enter key to continue");
            readResult = Console.ReadLine();
            break;
    }

} while (menuSelection != "exit");
