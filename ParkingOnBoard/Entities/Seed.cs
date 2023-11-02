using ParkingOnBoard.Context;

namespace ParkingOnBoard.Entities;

public class Seed
{
    public static async Task SeedData(DataContext context)
    {
        if (context.Streets.Any()) return;

        var streets = new List<Street>
        {
            new Street
            {
                Name = "Rruga Sitki Cico",
                Sides = 2,
                IsActive = true,
                TotalSlots = 6,
                Slots = new List<Slot>
                {
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    }
                 }
            },
            new Street
            {
                Name = "Rruga Pjeter Budi",
                Sides = 1,
                IsActive = true,
                TotalSlots = 7,
                Slots = new List<Slot>
                {
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    }
                }
            },
            new Street
            {   
                Name = "Rruga Petro Nini",
                Sides = 2,
                IsActive = true,
                TotalSlots = 12,
                Slots = new List<Slot>
                {
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    }
                }
            },
            new Street
            {
                Name = "Rruga Asim Vokshi",
                Sides = 1,
                IsActive = true,
                TotalSlots = 6,
                Slots = new List<Slot>
                {
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    }
                }
            },
            new Street
            {
                Name = "Rruga Kinostudio",
                Sides = 2,
                IsActive = true,
                TotalSlots = 8,
                Slots = new List<Slot>
                {
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    }
                }
            },
            new Street
            {
                Name = "Rruga Kombinati",
                Sides = 1,
                IsActive = true,
                TotalSlots = 10,
                Slots = new List<Slot>
                {
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    }
                }
            },
            new Street
            {
                Name = "Rruga Myslim Shyri",
                Sides = 1,
                IsActive = true,
                TotalSlots = 15,
                Slots = new List<Slot>
                {
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    }
                }
            },
            new Street
            {
                Name = "Rruga Islam Alla",
                Sides = 2,
                IsActive = true,
                TotalSlots = 20,
                Slots = new List<Slot>
                {
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    },
                    new Slot
                    {
                        IsActive = true
                    }
                }
            }
        };

        await context.AddRangeAsync(streets);
        await context.SaveChangesAsync();
    }
}
