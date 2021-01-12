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
    public class MediaPlayerHandler
    {
        MediaPlayer mediaplayer;
        Button browseButton;
        Button playButton;
        Button stopButton;
        TextBlock musicPlayedTexBlock;
        bool IsRunning = false;

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

        private string ExtractMusicName(Uri musicUrlIn)
        {
            return musicUrlIn.ToString().Split('/').Last();
        }

        private void UpdateGuiWithNameOfMusicMusicChosen(string nameOfMusicChoosen)
        {
            musicPlayedTexBlock.Dispatcher.Invoke(() => musicPlayedTexBlock.Text = nameOfMusicChoosen);
        }

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

        private void PlayMusic()
        {
            while (IsRunning)
            {
                mediaplayer.Dispatcher.Invoke(() => mediaplayer.Play());
                Thread.Sleep(1000);
            }
            mediaplayer.Dispatcher.Invoke(() => mediaplayer.Pause());
        }

        public void StartPlay()
        {
            IsRunning = true;
            DisableMusicPlayerButtons(ButtonBeingDisabled.Play);
            PlayMusic();
        }

        public void StopPlay()
        {
            IsRunning = false;
            DisableMusicPlayerButtons(ButtonBeingDisabled.Stop);
            PlayMusic();
        }
    }
}
