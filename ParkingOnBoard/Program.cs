
using Microsoft.EntityFrameworkCore;
using ParkingOnBoard.Context;
using ParkingOnBoard.Entities;
using System.Linq;
using System.Text.RegularExpressions;




//apply migrations and seed initial data in the database
try
{
    using DataContext context = new();
    context.Database.Migrate();
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

                        Console.WriteLine("\n\rPress the Enter key to continue");
                        readResult = Console.ReadLine();
                        break;

                    case "b":
                        Console.Clear();
                        Console.WriteLine("Option 'b' - Close a street selected:");
                        
                        using (DataContext context = new())
                        {
                            try
                            {
                                string name;
                                bool hasSpecialCharacters = false;
                                do
                                {

                                    Console.WriteLine("Please specify a few characters of the street you want to close in order to first find it(or click enter to list all the streets): ");
                                    name = Console.ReadLine();

                                    // Define the regular expression pattern to allow only alphanumeric characters
                                    Regex regex = new("^[a-zA-Z0-9 ]*$");

                                    if (regex.IsMatch(name))
                                    {
                                        // Your logic if the input is valid
                                        Console.WriteLine("Searching for matches: " + name + "\n");
                                        hasSpecialCharacters = false;
                                    }
                                    else
                                    {
                                        // Your logic if the input contains special characters
                                        Console.WriteLine("Input contains special characters. Please try again.");
                                        hasSpecialCharacters = true;
                                    }
                                } while (hasSpecialCharacters);

                            var searchResult = context.Streets.Where(x => x.Name.Contains(name) && x.IsActive == true).ToList();

                                
                                if (searchResult.Count > 0)
                                {
                                    Console.WriteLine("Found the below streets: ");
                                    Console.WriteLine("ID:\t Name:");
                                    foreach (var street in searchResult)
                                    {
                                        Console.WriteLine($"{street.Id}\t {street.Name}");
                                    }

                                    Console.WriteLine("Please select which of the above results you want to close(input ID)?");
                                    int selection;
                                    while (!int.TryParse(Console.ReadLine(), out selection))
                                    {
                                        
                                        Console.WriteLine("You entered an invalid value.");
                                        Console.WriteLine("Retry again!");
                                    }

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

                        using (DataContext context = new())
                        {
                            try
                            {

                                string name;
                                bool hasSpecialCharacters = false;
                                do
                                {

                                    Console.WriteLine("Please specify a few characters of the street you want to close in order to first find it(or click enter to list all the streets): ");
                                    name = Console.ReadLine();

                                    // Define the regular expression pattern to allow only alphanumeric characters
                                    Regex regex = new("^[a-zA-Z0-9 ]*$");

                                    if (regex.IsMatch(name))
                                    {
                                        // Your logic if the input is valid
                                        Console.WriteLine("Searching for matches: " + name + "\n");
                                        hasSpecialCharacters = false;
                                    }
                                    else
                                    {
                                        // Your logic if the input contains special characters
                                        Console.WriteLine("Input contains special characters. Please try again.\n");
                                        hasSpecialCharacters = true;
                                    }
                                } while (hasSpecialCharacters);

                                var searchResult = context.Streets.Where(x => x.Name.Contains(name) && x.IsActive == true).ToList();

                                if (searchResult.Count > 0)
                                {
                                    Console.WriteLine("Found the below streets: \n");
                                    Console.WriteLine("ID:\t Name:");
                                    foreach (var street in searchResult)
                                    {
                                        Console.WriteLine($"{street.Id}\t {street.Name}");
                                    }

                                    Console.WriteLine("Please select in which street you want to add a slot(please input ID)?");
                                    int selection;
                                    while (!int.TryParse(Console.ReadLine(), out selection))
                                    {

                                        Console.WriteLine("You entered an invalid value.");
                                        Console.WriteLine("Retry again!");
                                    }

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
                                int selection;
                                while (!int.TryParse(Console.ReadLine(), out selection))
                                {

                                    Console.WriteLine("You entered an invalid value.");
                                    Console.WriteLine("Retry again!");
                                }


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

                        Console.WriteLine("\n\rPress the Enter key to continue");
                        readResult = Console.ReadLine();
                        break;
                    case "d":
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

                                int selection;
                                while (!int.TryParse(Console.ReadLine(), out selection))
                                {

                                    Console.WriteLine("You entered an invalid value.");
                                    Console.WriteLine("Retry again!");
                                }

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

                        Console.WriteLine("\n\rPress the Enter key to continue");
                        readResult = Console.ReadLine();
                        break;
                    case "b":
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
                                int selection;
                                while (!int.TryParse(Console.ReadLine(), out selection))
                                {

                                    Console.WriteLine("You entered an invalid value.");
                                    Console.WriteLine("Retry again!");
                                }

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
