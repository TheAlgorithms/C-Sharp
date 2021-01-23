using System;
using Algorithms.Problems.MontyHall.Enums;
using Algorithms.Problems.MontyHall.Interfaces;

namespace Algorithms.Problems.MontyHall.Classes
{
    /// <summary>
    /// Simulates Monty-Hall problem.
    ///
    /// See https://en.wikipedia.org/wiki/Monty_Hall_problem.
    /// </summary>
    public class Player : IPlayer
    {
        private readonly Random random;

        public Player(IGame game)
        {
            Game = game;
            random = new Random();
        }

        public IGame? Game { get; }

        public void ManualPlay()
        {
            if (Game != null)
            {
                Game.WinCount = 0;
                for (var i = 0; i < Game.GameCount; i++)
                {
                    Console.WriteLine($"Games played: {i} out of {Game.GameCount}. Wins: {Game.WinCount}");
                    Console.WriteLine("You have three doors, type number you choose (0,1,2): ");
                    var number = int.Parse(Console.ReadLine() !);
                    Console.WriteLine($"You have chosen: {number}");
                    Game.UserChoosesDoor(number);
                    var speakerOpensDoor = Game.SpeakerOpensDoor();
                    Console.WriteLine($"Speaker opens door number {Game.IndexOfDoor(speakerOpensDoor)} " +
                                      "and there is bike!");
                    Console.WriteLine($"Would you like to change you choose {number}?");
                    Console.WriteLine("If not, type same number.");
                    number = int.Parse(Console.ReadLine() !);
                    Console.WriteLine($"You have chosen: {number}");
                    var userDoor = Game.UserChoosesDoor(number);

                    if (userDoor.Prize == "Car")
                    {
                        Game.WinCount++;
                        Console.WriteLine($"Congrats, you won a car. Door {number} is correct.");
                    }
                    else
                    {
                        Console.WriteLine($"Unfortunately, there is a bike behind door {number}");
                    }

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    Game.ResetGame();
                }
            }

            if (Game != null)
            {
                Console.WriteLine($"Session is finished. You have played {Game.GameCount} games " +
                                  $" wins: {Game.WinCount}." +
                                  $"\nWin rate {Game.WinRate}.");
            }
        }

        public void AutoPlay()
        {
            if (Game == null)
            {
                return;
            }

            Game.WinCount = 0;
            for (var i = 0; i < Game.GameCount; i++)
            {
                var initialChoose = random.Next(0, 3);
                Game.UserChoosesDoor(initialChoose);
                Game.SpeakerOpensDoor();
                var chosenDoor = Game.UserChoosesDoor(x => x.DoorState == State.Stateless);
                if (chosenDoor.Prize == "Car")
                {
                    Game.WinCount++;
                }

                Game.ResetGame();
            }

            Console.WriteLine($"Session is finished. You have played {Game.GameCount} games " +
                              $" wins: {Game.WinCount}." +
                              $"\nWin rate {Game.WinRate}.");
        }
    }
}
