using Algorithms.Problems.MontyHall.Classes;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Problems.MontyHall
{
    [TestFixture]
    public class MontyHallTests
    {
        [Test]
        public void MontyHall_66Rate_By_BigNumbers_Theorem_Test()
        {
            var player = new Player(new Game(1_000_000));
            player.AutoPlay();
            
            if (player.Game != null)
            {
                var probability = (int)player.Game.WinRate;
                probability.Should().Be(66);
            }
        }
    }
}