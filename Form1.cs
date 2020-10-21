using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net;

namespace app
{
    public partial class Hackheroes : Form
    {
        private readonly List<Panel> panels = new List<Panel>();
        private List<Button> answerButtons = new List<Button>();
        private List<Button> acitivtyMatcherParticipantsButtons = new List<Button>();
        private List<Button> acitivtyMatcherWeatherButtons = new List<Button>();
        private List<Button> acitivtyMatcherEffortButtons = new List<Button>();
        private Survey survey;

        public Hackheroes()
        {
            InitializeComponent(); 
        }

        private void DisableQuiz()
        {
            buttonQuiz.Enabled = false;
            buttonQuiz.BackColor = Color.FromArgb(127, 143, 166);
        }

        private void DisableActivityMatcher()
        {
            buttonActivity.Enabled = false;
            buttonActivity.BackColor = Color.FromArgb(127, 143, 166);
        }

        private void ChangePanel(int index)
        {
            panels[index].BringToFront();
            buttonReturn.Visible = index != 0;
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ButtonMinimizeClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Hackheroes_Load(object sender, EventArgs e)
        {
            if(!Quiz.LoadQuestions())
            {
                DisableQuiz();
            }

            if(!ActivityMatcher.LoadSports())
            {
                DisableActivityMatcher();
            }

            try
            {
                string[] JSON = File.ReadAllLines("..\\..\\users.json");
                List<string> usersJSON = new List<string>();

                for(int i = 0; i < JSON.Length; i += 7)
                {
                    usersJSON.Add(JSON[i] + JSON[i + 1] + JSON[i + 2] + JSON[i + 3] + JSON[i + 4] + JSON[i + 5] + JSON[i + 6]);
                }

	            foreach(string line in usersJSON)
	            {
	                User newUser = JsonSerializer.Deserialize<User>(line);
	                Program.users.Add(newUser);
	                listBoxUsers.Items.Add(newUser.name);
	            }
            }
            catch(FileNotFoundException exception)
            {
                MessageBox.Show("Wystąpił błąd podczas wczytywania profili.", exception.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Program.users.Add(new User("User", 18, 80f, 180, Gender.Male));
                listBoxUsers.Items.Add("User");
            }
            finally
            {
                panels.Add(panel0); //buttons
                panels.Add(panel1); //BMI
                panels.Add(panel2); //sport activity
                panels.Add(panel3); //quiz
                panels.Add(panel4); //calculator
                panels.Add(panel5); //surveys
                panels.Add(panel6); //profiles
                panels.Add(panel7); //acitivity results


                answerButtons.Add(ButtonAnswerA);
                answerButtons.Add(ButtonAnswerB);
                answerButtons.Add(ButtonAnswerC);
                answerButtons.Add(ButtonAnswerD);

                acitivtyMatcherParticipantsButtons.Add(buttonIndividual);
                acitivtyMatcherParticipantsButtons.Add(buttonPair);
                acitivtyMatcherParticipantsButtons.Add(buttonTeam);
                acitivtyMatcherParticipantsButtons.Add(buttonAnyParticipants);

                acitivtyMatcherWeatherButtons.Add(buttonGoodWeather);
                acitivtyMatcherWeatherButtons.Add(buttonBadWeather);
                acitivtyMatcherWeatherButtons.Add(buttonAnyWeather);

                acitivtyMatcherEffortButtons.Add(buttonLowEffort);
                acitivtyMatcherEffortButtons.Add(buttonMediumEffort);
                acitivtyMatcherEffortButtons.Add(buttonHighEffort);
                acitivtyMatcherEffortButtons.Add(buttonAnyEffort);


                ChangePanel(0);
            }
        }

        private void UpdateMacro()
        {
            Calculator.CalculateMacro(Program.users[Program.currentUserIndex]);
            labelKcal.Text = (Program.users[Program.currentUserIndex].calories).ToString();
            labelFats.Text = (Program.users[Program.currentUserIndex].fat).ToString();
            labelCarbohydrates.Text = (Program.users[Program.currentUserIndex].carbohydrates).ToString();
            labelProtein.Text = (Program.users[Program.currentUserIndex].protein).ToString();
        }

        private void UpdateButtonDeleteEnabledStatus()
        {
            if (listBoxUsers.Items.Count > 1)
            {
                buttonDelete.Enabled = true;
            }
            else
            {
                buttonDelete.Enabled = false;
            }
        }

        private void UpdateArrow()
        {
            float BMI = Program.users[Program.currentUserIndex].BMI;
            float value = BMI * 2.5f - 10f;

            if (value < 0f)
            {
                value = 0f;
            }
            else if(value > 100f)
            {
                value = 100f;
            }

            pictureBoxArrow.Location = new Point(75 + (int)value * 8, 147);
        }

        private string GetInterpretation(float BMI)
        {
            if(BMI < 18.5f)
            {
                return "Niedowaga";
            }
            else if(BMI < 25f)
            {
                return "Norma";
            }
            else if(BMI < 30f)
            {
                return "Nadwaga";
            }
            else
            {
                return "Otyłość";
            }
        }

        private void Center(Control control)
        {
            control.Location = new Point(control.Parent.Size.Width / 2 - control.Size.Width / 2, control.Location.Y);
        }

        private void ButtonBMI_Click(object sender, EventArgs e)
        {
            int userIndex = Program.currentUserIndex;

            if(!Calculator.CalculateBMI(Program.users[userIndex]))
            {
                ChangePanel(6);
                return;
            }

            UpdateArrow();

            labelBMI.Text = "Twoje BMI wynosi: " + Program.users[userIndex].BMI.ToString("0.##");
            labelBMIInterpretation.Text = GetInterpretation(Program.users[userIndex].BMI);

            Center(labelBMI);
            Center(labelBMIInterpretation);

            ChangePanel(1);
        }

        private void ButtonActivity_Click(object sender, EventArgs e)
        {
            ChangePanel(2);
            ActivityMatcher.LoadSports();

            SetButtonAsUnclicked(buttonIndividual);
            SetButtonAsUnclicked(buttonPair);
            SetButtonAsUnclicked(buttonTeam);
            SetButtonAsUnclicked(buttonAnyParticipants);

            SetButtonAsUnclicked(buttonGoodWeather);
            SetButtonAsUnclicked(buttonBadWeather);
            SetButtonAsUnclicked(buttonAnyWeather);

            SetButtonAsUnclicked(buttonLowEffort);
            SetButtonAsUnclicked(buttonMediumEffort);
            SetButtonAsUnclicked(buttonHighEffort);
            SetButtonAsUnclicked(buttonAnyEffort);

            labelWeatherInfo.Visible = false;
        }

        private void ButtonQuiz_Click(object sender, EventArgs e)
        {
            ChangePanel(3);
        }

        private void ButtonCalculator_Click(object sender, EventArgs e)
        {
            ChangePanel(4);
            UpdateActivityLevel();
        }

        private void ButtonSurvey_Click(object sender, EventArgs e)
        {
            tablePanelAnswer.Visible = false;
            flowPanelSurveys.Visible = true;
            labelFinish.Visible = false;
            labelSurveyTitle.Text = "Ankiety diagnostyczne";
            Center(labelSurveyTitle);
            labelSurveyQuestion.Visible = false;
            labelSurveyQuestionNumber.Visible = false;
            ChangePanel(5);
        }

        private void ButtonProfile_Click(object sender, EventArgs e)
        {
            ChangePanel(6);

            UpdateButtonDeleteEnabledStatus();
            buttonSaveChanges.Enabled = false;

            int userIndex = listBoxUsers.SelectedIndex = Program.currentUserIndex;

            textBoxCurrentName.Text = Program.users[userIndex].name;
            numericUpDownCurrentAge.Value = Program.users[userIndex].age;
            numericUpDownCurrentHeight.Value = Program.users[userIndex].height;
            numericUpDownCurrentWeight.Value = Convert.ToDecimal(Program.users[userIndex].weight);
            
            if (Program.users[userIndex].gender == Gender.Male)
            {
                radioButtonCurrentMale.Checked = true;
            }
            else
            {
                radioButtonCurrentFemale.Checked = true;
            }

            UpdateArrowButtons();
            SetEditInfoVisibility(false);
        }

        private void ButtonReturn_Click(object sender, EventArgs e)
        {
            ChangePanel(0);
        }

        private void SetEditInfoVisibility(bool visibility)
        {
            textBoxCurrentName.Visible = visibility;
            numericUpDownCurrentAge.Visible = visibility;
            numericUpDownCurrentHeight.Visible = visibility;
            numericUpDownCurrentWeight.Visible = visibility;
            radioButtonCurrentFemale.Visible = visibility;
            radioButtonCurrentMale.Visible = visibility;
            label18.Visible = visibility;
            label19.Visible = visibility;
            label20.Visible = visibility;
            label22.Visible = visibility;
            label23.Visible = visibility;
            buttonDelete.Visible = visibility;
            buttonSaveChanges.Visible = visibility;
        }

        private void UpdateArrowButtons()
        {
            buttonArrowUp.Enabled = (listBoxUsers.SelectedIndex > 0);
            buttonArrowDown.Enabled = (listBoxUsers.SelectedIndex < listBoxUsers.Items.Count - 1);
        }

        private void UpdateAgeForm(Label lbl, NumericUpDown numericUD)
        {
            uint val = Convert.ToUInt16(numericUD.Value);
            if (val == 1)
            {
                lbl.Text = "rok";
            }
            else if (val >= 2 && val <= 4)
            {
                lbl.Text = "lata";
            }
            else if (val >= 5 && val <= 21)
            {
                lbl.Text = "lat";
            }
            else
            {
                uint lastDigit = val % 10;
                switch (lastDigit)
                {
                    case 2:
                    case 3:
                    case 4:
                        lbl.Text = "lata";
                        break;
                    default:
                        lbl.Text = "lat";
                        break;
                }
            }
        }

        private void NumericUpDownAge_ValueChanged(object sender, EventArgs e)
        {
            UpdateAgeForm(label15, numericUpDownAge);
        }

        private void NumericUpDownCurrentAge_ValueChanged(object sender, EventArgs e)
        {
            UpdateAgeForm(label17, numericUpDownCurrentAge);

            if(numericUpDownCurrentAge.Value != Program.users[Program.currentUserIndex].age)
            {
                buttonSaveChanges.Enabled = true;
            }
        }

        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text == "" || (radioButtonFemale.Checked == false && radioButtonMale.Checked == false))
            {
                string missingInfo = "";
                
                if (textBoxName.Text == "" && radioButtonFemale.Checked == false && radioButtonMale.Checked == false)
                {
                    missingInfo = "imię, płeć";
                }
                else if (radioButtonFemale.Checked == false && radioButtonMale.Checked == false)
                {
                    missingInfo = "płeć";
                }
                else if(textBoxName.Text == "")
                {
                    missingInfo = "imię";
                }

                string message = "Aby utworzyć profil musisz podać wszystkie dane!\nBrakujące dane: " + missingInfo + ".";
                string caption = "Niepoprawnie wypełniony formularz";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                
                result = MessageBox.Show(message, caption, buttons);
                if (result == DialogResult.Yes)
                {
                    Close();
                }

                Program.currentUserIndex = listBoxUsers.SelectedIndex = listBoxUsers.Items.Count - 1;
            }
            else
            {
                Gender gender = radioButtonMale.Checked == true ? Gender.Male : Gender.Female;

                User newUser = new User(textBoxName.Text, Convert.ToByte(numericUpDownAge.Value), Convert.ToSingle(numericUpDownWeight.Value), Convert.ToUInt16(numericUpDownHeight.Value), gender);

                Program.users.Add(newUser);
                listBoxUsers.Items.Add(newUser.name);

                textBoxName.Text = "";

                Program.currentUserIndex = listBoxUsers.SelectedIndex = Program.users.Count - 1; 
            }

            UpdateButtonDeleteEnabledStatus();
            SetEditInfoVisibility(false);
            UpdateArrowButtons();
        }

