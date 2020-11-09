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
using QuizApplication.EventArgs;
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
        ObservableCollection<QuizItem> quizes;
        ObservableCollection<Answer> AnswersOfSelectedQuestion;
        ObservableCollection<Question> QuestionsOfSelectedQuiz;
        public MainWindow()
        {
            InitializeComponent();
            quizHandler = new QuizHandler();
        }

        private void CreateQuizButton_Click(object sender, RoutedEventArgs e)
        {
            QuizItem newQuiz = quizHandler.CreateQuiz(QuizNameTextBox.Text, QuizDescriptionTextBox.Text);
            if (quizHandler.AddQuiz(newQuiz))
            {
                quizes = new ObservableCollection<QuizItem>(quizHandler.quizManager.GetAllItems());
                QuizesListView.ItemsSource = quizes;
                MessageBoxes.ShowInformationMessageBox("Quiz Added");
            }
        }

        private void QuizesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (quizHandler.quizManager.Count > 0)
            {
                QuestionsAnswersTabControl.IsEnabled = true;
                AnswersTabItem.IsEnabled = false;
                int indexOfSelectedQuiz = QuizesListView.SelectedIndex;

                QuestionsOfSelectedQuiz = new ObservableCollection<Question>(quizHandler.quizManager.GetAt(indexOfSelectedQuiz).Questions.GetAllItems());
                QuestionsOfSelectedQuizListView.ItemsSource = QuestionsOfSelectedQuiz;
            }
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
                if (quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).RemoveQuestion(QuestionsOfSelectedQuizListView.SelectedIndex))
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

            if (opened)
            {
                try
                {
                    quizHandler.quizManager.XMLDeserialize(openFileDialog.FileName);
                    quizes = new ObservableCollection<QuizItem>(quizHandler.quizManager.GetAllItems());
                    QuizesListView.ItemsSource = quizes;
                    TransferQuestionsAndANswersToProgram();
                    MessageBoxes.ShowInformationMessageBox("The Created Questions where saved!");
                }
                catch (Exception exOpen)
                {
                    MessageBoxes.ShowErrorMessageBox($"{exOpen.Message} {exOpen.InnerException}");
                }
            }
        }

        private void TransferQuestionsAndANswersToProgram()
        {
            quizHandler.quizManager.GetAllItems().ForEach((x) =>
            {
                x.Questions.QuestionsXML.ForEach(q =>
                {
                    quizHandler.quizManager.GetAt(x.Id - 1).Questions.AddQuestionAfterLoad(q);
                });
                quizHandler.quizManager.GetAt(x.Id - 1).Questions.GetAllItems().ForEach(q =>
                {
                    x.Questions.GetAllItems().Where(lq => lq.Id == q.Id).First().Answers.AnswersXML.ForEach(a => q.Answers.AddAnswerAfterLoad(a));
                });

                ClearXMLListsAfterTransfer(x);
            });
        }

        private void ClearXMLListsAfterTransfer(QuizItem x)
        {
            x.Questions.QuestionsXML.Clear();

            quizHandler.quizManager.GetAt(x.Id - 1).Questions.GetAllItems().ForEach(q =>
            {
                x.Questions.GetAllItems().Where(lq => lq.Id == q.Id).First().Answers.AnswersXML.Clear();
            });
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

            if (QuestionsOfSelectedQuiz.Count > 0)
            {
                AnswersOfSelectedQuestion = new ObservableCollection<Answer>(QuestionsOfSelectedQuiz.Where(x => x.Id == indexOfSelectedQuestion + 1).FirstOrDefault().Answers.GetAllItems());
                AnswersOfSelectedQuestionListView.ItemsSource = AnswersOfSelectedQuestion;
                AnswersTabItem.IsEnabled = true;
                AnswersTabItem.IsSelected = true;
            }
        }

        private void DeleteQuiz_Click(object sender, RoutedEventArgs e)
        {
            if (QuizesListView.SelectedIndex >= 0)
            {
                if (quizHandler.RemoveQuiz(QuizesListView.SelectedIndex))
                {
                    quizes.RemoveAt(QuizesListView.SelectedIndex);
                    QuestionsOfSelectedQuiz.Clear();
                    QuestionsAnswersTabControl.IsEnabled = false;
                }
            }
            else
            {
                MessageBoxes.ShowInformationMessageBox("Please select a quiz to remove");
            }
        }

        private void ChangeQuiz_Click(object sender, RoutedEventArgs e)
        {
            GenericChangePopupUserControl popupCtrl = new GenericChangePopupUserControl();
            popupCtrl.TypeOfItemToChange = "Quiz";
            popupCtrl.HasItemDescription = true;
            popupCtrl.OldTitle = quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Title;
            popupCtrl.OldDescription = quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Description;
            popupCtrl.IsSaved += PopupCtrl_IsSaved;
            UcContainer.Children.Add(popupCtrl);

        }

        private void PopupCtrl_IsSaved(object sender, IsSavedEventArgs e)
        {
            ChangeQuizInformation(QuizesListView.SelectedIndex, e.NewTitle, e.NewDescription);
            UcContainer.Children.Remove(e.UserControl);
        }

        private void ChangeQuizInformation(int selectedIndex, string newTitle, string newDescription)
        {
            QuizItem changedQuiz = quizes.ElementAt(selectedIndex);
            changedQuiz.Title = newTitle;
            changedQuiz.Description = newDescription;

            quizHandler.ChangeQuiz(changedQuiz, selectedIndex);

            quizes = new ObservableCollection<QuizItem>(quizHandler.quizManager.GetAllItems());

            QuizesListView.ItemsSource = quizes;
        }
    }
}
