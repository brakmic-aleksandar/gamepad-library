namespace Gamepad
{
    /// <summary>
    ///     Represents default deadzone values for buttons, the dead zone is "movement" values
    ///     reported by the controller even when the analog thumbsticks are untouched and centered.
    /// </summary>
    internal static class Deadzones
    {
        public const float LeftThumb = 7849;
        public const float RightTHumb = 8689;
        public const float Trigger = 30;
    }
}