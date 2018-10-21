namespace Gamepad
{
    /// <summary>
    ///     The type of battery.
    /// </summary>
    public enum BatteryType : byte
    {
        /// <summary>
        ///     The device is not connected.
        /// </summary>
        Disconnected = 0x00,

        /// <summary>
        ///     The device is a wired device and does not have a battery.
        /// </summary>
        Wired = 0x01,

        /// <summary>
        ///     The device has an alkaline battery.
        /// </summary>
        Alkaline = 0x02,

        /// <summary>
        ///     The device has a nickel metal hydride battery.
        /// </summary>
        Nimh = 0x03,

        /// <summary>
        ///     The device has an unknown battery type.
        /// </summary>
        Unknown = 0xFF
    }
}