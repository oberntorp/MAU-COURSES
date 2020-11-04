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
using Microsoft.Win32;
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
        ObservableCollection<Answer> AnswersOfSelectedQuestion;
        ObservableCollection<Question> QuestionsOfSelectedQuiz;
        public MainWindow()
        {
            InitializeComponent();
            quizHandler = new QuizHandler();
            quizes = new List<QuizItem>();
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
            AnswersTabItem.IsEnabled = false;
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

                if (quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Questions.AddQuestion(newQuestion))
                {
                    QuestionsOfSelectedQuiz = new ObservableCollection<Question>(quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Questions.GetAllItems());
                    QuestionsOfSelectedQuizListView.ItemsSource = QuestionsOfSelectedQuiz;
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
                newQuestion.Answers.AddAnswer(x);
            });
        }

        private void LoadFromXMLMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML File | *.XML";
            bool opened = (bool)openFileDialog.ShowDialog();

            if(opened)
            {
                try
                {
                    quizHandler.quizManager.XMLDeserialize(openFileDialog.FileName);
                    QuizesListView.ItemsSource = quizHandler.quizManager.GetAllItems();
                    MessageBoxes.ShowInformationMessageBox("The Created Questions where saved!");
                }
                catch (Exception exOpen)
                {
                    MessageBoxes.ShowErrorMessageBox($"{exOpen.Message} {exOpen.InnerException}");
                }
            }
        }

        private void SaveToXMLMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML File | *.XML";
            bool saved = (bool)saveFileDialog.ShowDialog();

            if (saved)
            {
                try
                {
                    quizHandler.quizManager.XMLSerialize(saveFileDialog.FileName);
                    MessageBoxes.ShowInformationMessageBox("The Questions where loaded");
                }
                catch (Exception exSave)
                {
                    MessageBoxes.ShowErrorMessageBox($"{exSave.Message} {exSave.InnerException}");
                }
            }
        }

        private void QuestionsOfSelectedQuizListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indexOfSelectedQuestion = QuestionsOfSelectedQuizListView.SelectedIndex;

            AnswersOfSelectedQuestion = new ObservableCollection<Answer>(QuestionsOfSelectedQuiz.Where(x => x.Id == indexOfSelectedQuestion+1).FirstOrDefault().Answers.GetAllItems());
            AnswersOfSelectedQuestionListView.ItemsSource = AnswersOfSelectedQuestion;
            AnswersTabItem.IsEnabled = true;
            AnswersTabItem.IsSelected = true;
        }
    }
}
