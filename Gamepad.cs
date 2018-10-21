using System;
using System.Collections.Generic;
using Gamepad.Xinput;

namespace Gamepad
{
    /// <summary>
    ///     Represents gamepad.
    /// </summary>
    public class Gamepad
    {
        private bool _isActive;

        /// <summary>
        ///     Creates new instance of gamepad.
        /// </summary>
        /// <param name="id">GamepadState id, values are between 0-3.</param>
        internal Gamepad(ushort id)
        {
            if (id > 3)
                throw new ArgumentException("Id value cannot be higher than 3.");
            Id = id;
            XInputLib.Enable(true);
            UpdateState();
            //Events are being registered after initial state is set, so they aren't triggered by initial state setup.
            RegisterButtonEvents();
        }

        /// <summary>
        ///     Gets gamepad id.
        /// </summary>
        public ushort Id { get; }

        /// <summary>
        ///     Gets whether gamepad is active.
        /// </summary>
        public bool IsActive
        {
            get { return _isActive; }
            private set
            {
                if (value != _isActive)
                {
                    _isActive = value;
                    if (_isActive)
                        OnActivate?.Invoke(this, new EventArgs());
                    else
                        OnDeactivate?.Invoke(this, new EventArgs());
                }
            }
        }

        /// <summary>
        ///     Gets all gamepad buttons.
        /// </summary>
        public IReadOnlyDictionary<ButtonNames, Button> Buttons { get; } = new Dictionary<ButtonNames, Button>
        {
            {ButtonNames.X, new Button(ButtonNames.X)},
            {ButtonNames.Y, new Button(ButtonNames.Y)},
            {ButtonNames.A, new Button(ButtonNames.A)},
            {ButtonNames.B, new Button(ButtonNames.B)},
            {ButtonNames.Start, new Button(ButtonNames.Start)},
            {ButtonNames.Back, new Button(ButtonNames.Back)},
            {ButtonNames.DpadUp, new Button(ButtonNames.DpadUp)},
            {ButtonNames.DpadDown, new Button(ButtonNames.DpadDown)},
            {ButtonNames.DpadLeft, new Button(ButtonNames.DpadLeft)},
            {ButtonNames.DpadRight, new Button(ButtonNames.DpadRight)},
            {ButtonNames.LeftShoulder, new Button(ButtonNames.LeftShoulder)},
            {ButtonNames.RightShoulder, new Button(ButtonNames.RightShoulder)},
            {ButtonNames.LeftTrigger, new Trigger(ButtonNames.LeftTrigger) {DeadZone = Deadzones.Trigger}},
            {ButtonNames.RightTrigger, new Trigger(ButtonNames.RightTrigger) {DeadZone = Deadzones.Trigger}},
            {ButtonNames.LeftThumb, new ThumbStick(ButtonNames.LeftThumb) {DeadZone = Deadzones.LeftThumb}},
            {ButtonNames.RightTumb, new ThumbStick(ButtonNames.RightTumb) {DeadZone = Deadzones.RightTHumb}}
        };

        /// <summary>
        ///     Gets 'X' gamepad button.
        /// </summary>
        public Button X => Buttons[ButtonNames.X];

        /// <summary>
        ///     Gets 'Y' gamepad button.
        /// </summary>
        public Button Y => Buttons[ButtonNames.Y];

        /// <summary>
        ///     Gets 'A' gamepad button.
        /// </summary>
        public Button A => Buttons[ButtonNames.A];

        /// <summary>
        ///     Gets 'B' gamepad button.
        /// </summary>
        public Button B => Buttons[ButtonNames.B];

        /// <summary>
        ///     Gets 'Start' gamepad button.
        /// </summary>
        public Button Start => Buttons[ButtonNames.Start];

        /// <summary>
        ///     Gets 'Back' gamepad button.
        /// </summary>
        public Button Back => Buttons[ButtonNames.Back];

        /// <summary>
        ///     Gets 'Dpad Up' gamepad button.
        /// </summary>
        public Button DpadUp => Buttons[ButtonNames.DpadUp];

        /// <summary>
        ///     Gets 'Dpad down' gamepad button.
        /// </summary>
        public Button DpadDown => Buttons[ButtonNames.DpadDown];

        /// <summary>
        ///     Gets 'Dpad Left' gamepad button.
        /// </summary>
        public Button DpadLeft => Buttons[ButtonNames.DpadLeft];

