using MediaPlayerThread.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace MediaPlayerThread
{
    /// <summary>
    /// This class is responsible for moving the hands of the clock
    /// </summary>
    public class MoveHandsHandler
    {
        private Button startClockButton;
        private Button stopClockButton;
        private RotateTransform hourHand;
        private RotateTransform minuteHand;
        private RotateTransform secondHand;
        bool IsRunning = false;

        /// <summary>
        /// The constructor configures the essentials of the class
        /// </summary>
        /// <param name="startClockButtonFromGui">The start button</param>
        /// <param name="stopClockButtonFromGui">The stop button</param>
        /// <param name="hourHandFromGyi">The hours hand transformation from the gui</param>
        /// <param name="minuteHandFromGyi">The minute hand transformation from the gui</param>
        /// <param name="secondHandFromGyi">The second hand transformation from the gui</param>
        public MoveHandsHandler(Button startClockButtonFromGui, Button stopClockButtonFromGui, RotateTransform hourHandFromGyi, RotateTransform minuteHandFromGyi, RotateTransform secondHandFromGyi)
        {
            startClockButton = startClockButtonFromGui;
            stopClockButton = stopClockButtonFromGui;
            hourHand = hourHandFromGyi;
            minuteHand = minuteHandFromGyi;
            secondHand = secondHandFromGyi;
        }

        /// <summary>
        /// Disable buttons according to the button being used recently
        /// </summary>
        /// <param name="buttonToDisable">The button to disable</param>
        private void DisableClockButtonObjectButtons(ButtonBeingDisabled buttonToDisable)
        {
            switch (buttonToDisable)
            {
                case ButtonBeingDisabled.Play:
                    startClockButton.Dispatcher.Invoke(() => startClockButton.IsEnabled = false);
                    stopClockButton.Dispatcher.Invoke(() => stopClockButton.IsEnabled = true);
                    break;
                case ButtonBeingDisabled.Stop:
                    stopClockButton.Dispatcher.Invoke(() => stopClockButton.IsEnabled = false);
                    startClockButton.Dispatcher.Invoke(() => startClockButton.IsEnabled = true);
                    break;
            }
        }

        /// <summary>
        /// Starts the moving of the clocks hands
        /// </summary>
        public void StartPlay()
        {
            IsRunning = true;
            DisableClockButtonObjectButtons(ButtonBeingDisabled.Play);
            MoveHand();
        }

        /// <summary>
        /// Moves the hands
        /// </summary>
        private void MoveHand()
        {
            while (IsRunning)
            {
                hourHand.Dispatcher.Invoke(() => hourHand.Angle = (DateTime.Now.Hour * 30) + (DateTime.Now.Minute * 0.5));
                minuteHand.Dispatcher.Invoke(() => minuteHand.Angle = DateTime.Now.Minute * 6);
                secondHand.Dispatcher.Invoke(() => secondHand.Angle = DateTime.Now.Second * 6);
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Stops the player
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to cancel the task</param>
        public void StopPlay(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
            else
            {
                IsRunning = false;
                DisableClockButtonObjectButtons(ButtonBeingDisabled.Stop);
                MoveHand();
            }
        }
    }
}
