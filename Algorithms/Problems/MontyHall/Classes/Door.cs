using Algorithms.Problems.MontyHall.Enums;
using Algorithms.Problems.MontyHall.Interfaces;

namespace Algorithms.Problems.MontyHall.Classes
{
    public class Door : IDoor
    {
        public State DoorState { get; set; }

        public string? Prize { get; set; }
    }
}
