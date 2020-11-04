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

        public CreateQuestionWindow()
        {
            InitializeComponent();
            Answers = new ObservableCollection<Answer>();
            AnswersDataGrid.ItemsSource = Answers;
        }

        private void SaveCloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (AllInfoEntered())
            {
                QuestionTitle = QuestionTitleTextBox.Text;
                int dataGridCount = AnswersDataGrid.Items.Count;
                PopulateAnswers(dataGridCount);
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

        private void PopulateAnswers(int dataGridCount)
        {
            for (int i = 0; i < dataGridCount; i++)
            {
                Answers.Add((Answer)AnswersDataGrid.Items.GetItemAt(i));
            }
        }

        private void AddAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            AnswersDataGrid.IsReadOnly = false;
            Answers.Add(new Answer("Write your answer here"));
        }
    }
}
