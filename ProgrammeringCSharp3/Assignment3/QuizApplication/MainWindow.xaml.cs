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
        List<Question> QuestionsOfSelectedQuiz;
        public MainWindow()
        {
            InitializeComponent();
            quizHandler = new QuizHandler();
            quizes = new List<QuizItem>();
        }

        private void CreateQuizButton_Click(object sender, RoutedEventArgs e)
        {
            QuizItem newQuiz = quizHandler.CreateQuiz(QuizNameTextBox.Text, QuizDescriptionTextBox.Text);
            if(quizHandler.AddQuiz(newQuiz))
            {
                MessageBox.Show("QuizAdded");
                QuizesListView.ItemsSource = quizHandler.quizManager.GetAllItems();
            }
        }
    }
}
