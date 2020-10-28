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
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuizApplicationBussinessLogic.Handlers;
using QuizApplicationBussinessLogic.QuizClasses;

namespace QuizApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        QuizHandler quizHandler;
        List<QuizItem> quizes;
        List<Answer> AnswersOfSelectedQuestion;
        ObservableCollection<Question> QuestionsOfSelectedQuiz;
        public MainWindow()
        {
            InitializeComponent();
            quizHandler = new QuizHandler();
            quizes = new List<QuizItem>();
            AnswersOfSelectedQuestion = new List<Answer>();
        }

        private void CreateQuizButton_Click(object sender, RoutedEventArgs e)
        {
            QuizItem newQuiz = quizHandler.CreateQuiz(QuizNameTextBox.Text, QuizDescriptionTextBox.Text);
            if (quizHandler.AddQuiz(newQuiz))
            {
                QuizesListView.ItemsSource = quizHandler.quizManager.GetAllItems();
                MessageBoxes.ShowInformationMessageBox("Quiz Added");
            }
        }

        private void QuizesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            QuestionsAnswersTabControl.IsEnabled = true;
            int indexOfSelectedQuiz = QuizesListView.SelectedIndex;

            QuestionsOfSelectedQuiz = new ObservableCollection<Question>(quizHandler.quizManager.GetAt(indexOfSelectedQuiz).Questions.GetAllItems());
            QuestionsOfSelectedQuizListView.ItemsSource = QuestionsOfSelectedQuiz;
        }

        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            CreateQuestionWindow createQuestionWindow = new CreateQuestionWindow();
            bool result = (bool)createQuestionWindow.ShowDialog();
            if (result)
            {
                Question newQuestion = new Question(createQuestionWindow.QuestionTitle);
                AddAnswersToQuestion(createQuestionWindow.Answers.ToList(), newQuestion);

                if (quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).AddQuestion(newQuestion))
                {
                    QuestionsOfSelectedQuiz.Add(newQuestion);
                }
            }
        }

        private void DeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionsOfSelectedQuizListView.SelectedIndex >= 0)
            {
                if(quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).RemoveQuestion(QuestionsOfSelectedQuizListView.SelectedIndex))
                {
                    QuestionsOfSelectedQuiz.RemoveAt(QuestionsOfSelectedQuizListView.SelectedIndex);
                }
            }
            else
            {
                MessageBoxes.ShowInformationMessageBox("Please select a question to remove");
            }
        }

        private void AddAnswersToQuestion(List<Answer> answers, Question newQuestion)
        {
            answers.ForEach(x =>
            {
                newQuestion.Answers.Add(x);
            });
        }
    }
}
