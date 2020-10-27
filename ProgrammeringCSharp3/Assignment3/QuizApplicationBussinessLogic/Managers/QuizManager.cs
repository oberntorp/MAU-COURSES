using QuizApplicationBussinessLogic.QuizClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplicationBussinessLogic.Managers
{
    public class QuizManager: ListManager<QuizItem>
    {
        private int quizId = 1;
        public bool AddQuiz(QuizItem quizToAdd)
        {
            AddIdToQuiz(ref quizToAdd);
            return Add(quizToAdd);
        }

        public bool RemoveQuiz(int indexOfQuizToRemove)
        {
            quizId--;
            return DeleteAt(indexOfQuizToRemove);
        }

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
    }
}
