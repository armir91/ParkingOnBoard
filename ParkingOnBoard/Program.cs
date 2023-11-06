using ParkingOnBoard;
using ParkingOnBoard.ApplicationMenu;
using ParkingOnBoard.Context;
using ParkingOnBoard.Operation.ParkingOperation;
using ParkingOnBoard.Operation.SlotOperation;
using ParkingOnBoard.Operations.StreetOperation;



//automating migrations + seed initial data in the DB
AutoMigrateConfiguration.AutoMigrateAndSeed();

// variables that support data entry
string menuSelection;
string readResult;

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

                        break;

                    case "b":

                        StreetOperationClose.CloseStreet();
                        EndOfApplicationMessage.EndMessage();

                        break;

                    case "c":

                        StreetOperationValidate.ValidateStreet();
                        EndOfApplicationMessage.EndMessage();

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

                        break;
                    case "b":

                        SlotOperationRemove.RemoveSlot();
                        EndOfApplicationMessage.EndMessage();

                        break;
                    case "c":

                        SlotOperationClose.CloseSlot();
                        EndOfApplicationMessage.EndMessage();

                        break;
                    case "d":

                        SlotOperationValidate.ValidateSlot();
                        EndOfApplicationMessage.EndMessage();

                        break;
                }

            } while (menuSelection != "exit");

            EndOfApplicationMessage.EndMessage();

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

                        ParkingMenu.ParkMenu();
                        ParkingOperationPark.Park();
                        EndOfApplicationMessage.EndMessage();
                        break;

                    case "b":

                        ParkingOperationUnpark.Unpark();
                        EndOfApplicationMessage.EndMessage();
                        break;
                }
            } while (menuSelection != "exit");

            EndOfApplicationMessage.EndMessage();
            break;
    }
} while (menuSelection != "exit");