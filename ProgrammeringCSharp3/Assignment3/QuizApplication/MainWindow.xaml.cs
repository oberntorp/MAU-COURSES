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
using Utilities.Enums;

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
        bool dataSaved = false;
        public MainWindow()
        {
            InitializeComponent();
            quizHandler = new QuizHandler();
            DataContext = this;
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

        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
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

        private void DeleteQuestionButton_Click(object sender, RoutedEventArgs e)
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
            if (quizHandler.quizManager.Count == 0 || (quizHandler.quizManager.Count > 0 && WantToContinueWithoutSaving()))
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
                        MessageBoxes.ShowInformationMessageBox("The Created questions where saved!");
                    }
                    catch (Exception exOpen)
                    {
                        MessageBoxes.ShowErrorMessageBox($"{exOpen.Message} {exOpen.InnerException}");
                    }
                }
            }
        }

        /// <summary>
        /// Asks theuser if they want to continue without saving
        /// </summary>
        /// <returns>true/false</returns>
        private static bool WantToContinueWithoutSaving()
        {
            return (MessageBoxes.ShowSaveWarningMessageBox("Your current work will be lost, please save them first. Do you want to continue?") == MessageBoxResult.Yes);
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
            });
        }

        private void SaveToXMLMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (quizHandler.quizManager.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "XML File | *.XML";
                bool saved = (bool)saveFileDialog.ShowDialog();

                if (saved)
                {
                    try
                    {
                        quizHandler.quizManager.XMLSerialize(saveFileDialog.FileName);
                        dataSaved = true;
                        MessageBoxes.ShowInformationMessageBox("The Questions where saved");
                    }
                    catch (Exception exSave)
                    {
                        MessageBoxes.ShowErrorMessageBox($"{exSave.Message} {exSave.InnerException}");
                    }
                }
            }
            else
            {
                MessageBoxes.ShowInformationMessageBox("There is nothing to save yet, create some quizes");
            }
        }

        private void QuestionsOfSelectedQuizListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indexOfSelectedQuestion = QuestionsOfSelectedQuizListView.SelectedIndex;

            if (indexOfSelectedQuestion >= 0)
            {
                AnswersOfSelectedQuestion = new ObservableCollection<Answer>(QuestionsOfSelectedQuiz.Where(x => x.Id == indexOfSelectedQuestion + 1).FirstOrDefault().Answers.GetAllItems());
                AnswersOfSelectedQuestionListView.ItemsSource = AnswersOfSelectedQuestion;
                AnswersTabItem.IsEnabled = true;
                AnswersTabItem.IsSelected = true;
            }
        }

        private void DeleteQuizButton_Click(object sender, RoutedEventArgs e)
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

        private void ChangeQuizButton_Click(object sender, RoutedEventArgs e)
        {
            if (QuizesListView.SelectedIndex >= 0)
            {
                GenericChangePopupUserControl popupCtrl = new GenericChangePopupUserControl();
                popupCtrl.TypeOfAction = TypeOfAction.Edit;
                popupCtrl.TypeOfItemToHandle = TypeOfItemToChange.Quiz;
                popupCtrl.OldTitle = quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Title;
                popupCtrl.OldDescription = quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Description;
                popupCtrl.IsSaved += PopupCtrl_IsSavedQuizHandler;
                popupCtrl.InitializeGui();
                UcContainer.Children.Add(popupCtrl);
            }

        }

        private void PopupCtrl_IsSavedQuizHandler(object sender, IsSavedEventArgs e)
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

        private void EditQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionsOfSelectedQuizListView.SelectedIndex >= 0)
            {
                GenericChangePopupUserControl popupCtrl = new GenericChangePopupUserControl();
                popupCtrl.TypeOfAction = TypeOfAction.Edit;
                popupCtrl.TypeOfItemToHandle = TypeOfItemToChange.Question;
                popupCtrl.OldTitle = QuestionsOfSelectedQuiz.Where(x => x.Id == QuestionsOfSelectedQuizListView.SelectedIndex + 1).FirstOrDefault().Title;
                popupCtrl.IsSaved += PopupCtrl_IsSavedQuestionHandler;
                popupCtrl.InitializeGui();
                UcContainer.Children.Add(popupCtrl);
            }
            else
            {
                MessageBoxes.ShowInformationMessageBox("Please select an question to edit");
            }
        }

        private void PopupCtrl_IsSavedQuestionHandler(object sender, IsSavedEventArgs e)
        {
            ChangeQuestionInformation(QuestionsOfSelectedQuizListView.SelectedIndex, e.NewTitle);
            UcContainer.Children.Remove(e.UserControl);
        }

        private void ChangeQuestionInformation(int selectedIndexQuestion, string newQuestion)
        {
            Question changedQuestion = QuestionsOfSelectedQuiz.ElementAt(selectedIndexQuestion);
            changedQuestion.Title = newQuestion;

            quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Questions.ChangeAt(changedQuestion, selectedIndexQuestion);

            QuestionsOfSelectedQuiz = new ObservableCollection<Question>(quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Questions.GetAllItems());
            QuestionsOfSelectedQuizListView.ItemsSource = QuestionsOfSelectedQuiz;
        }

        private void AddAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            GenericChangePopupUserControl popupCtrl = new GenericChangePopupUserControl();
            popupCtrl.TypeOfAction = TypeOfAction.Add;
            popupCtrl.TypeOfItemToHandle = TypeOfItemToChange.Answer;
            popupCtrl.IsSaved += PopupCtrl_IsSavedAnswerHandler;
            popupCtrl.InitializeGui();
            UcContainer.Children.Add(popupCtrl);
        }

        private void PopupCtrl_IsSavedAnswerHandler(object sender, IsSavedEventArgs e)
        {
            CreateNewAnswer(e.NewTitle, e.IsRightAnswer);
            AnswersOfSelectedQuestion = new ObservableCollection<Answer>(quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Questions.GetAt(QuestionsOfSelectedQuizListView.SelectedIndex).Answers.GetAllItems());
            AnswersOfSelectedQuestionListView.ItemsSource = AnswersOfSelectedQuestion;
            UcContainer.Children.Remove(e.UserControl);
        }

        private void CreateNewAnswer(string newTitle, bool newIsRightAnswer)
        {
            Answer newAnswer = new Answer(newTitle, newIsRightAnswer);
            quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Questions.GetAt(QuestionsOfSelectedQuizListView.SelectedIndex).Answers.AddAnswer(newAnswer);
        }

        private void EditAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (AnswersOfSelectedQuestionListView.SelectedIndex >= 0)
            {
                GenericChangePopupUserControl popupCtrl = new GenericChangePopupUserControl();
                popupCtrl.TypeOfAction = TypeOfAction.Edit;
                popupCtrl.TypeOfItemToHandle = TypeOfItemToChange.Answer;
                popupCtrl.OldTitle = quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Questions.GetAt(QuestionsOfSelectedQuizListView.SelectedIndex).Answers.GetAt(AnswersOfSelectedQuestionListView.SelectedIndex).Title;
                popupCtrl.OldIsRightAnswer = quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Questions.GetAt(QuestionsOfSelectedQuizListView.SelectedIndex).Answers.GetAt(AnswersOfSelectedQuestionListView.SelectedIndex).RightAnswer;
                popupCtrl.IsSaved += PopupCtrl_IsEditAnswerSaved;
                popupCtrl.InitializeGui();
                UcContainer.Children.Add(popupCtrl);
            }
            else
            {
                MessageBoxes.ShowInformationMessageBox("Please select an answer to edit");
            }
        }

        private void PopupCtrl_IsEditAnswerSaved(object sender, IsSavedEventArgs e)
        {
            ChangeAnswerInformation(AnswersOfSelectedQuestionListView.SelectedIndex, e.NewTitle);
            UcContainer.Children.Remove(e.UserControl);
        }

        private void ChangeAnswerInformation(int selectedIndexAnswer, string newAnswer)
        {
            Answer changedAnswer = AnswersOfSelectedQuestion.ElementAt(selectedIndexAnswer);
            changedAnswer.Title = newAnswer;

            quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Questions.GetAt(QuestionsOfSelectedQuizListView.SelectedIndex).Answers.ChangeAt(changedAnswer, selectedIndexAnswer);

            AnswersOfSelectedQuestion = new ObservableCollection<Answer>(quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Questions.GetAt(QuestionsOfSelectedQuizListView.SelectedIndex).Answers.GetAllItems());
            AnswersOfSelectedQuestionListView.ItemsSource = AnswersOfSelectedQuestion;
        }

        private void DeleteAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (QuizesListView.SelectedIndex >= 0)
            {
                if (quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Questions.GetAt(QuestionsOfSelectedQuizListView.SelectedIndex).Answers.RemoveAnswer(AnswersOfSelectedQuestionListView.SelectedIndex))
                {
                    AnswersOfSelectedQuestion.RemoveAt(AnswersOfSelectedQuestionListView.SelectedIndex);
                }
            }
            else
            {
                MessageBoxes.ShowInformationMessageBox("Please select an answer to remove");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataToSave() && (!dataSaved && MessageBoxes.ShowSaveWarningMessageBox("You have not saved, do you really want to close the application?") == MessageBoxResult.No))
            {
                e.Cancel = true;
            }
        }

        private bool DataToSave()
        {
            return quizHandler.quizManager.Count > 0;
        }

        private void ShowSearchFieldButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTermTextBox.Visibility = Visibility.Visible;
        }

        private void SearchTermTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            PerformSearch(GetTypeOfSearchFromRadioButtons());
        }

        private BaseSearchOn GetTypeOfSearchFromRadioButtons()
        {
            if (SearchInQuizNameRaidoButton.IsChecked == true)
            {
                return BaseSearchOn.QuizName;
            }
            else if (SearchInQuestions.IsChecked == true)
            {
                return BaseSearchOn.Questions;
            }
            else
            {
                return BaseSearchOn.Answers;
            }
        }

        private void PerformSearch(BaseSearchOn searchIn)
        {
            quizHandler.SearchQuizes(SearchTermTextBox.Text, searchIn);
        }
    }
}
