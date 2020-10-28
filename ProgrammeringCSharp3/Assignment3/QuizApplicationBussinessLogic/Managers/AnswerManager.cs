using QuizApplicationBussinessLogic.QuizClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplicationBussinessLogic.Managers
{
    public class AnswerManager: ListManager<Answer>
    {
        private int answerId = 1;

        public AnswerManager()
        {

        }

        public bool AddQuestion(Answer answerToAdd)
        {
            AddIdToAnswer(ref answerToAdd);
            return Add(answerToAdd);
        }

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

        public bool ChangeQuiz(Answer changedAnswer, int indexToChangeAt)
        {
            return ChangeAt(changedAnswer, indexToChangeAt);
        }

        public bool RemoveAnswer(int indexOfAnswerToRemove)
        {
            answerId--;
            return DeleteAt(indexOfAnswerToRemove);
        }
    }
}
