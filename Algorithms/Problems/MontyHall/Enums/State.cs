namespace Algorithms.Problems.MontyHall.Enums
{
    /// <summary>
    /// Represents a states of the door in game.
    /// </summary>
    public enum State
    {
        /// <summary>
        /// State of a chosen door.
        /// </summary>
        Chosen = 0,

        /// <summary>
        /// State of an opened door.
        /// </summary>
        Opened = 1,

        /// <summary>
        /// Initial state of the door.
        /// </summary>
        Stateless = 2,
    }
}
