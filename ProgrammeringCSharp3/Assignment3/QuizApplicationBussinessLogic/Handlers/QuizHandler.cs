using QuizApplicationBussinessLogic.Managers;
using QuizApplicationBussinessLogic.QuizClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
