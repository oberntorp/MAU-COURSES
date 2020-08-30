using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WPFTutorial
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PanelMainGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show($"You clicked the window Mouse position {e.GetPosition(this).ToString()}");
        }

        private void ColtureInfoButtonSwitch(object sender, RoutedEventArgs args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo((sender as Button).Tag.ToString());
            labelNumber.Content = (123456789.42d).ToString("N2");
            labelDate.Content = DateTime.Now.ToString();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
        }

        private void Window_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowExampleContinue newWindow = new WindowExampleContinue();
            newWindow.Show();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            TextRenderingWindow textRenderingWindow = new TextRenderingWindow();
            textRenderingWindow.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            TabOrderExample tabOrderEx = new TabOrderExample();
            tabOrderEx.Show();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            AccessKeysEx accessKeys = new AccessKeysEx();
            accessKeys.Show();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            WrapPanelExample ex = new WrapPanelExample();
            ex.Show();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            DockPanelExample ex = new DockPanelExample();
            ex.Show();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            StackPanelExample ex = new StackPanelExample();
            ex.Show();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            GridExample ex = new GridExample();
            ex.Show();
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            PasswordAndImageControlWindow ex = new PasswordAndImageControlWindow();
            ex.Show();
        }
    }
}
