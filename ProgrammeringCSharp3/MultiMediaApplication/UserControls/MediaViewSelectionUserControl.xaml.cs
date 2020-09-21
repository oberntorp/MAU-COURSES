using MultiMediaApplication.PlaylistWindows;
using MultiMediaBussinessLogic;
using MultiMediaClassesAndManagers.Managers;
using MutiMediaClassesAndManagers;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace MultiMediaApplication.UserControls
{
    /// <summary>
    /// Interaction logic for MediaViewSelectionUserControl.xaml
    /// </summary>
    public partial class MediaViewSelectionUserControl : UserControl
    {
        private PlaylistHandler playlistHandler = null;
        public int PlaylistIndex { get; set; }



        public int MediaId
        {
            get { return (int)GetValue(MediaIdProperty); }
            set { SetValue(MediaIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MediaId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MediaIdProperty =
            DependencyProperty.Register("MediaId", typeof(int), typeof(MediaViewSelectionUserControl));



        public string MediaName
        {
            get { return (string)GetValue(MediaNameProperty); }
            set { SetValue(MediaNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MediaName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MediaNameProperty =
            DependencyProperty.Register("MediaName", typeof(string), typeof(MediaViewSelectionUserControl));



        public string MediaImageSource
        {
            get { return (string)GetValue(MediaImageSourceProperty); }
            set { SetValue(MediaImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MediaImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MediaImageSourceProperty =
            DependencyProperty.Register("MediaImageSource", typeof(string), typeof(MediaViewSelectionUserControl));



        public string MediaPreviewSource
        {
            get { return (string)GetValue(MediaPreviewSourceProperty); }
            set { SetValue(MediaPreviewSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MediaPreviewSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MediaPreviewSourceProperty =
            DependencyProperty.Register("MediaPreviewSource", typeof(string), typeof(MediaViewSelectionUserControl));








        //public int MediaId { get; set; }
        //public string MediaName { get; set; }
        //public string MediaImageSource { get; set; }
        //public string MediaPreviewSource { get; set; }
        public MediaViewSelectionUserControl()
        {
            InitializeComponent();
        }

        private void StackPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PlaylistPlayWindow newPlayWindow = new PlaylistPlayWindow(PlaylistIndex);
        }
    }
}
