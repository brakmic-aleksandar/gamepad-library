using System.Runtime.InteropServices;

namespace Gamepad.Xinput
{
    /// <summary>
    ///     Specifies keystroke data returned by XInputLib.GetKeystroke.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    internal struct KeyStrokeInfo
    {
        /// <summary>
        ///     Virtual-key code of the key, button, or stick movement.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(0)] public ushort VirtualKey;

        /// <summary>
        ///     This member is unused and the value is zero.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(2)] public char Unicode;

        /// <summary>
        ///     Flags that indicate the keyboard state at the time of the input event.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(4)] public KeyStrokeInfoFlags Flags;

        /// <summary>
        ///     Index of the signed-in gamer associated with the device. Can be a value in the range 0–3.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] [FieldOffset(5)] public byte UserIndex;

        /// <summary>
        ///     HID code corresponding to the input. If there is no corresponding HID code, this value is zero.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [FieldOffset(6)] public byte HidCode;
    }
}