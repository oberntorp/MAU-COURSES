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

        /// <summary>
        /// The default constructor, initializing the component, as well as setting the dataContext and initializing QuizHandler
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            quizHandler = new QuizHandler();
            DataContext = this;
        }

        /// <summary>
        /// Event handler for the "Create Quiz" button
        /// </summary>
        /// <param name="sender">The object sending the request, in this case a button</param>
        /// <param name="e">The event arguments</param>
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

        /// <summary>
        /// Event handler for selecting a quiz, filles the questions of the selected question and enables the tabControl for Questions/Answers
        /// </summary>
        /// <param name="sender">The object sending the request, in this case a ListView</param>
        /// <param name="e">The event arguments</param>
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

        /// <summary>
        /// Event handler for the "Add question" button
        /// </summary>
        /// <param name="sender">The object sending the request, in this case a button</param>
        /// <param name="e">The event arguments</param>
        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            CreateQuestionWindow createQuestionWindow = new CreateQuestionWindow();
            bool result = (bool)createQuestionWindow.ShowDialog();
            if (result)
            {
                QuizItem associatedQuizItem = quizHandler.quizManager.GetAllItems().Where(x => x.Id == (QuizesListView.SelectedIndex + 1)).First();
                Question newQuestion = new Question(createQuestionWindow.QuestionTitle, associatedQuizItem);
                AddAnswersToQuestion(createQuestionWindow.Answers.ToList(), newQuestion);

                if (quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Questions.AddQuestion(newQuestion))
                {
                    QuestionsOfSelectedQuiz = new ObservableCollection<Question>(quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Questions.GetAllItems());
                    QuestionsOfSelectedQuizListView.ItemsSource = QuestionsOfSelectedQuiz;
                }
            }
        }

        /// <summary>
        /// After creating a new question in a separate window, this method receives the question and adds it to the program 
        /// </summary>
        /// <param name="answers">The answers, belonging to the newly created question</param>
        /// <param name="newQuestion">The new question</param>
        private void AddAnswersToQuestion(List<Answer> answers, Question newQuestion)
        {
            answers.ForEach(x =>
            {
                newQuestion.Answers.AddAnswer(x);
            });
        }

        /// <summary>
        /// Event handler for "Delete question" button
        /// </summary>
        /// <param name="sender">The object sending the request, in this case a button</param>
        /// <param name="e">The event arguments</param>
        private void DeleteQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionsOfSelectedQuizListView.SelectedIndex >= 0)
            {
                if (quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Questions.RemoveQuestion(QuestionsOfSelectedQuizListView.SelectedIndex))
                {
                    QuestionsOfSelectedQuiz.RemoveAt(QuestionsOfSelectedQuizListView.SelectedIndex);
                }
            }
            else
            {
                MessageBoxes.ShowInformationMessageBox("Please select a question to remove");
            }
        }

        /// <summary>
        /// Event handler for "Load from XML" menu item
        /// </summary>
        /// <param name="sender">The object sending the request, in this case a MenuItem</param>
        /// <param name="e">The event arguments</param>
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
                        TransferQuizesToProgram();
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

        /// <summary>
        /// Adds the newly loaded Quiz into the program
        /// </summary>
        private void TransferQuizesToProgram()
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

        /// <summary>
        /// Event handler for "Save to XML" MenuItem
        /// </summary>
        /// <param name="sender">The object sending the request, in this case a MenuItem</param>
        /// <param name="e">The event arguments</param>
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
                MessageBoxes.ShowInformationMessageBox("There is nothing to save yet, please create some quizes");
            }
        }

        /// <summary>
        /// Event handler for selecting a Question
        /// </summary>
        /// <param name="sender">The object sending the request, in this case a ListView</param>
        /// <param name="e">The event arguments</param>
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

        /// <summary>
        /// Event handler for "Delete quiz" button
        /// </summary>
        /// <param name="sender">The object sending the request, in this case a button</param>
        /// <param name="e">Event arguments</param>
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

        /// <summary>
        /// Event handler for "Change quiz" button
        /// </summary>
        /// <param name="sender">The object sending the request, in this case a button</param>
        /// <param name="e">Event arguments</param>
        private void ChangeQuizButton_Click(object sender, RoutedEventArgs e)
        {
            if (QuizesListView.SelectedIndex >= 0)
            {
                GenericChangePopupUserControl popupCtrl = new GenericChangePopupUserControl();
                popupCtrl.TypeOfAction = Mode.Edit;
                popupCtrl.TypeOfItemToHandle = TypeOfItemToChange.Quiz;
                popupCtrl.OldTitle = quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Title;
                popupCtrl.OldDescription = quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Description;
                popupCtrl.IsSavedEvent += (senderIsSaved, eIsSaved) =>
                {
                    ChangeQuizInformation(QuizesListView.SelectedIndex, eIsSaved.NewTitle, eIsSaved.NewDescription);
                    UcContainer.Children.Remove(eIsSaved.UserControl);
                };
                popupCtrl.InitializeGui();
                UcContainer.Children.Add(popupCtrl);
            }

        }

        /// <summary>
        /// The Method assists the ChangeQuizButtonHandler with changing the quiz
        /// </summary>
        /// <param name="selectedIndex">The index of the quiz to change</param>
        /// <param name="newTitle">The new title of the quiz</param>
        /// <param name="newDescription">The new description ofthequiz</param>
        private void ChangeQuizInformation(int selectedIndex, string newTitle, string newDescription)
        {
            QuizItem changedQuiz = quizes.ElementAt(selectedIndex);
            changedQuiz.Title = newTitle;
            changedQuiz.Description = newDescription;

            quizHandler.ChangeQuiz(changedQuiz, selectedIndex);

            quizes = new ObservableCollection<QuizItem>(quizHandler.quizManager.GetAllItems());
            QuizesListView.ItemsSource = quizes;
        }

        /// <summary>
        /// Event handler for "Edit quiz" button
        /// </summary>
        /// <param name="sender">The object sending the request, in this case a ListView</param>
        /// <param name="e">The event arguments</param>
        private void EditQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionsOfSelectedQuizListView.SelectedIndex >= 0)
            {
                GenericChangePopupUserControl popupCtrl = new GenericChangePopupUserControl();
                popupCtrl.TypeOfAction = Mode.Edit;
                popupCtrl.TypeOfItemToHandle = TypeOfItemToChange.Question;
                popupCtrl.OldTitle = QuestionsOfSelectedQuiz.Where(x => x.Id == QuestionsOfSelectedQuizListView.SelectedIndex + 1).FirstOrDefault().Title;
                popupCtrl.IsSavedEvent += (senderIsSaved, eIsSaved) =>
                {
                    ChangeQuestionInformation(QuestionsOfSelectedQuizListView.SelectedIndex, eIsSaved.NewTitle);
                    UcContainer.Children.Remove(eIsSaved.UserControl);
                };
                popupCtrl.InitializeGui();
                UcContainer.Children.Add(popupCtrl);
            }
            else
            {
                MessageBoxes.ShowInformationMessageBox("Please select an question to edit");
            }
        }

        /// <summary>
        /// The Method assists the ChangeQuestionButtonHandler with changing the question
        /// </summary>
        /// <param name="selectedIndexQuestion">The index of the question to change</param>
        /// <param name="newQuestion">The new Question</param>
        private void ChangeQuestionInformation(int selectedIndexQuestion, string newQuestion)
        {
            Question changedQuestion = QuestionsOfSelectedQuiz.ElementAt(selectedIndexQuestion);
            changedQuestion.Title = newQuestion;

            quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Questions.ChangeQuestion(changedQuestion, selectedIndexQuestion);

            QuestionsOfSelectedQuiz = new ObservableCollection<Question>(quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Questions.GetAllItems());
            QuestionsOfSelectedQuizListView.ItemsSource = QuestionsOfSelectedQuiz;
        }

        /// <summary>
        /// Event handler for the "Add answer" button
        /// </summary>
        /// <param name="sender">The object sending the request, in this case a button</param>
        /// <param name="e">The event arguments</param>
        private void AddAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            GenericChangePopupUserControl popupCtrl = new GenericChangePopupUserControl();
            popupCtrl.TypeOfAction = Mode.Add;
            popupCtrl.TypeOfItemToHandle = TypeOfItemToChange.Answer;
            popupCtrl.IsSavedEvent += (senderIsSaved, eIsSaved) =>
            {
                CreateNewAnswer(eIsSaved.NewTitle, eIsSaved.IsRightAnswer);
                AnswersOfSelectedQuestion = new ObservableCollection<Answer>(quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Questions.GetAt(QuestionsOfSelectedQuizListView.SelectedIndex).Answers.GetAllItems());
                AnswersOfSelectedQuestionListView.ItemsSource = AnswersOfSelectedQuestion;
                UcContainer.Children.Remove(eIsSaved.UserControl);
            };
            popupCtrl.InitializeGui();
            UcContainer.Children.Add(popupCtrl);
        }

        /// <summary>
        /// This method assists AddNewAnswerButtonHandler in creating an Answer
        /// </summary>
        /// <param name="newTitle">The title of the Answer</param>
        /// <param name="newIsRightAnswer">right answer of not (bool)</param>
        private void CreateNewAnswer(string newTitle, bool newIsRightAnswer)
        {
            Answer newAnswer = new Answer(newTitle, newIsRightAnswer);
            quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Questions.GetAt(QuestionsOfSelectedQuizListView.SelectedIndex).Answers.AddAnswer(newAnswer);
        }

        /// <summary>
        /// Event handler for "Edit answer" button
        /// </summary>
        /// <param name="sender">The object sending the request, in this case a button</param>
        /// <param name="e">The event  arguments</param>
        private void EditAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (AnswersOfSelectedQuestionListView.SelectedIndex >= 0)
            {
                GenericChangePopupUserControl popupCtrl = new GenericChangePopupUserControl();
                popupCtrl.TypeOfAction = Mode.Edit;
                popupCtrl.TypeOfItemToHandle = TypeOfItemToChange.Answer;
                popupCtrl.OldTitle = quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Questions.GetAt(QuestionsOfSelectedQuizListView.SelectedIndex).Answers.GetAt(AnswersOfSelectedQuestionListView.SelectedIndex).Title;
                popupCtrl.OldIsRightAnswer = quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Questions.GetAt(QuestionsOfSelectedQuizListView.SelectedIndex).Answers.GetAt(AnswersOfSelectedQuestionListView.SelectedIndex).RightAnswer;
                popupCtrl.IsSavedEvent += (senderIsSaved, eIsSaved) =>
                {
                    ChangeAnswerInformation(AnswersOfSelectedQuestionListView.SelectedIndex, eIsSaved.NewTitle);
                    UcContainer.Children.Remove(eIsSaved.UserControl);
                };
                popupCtrl.InitializeGui();
                UcContainer.Children.Add(popupCtrl);
            }
            else
            {
                MessageBoxes.ShowInformationMessageBox("Please select an answer to edit");
            }
        }

        /// <summary>
        /// This method assists the EditAnswerHandler
        /// </summary>
        /// <param name="selectedIndexAnswer">The index of the answer to change</param>
        /// <param name="newAnswer">The new answer</param>
        private void ChangeAnswerInformation(int selectedIndexAnswer, string newAnswer)
        {
            Answer changedAnswer = AnswersOfSelectedQuestion.ElementAt(selectedIndexAnswer);
            changedAnswer.Title = newAnswer;

            quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Questions.GetAt(QuestionsOfSelectedQuizListView.SelectedIndex).Answers.ChangeAnswer(changedAnswer, selectedIndexAnswer);

            AnswersOfSelectedQuestion = new ObservableCollection<Answer>(quizHandler.quizManager.GetAt(QuizesListView.SelectedIndex).Questions.GetAt(QuestionsOfSelectedQuizListView.SelectedIndex).Answers.GetAllItems());
            AnswersOfSelectedQuestionListView.ItemsSource = AnswersOfSelectedQuestion;
        }

        /// <summary>
        /// Event handler for "Delete answer" button
        /// </summary>
        /// <param name="sender">The object sending the request, in this case a button</param>
        /// <param name="e">The event arguments</param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataToSave() && (!dataSaved && MessageBoxes.ShowSaveWarningMessageBox("You have not saved, do you really want to close the application?") == MessageBoxResult.No))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool DataToSave()
        {
            return quizHandler.quizManager.Count > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowSearchFieldButton_Click(object sender, RoutedEventArgs e)
        {
            if (quizHandler.quizManager.Count > 0)
            {
                SearchTermTextBox.Visibility = Visibility.Visible;
                SearchGroupBox.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBoxes.ShowInformationMessageBox("There is nothing to search, please create some quizes.");
            }
        }

        /// <summary>
        /// Event handler for writing in the search text box
        /// </summary>
        /// <param name="sender">The object sending the request, in this case a text box</param>
        /// <param name="e">The event arguments</param>
        private void SearchTermTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            PerformSearch(GetTypeOfSearchFromRadioButtons()).ForEach(res => SearchResultsListBox.Items.Add(res));
        }

        /// <summary>
        /// From the radio buttons, get what to search in the Quizes
        /// </summary>
        /// <returns>What to search in in the Quizes</returns>
        private SearchMode GetTypeOfSearchFromRadioButtons()
        {
            if (SearchInQuizNameRaidoButton.IsChecked == true)
            {
                return SearchMode.QuizName;
            }
            else if (SearchInQuestionsRadioButton.IsChecked == true)
            {
                return SearchMode.Questions;
            }
            else
            {
                return SearchMode.Answers;
            }
        }

        /// <summary>
        /// Performs a search
        /// </summary>
        /// <param name="searchIn">What to search in in the Quizes</param>
        private List<QuizItem> PerformSearch(SearchMode searchIn)
        {
            return quizHandler.SearchQuizes(SearchTermTextBox.Text, searchIn);
        }
    }
}
