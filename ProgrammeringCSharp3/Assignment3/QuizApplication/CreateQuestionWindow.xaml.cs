using QuizApplicationBussinessLogic.Managers;
using QuizApplicationBussinessLogic.QuizClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace QuizApplication
{
    /// <summary>
    /// Interaction logic for CreateQuestionWindow.xaml
    /// </summary>
    public partial class CreateQuestionWindow : Window
    {
        public string QuestionTitle { get; set; }
        public ObservableCollection<Answer> Answers { get; set; }
        AnswerManager AnswerManager { get; set; }

        /// <summary>
        /// Constructor, initializing Lists as well as setting the ItemsSource for the dataGrid
        /// </summary>
        public CreateQuestionWindow()
        {
            InitializeComponent();
            Answers = new ObservableCollection<Answer>();
            AnswerManager = new AnswerManager();
            AnswersDataGrid.ItemsSource = Answers;
        }

        /// <summary>
        /// Event handler for the save button, checks if all necessary data was entered, iy yes, the window is closed
        /// </summary>
        /// <param name="sender">the object sending the request, in this case a button</param>
        /// <param name="e">The arguments associated with the event</param>
        private void SaveCloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (AllInfoEntered())
            {
                QuestionTitle = QuestionTitleTextBox.Text;
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBoxes.ShowErrorMessageBox("You have not entered all necessary information");
            }
        }

        /// <summary>
        /// Checks that all information was entered
        /// </summary>
        /// <returns></returns>
        private bool AllInfoEntered()
        {
            return QuestionTitleTextBox.Text != "" && AnswersDataGrid.Items.Count > 0;
        }

        /// <summary>
        /// Event handler for the button "Add Answer", it adds a new row to the DataGrid
        /// </summary>
        /// <param name="sender">The object sending the response, in this case a button</param>
        /// <param name="e">Event arguments</param>
        private void AddAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            AnswersDataGrid.IsReadOnly = false;
            AnswerManager.AddAnswer(new Answer("Write your answer here"));
            Answers.Add(AnswerManager.GetAt(AnswerManager.Count - 1));
        }
    }
}
