using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MediaPlayerThread;
using System.Threading;

namespace Assignment5Alt1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Task mediaPlayerTask;
        Task movingObjectTask;
        Task moveHandsTask;

        CancellationTokenSource source = new CancellationTokenSource();
        CancellationToken tokenMovingObject;
        CancellationToken tokenMediaPlayer;
        CancellationToken tokenHands;

        MediaPlayerHandler mediaPlayerHandler;
        MovingObjectHandler movingObjectHandler;
        MoveHandsHandler moveHandsHandler;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenMusicFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fOpenDialog = new OpenFileDialog();
            fOpenDialog.Filter = "Music files (*.mp3)|*.mp3";

            if (fOpenDialog.ShowDialog() == true)
            {
                mediaPlayerHandler = new MediaPlayerHandler(new MediaPlayer(), new Uri(fOpenDialog.FileName), OpenMusicFileButton, PlayMusicButton, StopMusicButton, NameOfPlayingMusicTextBox);
            }
        }

        private void PlayMusicButton_Click(object sender, RoutedEventArgs e)
        {
            source = new CancellationTokenSource();
            tokenMediaPlayer = source.Token;
            mediaPlayerTask = new Task(() => mediaPlayerHandler.StartPlay(tokenMediaPlayer), tokenMediaPlayer);
            mediaPlayerTask.Start();
        }

        private void StopMusicButton_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayerHandler.StopPlay(tokenMediaPlayer);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            source.Cancel();
        }

        private void StartMoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (movingObjectHandler == null)
            {
                source = new CancellationTokenSource();
                tokenMovingObject = source.Token;
                movingObjectHandler = new MovingObjectHandler(StartMoveButton, StopMoveButton, SpinningObjectCanvas);

                movingObjectTask = new Task(() => movingObjectHandler.StartPlay(tokenMovingObject));
                movingObjectTask.Start();
            }
            else
            {
                movingObjectTask = new Task(() => movingObjectHandler.StartPlay(tokenMovingObject));
                movingObjectTask.Start();
            }
        }

        private void StopMoveButton_Click(object sender, RoutedEventArgs e)
        {
            movingObjectHandler.StopPlay(tokenMovingObject);
        }

        private void StartClockButton_Click(object sender, RoutedEventArgs e)
        {
            if (moveHandsHandler == null)
            {
                source = new CancellationTokenSource();
                tokenHands = source.Token;
                moveHandsHandler = new MoveHandsHandler(StartClockButton, StopClockButton, HourHandTransform, MinuteHandTransform, SecondHandTransform); 

                moveHandsTask = new Task(() => moveHandsHandler.StartPlay(tokenMovingObject));
                moveHandsTask.Start();
            }
            else
            {
                moveHandsTask = new Task(() => moveHandsHandler.StartPlay(tokenMovingObject));
                moveHandsTask.Start();
            }
        }

        private void StopClockButton_Click(object sender, RoutedEventArgs e)
        {
            moveHandsHandler.StopPlay(tokenHands);
        }
    }
}
