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
        public string title;
        public List<Question> questions;

        public enum QuestionType
        {
            YES_OR_NO,
            INPUT_VALUE,
            ABCD
        }

        public struct Question
        {
            public string questionTitle;
            public QuestionType questionType;

            public Question(string _questionTitle, QuestionType _questionType)
            {
                questionTitle = _questionTitle;
                questionType = _questionType;
            }

            public struct Answers
            {
                public List<KeyValuePair<string, uint>> answersValues;

                public Answers(List<KeyValuePair<string, uint>> _answersValues)
                {
                    answersValues = _answersValues;
                }
            }

        }      

        public Survey(string _title)
        {
            title = _title;
            questions = new List<Question>();
        }

        public void AddQuestion(string title, QuestionType type)
        {
            questions.Add(new Question(title, type));
        }
    }
}
