using QuizApplicationBussinessLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplicationBussinessLogic.QuizClasses
{
    /// <summary>
    /// THe class making up a question
    /// </summary>
    [Serializable]
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public AnswerManager Answers { get; set; }

        /// <summary>
        /// The default constructor needed for the serialization to work
        /// </summary>
        public Question()
        {
              
        }

        /// <summary>
        /// THe constructor used when creating a question
        /// </summary>
        /// <param name="title">The title of the question</param>
        public Question(string title)
        {
            Title = title;
            Answers = new AnswerManager();
        }
    }
}
