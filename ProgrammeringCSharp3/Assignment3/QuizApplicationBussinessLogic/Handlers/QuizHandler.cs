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
    /// <summary>
    /// This handler is used by the UI to use the QuizClasses, (Adding Quiz, and the QuizManager handles other actions like adding Questions and alike)
    /// This class meinly focuses on Add/Change/Delete a Quiz and search
    /// </summary>
    public class QuizHandler
    {
        public QuizManager quizManager;

        /// <summary>
        /// The default constructor initializing the QuizManager
        /// </summary>
        public QuizHandler()
        {
            quizManager = new QuizManager();
        }

        /// <summary>
        /// Creates a new QuizItem object with the provided title and description
        /// </summary>
        /// <param name="title">The title given to the new qiuz</param>
        /// <param name="description">The description given to the new qiuz</param>
        /// <returns>The newly created QuizItem object</returns>
        public QuizItem CreateQuiz(string title, string description)
        {
            return new QuizItem(title, description);
        }

        /// <summary>
        /// Adds a given QuizItem to the quizManager
        /// </summary>
        /// <param name="quizToAdd">The quizItem being added</param>
        /// <returns>true/false for success/feilure</returns>
        public bool AddQuiz(QuizItem quizToAdd)
        {
            return quizManager.Add(quizToAdd);
        }

        /// <summary>
        /// Changes a QuizItem at a given index
        /// </summary>
        /// <param name="quizBeingChanged">The quizItem to change</param>
        /// <param name="indexToChangeAt">The position to put the changed QuizItem</param>
        /// <returns>true/false for success/feilure</returns>
        public bool ChangeQuiz(QuizItem quizBeingChanged, int indexToChangeAt)
        {
            return quizManager.ChangeQuiz(quizBeingChanged, indexToChangeAt);
        }

        /// <summary>
        /// Removes a QuizItem from the quizManager
        /// </summary>
        /// <param name="indexOfQuizToRemove">The index from which to remove the QuizItem</param>
        /// <returns>true/false for success/feilure</returns>
        public bool RemoveQuiz(int indexOfQuizToRemove)
        {
            return quizManager.RemoveQuiz(indexOfQuizToRemove);
        }

        /// <summary>
        /// Serializes (saves) the items in the QuizManager inte XML
        /// </summary>
        /// <param name="filePath">The path where the XML is saved</param>
        public void SerializeToXML(string filePath)
        {
            quizManager.XMLSerialize(filePath);
        }

        /// <summary>
        /// Deserializes (loads) the items in the XML into classes of the application
        /// </summary>
        /// <param name="filePath">The path to deserialize the XML from</param>
        public void DeserializeFromXML(string filePath)
        {
            quizManager.XMLDeserialize(filePath);
        }

        /// <summary>
        /// Search the collection of quizes
        /// </summary>
        /// <param name="searchTerm">The search term used in the search</param>
        /// <param name="baseSearchOn">Based on different seach modes the search will happen in different forms</param>
        /// <returns>Object (The search methods returns different things)</returns>
        public object SearchQuizes(string searchTerm, SearchMode baseSearchOn)
        {
            switch(baseSearchOn)
            {
                case SearchMode.QuizName:
                    return SearchQuizesBasedOnQuizName(searchTerm);
                case SearchMode.Questions:
                    return SearchQuizesBasedOnQuestions(searchTerm);
                default:
                    return SearchQuizesBasedOnAnswers(searchTerm);
            }
        }

        /// <summary>
        /// The search method for search mode "QuizName"
        /// </summary>
        /// <param name="searchTerm">The term used in the search</param>
        /// <returns>List of QuizItems</returns>
        private List<QuizItem> SearchQuizesBasedOnQuizName(string searchTerm)
        {
            return (from quiz in quizManager.GetAllItems() where quiz.Title.Contains(searchTerm) select quiz).ToList();
        }

        /// <summary>
        /// The search method for search mode "Question"
        /// </summary>
        /// <param name="searchTerm">The term used in the search</param>
        /// <returns>List of Question</returns>
        private List<Question> SearchQuizesBasedOnQuestions(string searchTerm)
        {
            var questions = (from quiz in quizManager.GetAllItems() select new { questions = from question in quiz.Questions.GetAllItems() where question.Title.Contains(searchTerm) select question });
            return questions.SelectMany(q => q.questions.Where(x => x.Title.Contains(searchTerm))).ToList();
        }

        /// <summary>
        /// The search method for search mode "Answer"
        /// </summary>
        /// <param name="searchTerm">The term used in the search</param>
        /// <returns>List of Answer</returns>
        private List<Answer> SearchQuizesBasedOnAnswers(string searchTerm)
        {
            var questions = (from quiz in quizManager.GetAllItems() select new { questions = from question in quiz.Questions.GetAllItems() select question }).ToList();
            return questions.SelectMany(q1 => q1.questions.SelectMany(q2 => q2.Answers.GetAllItems().Where(a => a.Title.Contains(searchTerm)))).ToList();
        }
    }
}
