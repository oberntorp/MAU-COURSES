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
        CancellationTokenSource tokenSource;

        CancellationToken cancellationTokenMediaPlayer;
        CancellationToken cancellationTokenMovingObject;
        CancellationToken cancellationTokenMoveHands;


        Task mediaPlayerTask;
        Task movingObjectTask;
        Task moveHandsTask;

        MediaPlayerHandler mediaPlayerHandler;
        MovingObjectHandler movingObjectHandler;
        MoveHandsHandler moveHandsHandler;

        /// <summary>
        /// Constructor, sets cancellationSource
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            tokenSource = new CancellationTokenSource();
        }
        /// <summary>
        /// Handler for OpenMusicFile
        /// </summary>
        /// <param name="sender">The button sending the request</param>
        /// <param name="e">Event arguments</param>
        private void OpenMusicFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fOpenDialog = new OpenFileDialog();
            fOpenDialog.Filter = "Music files (*.mp3)|*.mp3";

            if (fOpenDialog.ShowDialog() == true)
            {
                mediaPlayerHandler = new MediaPlayerHandler(new MediaPlayer(), new Uri(fOpenDialog.FileName), OpenMusicFileButton, PlayMusicButton, StopMusicButton, NameOfPlayingMusicTextBox);
            }
        }

        /// <summary>
        /// Handler for PlayMusic
        /// </summary>
        /// <param name="sender">The button sending the request</param>
        /// <param name="e">Event arguments</param>
        private void PlayMusicButton_Click(object sender, RoutedEventArgs e)
        {
            cancellationTokenMediaPlayer = tokenSource.Token;
            mediaPlayerTask = Task.Factory.StartNew(mediaPlayerHandler.StartPlay, cancellationTokenMediaPlayer);
        }

        /// <summary>
        /// Handler for StopMusic
        /// </summary>
        /// <param name="sender">The button sending the request</param>
        /// <param name="e">Event arguments</param>
        private void StopMusicButton_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayerHandler.StopPlay(cancellationTokenMediaPlayer);
        }

        /// <summary>
        /// Handler that starts the move of the object
        /// </summary>
        /// <param name="sender">The button sending the request</param>
        /// <param name="e">Event arguments</param>
        private void StartMoveButton_Click(object sender, RoutedEventArgs e)
        {
            cancellationTokenMovingObject = tokenSource.Token;
            if (movingObjectHandler == null)
            {
                movingObjectHandler = new MovingObjectHandler(StartMoveButton, StopMoveButton, SpinningObjectCanvas);

                movingObjectTask = Task.Factory.StartNew(movingObjectHandler.StartPlay, cancellationTokenMovingObject);
            }
            else
            {
                movingObjectTask = Task.Factory.StartNew(movingObjectHandler.StartPlay, cancellationTokenMovingObject);
            }
        }

        /// <summary>
        /// Handler for Stop moving of the object
        /// </summary>
        /// <param name="sender">The button sending the request</param>
        /// <param name="e">Event arguments</param>
        private void StopMoveButton_Click(object sender, RoutedEventArgs e)
        {
            movingObjectHandler.StopPlay(cancellationTokenMovingObject);
        }

        /// <summary>
        /// Handler for starting the clock
        /// </summary>
        /// <param name="sender">The button sending the request</param>
        /// <param name="e">Event arguments</param>
        private void StartClockButton_Click(object sender, RoutedEventArgs e)
        {
            cancellationTokenMoveHands = tokenSource.Token;
            if (moveHandsHandler == null)
            {
                moveHandsHandler = new MoveHandsHandler(StartClockButton, StopClockButton, HourHandTransform, MinuteHandTransform, SecondHandTransform);

                moveHandsTask = Task.Factory.StartNew(moveHandsHandler.StartPlay, cancellationTokenMoveHands);
            }
            else
            {
                moveHandsTask = Task.Factory.StartNew(moveHandsHandler.StartPlay, cancellationTokenMoveHands);
            }
        }

        /// <summary>
        /// Handler for stopping the clock
        /// </summary>
        /// <param name="sender">The button sending the request</param>
        /// <param name="e">Event arguments</param>
        private void StopClockButton_Click(object sender, RoutedEventArgs e)
        {
            moveHandsHandler.StopPlay(cancellationTokenMoveHands);
        }

        /// <summary>
        /// Handler for Closing the window, terminates the tasks
        /// </summary>
        /// <param name="sender">The button sending the request</param>
        /// <param name="e">Event arguments</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                tokenSource.Cancel();
                mediaPlayerHandler?.StopPlay(cancellationTokenMediaPlayer);
                movingObjectHandler?.StopPlay(cancellationTokenMovingObject);
                moveHandsHandler?.StopPlay(cancellationTokenMoveHands);
            }
            catch(OperationCanceledException ex)
            {
                MessageBox.Show("Thank you, the application is now closed.");
            }
            finally
            {
                tokenSource.Dispose();
            }
        }
    }
}
