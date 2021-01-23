using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Problems.MontyHall.Enums;
using Algorithms.Problems.MontyHall.Interfaces;

namespace Algorithms.Problems.MontyHall.Classes
{
    public class Game : IGame
    {
        private List<IDoor> doors = new List<IDoor>
        {
            new Door { DoorState = State.Stateless, Prize = "Bike" },
            new Door { DoorState = State.Stateless, Prize = "Bike" },
            new Door { DoorState = State.Stateless, Prize = "Car" },
        };

        public Game(int gameCount) => GameCount = gameCount;

        public int GameCount { get; }

        public int WinCount { get; set; }

        public double WinRate => (double)WinCount / GameCount * 100;

        public IDoor UserChoosesDoor(int doorIndex)
        {
            if (doorIndex < 0 || doorIndex > 2)
            {
                throw new InvalidOperationException($"Door {doorIndex} doesn't exist.");
            }

            if (doors[doorIndex].DoorState == State.Opened)
            {
                throw new InvalidOperationException("Door is already opened by speaker.");
            }

            doors[doorIndex].DoorState = State.Chosen;
            return doors[doorIndex];
        }

        public IDoor UserChoosesDoor(Func<IDoor, bool> predicate)
        {
            var door = doors.First(predicate);
            door.DoorState = State.Chosen;
            return door;
        }

        public int IndexOfDoor(IDoor door) => doors.IndexOf(door);

        public IDoor SpeakerOpensDoor()
        {
            var door = doors.First(x => x.Prize == "Bike" && x.DoorState != State.Chosen);
            door.DoorState = State.Opened;
            return door;
        }

        public void ResetGame()
        {
            doors.ForEach(x => x.DoorState = State.Stateless);
            doors = doors.OrderBy(x => new Random().Next()).ToList();
        }
    }
}
