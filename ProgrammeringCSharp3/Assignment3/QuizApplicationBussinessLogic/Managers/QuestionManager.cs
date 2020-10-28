using QuizApplicationBussinessLogic.QuizClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplicationBussinessLogic.Managers
{
    public class QuestionManager : ListManager<Question>
    {
        private int questionId = 1;

        public QuestionManager()
        {

        }

        public bool AddQuestion(Question questionToAdd)
        {
            AddIdToQuestion(ref questionToAdd);
            return Add(questionToAdd);
        }

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

        public bool ChangeQuestion(Question changedQuestion, int indexToChangeAt)
        {
            return ChangeAt(changedQuestion, indexToChangeAt);
        }

        public bool RemoveQuestion(int indexOfQuestionToRemove)
        {
            questionId--;
            return DeleteAt(indexOfQuestionToRemove);
        }
    }
}
