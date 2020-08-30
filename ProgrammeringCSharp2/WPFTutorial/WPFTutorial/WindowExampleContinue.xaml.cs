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
    /// Interaction logic for WindowExampleContinue.xaml
    /// </summary>
    public partial class WindowExampleContinue : Window
    {
        public WindowExampleContinue()
        {
            InitializeComponent();
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            statusTextBox.Text = $"Text selection starts at # {textBox.SelectionStart} {Environment.NewLine}";
            statusTextBox.Text += $"The selection is {textBox.SelectionLength} characthers long {Environment.NewLine}";
            statusTextBox.Text += $"Selected text: '{textBox.SelectedText}'";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_ShowHelloWorldWhenClicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello, World!");
        }

        private void selectAll_changed(object sender, RoutedEventArgs e)
        {
            bool checkedValue = selectAllCheckBox.IsChecked == true;
            checkBoxA.IsChecked = checkedValue;
            checkBoxB.IsChecked = checkedValue;
            checkBoxB.IsChecked = checkedValue;
        }

        private void checkBoxFeature_changed(object sender, RoutedEventArgs e)
        {
            selectAllCheckBox.IsChecked = null;
            if((checkBoxA.IsChecked == true) && (checkBoxB.IsChecked == true) && (checkBoxC.IsChecked == true))
            {
                selectAllCheckBox.IsChecked = true;
            }
            if ((checkBoxA.IsChecked == false) && (checkBoxB.IsChecked == false) && (checkBoxC.IsChecked == false))
            {
                selectAllCheckBox.IsChecked = false;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PasswordAndImageControlWindow pwdAndImgControlWindow = new PasswordAndImageControlWindow();
            pwdAndImgControlWindow.Show();
        }
    }
}
