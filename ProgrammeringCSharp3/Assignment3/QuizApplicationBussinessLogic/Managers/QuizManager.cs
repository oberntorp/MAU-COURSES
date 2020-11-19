using QuizApplicationBussinessLogic.QuizClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplicationBussinessLogic.Managers
{
    /// <summary>
    /// Quiz Manager handling quizes
    /// </summary>
    public class QuizManager: ListManager<QuizItem>
    {
        private int quizId = 1;

        /// <summary>
        /// Default constructor
        /// </summary>
        public QuizManager()
        {

        }

        /// <summary>
        /// Adds a Quiz to the manager
        /// </summary>
        /// <param name="quizToAdd">The Quiz to add</param>
        /// <returns>true/false for success/failure</returns>
        public bool AddQuiz(QuizItem quizToAdd)
        {
            AddIdToQuiz(ref quizToAdd);
            return Add(quizToAdd);
        }

        /// <summary>
        /// Adds an id to the quiz before it is added
        /// </summary>
        /// <param name="QuizToAddAnId">The quiz getting an id (ref parameter)</param>
        private void AddIdToQuiz(ref QuizItem QuizToAddAnId)
        {
            if (Count == 0)
            {
                QuizToAddAnId.Id = quizId++;
            }
            else
            {
                QuizToAddAnId.Id = Count + 1;
            }
        }

        /// <summary>
        /// Change a quiz at a given index
        /// </summary>
        /// <param name="changedQuiz">The changed quiz</param>
        /// <param name="indexToChangeAt">The index getting the changed index</param>
        /// <returns>true/false for success/failure</returns>
        public bool ChangeQuiz(QuizItem changedQuiz, int indexToChangeAt)
        {
            return ChangeAt(changedQuiz, indexToChangeAt);
        }

        /// <summary>
        /// Removes a quiz at a given index
        /// </summary>
        /// <param name="indexOfQuizToRemove">The index of the answer to remove</param>
        /// <returns>true/false for success/failure</returns>
        public bool RemoveQuiz(int indexOfQuizToRemove)
        {
            quizId--;
            return DeleteAt(indexOfQuizToRemove);
        }
    }
}
