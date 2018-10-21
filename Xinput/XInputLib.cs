using System.Runtime.InteropServices;

namespace Gamepad.Xinput
{
    /// <summary>
    ///     Import of xinput1_4.dll library functions.
    /// </summary>
    internal static class XInputLib
    {
        /// <summary>
        ///     Retrieves the current state of the specified gamepad.
        /// </summary>
        /// <param name="gamepadId">Index of the user's gamepad. Can be a value from 0 to 3.</param>
        /// <param name="state">Pointer to an State structure that receives the current state of the gamepad.</param>
        /// <returns>
        ///     If the function succeeds, the return value is ERROR_SUCCESS.
        ///     If the gamepad is not connected, the return value is ERROR_DEVICE_NOT_CONNECTED.
        ///     If the function fails, the return value is an error code defined in Winerror.h. The function does not use
        ///     SetLastError to set the calling thread's last-error code.
        /// </returns>
        [DllImport("xinput1_4.dll", EntryPoint = "XInputGetState")]
        public static extern uint GetState(uint gamepadId, ref State state);

        /// <summary>
        ///     Sends data to a connected gamepad. This function is used to activate the vibration function of a gamepad.
        /// </summary>
        /// <param name="gamepadId">Index of the user's gamepad. Can be a value from 0 to 3. </param>
        /// <param name="vibration">Pointer to an Vibration structure containing the vibration information to send to the gamepad.</param>
        /// <returns>
        ///     If the function succeeds, the return value is ERROR_SUCCESS.
        ///     If the gamepad is not connected, the return value is ERROR_DEVICE_NOT_CONNECTED.
        ///     If the function fails, the return value is an error code defined in WinError.h. The function does not use
        ///     SetLastError to set the calling thread's last-error code.
        /// </returns>
        [DllImport("xinput1_4.dll", EntryPoint = "XInputGetState")]
        public static extern uint SetState(uint gamepadId, ref Vibration vibration);

        /// <summary>
        ///     Retrieves the battery type and charge status of a wireless gamepad.
        /// </summary>
        /// <param name="gamepadId">
        ///     Index of the signed-in gamer associated with the device. Can be a value in the range 0 – 3.
        /// </param>
        /// <param name="devType">
        ///     Specifies which device associated with this user index should be queried. Must be
        ///     BATTERY_DEVTYPE_GAMEPAD or BATTERY_DEVTYPE_HEADSET.
        /// </param>
        /// <param name="batteryInformation">Pointer to an BatteryInformation structure that receives the battery information.</param>
        /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.</returns>
        [DllImport("xinput1_4.dll", EntryPoint = "XInputGetBatteryInformation")]
        public static extern uint GetBatteryInformation(uint gamepadId, BatteryInformationDeviceType devType,
            ref BatteryInformation batteryInformation);

        /// <summary>
        ///     Retrieves a gamepad input event.
        /// </summary>
        /// <param name="gamepadId">
        ///     Index of the signed-in gamer associated with the device.
        ///     Can be a value in the range 0 – 3, or 255 to fetch the next available input event from any user.
        /// </param>
        /// <param name="reserved">Reserved</param>
        /// <param name="keystroke">Pointer to an KeyStrokeInfo structure that receives an input event. </param>
        /// <returns></returns>
        [DllImport("xinput1_4.dll", EntryPoint = "XInputGetKeystroke")]
        public static extern uint GetKeystroke(uint gamepadId, uint reserved, ref KeyStrokeInfo keystroke);

        /// <summary>
        ///     Sets the reporting state of XInput.
        /// </summary>
        /// <param name="enable">
        ///     If enable is FALSE, XInput will only send neutral data in response to GetState (all buttons up, axes centered, and
        ///     triggers at 0).
        ///     SetState calls will be registered but not sent to the device. Sending any value other than FALSE will restore
        ///     reading and writing functionality to normal.
        /// </param>
        [DllImport("xinput1_4.dll", EntryPoint = "XInputEnable")]
        public static extern void Enable(bool enable);

        /// <summary>
        ///     Retrieves the sound rendering and sound capture audio device IDs that are associated with the headset connected to
        ///     the specified gamepad.
        /// </summary>
        /// <param name="gamepadId">Index of the gamer associated with the device.</param>
        /// <param name="pRenderDeviceId">Windows Core Audio device ID string for render (speakers).</param>
        /// <param name="pRenderCount">Size, in wide-chars, of the render device ID string buffer. </param>
        /// <param name="pCaptureDeviceId">Windows Core Audio device ID string for capture (microphone).</param>
        /// <param name="pCaptureCount">Size, in wide-chars, of capture device ID string buffer.</param>
        /// <returns>
        ///     If the function successfully retrieves the device IDs for render and capture, the return code is ERROR_SUCCESS.
        ///     If there is no headset connected to the gamepad, the function will also retrieve ERROR_SUCCESS with NULL as the
        ///     values for pRenderDeviceId and pCaptureDeviceId.
        ///     If the gamepad port device is not physically connected, the function will return ERROR_DEVICE_NOT_CONNECTED.
        ///     If the function fails, it will return a valid Win32 error code.
        /// </returns>
        [DllImport("xinput1_4.dll", EntryPoint = "XInputGetAudioDeviceIds")]
        public static extern uint GetAudioDeviceIds(uint gamepadId,
            [MarshalAs(UnmanagedType.LPWStr)] out string pRenderDeviceId, ref uint pRenderCount,
            [MarshalAs(UnmanagedType.LPWStr)] out string pCaptureDeviceId, ref uint pCaptureCount);

        /// <summary>
        ///     Retrieves the capabilities and features of a connected gamepad.
        /// </summary>
        /// <param name="gamepadId">Index of the gamepad. Can be a value in the range 0–3.</param>
        /// <param name="flags">
        ///     Input flags that identify the gamepad type.
        ///     If this value is 0, then the capabilities of all gamepads connected to the system are returned.
        /// </param>
        /// <param name="capabilities">Pointer to an Capabilities structure that receives the gamepad capabilities.</param>
        /// <returns>
        ///     If the function succeeds, the return value is ERROR_SUCCESS.
        ///     If the gamepad is not connected, the return value is ERROR_DEVICE_NOT_CONNECTED.
        ///     If the function fails, the return value is an error code defined in WinError.h. The function does not use
        ///     SetLastError to set the calling thread's last-error code.
        /// </returns>
        [DllImport("xinput1_4.dll", EntryPoint = "XInputGetCapabilities")]
        public static extern uint GetCapabilities(uint gamepadId, uint flags, ref Capabilities capabilities);
    }
}