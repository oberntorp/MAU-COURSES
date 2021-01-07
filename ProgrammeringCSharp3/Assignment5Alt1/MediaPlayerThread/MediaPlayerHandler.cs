using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MediaPlayerThread
{
    public class MediaPlayerHandler
    {
        MediaPlayer mediaMplayer;
        bool IsRunning = false;
        object myLock = new object();

        public MediaPlayerHandler(MediaPlayer mediaPlayer, Uri musicUrl)
        {
            mediaMplayer = mediaPlayer;
            mediaMplayer.Open(musicUrl);
        }

        private void PlayMusic(CancellationToken cancelationToken)
        {
            if (cancelationToken.IsCancellationRequested)
            {
                cancelationToken.ThrowIfCancellationRequested();
            }
            while (IsRunning)
            {
                mediaMplayer.Dispatcher.Invoke(() => mediaMplayer.Play());
                Thread.Sleep(1000);
            }
            mediaMplayer.Dispatcher.Invoke(() => mediaMplayer.Pause());
        }

        public void StartPlay(CancellationToken cancelationToken)
        {
            IsRunning = true;
            PlayMusic(cancelationToken);
        }

        public void StopPlay(CancellationToken cancelationToken)
        {
            IsRunning = false;
            PlayMusic(cancelationToken);
        }
    }
}
