using QuizApplicationBussinessLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplicationBussinessLogic.QuizClasses
{
    /// <summary>
    /// The class making up a quizItem
    /// </summary>
    [Serializable]
    public class QuizItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public QuestionManager Questions { get; set; }

        /// <summary>
        /// The default constructor needed for serialization to work
        /// </summary>
        public QuizItem()
        {

        }

        /// <summary>
        /// The constructor used when creating a question
        /// </summary>
        /// <param name="title">The title of the quiz</param>
        /// <param name="description">The description of the quiz</param>
        public QuizItem(string title, string description)
        {
            Title = title;
            Description = description;
            Questions = new QuestionManager();
        }
    }
}
