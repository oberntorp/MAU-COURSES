using QuizApplicationBussinessLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplicationBussinessLogic.QuizClasses
{
    [Serializable]
    public class QuizItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public QuestionManager Questions { get; set; }

        public QuizItem()
        {

        }

        public QuizItem(string title, string description)
        {
            Title = title;
            Description = description;
            Questions = new QuestionManager();
        }

        public bool RemoveQuestion(int indexOfQuestionToRemove)
        {
            return Questions.RemoveQuestion(indexOfQuestionToRemove);
        }
    }
}
