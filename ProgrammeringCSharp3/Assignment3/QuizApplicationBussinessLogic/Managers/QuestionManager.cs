using QuizApplicationBussinessLogic.QuizClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplicationBussinessLogic.Managers
{
    /// <summary>
    /// The Manager handles Questions
    /// </summary>
    [Serializable]
    public class QuestionManager : ListManager<Question>
    {
        private int questionId = 1;

        public List<Question> QuestionsXML { get; set; }

        /// <summary>
        /// Default constructor, needed for serialization to work, initializes QuestionsXML, which acts as a interface for the serializer to read/write
        /// </summary>
        public QuestionManager()
        {
            QuestionsXML = new List<Question>();
        }

        /// <summary>
        /// Adds a question to the manager
        /// </summary>
        /// <param name="questionToAdd">the question being added</param>
        /// <returns>true/false for success/failure</returns>
        public bool AddQuestion(Question questionToAdd)
        {
            AddIdToQuestion(ref questionToAdd);
            if(Add(questionToAdd))
            {
                QuestionsXML.Add(questionToAdd);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds a question after deserialization, with the difference that it is not added to the QuestionsXML as it is already populated with these questions
        /// </summary>
        /// <param name="questionToAdd">The question being added</param>
        /// <returns>true/false for success/failure</returns>
        public bool AddQuestionAfterLoad(Question questionToAdd)
        {
            AddIdToQuestion(ref questionToAdd);
            return Add(questionToAdd);
        }

        /// <summary>
        /// Adds an id to the Question being added before adding it to the Manager
        /// </summary>
        /// <param name="QuestionToAddAnId">The question getting an id (ref parameter)</param>
        private void AddIdToQuestion(ref Question QuestionToAddAnId)
        {
            if (Count == 0)
            {
                QuestionToAddAnId.Id = questionId++;
            }
            else
            {
                QuestionToAddAnId.Id = Count + 1;
            }
        }

        /// <summary>
        /// Change a question at a given index
        /// </summary>
        /// <param name="changedQuestion">The changed question</param>
        /// <param name="indexToChangeAt">The index getting the changed question at</param>
        /// <returns>true/false for success/failure</returns>
        public bool ChangeQuestion(Question changedQuestion, int indexToChangeAt)
        {
            return ChangeAt(changedQuestion, indexToChangeAt);
        }

        /// <summary>
        /// Removes a question from the manager
        /// </summary>
        /// <param name="indexOfQuestionToRemove">The index of the question to remove</param>
        /// <returns>true/false for success/failure</returns>
        /// <returns>true/false for success/failure</returns>
        public bool RemoveQuestion(int indexOfQuestionToRemove)
        {
            questionId--;
            return DeleteAt(indexOfQuestionToRemove);
        }
    }
}
