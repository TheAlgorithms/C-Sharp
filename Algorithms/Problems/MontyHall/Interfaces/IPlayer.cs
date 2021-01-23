namespace Algorithms.Problems.MontyHall.Interfaces
{
    public interface IPlayer
    {
        IGame? Game { get; }

        void ManualPlay();

        void AutoPlay();
    }
}