        private void ListBoxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateArrowButtons();
            SetEditInfoVisibility(false);

            if (listBoxUsers.SelectedIndex != -1)
            {
                int userIndex = Program.currentUserIndex = listBoxUsers.SelectedIndex;

                textBoxCurrentName.Text = Program.users[userIndex].name;
                numericUpDownCurrentAge.Value = Program.users[userIndex].age;
                numericUpDownCurrentWeight.Value = Convert.ToDecimal(Program.users[userIndex].weight);
                numericUpDownCurrentHeight.Value = Convert.ToUInt16(Program.users[userIndex].height);
                radioButtonCurrentMale.Checked = Program.users[userIndex].gender == Gender.Male;
                radioButtonCurrentFemale.Checked = Program.users[userIndex].gender == Gender.Female;
            }
        }

        private void ButtonDelete_Click_1(object sender, EventArgs e)
        {
            int indexToRemove = listBoxUsers.SelectedIndex;
            if (listBoxUsers.Items.Count <= 1)
            {
                string message = "Nie można usunąć jedynego istniejącego profilu!\nUtwórz nowy profil lub edytuj już istniejący.";
                string caption = "Nie można usunąć profilu";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
                if (result == DialogResult.Yes)
                {
                    Close();
                }
            }
            else
            {
                if (indexToRemove > 0)
                {
                    --listBoxUsers.SelectedIndex;
                }
                else
                {
                    ++listBoxUsers.SelectedIndex;
                }
                Program.users.RemoveAt(indexToRemove);
                listBoxUsers.Items.RemoveAt(indexToRemove);

                listBoxUsers.SelectedIndex = Program.currentUserIndex = 0;

                UpdateButtonDeleteEnabledStatus();
                UpdateArrowButtons();
                SetEditInfoVisibility(false);
                buttonSaveChanges.Enabled = false;
            }
        }

