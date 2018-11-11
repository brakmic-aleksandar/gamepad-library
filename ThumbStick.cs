using System;
using System.Drawing;
using Gamepad.Xinput;

namespace Gamepad
{
    /// <summary>
    ///     Represents thumbstick controller button.
    /// </summary>
    public class ThumbStick : Button
    {
        /// <summary>
        ///     Creates instance of thumbstick button.
        /// </summary>
        /// <param name="name">Name of button being constructed.</param>
        internal ThumbStick(ButtonNames name) : base(name)
        {
        }

        /// <summary>
        ///     Gets normalized position of thumbstick.
        /// </summary>
        public PointF Position { get; private set; }

        /// <summary>
        ///     Gets X of normalized position of thumbstick.
        /// </summary>
        public float X => Position.X;

        /// <summary>
        ///     Gets Y of normalized position of thumbstick.
        /// </summary>
        public float Y => Position.Y;

        /// <summary>
        ///     Gets magnitude of current thumbstick position.
        /// </summary>
        public float Magnitude { get; private set; }

        /// <summary>
        ///     Gets or sets deadzone.
        /// </summary>
        public float DeadZone { get; set; } = 0;

        /// <summary>
        ///     Event that triggers when thumbstick position changes.
        /// </summary>
        public event EventHandler OnPositionChanged;

        /// <summary>
        ///     Event that is constantly triggering while position is not zero.
        /// </summary>
        public event EventHandler OnWhilePositionNotZero;

        /// <summary>
        ///     Updates position values of thumbstick.
        /// </summary>
        /// <param name="x">New X coordinate.</param>
        /// <param name="y">NEw y coordinate.</param>
        private void UpdatePosition(float x, float y)
        {
            var magnitude = (float) Math.Sqrt(x * x + y * y);
            PointF newPosition;
            if (magnitude > DeadZone)
            {
                newPosition = new PointF(x / magnitude, y / magnitude);
            }
            else
            {
                newPosition = new PointF(0, 0);
                magnitude = 0;
            }
            if (!Position.Equals(newPosition) || magnitude != Magnitude)
            {
                Position = newPosition;
                Magnitude = magnitude;
                OnPositionChanged?.Invoke(this, new EventArgs());
            }
            if (Magnitude != 0)
                OnWhilePositionNotZero?.Invoke(this, new EventArgs());
        }

        /// <inheritdoc />
        internal override void UpdateState(State state)
        {
            base.UpdateState(state);
            switch (Name)
            {
                case ButtonNames.LeftThumb:
                    UpdatePosition(state.GamepadState.LeftThumbStickXPosition,
                        state.GamepadState.LeftThumbStickYPosition);
                    break;
                case ButtonNames.RightThumb:
                    UpdatePosition(state.GamepadState.RightThumbStickXPosition,
                        state.GamepadState.RightThumbStickYPosition);
                    break;
            }
        }
    }
}