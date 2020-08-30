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
using System.Windows.Shapes;

namespace WPFTutorial
{
    /// <summary>
    /// Interaction logic for PasswordAndImageControlWindow.xaml
    /// </summary>
    public partial class PasswordAndImageControlWindow : Window
    {
        public PasswordAndImageControlWindow()
        {
            InitializeComponent();
        }

        private void loadImageFromDisk_clicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            if(openDialog.ShowDialog() == true)
            {
                ImageFromDiskDisp.Source = new BitmapImage(new Uri(@"\\Images\\chicken.png", UriKind.Relative));
            }
        }
    }
}