        /// <summary>
        ///     Gets 'Dpad Right' gamepad button.
        /// </summary>
        public Button DpadRight => Buttons[ButtonNames.DpadRight];

        /// <summary>
        ///     Gets 'Left shoulder' gamepad button.
        /// </summary>
        public Button LeftShoulder => Buttons[ButtonNames.LeftShoulder];

        /// <summary>
        ///     Gets 'RIght shoulder' gamepad button.
        /// </summary>
        public Button RightShoulder => Buttons[ButtonNames.RightShoulder];

        /// <summary>
        ///     Gets left gamepad trigger.
        /// </summary>
        public Trigger LeftTrigger => (Trigger) Buttons[ButtonNames.LeftTrigger];

        /// <summary>
        ///     Gets right gamepad trigger.
        /// </summary>
        public Trigger RightTrigger => (Trigger) Buttons[ButtonNames.RightTrigger];

        /// <summary>
        ///     Gets left gamepad thumbstick.
        /// </summary>
        public ThumbStick LeftThumbStick => (ThumbStick) Buttons[ButtonNames.LeftThumb];

        /// <summary>
        ///     Gets right gamepad thumbstick.
        /// </summary>
        public ThumbStick RightThumbStick => (ThumbStick) Buttons[ButtonNames.RightTumb];

        /// <summary>
        ///     Gets battery information.
        /// </summary>
        public BatteryInformation BatteryInformation
        {
            get
            {
                var batteryInformation = new BatteryInformation();
                XInputLib.GetBatteryInformation(Id, BatteryInformationDeviceType.Gamepad,
                    ref batteryInformation);
                return batteryInformation;
            }
        }

        /// <summary>
        ///     Event that triggers when gamepad activates.
        /// </summary>
        public event EventHandler OnActivate;

        /// <summary>
        ///     Event that triggers when gamepad deactivates.
        /// </summary>
        public event EventHandler OnDeactivate;

        /// <summary>
        ///     Event that triggers when one of buttons is pressed.
        /// </summary>
        public event EventHandler<ButtonPressedEventArgs> OnButtonPressed;

        /// <summary>
        ///     Event that triggers when one of buttons is released.
        /// </summary>
        public event EventHandler<ButtonReleasedEventArgs> OnButtonReleased;

        /// <summary>
        ///     Event that triggers when value of one of triggers changes.
        /// </summary>
        public event EventHandler<TriggerValueChangedEventArgs> OnTriggerValueChangedChanged;

        /// <summary>
        ///     Event that triggers when value of one of thumbsticks changes.
        /// </summary>
        public event EventHandler<ThumbStickPositionChangedEventArgs> OnThumbStickValueChanged;

        /// <summary>
        ///     Event that triggers continuously as long as one of buttons on controller is being pressed.
        /// </summary>
        public event EventHandler<WhileButtonPressedEventArgs> OnWhileButtonPressed;

        /// <summary>
        ///     Event that triggers constantly as long as one of thumbsticks is not in neutral position.
        /// </summary>
        public event EventHandler<WhileThumbStickPositionNotZeroEventArgs> OnWhileThumbStickPositionNotZero;

        /// <summary>
        ///     Updates gamepad state.
        /// </summary>
        public void UpdateState()
        {
            var state = new State();
            if (XInputLib.GetState(Id, ref state) == 0)
            {
                if (!IsActive)
                    IsActive = true;
                foreach (var button in Buttons.Values)
                    button.UpdateState(state);
            }
            else
            {
                if (IsActive)
                    IsActive = false;
            }
        }

        /// <summary>
        ///     Vibrates gamepad.
        /// </summary>
        /// <param name="leftMotorSpeed">
        ///     Speed of the left motor. Valid values are in the range 0 to 1. Zero signifies no motor use; 1 signifies
        ///     100 percent motor use.
        /// </param>
        /// <param name="rightMotorSpeed">
        ///     Speed of the Right motor. Valid values are in the range 0 to 65,535. Zero signifies no motor use; 65,535 signifies
        ///     100 percent motor use.
        /// </param>
        public void Vibrate(float leftMotorSpeed, float rightMotorSpeed)
        {
            if (leftMotorSpeed > 1 || rightMotorSpeed > 1)
                throw new ArgumentException("Motor speeds must be between 0 and 1.");

            var vibration = new Vibration
            {
                LeftMotorSpeed = (ushort) (leftMotorSpeed * 65535),
                RightMotorSpeed = (ushort) (rightMotorSpeed * 65535)
            };
            XInputLib.SetState(Id, ref vibration);
        }

