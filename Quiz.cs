﻿using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

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
            incorrectAnswers = new string[3];
            incorrectAnswers[0] = _incorrectAnswers[0];
            incorrectAnswers[1] = _incorrectAnswers[1];
            incorrectAnswers[2] = _incorrectAnswers[2];
       }
    }

    static class Quiz
    {
        public static List<Question> questions;
        public static List<Question> drawnQuestions = new List<Question>();
        public static int score;
        public static int questionNumber;
        public static bool isAnswerChosen;

        public static bool LoadQuestions()
        {
            questions = new List<Question>();
            try
            {
                using(StreamReader loading = new StreamReader("..\\..\\Resources\\Questions"))
                {
                    string ask;
                    string correctanswer;
                    string[] incorrectanswer = new string[3];

                    while(!loading.EndOfStream)
                    {
                        ask = loading.ReadLine().Split(new string[] { ". " }, System.StringSplitOptions.None)[1];
                        correctanswer = loading.ReadLine().Split(new string[] { ". " }, System.StringSplitOptions.None)[1];
                        for (int i = 0; i < 3; i++)
                        {
                            incorrectanswer[i] = loading.ReadLine().Split(new string[] { ". " }, System.StringSplitOptions.None)[1];
                        }
                        Question newQuestion = new Question(ask, correctanswer, incorrectanswer);
                        questions.Add(newQuestion);
                        string empty = loading.ReadLine();
                    }
                }

                return true;
            }
            catch(FileNotFoundException e)
            {
                MessageBox.Show("Wystąpił błąd podczas wczytywania pytań. Quizy nie będą dostępne.", e.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public static void DrawQuestion()
        {
            int index;
            do
            {
                index = Program.rnd.Next(questions.Count);
            } while(drawnQuestions.Contains(questions[index]));

            drawnQuestions.Add(questions[index]);
        }

        public static void GetQuestions()
        {
            drawnQuestions.Clear();

            for (int i = 0; i < 5; i++)
            {
                DrawQuestion();
            }
        }
    }
}
