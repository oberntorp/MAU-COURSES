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
        CancellationTokenSource source = new CancellationTokenSource();
        CancellationToken token;
        Task mediaPlayerTask;
        MediaPlayerHandler mediaPlayerHandler;
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
                mediaPlayerHandler = new MediaPlayerHandler(new MediaPlayer(), new Uri(fOpenDialog.FileName));
            }
        }

        private void PlayMusicButton_Click(object sender, RoutedEventArgs e)
        {
            source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            mediaPlayerTask = new Task(() => mediaPlayerHandler.StartPlay(token), token);
            mediaPlayerTask.Start();
        }

        private void StopMusicButton_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayerHandler.StopPlay(token);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            source.Cancel();
        }
    }
}
