namespace ParkingOnBoard.Entities;

public class Slot
{
    public int Id { get; set;}
    public bool IsDeleted { get; set; } = false;
    public bool IsActive { get; set; } = false;
    public bool IsOccupied { get; set; } = false;
    public int StreetId { get; set;}
    public Street Street { get; set; }
}
