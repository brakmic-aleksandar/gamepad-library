using System.Runtime.InteropServices;

namespace Gamepad.Xinput
{
    /// <summary>
    ///     Contains information on battery type and charge state.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct BatteryInformation
    {
        /// <summary>
        ///     GamepadState battery type.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [FieldOffset(0)] public BatteryType BatteryType;

        /// <summary>
        ///     The charge state of the battery. This value is only valid for wireless devices with a known battery type.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [FieldOffset(1)] public BatteryLevel BatteryLevel;
    }
}