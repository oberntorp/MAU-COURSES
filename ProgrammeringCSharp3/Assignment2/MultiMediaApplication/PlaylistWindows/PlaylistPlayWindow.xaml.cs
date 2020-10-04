using MultiMediaBussinessLogic;
using MultiMediaClassesAndManagers.Managers;
using MultiMediaClassesAndManagers.MediaBaseClass;
using MultiMediaClassesAndManagers.MediaSubClasses;
using MutiMediaClassesAndManagers;
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
using System.Windows.Shapes;
using Utilities;
using System.Timers;
using System.Windows.Threading;

namespace MultiMediaApplication.PlaylistWindows
{
    /// <summary>
    /// Interaction logic for PlaylistPlayWindow.xaml, handles playing of media from playlists
    /// </summary>
    public partial class PlaylistPlayWindow : Window
    {
        private int playlistPlaybackDelayBetweenMediaSec = 5;
        private List<MediaFile> playlistContentToPlay = null;
        private MediaHandler mediaHandler = null;
        private int indexOfMediaToPlay = 0;
        private DispatcherTimer timer = null;

        /// <summary>
        /// Initiates the window with media that should be played
        /// </summary>
        /// <param name="playlistContent">Media to be played</param>
        /// <param name="secondsBetweenImages">Seconds between images in playing</param>
        public PlaylistPlayWindow(string titleOfPlaylist, List<MediaFile> playlistContent, int secondsBetweenImages)
        {
            InitializeComponent();
            Title = $"Playing playlist: {titleOfPlaylist}";
            playlistContentToPlay = playlistContent;
            playlistPlaybackDelayBetweenMediaSec = secondsBetweenImages;
            mediaHandler = new MediaHandler();
        }

        /// <summary>
        /// Begins to play media
        /// </summary>
        public void BeginPlayingMedia()
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (indexOfMediaToPlay < playlistContentToPlay.Count)
            {
                timer.Interval = TimeSpan.FromSeconds(playlistPlaybackDelayBetweenMediaSec);
                SetMediaToPlay();
                indexOfMediaToPlay++;
            }
            else
            {
                timer.Stop();
                MessageBoxes.ShowInformationMessageBox("There is no more media to play, the player will now close.");
                this.Close();
            }
        }

        private void SetMediaToPlay()
        {
            if (!mediaHandler.IsMediaVideo(playlistContentToPlay[indexOfMediaToPlay]))
            {
                VideoMediaElement.Visibility = Visibility.Hidden;

                ImageMediaElement.Visibility = Visibility.Visible;
                ImageMediaElement.Source = new BitmapImage(new Uri(playlistContentToPlay[indexOfMediaToPlay].SourceUrl));

            }
            else
            {
                Video video = (playlistContentToPlay[indexOfMediaToPlay] as Video);
                TimeSpan lengthAsMs = TimeSpan.FromSeconds((int)video.LengthInSeconds + playlistPlaybackDelayBetweenMediaSec);

                ImageMediaElement.Visibility = Visibility.Hidden;

                VideoMediaElement.Visibility = Visibility.Visible;
                VideoMediaElement.Source = new Uri(playlistContentToPlay[indexOfMediaToPlay].SourceUrl);

                timer.Stop();
                timer.Interval = lengthAsMs;
                timer.Start();
            }
        }
    }
}
