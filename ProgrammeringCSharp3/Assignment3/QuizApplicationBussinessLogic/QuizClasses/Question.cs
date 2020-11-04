using QuizApplicationBussinessLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplicationBussinessLogic.QuizClasses
{
    [Serializable]
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public AnswerManager Answers { get; set; }

        public Question()
        {
              
        }

        public Question(string title)
        {
            Title = title;
            Answers = new AnswerManager();
        }

        public bool AddAnswer(Answer answerToAdd)
        {
            return Answers.Add(answerToAdd);
        }

        public bool RemoveAnswer(int indexOfAnswerToRemove)
        {
            return Answers.RemoveAnswer(indexOfAnswerToRemove);
        }
    }
}
