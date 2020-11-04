using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplicationBussinessLogic.QuizClasses
{
    [Serializable]
    public class Answer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool RightAnswer { get; set; }

        public Answer()
        {
             
        }

        public Answer(string title, bool rightAnswer = false): this()
        {
            Title = title;
            RightAnswer = rightAnswer;
        }
    }
}
