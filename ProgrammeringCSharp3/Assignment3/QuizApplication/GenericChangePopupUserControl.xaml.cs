using Utilities.Enums;
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

        public string TextActionLabel
        {
            get { return (string)GetValue(TextActionLabelProperty); }
            set { SetValue(TextActionLabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextActionLabel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextActionLabelProperty =
            DependencyProperty.Register("TextActionLabel", typeof(string), typeof(GenericChangePopupUserControl), new PropertyMetadata(""));

        public string ItemTypeBeingChanged
        {
            get { return (string)GetValue(ItemTypeBeingChangedProperty); }
            set { SetValue(ItemTypeBeingChangedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemTypeBeingChanged.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTypeBeingChangedProperty =
            DependencyProperty.Register("ItemTypeBeingChanged", typeof(string), typeof(GenericChangePopupUserControl), new PropertyMetadata("Quiz"));

        public event EventHandler<IsSavedEventArgs> IsSavedEvent;

        public Mode TypeOfAction { get; set; }
        public TypeOfItemToChange TypeOfItemToHandle { get; set; }

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

        public bool OldIsRightAnswer { get { return (bool)GetValue(OldIsRightAnswerProperty); } set { SetValue(OldIsRightAnswerProperty, value); } }

        // Using a DependencyProperty as the backing store for OldIsRightAnswer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OldIsRightAnswerProperty =
            DependencyProperty.Register("OldIsRightAnswer", typeof(bool), typeof(GenericChangePopupUserControl), new PropertyMetadata(false));

        /// <summary>
        /// Constructor, initializes the component, sets the dataContect, necessary for the dependency properties to fully work
        /// </summary>
        public GenericChangePopupUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// This method initializes labels on the gui, and depending an if it is a Quiz, Question or Answer displays different input fields and labels
        /// </summary>
        public void InitializeGui()
        {
            TextActionLabel = (TypeOfAction == Mode.Add) ? "Add new " : "Edit a ";
            if (TypeOfItemToHandle != TypeOfItemToChange.Quiz)
            {
                ItemTypeBeingChanged = (TypeOfItemToHandle == TypeOfItemToChange.Question) ? "Question" : "Answer";
                ChangedItemDescriptionLabel.Visibility = Visibility.Collapsed;
                ChangedItemDescriptionTextBox.Visibility = Visibility.Collapsed;

                if (TypeOfItemToHandle == TypeOfItemToChange.Answer)
                {
                    ChangedItemRightAnswerLabel.Visibility = Visibility.Visible;
                    ChangedItemRightAnswerCheckBox.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Event handler for Save button, checks that all information has been filled out, sets the event arguments for the event and fires the event
        /// </summary>
        /// <param name="sender">the object sending the request, in this case a button</param>
        /// <param name="e">The event arguments</param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            WarningLabel.Visibility = Visibility.Hidden;
            IsSavedEventArgs eventArgs = new IsSavedEventArgs();
            if (IsTextFilledIn())
            {
                eventArgs.NewTitle = ChangedItemNameTextBox.Text;
                if (TypeOfItemToHandle == TypeOfItemToChange.Quiz)
                {
                    eventArgs.NewDescription = ChangedItemDescriptionTextBox.Text;
                }
                eventArgs.UserControl = this;
                Action<IsSavedEventArgs> FireEvent = OnIsSavedEvent;
                FireEvent(eventArgs);
            }
            else
            {
                WarningLabel.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Initializing the IsSavedEvent
        /// </summary>
        /// <param name="eventArgs">Event arguments</param>
        private void OnIsSavedEvent(IsSavedEventArgs eventArgs)
        {
            IsSavedEvent?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// Checks that all input fields has been filled out
        /// </summary>
        /// <returns>bool</returns>
        private bool IsTextFilledIn()
        {
            if (TypeOfItemToHandle == TypeOfItemToChange.Quiz)
            {
                return ChangedItemNameTextBox.Text != "" && ChangedItemDescriptionTextBox.Text != "";
            }
            else
            {
                return ChangedItemNameTextBox.Text != "";
            }
        }
    }
}
