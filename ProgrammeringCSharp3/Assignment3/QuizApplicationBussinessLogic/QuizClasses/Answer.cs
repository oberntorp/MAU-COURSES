using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplicationBussinessLogic.QuizClasses
{
    public class Answer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool RightAnswer { get; set; }
        public Answer(string title, bool rightAnswer = false)
        {
            Title = title;
            RightAnswer = rightAnswer;
        }
    }
}
