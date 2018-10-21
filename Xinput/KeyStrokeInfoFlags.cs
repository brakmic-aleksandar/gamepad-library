namespace Gamepad.Xinput
{
    /// <summary>
    ///     Flags that indicate the keyboard state at the time of the input event.
    /// </summary>
    internal enum KeyStrokeInfoFlags : short
    {
        /// <summary>
        ///     The key was pressed.
        /// </summary>
        KeyDown = 0x01,

        /// <summary>
        ///     The key was released.
        /// </summary>
        KeyUp = 0x02,

        /// <summary>
        ///     A repeat of a held key.
        /// </summary>
        Repeat = 0x04
    }
}