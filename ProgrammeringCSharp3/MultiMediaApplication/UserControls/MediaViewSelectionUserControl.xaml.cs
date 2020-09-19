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
        public int MediaId { get; set; }
        public string MediaName { get; set; }
        public string MediaImageSource { get; set; }
        public MediaViewSelectionUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void StackPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(MediaImageSource);
        }
    }
}
