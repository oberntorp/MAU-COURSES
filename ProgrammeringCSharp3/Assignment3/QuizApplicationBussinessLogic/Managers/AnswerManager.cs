using QuizApplicationBussinessLogic.QuizClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplicationBussinessLogic.Managers
{
    /// <summary>
    /// The Manager handling Answers
    /// </summary>
    [Serializable]
    public class AnswerManager: ListManager<Answer>
    {
        private int answerId = 1;
        public List<Answer> AnswersXML { get; set; }

        /// <summary>
        /// Default constructor, needed for serialization to work, initializes AnswersXML, which acts as a interface for the serializer to read/write
        /// </summary>
        public AnswerManager()
        {
            AnswersXML = new List<Answer>();
        }

        /// <summary>
        /// Adds an answer
        /// </summary>
        /// <param name="answerToAdd">The answer being added</param>
        /// <returns>true/false for success/failure</returns>
        public bool AddAnswer(Answer answerToAdd)
        {
            AddIdToAnswer(ref answerToAdd);

            if(Add(answerToAdd))
            {
                AnswersXML.Add(answerToAdd);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds an id to the Answer being added before adding it to the Manager
        /// </summary>
        /// <param name="answerToAddAnId">The answer getting an id (ref parameter)</param>
        private void AddIdToAnswer(ref Answer answerToAddAnId)
        {
            if (Count == 0)
            {
                answerToAddAnId.Id = answerId++;
            }
            else
            {
                answerToAddAnId.Id = Count + 1;
            }
        }

        /// <summary>
        /// Changes an Answer at a given index
        /// </summary>
        /// <param name="changedAnswer">The Answer to change</param>
        /// <param name="indexToChangeAt">The index getting the changed Answer</param>
        /// <returns>true/false for success/failure</returns>
        public bool ChangeAnswer(Answer changedAnswer, int indexToChangeAt)
        {
            return ChangeAt(changedAnswer, indexToChangeAt);
        }

        /// <summary>
        /// Removes an Answer for the manager
        /// </summary>
        /// <param name="indexOfAnswerToRemove">Index of the answer to remove</param>
        /// <returns>true/false for success/failure</returns>
        public bool RemoveAnswer(int indexOfAnswerToRemove)
        {
            answerId--;
            return DeleteAt(indexOfAnswerToRemove);
        }

        /// <summary>
        /// Adds an answer after deserialization, with the difference that it is not added to the AnswersXML as it is already populated with these answers
        /// </summary>
        /// <param name="answerToAdd">The answer being added</param>
        /// <returns>true/false for success/failure</returns>
        public bool AddAnswerAfterLoad(Answer answerToAdd)
        {
            AddIdToAnswer(ref answerToAdd);
            return Add(answerToAdd);
        }
    }
}
