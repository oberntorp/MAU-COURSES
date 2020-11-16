using QuizApplicationBussinessLogic.Managers;
using QuizApplicationBussinessLogic.QuizClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Enums;

namespace QuizApplicationBussinessLogic.Handlers
{
    public class QuizHandler
    {
        public QuizManager quizManager;
        public QuizHandler()
        {
            quizManager = new QuizManager();
        }

        public QuizItem CreateQuiz(string title, string description)
        {
            return new QuizItem(title, description);
        }

        public bool AddQuiz(QuizItem quizToAdd)
        {
            return quizManager.Add(quizToAdd);
        }

        public bool ChangeQuiz(QuizItem quizToAdd, int indexToChangeAt)
        {
            return quizManager.ChangeAt(quizToAdd, indexToChangeAt);
        }

        public bool RemoveQuiz(int indexOfQuizToRemove)
        {
            return quizManager.RemoveQuiz(indexOfQuizToRemove);
        }

        public void SerializeToXML(string filePath)
        {
            quizManager.XMLSerialize(filePath);
        }

        public void DeserializeFromXML(string filePath)
        {
            quizManager.XMLDeserialize(filePath);
        }

        public object SearchQuizes(string text, BaseSearchOn baseSearchOn)
        {
            switch(baseSearchOn)
            {
                case BaseSearchOn.QuizName:
                    return SearchQuizesBasedOnQuizName(text);
                case BaseSearchOn.Questions:
                    return SearchQuizesBasedOnQuestions(text);
                default:
                    return SearchQuizesBasedOnAnswers(text);
            }
        }

        private List<QuizItem> SearchQuizesBasedOnQuizName(string searchTerm)
        {
            return (from quiz in quizManager.GetAllItems() where quiz.Title.Contains(searchTerm) select quiz).ToList();
        }

        private List<Question> SearchQuizesBasedOnQuestions(string searchTerm)
        {
            var questions = (from quiz in quizManager.GetAllItems() select new { questions = from question in quiz.Questions.GetAllItems() where question.Title.Contains(searchTerm) select question });
            return questions.SelectMany(q => q.questions.Where(x => x.Title.Contains(searchTerm))).ToList();
        }

        private List<Answer> SearchQuizesBasedOnAnswers(string searchTerm)
        {
            var questions = (from quiz in quizManager.GetAllItems() select new { questions = from question in quiz.Questions.GetAllItems() select question }).ToList();
            return questions.SelectMany(q1 => q1.questions.SelectMany(q2 => q2.Answers.GetAllItems().Where(a => a.Title.Contains(searchTerm)))).ToList();
        }
    }
}
