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
    public class MoveHandsHandler
    {
        private Button startClockButton;
        private Button stopClockButton;
        private RotateTransform hourHand;
        private RotateTransform minuteHand;
        private RotateTransform secondHand;
        bool IsRunning = false;

        public MoveHandsHandler(Button startClockButtonFromGui, Button stopClockButtonFromGui, RotateTransform hourHandFromGyi, RotateTransform minuteHandFromGyi, RotateTransform secondHandFromGyi)
        {
            startClockButton = startClockButtonFromGui;
            stopClockButton = stopClockButtonFromGui;
            hourHand = hourHandFromGyi;
            minuteHand = minuteHandFromGyi;
            secondHand = secondHandFromGyi;
        }

        public void StartPlay(CancellationToken cancelationToken)
        {
            IsRunning = true;
            DisableClockButtonObjectButtons(ButtonBeingDisabled.Play);
            MoveHand(cancelationToken);
        }

        private void MoveHand(CancellationToken cancelationToken)
        {
            while(IsRunning)
            {
                hourHand.Dispatcher.Invoke(() => hourHand.Angle = (DateTime.Now.Hour * 30) + (DateTime.Now.Minute * 0.5));
                minuteHand.Dispatcher.Invoke(() => minuteHand.Angle = DateTime.Now.Minute * 6);
                secondHand.Dispatcher.Invoke(() => secondHand.Angle = DateTime.Now.Second * 6);
                Thread.Sleep(1000);
            }
        }

        public void StopPlay(CancellationToken cancelationToken)
        {
            IsRunning = false;
            DisableClockButtonObjectButtons(ButtonBeingDisabled.Stop);
            MoveHand(cancelationToken);
        }

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
    }
}
