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
        AnswerManager answerManager { get; set; }

        public CreateQuestionWindow()
        {
            InitializeComponent();
            Answers = new ObservableCollection<Answer>();
            answerManager = new AnswerManager();
            AnswersDataGrid.ItemsSource = Answers;
        }

        private void SaveCloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (AllInfoEntered())
            {
                QuestionTitle = QuestionTitleTextBox.Text;
                int dataGridCount = AnswersDataGrid.Items.Count;
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBoxes.ShowErrorMessageBox("You have not entered all necessary information");
            }
        }

        private bool AllInfoEntered()
        {
            return QuestionTitleTextBox.Text != "" && AnswersDataGrid.Items.Count > 0;
        }

        private void AddAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            AnswersDataGrid.IsReadOnly = false;
            answerManager.AddAnswer(new Answer("Write your answer here"));
            Answers.Add(answerManager.GetAt(answerManager.Count - 1));
        }
    }
}
