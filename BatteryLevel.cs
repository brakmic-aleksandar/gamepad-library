namespace Gamepad
{
    /// <summary>
    ///     The charge state of the battery.
    /// </summary>
    public enum BatteryLevel : byte
    {
        /// <summary>
        ///     Battery is empty.
        /// </summary>
        Empty = 0x00,

        /// <summary>
        ///     Battery is low.
        /// </summary>
        Low = 0x01,

        /// <summary>
        ///     Battery is medium.
        /// </summary>
        Medium = 0x02,

        /// <summary>
        ///     Battery is full.
        /// </summary>
        Full = 0x03
    }
}