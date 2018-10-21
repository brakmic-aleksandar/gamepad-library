using System.Runtime.InteropServices;

namespace Gamepad.Xinput
{
    /// <summary>
    ///     Describes the capabilities of a connected gamepad.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    internal struct Capabilities
    {
        /// <summary>
        ///     Gamepad type.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [FieldOffset(0)] public byte Type;

        /// <summary>
        ///     Subtype of the gamepad.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [FieldOffset(1)] public byte SubType;

        /// <summary>
        ///     GamepadState flags.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(2)] public ushort Flags;

        /// <summary>
        ///     GamepadState state.
        /// </summary>
        [FieldOffset(4)] public GamepadState GamepadState;

        /// <summary>
        ///     GamepadState vibration info.
        /// </summary>
        [FieldOffset(16)] public Vibration Vibration;
    }
}