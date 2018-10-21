using System.Runtime.InteropServices;

namespace Gamepad.Xinput
{
    /// <summary>
    ///     Represents the state of a gamepad.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    internal struct State
    {
        /// <summary>
        ///     State packet number. The packet number indicates whether there have been any changes in the state of the
        ///     controller.
        /// </summary>
        [FieldOffset(0)] public uint PacketNumber;

        /// <summary>
        ///     GamepadState structure containing the current state of gamepad.
        /// </summary>
        [FieldOffset(4)] public GamepadState GamepadState;
    }
}