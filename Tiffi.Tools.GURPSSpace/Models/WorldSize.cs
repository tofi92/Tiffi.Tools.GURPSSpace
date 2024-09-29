namespace Tiffi.Tools.GURPSSpace.Models
{
    /// <summary>
    /// A rough interpretation how big a world is, not including gas giants
    /// </summary>
    public enum WorldSize
    {
        /// <summary>
        /// Small worlds like moons or very small planets (e.g. Mercury)
        /// </summary>
        Tiny = 0,

        /// <summary>
        /// Slightly bigger moons and planets, e.g. Titan or Mars
        /// </summary>
        Small = 1,

        /// <summary>
        /// Standard size worlds like Earth
        /// </summary>
        Standard = 2,

        /// <summary>
        /// Large worlds that have no counterpart in our solar system
        /// </summary>
        Large = 3,

        /// <summary>
        /// An asteroid belt
        /// </summary>
        None = -1
    }
}
