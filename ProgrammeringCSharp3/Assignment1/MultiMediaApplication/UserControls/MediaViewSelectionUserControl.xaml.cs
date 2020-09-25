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
    /// A UserControl is a control that you can create to display data on specific places in your application.
    /// This is done by binding data to controls already existing in WPF, this allows you to build customized experiences
    /// It uses DependencyProperties to be able to get/set its data used on controls in the control itself 
    /// </summary>
    public partial class MediaViewSelectionUserControl : UserControl
    {
        public int MediaId
        {
            get { return (int)GetValue(MediaIdProperty); }
            set { SetValue(MediaIdProperty, value); }
        }
        
        public string MediaName
        {
            get { return (string)GetValue(MediaNameProperty); }
            set { SetValue(MediaNameProperty, value); }
        }

        public string MediaImageSource
        {
            get { return (string)GetValue(MediaImageSourceProperty); }
            set { SetValue(MediaImageSourceProperty, value); }
        }

        public string MediaPreviewSource
        {
            get { return (string)GetValue(MediaPreviewSourceProperty); }
            set { SetValue(MediaPreviewSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MediaId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MediaIdProperty =
            DependencyProperty.Register("MediaId", typeof(int), typeof(MediaViewSelectionUserControl));

        // Using a DependencyProperty as the backing store for MediaName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MediaNameProperty =
            DependencyProperty.Register("MediaName", typeof(string), typeof(MediaViewSelectionUserControl));

        // Using a DependencyProperty as the backing store for MediaImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MediaImageSourceProperty =
            DependencyProperty.Register("MediaImageSource", typeof(string), typeof(MediaViewSelectionUserControl));

        // Using a DependencyProperty as the backing store for MediaPreviewSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MediaPreviewSourceProperty =
            DependencyProperty.Register("MediaPreviewSource", typeof(string), typeof(MediaViewSelectionUserControl));

        /// <summary>
        /// MediaViewSelectionUserControls constructor
        /// </summary>
        public MediaViewSelectionUserControl()
        {
            InitializeComponent();
        }
    }
}
