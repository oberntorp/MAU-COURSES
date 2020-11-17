using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplicationBussinessLogic.QuizClasses
{
    /// <summary>
    /// The class making up an Answer
    /// </summary>
    [Serializable]
    public class Answer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool RightAnswer { get; set; }

        /// <summary>
        /// Default constructor, needed for the serialization to work
        /// </summary>
        public Answer()
        {
             
        }

        /// <summary>
        /// The constructor used when creating an answer
        /// </summary>
        /// <param name="title">The answers title</param>
        /// <param name="rightAnswer">If the answer is right, defaults to false</param>
        public Answer(string title, bool rightAnswer = false): this()
        {
            Title = title;
            RightAnswer = rightAnswer;
        }
    }
}