        private void ButtonSaveChanges_Click(object sender, EventArgs e)
        {
            if (listBoxUsers.SelectedIndex != -1)
            {
                int userIndex = listBoxUsers.SelectedIndex;

                Program.users[userIndex].name = textBoxCurrentName.Text;
                Program.users[userIndex].age = Convert.ToByte(numericUpDownCurrentAge.Value);
                Program.users[userIndex].weight = Convert.ToSingle(numericUpDownCurrentWeight.Value);
                Program.users[userIndex].height = Convert.ToUInt16(numericUpDownCurrentHeight.Value);

                if (radioButtonCurrentMale.Checked)
                {
                    Program.users[userIndex].gender = Gender.Male;
                }
                else
                {
                    Program.users[userIndex].gender = Gender.Female;
                }
                listBoxUsers.Items[userIndex] = Program.users[userIndex].name;
                SetEditInfoVisibility(false);
                buttonSaveChanges.Enabled = false;
            }
            else
            {
                string message = "Aby edytować dane, musisz najpierw zaznaczyć profil!";
                string caption = "Nie zaznaczono profilu";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
                if (result == DialogResult.Yes)
                {
                    Close();
                }
            }
        }

        private void ButtonArrowUp_Click(object sender, EventArgs e)
        {
            if (listBoxUsers.SelectedIndex > 0)
            {
                --listBoxUsers.SelectedIndex;
            }
        }

