using QuizApplicationBussinessLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplicationBussinessLogic.QuizClasses
{
    public class QuizItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public QuestionManager Questions { get; set; }
        public QuizItem(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
