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
    /// This class is responsible for Playing the media that has been selected
    /// </summary>
    public class MediaPlayerHandler
    {
        MediaPlayer mediaplayer;
        Button browseButton;
        Button playButton;
        Button stopButton;
        TextBlock musicPlayedTexBlock;
        bool IsRunning = false;

        /// <summary>
        /// The constructor configures the essentials of the class
        /// </summary>
        /// <param name="mediaPlayerIn">MediaPlayer to be able to play</param>
        /// <param name="musicUrlIn">The url tof the choosen music file</param>
        /// <param name="browseButtonIn">Browse button</param>
        /// <param name="playButtonIn">Play button</param>
        /// <param name="stopButtonIn">Stop button</param>
        /// <param name="musicPlayedTexBlockIn">The textblock displaying the title played</param>
        public MediaPlayerHandler(MediaPlayer mediaPlayerIn, Uri musicUrlIn, Button browseButtonIn, Button playButtonIn, Button stopButtonIn, TextBlock musicPlayedTexBlockIn)
        {
            mediaplayer = mediaPlayerIn;
            mediaplayer.Open(musicUrlIn);

            browseButton = browseButtonIn;
            playButton = playButtonIn;
            stopButton = stopButtonIn;
            musicPlayedTexBlock = musicPlayedTexBlockIn;

            UpdateGuiWithNameOfMusicMusicChosen(ExtractMusicName(musicUrlIn));
            DisableMusicPlayerButtons(ButtonBeingDisabled.Browse);
        }

        /// <summary>
        /// Split on the url to get the name of the file
        /// </summary>
        /// <param name="musicUrlIn">The url to split</param>
        /// <returns>string (name of music played)</returns>
        private string ExtractMusicName(Uri musicUrlIn)
        {
            return musicUrlIn.ToString().Split('/').Last();
        }

        /// <summary>
        /// Updates the gui with the name of the played music
        /// </summary>
        /// <param name="nameOfMusicChoosen">Name to output</param>
        private void UpdateGuiWithNameOfMusicMusicChosen(string nameOfMusicChoosen)
        {
            musicPlayedTexBlock.Dispatcher.Invoke(() => musicPlayedTexBlock.Text = nameOfMusicChoosen);
        }

        /// <summary>
        /// Disable buttons according to the button being used recently
        /// </summary>
        /// <param name="buttonToDisable">The button to disable</param>
        private void DisableMusicPlayerButtons(ButtonBeingDisabled buttonToDisable)
        {
            switch(buttonToDisable)
            {
                case ButtonBeingDisabled.Browse:
                    browseButton.Dispatcher.Invoke(() => browseButton.IsEnabled = false);
                    playButton.Dispatcher.Invoke(() => playButton.IsEnabled = true);
                    break;
                case ButtonBeingDisabled.Play:
                    playButton.Dispatcher.Invoke(() => playButton.IsEnabled = false);
                    stopButton.Dispatcher.Invoke(() => stopButton.IsEnabled = true);
                    break;
                case ButtonBeingDisabled.Stop:
                    stopButton.Dispatcher.Invoke(() => stopButton.IsEnabled = false);
                    playButton.Dispatcher.Invoke(() => playButton.IsEnabled = true);
                    break;
            }
        }

        /// <summary>
        /// Play the music
        /// </summary>
        private void PlayMusic()
        {
            while (IsRunning)
            {
                mediaplayer.Dispatcher.Invoke(() => mediaplayer.Play());
                Thread.Sleep(1000);
            }
            mediaplayer.Dispatcher.Invoke(() => mediaplayer.Pause());
        }

        /// <summary>
        /// Starts the player
        /// </summary>
        public void StartPlay()
        {
            IsRunning = true;
            DisableMusicPlayerButtons(ButtonBeingDisabled.Play);
            PlayMusic();
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
                DisableMusicPlayerButtons(ButtonBeingDisabled.Stop);
                PlayMusic();
            }
        }
    }
}
