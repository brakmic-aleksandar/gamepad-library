using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Gamepad
{
    /// <summary>
    ///     Controller for gamepads, listens to their events and dispatches them.
    ///     WARNING: Controller will spawn 4 threads while it's active.
    /// </summary>
    public class GamepadController
    {
        private bool _isEnabled;

        /// <summary>
        ///     Creates instance of gamepad controller.
        /// </summary>
        /// <param name="isEnabled">Indicates whether gamepad controller is immediately enabled.</param>
        public GamepadController(bool isEnabled = true)
        {
            IsEnabled = isEnabled;
            foreach (var gamepad in Gamepads)
            {
                gamepad.OnActivate += (sender, args) => OnGamepadActivate?.Invoke(sender, args);
                gamepad.OnDeactivate += (sender, args) => OnGamepadDeactivate?.Invoke(sender, args);
                gamepad.OnButtonPressed += (sender, args) =>  OnGamepadButtonPressed?.Invoke(sender, args);
                gamepad.OnButtonReleased += (sender, args) => OnGamepadButtonReleased?.Invoke(sender, args);
                gamepad.OnTriggerValueChangedChanged += (sender, args) => OnGamepadTriggerValueChangedChanged?.Invoke(sender, args);
                gamepad.OnThumbStickValueChanged += (sender, args) => OnGamepadThumbStickValueChanged?.Invoke(sender, args);
                gamepad.OnWhileButtonPressed += (sender, args) => OnWhileGamepadButtonPressed?.Invoke(sender, args);
                gamepad.OnWhileThumbStickPositionNotZero += (sender, args) => OnWhileGamepadThumbStickPositionNotZero?.Invoke(sender, args);
            }
        }

        /// <summary>
        ///     Gets or sets whether controller is polling for gamepad events.
        /// </summary>
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (IsEnabled != value)
                {
                    _isEnabled = value;
                    if (_isEnabled)
                        StartPolling();
                }
            }
        }

        /// <summary>
        ///     Gets or sets gamepad state updates per second.
        /// </summary>
        public ushort UpdatesPerSecond { get; set; } = 30;

        /// <summary>
        ///     Gets list of gamepads.
        /// </summary>
        public IReadOnlyCollection<Gamepad> Gamepads { get; } = new List<Gamepad>
        {
            new Gamepad(0),
            new Gamepad(1),
            new Gamepad(2),
            new Gamepad(3)
        }.AsReadOnly();

        /// <summary>
        ///     Gets list of currently active gamepads.
        /// </summary>
        public IList<Gamepad> ActiveGamepads => Gamepads.Where(x => x.IsActive).ToList();

        /// <summary>
        ///     Gets number of currently active gamepads.
        /// </summary>
        public int ActiveGamepadsCount => Gamepads.Count(x => x.IsActive);

        /// <summary>
        ///     Event that triggers when gamepad activates, sender value is gamepad.
        /// </summary>
        public event EventHandler OnGamepadActivate;

        /// <summary>
        ///     Event that triggers when gamepad deactivates, sender value is gamepad.
        /// </summary>
        public event EventHandler OnGamepadDeactivate;

        /// <summary>
        ///     Event that triggers when one of gamepad buttons is pressed, sender value is gamepad.
        /// </summary>
        public event EventHandler<ButtonPressedEventArgs> OnGamepadButtonPressed;

        /// <summary>
        ///     Event that triggers when one of gamepad buttons is released, sender value is gamepad.
        /// </summary>
        public event EventHandler<ButtonReleasedEventArgs> OnGamepadButtonReleased;

        /// <summary>
        ///     Event that triggers when value of one of gamepad triggers changes, sender value is gamepad.
        /// </summary>
        public event EventHandler<TriggerValueChangedEventArgs> OnGamepadTriggerValueChangedChanged;

        /// <summary>
        ///     Event that triggers when value of one of gamepad thumbsticks changes, sender value is gamepad.
        /// </summary>
        public event EventHandler<ThumbStickPositionChangedEventArgs> OnGamepadThumbStickValueChanged;

        /// <summary>
        ///     Event that triggers continuously as long as one of gamepad buttons on controller is being pressed, sender value is
        ///     gamepad.
        /// </summary>
        public event EventHandler<WhileButtonPressedEventArgs> OnWhileGamepadButtonPressed;

        /// <summary>
        ///     Event that triggers constantly as long as one of thumbsticks is not in neutral position, sender value is gamepad.
        /// </summary>
        public event EventHandler<WhileThumbStickPositionNotZeroEventArgs> OnWhileGamepadThumbStickPositionNotZero;

        /// <summary>
        ///     Starts polling for gamepad updates, spawns one polling thread for each gamepad;
        /// </summary>
        private void StartPolling()
        {
            foreach (var gamepad in Gamepads)
            {
                var listenerThread = new Thread(() =>
                {
                    while (IsEnabled)
                    {
                        gamepad.UpdateState();
                        Thread.Sleep(1000 / UpdatesPerSecond);
                    }
                });
                listenerThread.Name = "Listener thread for gamepad " + gamepad.Id;
                listenerThread.Start();
            }
        }
    }
}