        private void ButtonArrowDown_Click(object sender, EventArgs e)
        {
            if (listBoxUsers.SelectedIndex < listBoxUsers.Items.Count - 1)
            {
                ++listBoxUsers.SelectedIndex;
            }
        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            SetEditInfoVisibility(true);
        }
        
        private void TextBoxCurrentName_TextChanged(object sender, EventArgs e)
        {
            if (textBoxCurrentName.Text != Program.users[Program.currentUserIndex].name)
            {
                buttonSaveChanges.Enabled = true;
            }
        }

        private void NumericUpDownCurrentWeight_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownCurrentWeight.Value != Convert.ToDecimal(Program.users[Program.currentUserIndex].weight))
            {
                buttonSaveChanges.Enabled = true;
            }
        }

        private void NumericUpDownCurrentHeight_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownCurrentHeight.Value != Program.users[Program.currentUserIndex].height)
            {
                buttonSaveChanges.Enabled = true;
            }
        }

        private void RadioButtonCurrentMale_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCurrentMale.Checked && Program.users[Program.currentUserIndex].gender != Gender.Male)
            {
                buttonSaveChanges.Enabled = true;
            }
        }

        private void RadioButtonCurrentFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCurrentFemale.Checked && Program.users[Program.currentUserIndex].gender != Gender.Female)
            {
                buttonSaveChanges.Enabled = true;
            }
        }

        private void SetupQuiz()
        {
            ButtonStartQuiz.Visible = false;
            labelNumber.Visible = true;
            pictureBoxTime.Visible = true;
            pictureBoxTimeBorder.Visible = true;
            labelQuestion.Visible = true;
            tableLayoutPanelAnswers.Visible = true;
            ButtonAnswerA.Visible = true;
            ButtonAnswerB.Visible = true;
            ButtonAnswerC.Visible = true;
            ButtonAnswerD.Visible = true;

            pictureBoxTime.Size = new Size(0, 30);

            Quiz.GetQuestions();
            Quiz.score = 0;
            Quiz.questionNumber = 0;

            NextQuestion();
        }

        private void UpdateTimeLeft(Stopwatch timeCounter)
        {
            int length = 500 * (int)timeCounter.Elapsed.TotalMilliseconds / 10000;
            if(length > 498)
            {
                length = 498;
            }
            pictureBoxTime.Size = new Size(length, 30);
        }

        private async void NextQuestion()
        {
            if(Quiz.questionNumber == 5)
            {
                FinishQuiz();
                return;
            }

            labelNumber.Text = (Quiz.questionNumber + 1) + "/5";
            labelQuestion.Text = Quiz.drawnQuestions[Quiz.questionNumber].ask;
            Center(labelQuestion);

            foreach(Button button in answerButtons)
            {
                button.BackColor = Color.FromArgb(127, 143, 166);
            }

            int correctIndex = Program.rnd.Next(4);
            answerButtons[correctIndex].Text = Quiz.drawnQuestions[Quiz.questionNumber].correctAnswer;
            answerButtons[correctIndex].Enabled = true;

            int buttonIndex;
            int answerIndex = 0;
            do
            {
                buttonIndex = Program.rnd.Next(4);

                if(!answerButtons[buttonIndex].Enabled)
                {
                    answerButtons[buttonIndex].Text = Quiz.drawnQuestions[Quiz.questionNumber].incorrectAnswers[answerIndex++];
                    answerButtons[buttonIndex].Enabled = true;
                }
            } while(!(ButtonAnswerA.Enabled && ButtonAnswerB.Enabled && ButtonAnswerC.Enabled && ButtonAnswerD.Enabled));

            Quiz.isAnswerChosen = false;

            Stopwatch timeCounter = new Stopwatch();
            timeCounter.Start();
            TimeSpan limit = new TimeSpan(0, 0, 10);

            while(true)
            {
                await Task.Delay(1);
                if (timeCounter.Elapsed >= limit) break;
                if(Quiz.isAnswerChosen == true) break;
                UpdateTimeLeft(timeCounter);
            }
            
            timeCounter.Stop();

            if(Quiz.isAnswerChosen == false)
            {
                MarkCorrectAnswer();
            }

            await Task.Delay(2000);

            Quiz.questionNumber++;
            NextQuestion();
        }

        private void FinishQuiz()
        {
            labelQuizResult.Text = "Wynik: " + Quiz.score + "/5";
            Center(labelQuizResult);
            labelQuestion.Visible = false;
            tableLayoutPanelAnswers.Visible = false;
            ButtonAnswerA.Visible = false;
            ButtonAnswerB.Visible = false;
            ButtonAnswerC.Visible = false;
            ButtonAnswerD.Visible = false;
            labelQuizResult.Visible = true;
            ButtonFinishQuiz.Visible = true;
            labelNumber.Visible = false;
            pictureBoxTime.Visible = false;
            pictureBoxTimeBorder.Visible = false;
        }

        private void ButtonStartQuiz_Click(object sender, EventArgs e)
        {
            SetupQuiz();
        }

        private void MarkCorrectAnswer()
        {
            foreach(Button button in answerButtons)
            {
                if(button.Text == Quiz.drawnQuestions[Quiz.questionNumber].correctAnswer)
                {
                    button.BackColor = Color.FromArgb(76, 209, 55);
                }
                else
                {
                    button.BackColor = Color.FromArgb(232, 65, 24);
                }
                button.Enabled = false;
            }
        }

        private void MarkCorrectAnswer(Button clickedButton)
        {
            foreach (Button button in answerButtons)
            {
                if(button.Text == Quiz.drawnQuestions[Quiz.questionNumber].correctAnswer)
                {
                    button.BackColor = Color.FromArgb(76, 209, 55);
                }
                else if(button == clickedButton && clickedButton.Text != Quiz.drawnQuestions[Quiz.questionNumber].correctAnswer)
                {
                    clickedButton.BackColor = Color.FromArgb(232, 65, 24);
                }
                button.Enabled = false;
            }
        }

        private void AnswerClicked(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            if(clickedButton.Text == Quiz.drawnQuestions[Quiz.questionNumber].correctAnswer)
            {
                ++Quiz.score;
            }
            Quiz.isAnswerChosen = true; 
            MarkCorrectAnswer(clickedButton);
        }

        private void Reset()
        {
            labelQuizResult.Visible = false;
            ButtonFinishQuiz.Visible = false;
            ButtonStartQuiz.Visible = true;
        }

        private void ButtonFinishQuiz_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void UpdateActivityLevel()
        {
            Program.users[Program.currentUserIndex].activityLevel = 1.1f + 0.1625f * trackBarActivityLevel.Value;
            UpdateMacro();
        }

        private void SetupSurvey()
        {
            flowPanelSurveys.Visible = false;
            tablePanelAnswer.Visible = true;
            labelSurveyTitle.Text = survey.title;

            Center(labelSurveyTitle);
        }

        private void SetupSurveyQuestion()
        {
            labelSurveyQuestion.Visible = true;
            labelSurveyQuestionNumber.Visible = true;
            NextSurveyQuestion();
        }

        private void NextSurveyQuestion()
        {
            int currentSurverQuestionCount = survey.currentQuestionIndex + 1;
            labelSurveyQuestionNumber.Text = "Pytanie " + currentSurverQuestionCount.ToString() + "/" + survey.questions.Count.ToString();
            switch(survey.questions[survey.currentQuestionIndex].questionType)
            {
                case Survey.QuestionType.YES_OR_NO:
                    {
                        buttonSurveyA.Visible = false;
                        buttonSurveyB.Visible = false;
                        buttonSurveyYes.Visible = true;
                        buttonSurveyNo.Visible = true;
                        textBoxSurveyText.Visible = false;
                        buttonSurveyConfirm.Visible = false;

                        buttonSurveyYes.Text = "Tak";
                        buttonSurveyNo.Text = "Nie";
                        break;
                    }
                case Survey.QuestionType.ABCD:
                    {
                        buttonSurveyA.Visible = true;
                        buttonSurveyB.Visible = true;
                        buttonSurveyYes.Visible = true;
                        buttonSurveyNo.Visible = true;
                        textBoxSurveyText.Visible = false;
                        buttonSurveyConfirm.Visible = false;

                        buttonSurveyA.Text = survey.questions[survey.currentQuestionIndex].answersValues[0].Key;
                        buttonSurveyB.Text = survey.questions[survey.currentQuestionIndex].answersValues[1].Key;
                        buttonSurveyYes.Text = survey.questions[survey.currentQuestionIndex].answersValues[2].Key;
                        buttonSurveyNo.Text = survey.questions[survey.currentQuestionIndex].answersValues[3].Key;
                        break;
                    } 
                case Survey.QuestionType.INPUT:
                    {
                        buttonSurveyA.Visible = false;
                        buttonSurveyB.Visible = false;
                        buttonSurveyYes.Visible = false;
                        buttonSurveyNo.Visible = false;
                        textBoxSurveyText.Visible = true;
                        buttonSurveyConfirm.Visible = true;
                        break;
                    }
                default:
                    {
                        buttonSurveyA.Visible = false;
                        buttonSurveyB.Visible = false;
                        buttonSurveyYes.Visible = true;
                        buttonSurveyNo.Visible = true;
                        textBoxSurveyText.Visible = false;
                        buttonSurveyConfirm.Visible = false;

                        buttonSurveyYes.Text = "Tak";
                        buttonSurveyNo.Text = "Nie";
                        break;
                    }   
            }
            labelSurveyQuestion.Text = survey.questions[survey.currentQuestionIndex].questionTitle;
            Center(labelSurveyQuestionNumber);
            Center(labelSurveyQuestion);
        }

        private void ButtonActivityLevelSurvey_Click(object sender, EventArgs e)
        {
            survey = new Survey("Poziom aktywności fizycznej");
            SetupSurvey();
            survey.AddQuestion("Czy pracujesz fizycznie?", Survey.QuestionType.YES_OR_NO);
            survey.AddQuestion("Ile razy trenujesz w tygodniu?", Survey.QuestionType.INPUT);
            survey.questions[1].maxInputValue = 7;
            survey.AddQuestion("Oceń swoją aktynowść fizyczną? (0 - 10)", Survey.QuestionType.INPUT);
            survey.questions[2].maxInputValue = 10;
            SetupSurveyQuestion();
        }

        private void SurveyAnswerButtonClicked(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            bool correctValue = true;
            if(clickedButton.Text == "Potwierdź")
            {
                try
                {          
                    if (Convert.ToInt32(textBoxSurveyText.Text) >= 0 && Convert.ToInt32(textBoxSurveyText.Text) <= survey.questions[survey.currentQuestionIndex].maxInputValue)
                    {
                        survey.surveyAnswersInt.Add(Convert.ToUInt32(textBoxSurveyText.Text));
                        correctValue = true;
                    }
                    else
                    {
                        correctValue = false;
                    }
                }
                catch(FormatException)
                {
                    correctValue = false;
                }   
            }
            else
            {
                foreach (KeyValuePair<string, uint> answer in survey.questions[survey.currentQuestionIndex].answersValues)
                {
                    if (clickedButton.Text == answer.Key)
                    {
                        survey.surveyAnswersInt.Add(answer.Value);
                    }
                }
            }
            if(correctValue)
            {
                if (survey.currentQuestionIndex + 1 < survey.questions.Count)
                {
                    ++survey.currentQuestionIndex;
                    NextSurveyQuestion();
                }
                else
                {
                    FinishSurvey();
                }
            }  
        }

        private void FinishSurvey()
        {
            labelFinish.Visible = true;
            flowPanelSurveys.Visible = false;
            tablePanelAnswer.Visible = false;
            buttonSurveyConfirm.Visible = false;
            textBoxSurveyText.Visible = false;
            labelSurveyQuestion.Visible = false;
            labelSurveyQuestionNumber.Visible = false;
            if (survey.title == "Poziom aktywności fizycznej")
            {
                Program.users[Program.currentUserIndex].physicalJob = survey.surveyAnswersInt[0] == 1;

                Program.users[Program.currentUserIndex].trainingsInWeek = survey.surveyAnswersInt[1];
                Program.users[Program.currentUserIndex].dailyMovementLevel = survey.surveyAnswersInt[2];

                Calculator.CalculateActivityLevel(Program.users[Program.currentUserIndex]);
                labelFinish.Text = "Poziom aktywności wynosi " + Program.users[Program.currentUserIndex].activityLevel.ToString();
            }       
            
            Center(labelFinish);
        }
        private void TrackBarActivityLevel_Scroll(object sender, EventArgs e)
        {
            UpdateActivityLevel();
        }

        private void SetButtonAsUnclicked(Button button)
        {
            button.BackColor = Color.FromArgb(39, 60, 117);
            button.ForeColor = Color.FromArgb(255, 255, 255);
            button.Enabled = true;
        }
        private void SetButtonAsClicked(Button button)
        {
            button.BackColor = Color.FromArgb(251, 197, 3);
            button.ForeColor = Color.FromArgb(47, 54, 64);
            button.Enabled = false;
        }

        private void ButtonParticipants_Click(object sender, EventArgs e)
        {
            SetButtonAsUnclicked(buttonIndividual);
            SetButtonAsUnclicked(buttonPair);
            SetButtonAsUnclicked(buttonTeam);
            SetButtonAsUnclicked(buttonAnyParticipants);

            Button button = (Button)sender;
            SetButtonAsClicked(button);
        }

        private void ButtonWeather_Click(object sender, EventArgs e)
        {
            SetButtonAsUnclicked(buttonGoodWeather);
            SetButtonAsUnclicked(buttonBadWeather);
            SetButtonAsUnclicked(buttonAnyWeather);

            Button button = (Button)sender;
            SetButtonAsClicked(button);
        }

        private void ButtonEffort_Click(object sender, EventArgs e)
        {
            SetButtonAsUnclicked(buttonLowEffort);
            SetButtonAsUnclicked(buttonMediumEffort);
            SetButtonAsUnclicked(buttonHighEffort);
            SetButtonAsUnclicked(buttonAnyEffort);

            Button button = (Button)sender;
            SetButtonAsClicked(button);
        }

        private void ButtonCheckWeather_Click(object sender, EventArgs e)
        {
            try
            {
                Tuple<float, bool> weatherInfo = ActivityMatcher.currentWeather.GetWeather(textBoxCity.Text);
                string weatherMessage = "";
                if (weatherInfo.Item1 > 12 && weatherInfo.Item1 < 30)
                {
                    if(weatherInfo.Item2)
                    {
                        weatherMessage = string.Format("Odczuwalna temperatura w Twojej okolicy wynosi {0}°C.\nPogodę uznaliśmy za dobrą.", weatherInfo.Item1);
                        SetButtonAsUnclicked(buttonBadWeather);
                        SetButtonAsUnclicked(buttonAnyWeather);
                        SetButtonAsClicked(buttonGoodWeather);
                    }
                    else
                    {
                        weatherMessage = string.Format("Odczuwalna temperatura w Twojej okolicy wynosi {0}°C.\nPogodę uznaliśmy za niekorzystną ze względu na inne warunki (np. opady).", weatherInfo.Item1);
                        SetButtonAsUnclicked(buttonGoodWeather);
                        SetButtonAsUnclicked(buttonAnyWeather);
                        SetButtonAsClicked(buttonBadWeather);
                    }
                }
                else
                {
                    weatherMessage = string.Format("Odczuwalna temperatura w Twojej okolicy wynosi {0}°C.\nPogodę uznaliśmy za niekorzystną.", weatherInfo.Item1);
                    SetButtonAsUnclicked(buttonGoodWeather);
                    SetButtonAsUnclicked(buttonAnyWeather);
                    SetButtonAsClicked(buttonBadWeather);
                }
                Center(labelWeatherInfo);

                labelWeatherInfo.Text = weatherMessage;
                labelWeatherInfo.Visible = true;
            }
            catch(WebException exception)
            {
                string message = string.Format("Nie znaleziono takiego miejsca ({0}.\nUpewnij się czy nazwa jest wpisana poprawnie i spróbuj ponownie.", textBoxCity.Text);
                if (exception.Message == "No internet connection")
                {
                    message = "Do sprawdzenia pogody konieczne jest połączenie z internetem.";
                }
                string caption = "Nie można sprawdzić pogody";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    Close();
                }
            }
        }

        private bool OneOfButtonsDisabled(List<Button> list)
        {
            bool oneDisabled = false;
            foreach(Button button in list)
            {
                if (!oneDisabled && !button.Enabled)
                {
                    oneDisabled = true;
                }
                if (!oneDisabled && !button.Enabled)
                {
                    return false;
                }
            }
            return oneDisabled;
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            Participants participants = 0;
            Weather weather = 0;
            EffortLevel effortLevel = 0;

            if (OneOfButtonsDisabled(acitivtyMatcherParticipantsButtons) && OneOfButtonsDisabled(acitivtyMatcherWeatherButtons) && OneOfButtonsDisabled(acitivtyMatcherEffortButtons))
            {
                if (!buttonIndividual.Enabled)
                {
                    participants = Participants.One;
                }
                else if (!buttonPair.Enabled)
                {
                    participants = Participants.Two;
                }
                else if (!buttonTeam.Enabled)
                {
                    participants = Participants.More;
                }
                else if (!buttonAnyParticipants.Enabled)
                {
                    participants = Participants.Any;
                }

                if (!buttonGoodWeather.Enabled)
                {
                    weather = Weather.Good;
                }
                else if (!buttonBadWeather.Enabled)
                {
                    weather = Weather.Bad;
                }
                else if (!buttonAnyWeather.Enabled)
                {
                    weather = Weather.Any;
                }

                if (!buttonLowEffort.Enabled)
                {
                    effortLevel = EffortLevel.Low;
                }
                else if (!buttonMediumEffort.Enabled)
                {
                    effortLevel = EffortLevel.Medium;
                }
                else if (!buttonHighEffort.Enabled)
                {
                    effortLevel = EffortLevel.High;
                }
                else if (!buttonAnyEffort.Enabled)
                {
                    effortLevel = EffortLevel.Any;
                }

                labelActivityResult.Text = ActivityMatcher.Search(participants, weather, effortLevel);
                if (labelActivityResult.Text == "")
                {
                    labelActivityResult.Text = "Nie znaleziono aktywności o podanych cechach.\nSpróbuj ponownie z innymi kryteriami.";
                }
                Center(labelActivityResult);
                ChangePanel(7);
            }
            else
            {
                string message = "Aby wyszukać aktywność należy zaznaczyć jedną cechę z każdej kategori. Jeśli nie wiesz na co się zdecydować, zaznacz ostanią opcję.";
                string caption = "Nie można wyszukać aktywności";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    Close();
                }
            }
        }
        private void ButtonShowNext_Click(object sender, EventArgs e)
        {
            labelActivityResult.Text = ActivityMatcher.Search();
            Center(labelActivityResult);
            if (labelActivityResult.Text == "")
            {
                labelActivityResult.Text = "Nie znaleziono aktywności o podanych cechach.\nSpróbuj ponownie z innymi kryteriami.";
                Center(labelActivityResult);
            }
        }

        private void ButtonChangeSearchingData_Click(object sender, EventArgs e)
        {
            ActivityMatcher.LoadSports();
            ChangePanel(2);
        }

        private void Hackheroes_FormClosing(object sender, FormClosingEventArgs e)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            List<string> JSON = new List<string>();

            foreach(User user in Program.users)
            {
                JSON.Add(JsonSerializer.Serialize(user, options));
            }

            File.WriteAllLines("..\\..\\users.json", JSON);
        }
    }
}