using System.Collections.Generic;

namespace app
{
    public enum QuestionType
    {
        YES_OR_NO,
        ABCD,
        INPUT
    }

    class Survey
    {
        public class SurveyQuestion
        {
            public string questionTitle { get; set; }
            public QuestionType questionType { get; set; }
            public List<KeyValuePair<string, uint>> answersValues { get; set; }
            public int maxInputValue { get; set; }

            public void AddAnswer(string answerText, uint answerValue)
            {
                KeyValuePair<string, uint> newPair = new KeyValuePair<string, uint>(answerText, answerValue);
                answersValues.Add(newPair);
            }

            public SurveyQuestion(string _questionTitle, QuestionType _questionType)
            {
                questionTitle = _questionTitle;
                questionType = _questionType;

                answersValues = new List<KeyValuePair<string, uint>>();
                if (questionType == QuestionType.YES_OR_NO)
                {
                    AddAnswer("Nie", 0);
                    AddAnswer("Tak", 1);
                }
                maxInputValue = 0;
            }

            public SurveyQuestion()
            {
            }
        }

        public static int currentQuestionIndex;

        public string title { get; set; }
        public List<SurveyQuestion> questions { get; set; }
        public List<uint> surveyAnswersInt = new List<uint>();

        public Survey(string _title)
        {
            currentQuestionIndex = 0;
            title = _title;
            questions = new List<SurveyQuestion>();
            surveyAnswersInt = new List<uint>();
        }
        
        public Survey()
        {
        }

        public void AddQuestion(string title, QuestionType type)
        {
            questions.Add(new SurveyQuestion(title, type));
        }
    }
}
