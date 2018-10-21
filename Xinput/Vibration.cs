using System.Runtime.InteropServices;

namespace Gamepad.Xinput
{
    /// <summary>
    ///     Specifies motor speed levels for the vibration function of a gamepad.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct Vibration
    {
        /// <summary>
        ///     Speed of the left motor. Valid values are in the range 0 to 65,535. Zero signifies no motor use; 65,535 signifies
        ///     100 percent motor use.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] public ushort LeftMotorSpeed;

        /// <summary>
        ///     Speed of the right motor. Valid values are in the range 0 to 65,535. Zero signifies no motor use; 65,535 signifies
        ///     100 percent motor use.
        /// </summary>
        [MarshalAs(UnmanagedType.I2)] public ushort RightMotorSpeed;
    }
}