using System.Runtime.InteropServices;

namespace Gamepad.Xinput
{
    /// <summary>
    ///     Describes the current state of the gamepad.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    internal struct GamepadState
    {
        /// <summary>
        ///     Bitmask of the device digital buttons, as follows. A set bit indicates that the corresponding button is pressed.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(0)] public ButtonNames PressedButtons;

        /// <summary>
        ///     The current value of the left trigger analog control. The value is between 0 and 255.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [FieldOffset(2)] public byte LeftTriggerValue;

        /// <summary>
        ///     The current value of the right trigger analog control. The value is between 0 and 255.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [FieldOffset(3)] public byte RightTriggerValue;

        /// <summary>
        ///     Left thumbstick x-axis value. The value is between -32768 and 32767.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(4)] public short LeftThumbStickXPosition;

        /// <summary>
        ///     Left thumbstick y-axis value. The value is between -32768 and 32767.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(6)] public short LeftThumbStickYPosition;

        /// <summary>
        ///     Right thumbstick x-axis value. The value is between -32768 and 32767.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(8)] public short RightThumbStickXPosition;

        /// <summary>
        ///     Right thumbstick y-axis value. The value is between -32768 and 32767.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(10)] public short RightThumbStickYPosition;
    }
}