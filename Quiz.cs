using System;
using System.Collections.Generic;
using System.IO;

namespace app
{
    class Question
    {
       public string ask;
       public string correctAnswer;
       public string[] incorrectAnswers;

        public Question(string _ask, string _correctAnswer, string[] _incorrectAnswers)
        {
            ask = _ask;
            correctAnswer = _correctAnswer;
            incorrectAnswers = _incorrectAnswers;
        }
    }

    static class Quiz
    {
        public static List<Question> questions;
        public static int score;

        public static void LoadQuestions()
        {
            questions = new List<Question>();
            using (StreamReader loading = new StreamReader("..\\..\\Resources\\Questions"))
            {
                string ask;
                string correctanswer;
                string[] incorrectanswer = new string[3];

                while (!loading.EndOfStream)
                {
                    ask = loading.ReadLine().Split(new string[] { ". " }, System.StringSplitOptions.None)[1];
                    correctanswer = loading.ReadLine().Split(new string[] { ". " }, System.StringSplitOptions.None)[1]; 
                    for(int i=0; i<3; i++)
                    {
                        incorrectanswer[i] = loading.ReadLine().Split(new string[] { ". " }, System.StringSplitOptions.None)[1]; 
                    }
                    questions.Add(new Question(ask, correctanswer, incorrectanswer));
                    string empty = loading.ReadLine();
                }
            }
        }

        public static Question DrawQuestion(List<Question> alreadyDrawn)
        {
            Random rnd = new Random();
            int index = rnd.Next(questions.Count);

            if(alreadyDrawn.Contains(questions[index]))
            {
                return DrawQuestion(alreadyDrawn);
            }

            return questions[index];
        }
    }
}