        /// <summary>
        ///     Registers all button events.
        /// </summary>
        private void RegisterButtonEvents()
        {
            foreach (var button in Buttons.Values)
            {
                button.OnPressed += OnButtonPressedInternal;
                button.OnReleased += OnButtonReleasedInternal;
                button.OnWhilePressed += OnWhileButtonPressedInternal;
                if (button.GetType() == typeof(Trigger))
                {
                    ((Trigger) button).OnValueChanged += OnTriggerValueChangedInternal;
                }
                else if (button.GetType() == typeof(ThumbStick))
                {
                    ((ThumbStick) button).OnPositionChanged += OnThumbStickPositionChangedInternal;
                    ((ThumbStick) button).OnWhilePositionNotZero += OnWhilePositionNotZeroInternal;
                }
            }
        }

        /// <summary>
        ///     Dispatches Button.OnPressed event for all buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnButtonPressedInternal(object sender, EventArgs args)
        {
            var button = (Button) sender;
            OnButtonPressed?.Invoke(this, new ButtonPressedEventArgs(button));
        }

        /// <summary>
        ///     Dispatches Button.OnReleased event for all buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnButtonReleasedInternal(object sender, EventArgs args)
        {
            var button = (Button) sender;
            OnButtonReleased?.Invoke(this, new ButtonReleasedEventArgs(button));
        }

        /// <summary>
        ///     Dispatches Trigger.OnValueChanged event for all buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnTriggerValueChangedInternal(object sender, EventArgs args)
        {
            var button = (Trigger) sender;
            OnTriggerValueChangedChanged?.Invoke(this, new TriggerValueChangedEventArgs(button));
        }

        /// <summary>
        ///     Dispatches ThumbStick.OnPositionChanged event for all buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnThumbStickPositionChangedInternal(object sender, EventArgs args)
        {
            var button = (ThumbStick) sender;
            OnThumbStickValueChanged?.Invoke(this, new ThumbStickPositionChangedEventArgs(button));
        }

        /// <summary>
        ///     Dispatches Button.OnWhilePressed event for all buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnWhilePositionNotZeroInternal(object sender, EventArgs args)
        {
            var button = (ThumbStick) sender;
            OnWhileThumbStickPositionNotZero?.Invoke(this, new WhileThumbStickPositionNotZeroEventArgs(button));
        }

        /// <summary>
        ///     Dispatches Button.OnWhilePressed event for all buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnWhileButtonPressedInternal(object sender, EventArgs args)
        {
            var button = (Button) sender;
            OnWhileButtonPressed?.Invoke(this, new WhileButtonPressedEventArgs(button));
        }
    }

    public class ButtonPressedEventArgs : EventArgs
    {
        public ButtonPressedEventArgs(Button button)
        {
            Button = button;
        }

        public Button Button { get; }
    }

    public class ButtonReleasedEventArgs : EventArgs
    {
        public ButtonReleasedEventArgs(Button button)
        {
            Button = button;
        }

        public Button Button { get; }
    }

    public class ThumbStickPositionChangedEventArgs : EventArgs
    {
        public ThumbStickPositionChangedEventArgs(ThumbStick thumbStick)
        {
            ThumbStick = thumbStick;
        }

        public ThumbStick ThumbStick { get; }
    }

    public class TriggerValueChangedEventArgs : EventArgs
    {
        public TriggerValueChangedEventArgs(Trigger trigger)
        {
            Trigger = trigger;
        }

        public Trigger Trigger { get; }
    }

    public class WhileButtonPressedEventArgs : EventArgs
    {
        public WhileButtonPressedEventArgs(Button button)
        {
            Button = button;
        }

        public Button Button { get; }
    }

    public class WhileThumbStickPositionNotZeroEventArgs : EventArgs
    {
        public WhileThumbStickPositionNotZeroEventArgs(ThumbStick thumbStick)
        {
            ThumbStick = thumbStick;
        }

        public ThumbStick ThumbStick { get; }
    }
}