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
        public int currentQuestionIndex;
        public string title;
        public List<Question> questions;

        public Survey(string _title)
        {
            currentQuestionIndex = 0;
            title = _title;
            questions = new List<Question>();
        }

        public enum QuestionType
        {
            YES_OR_NO,
            ABCD
        }

        public void AddQuestion(string title, QuestionType type)
        {
            questions.Add(new Question(title, type));
        }

        public class Question
        {
            public string questionTitle;
            public QuestionType questionType;
            public List<KeyValuePair<string, uint>> answersValues;

            public Question(string _questionTitle, QuestionType _questionType)
            {     
                questionTitle = _questionTitle;
                questionType = _questionType;

                answersValues = new List<KeyValuePair<string, uint>>();
                if (questionType == QuestionType.YES_OR_NO)
                {
                    AddAnswer("Nie", 0);
                    AddAnswer("Tak", 1);
                }
            }

            public void AddAnswer(string answerText, uint answerValue)
            {
                KeyValuePair<string, uint> newPair = new KeyValuePair<string, uint>(answerText, answerValue);
                answersValues.Add(newPair);
            }
        }      
    }
}
