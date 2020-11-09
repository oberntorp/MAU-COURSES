using QuizApplication.EventArgs;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuizApplication
{
    /// <summary>
    /// Interaction logic for GenericChangePopupUserControl.xaml
    /// </summary>
    public partial class GenericChangePopupUserControl : UserControl
    {

        public event EventHandler<IsSavedEventArgs> IsSaved;
        public bool HasItemDescription
        {
            get;
            set;
        }

        public string TypeOfItemToChange
        {
            get;
            set;
        }

        public string OldTitle
        {
            get { return (string)GetValue(OldTitleProperty); }
            set { SetValue(OldTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OldTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OldTitleProperty =
            DependencyProperty.Register("OldTitle", typeof(string), typeof(GenericChangePopupUserControl), new PropertyMetadata(""));

        public string OldDescription
        {
            get { return (string)GetValue(OldDescriptionProperty); }
            set { SetValue(OldDescriptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OldDescription.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OldDescriptionProperty =
            DependencyProperty.Register("OldDescription", typeof(string), typeof(GenericChangePopupUserControl), new PropertyMetadata(""));


        public GenericChangePopupUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            WarningLabel.Visibility = Visibility.Hidden;
            IsSavedEventArgs eventArgs = new IsSavedEventArgs();
            if (TextFilledIn())
            {
                eventArgs.NewTitle = ChangedItemNameTextBox.Text;
                if (HasItemDescription)
                {
                    eventArgs.NewDescription = ChangedItemDescriptionTextBox.Text;
                }
                eventArgs.UserControl = this;
                OnIsSavedEvebt(eventArgs);
            }
            else
            {
                WarningLabel.Visibility = Visibility.Visible;
            }
        }

        private void OnIsSavedEvebt(IsSavedEventArgs eventArgs)
        {
            IsSaved?.Invoke(this, eventArgs);
        }

        private bool TextFilledIn()
        {
            return ChangedItemNameTextBox.Text != "" && ChangedItemDescriptionTextBox.Text != "";
        }
    }
    }
