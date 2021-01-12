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
            mediaPlayerTask = new Task(() => mediaPlayerHandler.StartPlay());
            mediaPlayerTask.Start();
        }

        private void StopMusicButton_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayerHandler.StopPlay();
        }


        private void StartMoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (movingObjectHandler == null)
            {
                movingObjectHandler = new MovingObjectHandler(StartMoveButton, StopMoveButton, SpinningObjectCanvas);

                movingObjectTask = new Task(() => movingObjectHandler.StartPlay());
                movingObjectTask.Start();
            }
            else
            {
                movingObjectTask = new Task(() => movingObjectHandler.StartPlay());
                movingObjectTask.Start();
            }
        }

        private void StopMoveButton_Click(object sender, RoutedEventArgs e)
        {
            movingObjectHandler.StopPlay();
        }

        private void StartClockButton_Click(object sender, RoutedEventArgs e)
        {
            if (moveHandsHandler == null)
            {
                moveHandsHandler = new MoveHandsHandler(StartClockButton, StopClockButton, HourHandTransform, MinuteHandTransform, SecondHandTransform); 

                moveHandsTask = new Task(() => moveHandsHandler.StartPlay());
                moveHandsTask.Start();
            }
            else
            {
                moveHandsTask = new Task(() => moveHandsHandler.StartPlay());
                moveHandsTask.Start();
            }
        }

        private void StopClockButton_Click(object sender, RoutedEventArgs e)
        {
            moveHandsHandler.StopPlay();
        }
    }
}
