using System;
using Gamepad.Xinput;

namespace Gamepad
{
    /// <summary>
    ///     Represents button on gamepad.
    /// </summary>
    public class Button
    {
        private bool _isPressed;

        /// <summary>
        ///     Constructs instance of button.
        /// </summary>
        /// <param name="name">Name of button being constructed.</param>
        internal Button(ButtonNames name)
        {
            Name = name;
        }

        /// <summary>
        ///     Gets current buton name.
        /// </summary>
        public ButtonNames Name { get; protected set; }

        /// <summary>
        ///     Gets whether button is currently pressed.
        /// </summary>
        public bool IsPressed
        {
            get => _isPressed;
            protected set
            {
                if (_isPressed != value)
                {
                    _isPressed = value;
                    if (_isPressed)
                        OnPressed?.Invoke(this, new EventArgs());
                    else
                        OnReleased?.Invoke(this, new EventArgs());
                }
                if (_isPressed)
                    OnWhilePressed?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        ///     Event that triggers when button is pressed.
        /// </summary>
        public event EventHandler OnPressed;

        /// <summary>
        ///     Event that triggers when button is released.
        /// </summary>
        public event EventHandler OnReleased;

        /// <summary>
        ///     Event that is continuously triggering while button is being pressed.
        /// </summary>
        public event EventHandler OnWhilePressed;

        /// <summary>
        ///     Updates button state from xinput librarys get state function response.
        /// </summary>
        /// <param name="state"></param>
        internal virtual void UpdateState(State state)
        {
            IsPressed = (state.GamepadState.PressedButtons & Name) == Name;
        }
    }
}