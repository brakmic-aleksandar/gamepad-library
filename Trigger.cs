using System;
using Gamepad.Xinput;

namespace Gamepad
{
    /// <summary>
    ///     Represents trigger controller button.
    /// </summary>
    public class Trigger : Button
    {
        private ushort _value;

        /// <summary>
        ///     Creates instance of trigger button.
        /// </summary>
        /// <param name="name">Name of button being constructed.</param>
        internal Trigger(ButtonNames name) : base(name)
        {
        }

        /// <summary>
        ///     Gets current trigger value.
        /// </summary>
        public ushort Value
        {
            get => _value;
            private set
            {
                if (value < DeadZone)
                    value = 0;

                if (_value != value)
                {
                    _value = value;
                    OnValueChanged?.Invoke(this, new EventArgs());
                }
                //its outside value check, so we can trigger OnWhilePressed event.
                IsPressed = _value > 0;
            }
        }

        /// <summary>
        ///     Gets or sets deadzone.
        /// </summary>
        public float DeadZone { get; set; }

        /// <summary>
        ///     Event that triggers when trigger value changes.
        /// </summary>
        public event EventHandler OnValueChanged;

        /// <inheritdoc />
        internal override void UpdateState(State state)
        {
            switch (Name)
            {
                case ButtonNames.LeftTrigger:
                    Value = state.GamepadState.LeftTriggerValue;
                    break;
                case ButtonNames.RightTrigger:
                    Value = state.GamepadState.RightTriggerValue;
                    break;
            }
        }
    }
}