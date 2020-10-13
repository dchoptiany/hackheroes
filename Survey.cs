using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace app
{
    class Survey
    {
        public enum QuestionType
        {
            YES_OR_NO,
            INPUT_VALUE
        }

        struct Question
        {
            public Question(string _questionTitle, QuestionType _questionType)
            {
                questionTitle = _questionTitle;
                questionType = _questionType;
            }

            string questionTitle;
            QuestionType questionType;
        }

        private List<Question> questions;

        public Survey()
        {
            questions = new List<Question>();
        }

        public void AddQuestion(string title, QuestionType type)
        {
            questions.Add(new Question(title, type));
        }
    }
}
