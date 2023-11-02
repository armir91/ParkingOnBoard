namespace ParkingOnBoard.Entities;

public class Street
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Sides { get; set; }
    public bool IsActive { get; set; } = false;
    public int TotalSlots { get; set; }

    public ICollection<Slot> Slots { get; set; }
}
