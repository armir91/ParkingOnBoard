using System.Text.RegularExpressions;

namespace ParkingOnBoard.Operation;

public static class ValidateRegEx
{
    public static bool ValidateName(string name)
    {
        Regex regex = new("^[a-zA-Z ]+$"); // This pattern allows only letters and spaces
        return regex.IsMatch(name);
    }
}
