using Algorithms.Problems.MontyHall.Enums;

namespace Algorithms.Problems.MontyHall.Interfaces
{
    public interface IDoor
    {
        State DoorState { get; set; }

        string? Prize { get; set; }
    }
}
