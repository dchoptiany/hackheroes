﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net;
using app.Properties;

namespace app
{
    public partial class Hackheroes : Form
    {
        private List<Survey> surveys;
        private int currentSurveyIndex;

        private List<User> users;
        private int currentUserIndex;
        private readonly List<Button> surveyButtons = new List<Button>();
        private List<Button> activtyMatcherParticipantsButtons = new List<Button>();
        private List<Button> activtyMatcherWeatherButtons = new List<Button>();
        private List<Button> activtyMatcherEffortButtons = new List<Button>();

        private List<Button> answerButtons = new List<Button>();
        private List<Button> menuButtons = new List<Button>();
        private readonly Color blue1 = Color.FromArgb(0, 168, 255);
        private readonly Color purple1 = Color.FromArgb(156, 136, 255);
        private readonly Color darkblue1 = Color.FromArgb(39, 60, 117);
        private readonly Color darkblue2 = Color.FromArgb(25, 42, 86);
        private readonly Color red1 = Color.FromArgb(232, 65, 24);
        private readonly Color red2 = Color.FromArgb(194, 54, 22);
        private readonly Color green1 = Color.FromArgb(76, 209, 55);
        private readonly Color green2 = Color.FromArgb(68, 189, 50);
        private readonly Color yellow1 = Color.FromArgb(251, 197, 49);
        private readonly Color yellow2 = Color.FromArgb(225, 177, 44);
        private readonly Color white1 = Color.FromArgb(220, 221, 225);
        private readonly Color white2 = Color.FromArgb(245, 246, 250);

        public Hackheroes()
        {
            InitializeComponent();
            InitializeButtons();
            InitializeColors();
        }

        private void InitializeColors()
        {
            BackColor = white2;

            panelPointer.BackColor = blue1;

            Color leftPanelBackColor = darkblue1;
            flowLayoutPanelSidebar.BackColor = leftPanelBackColor;
            panelProfileSetup.BackColor = leftPanelBackColor;
            Color leftPanelButtonsColor = darkblue1;
            buttonBMI.BackColor = leftPanelButtonsColor;
            buttonActivity.BackColor = leftPanelButtonsColor;
            buttonQuiz.BackColor = leftPanelButtonsColor;
            buttonCalculator.BackColor = leftPanelButtonsColor;
            buttonSurvey.BackColor = leftPanelButtonsColor;
            buttonProfile.BackColor = leftPanelButtonsColor;
        }

        private void InitializeButtons()
        {
            menuButtons.Add(buttonBMI);
            menuButtons.Add(buttonActivity);
            menuButtons.Add(buttonQuiz);
            menuButtons.Add(buttonCalculator);
            menuButtons.Add(buttonSurvey);
            menuButtons.Add(buttonProfile);
        }

        private void DisableButton(object sender, EventArgs e)
        {
            var clickedButton = (Button)sender;
            foreach (Button _button in menuButtons)
            {
                if (_button.Text == clickedButton.Text)
                {
                    _button.Enabled = false;
                }
                else
                {
                    _button.Enabled = true;
                    _button.BackColor = green2;
                }
            }
            clickedButton.BackColor = green1;

            panelPointer.Visible = true;
            panelPointer.Location = new Point(0, clickedButton.Location.Y + panelProfileSetup.Size.Height);
            panelPointer.Height = clickedButton.Height;
        }

        private void LoadQuestions()
        {
            Quiz.questions = new List<Question>();
            try
            {
                string[] JSON = File.ReadAllLines("..\\..\\Resources\\Questions.json");
                List<string> questionsJSON = new List<string>();
                string questionLine;

                for (int i = 0; i < JSON.Length; i += 9)
                {
                    questionLine = string.Empty;

                    for (int line = 0; line < 9; line++)
                    {
                        questionLine += JSON[i + line];
                    }

                    Quiz.questions.Add(JsonSerializer.Deserialize<Question>(questionLine));
                }
            }
            catch (FileNotFoundException exception)
            {
                MessageBox.Show("Wystąpił błąd podczas wczytywania pytań. Quizy nie będą dostępne.", exception.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Disable(buttonQuiz);
            }
        }

        private void LoadSports()
        {
            try
            {
                ActivityMatcher.sports = new List<Sport>();
                ActivityMatcher.approvedSports = new List<Sport>();

                string[] JSON = File.ReadAllLines("..\\..\\Resources\\Sports.json");
                List<string> sportsJSON = new List<string>();
                string sportLine;

                for (int i = 0; i < JSON.Length; i += 7)
                {
                    sportLine = string.Empty;

                    for (int line = 0; line < 7; line++)
                    {
                        sportLine += JSON[i + line];
                    }

                    ActivityMatcher.sports.Add(JsonSerializer.Deserialize<Sport>(sportLine));
                }
            }
            catch (FileNotFoundException exception)
            {
                MessageBox.Show("Wystąpił błąd podczas wczytywania aktywności. Wyszukiwarka aktywności nie będzie dostępna.", exception.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Disable(buttonActivity);
            }
        }

        private void LoadSurveys()
        {
            surveys = new List<Survey>();
            try
            {
                string[] surveysJSON = File.ReadAllLines("..\\..\\Resources\\Surveys.json");

                foreach (string line in surveysJSON)
                {
                    Survey newSurvey = JsonSerializer.Deserialize<Survey>(line);
                    surveys.Add(newSurvey);
                }

                for (int i = 0; i < surveys.Count; i++)
                {
                    surveyButtons[i].Text = surveys[i].title;
                    surveyButtons[i].Visible = true;
                }
            }
            catch (FileNotFoundException exception)
            {
                MessageBox.Show("Wystąpił błąd podczas wczytywania ankiet. Ankiety nie będą dostępne.", exception.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Disable(buttonSurvey);
            }
        }

        private void LoadUsers()
        {
            users = new List<User>();
            currentUserIndex = 0;

            try
            {
                string[] JSON = File.ReadAllLines("..\\..\\users.json");
                List<string> usersJSON = new List<string>();
                string userLine;

                for (int i = 0; i < JSON.Length; i += 9)
                {
                    userLine = string.Empty;
                    for (int line = 0; line < 9; line++)
                    {
                        userLine += JSON[i + line];
                    }
                    usersJSON.Add(userLine);
                }

                foreach (string line in usersJSON)
                {
                    User newUser = JsonSerializer.Deserialize<User>(line);
                    users.Add(newUser);
                }
            }
            catch (FileNotFoundException exception)
            {
                MessageBox.Show("Wystąpił błąd podczas wczytywania profili.", exception.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                users.Add(new User("User", 18, 80f, 180, Gender.Male, Avatar.Gray));
            }
        }

        private void Hackheroes_Load(object sender, EventArgs e)
        {
            answerButtons.Add(buttonAnswerA);
            answerButtons.Add(buttonAnswerB);
            answerButtons.Add(buttonAnswerC);
            answerButtons.Add(buttonAnswerD);

            surveyButtons.Add(buttonSurvey1);
            surveyButtons.Add(buttonSurvey2);
            surveyButtons.Add(buttonSurvey3);
            surveyButtons.Add(buttonSurvey4);
            surveyButtons.Add(buttonSurvey5);
            surveyButtons.Add(buttonSurvey6);

            activtyMatcherParticipantsButtons.Add(buttonIndividual);
            activtyMatcherParticipantsButtons.Add(buttonPair);
            activtyMatcherParticipantsButtons.Add(buttonTeam);
            activtyMatcherParticipantsButtons.Add(buttonAnyParticipants);

            activtyMatcherWeatherButtons.Add(buttonGoodWeather);
            activtyMatcherWeatherButtons.Add(buttonBadWeather);
            activtyMatcherWeatherButtons.Add(buttonAnyWeather);

            activtyMatcherEffortButtons.Add(buttonLowEffort);
            activtyMatcherEffortButtons.Add(buttonMediumEffort);
            activtyMatcherEffortButtons.Add(buttonHighEffort);
            activtyMatcherEffortButtons.Add(buttonAnyEffort);

            LoadQuestions();
            LoadSports();
            LoadSurveys();
            LoadUsers();

            panelLandingPage.BringToFront();
        }

        private void Disable(Button button)
        {
            button.Enabled = false;
            button.BackColor = Color.FromArgb(127, 143, 166);
        }

        private int GetSurveyID(Button button)
        {
            for (int i = 0; i < surveyButtons.Count; i++)
            {
                if (button == surveyButtons[i])
                {
                    return i;
                }
            }
            return -1;
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ButtonMinimizeClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void UpdateMacro()
        {
            Calculator.CalculateMacro(users[currentUserIndex]);
            labelKcal.Text = (users[currentUserIndex].calories).ToString();
            labelFats.Text = (users[currentUserIndex].fat).ToString();
            labelCarbohydrates.Text = (users[currentUserIndex].carbohydrates).ToString();
            labelProtein.Text = (users[currentUserIndex].protein).ToString();
        }

        private void UpdateArrow()
        {
            float BMI = users[currentUserIndex].BMI;
            float value = BMI * 2.5f - 10f;

            if (value < 0f)
            {
                value = 0f;
            }
            else if (value > 100f)
            {
                value = 100f;
            }

            pictureBoxArrow.Location = new Point(75 + (int)value * 8, pictureBoxArrow.Location.Y);
        }

        private string GetInterpretation(float BMI)
        {
            if (BMI < 16f)
            {
                return "Wygłodzenie";
            }
            if(BMI < 17f)
            {
                return "Wychudzenie";
            }
            if(BMI < 18.5f)
            {
                return "Niedowaga";
            }
            if(BMI < 25f)
            {
                return "Norma";
            }
            if(BMI < 30f)
            {
                return "Nadwaga";
            }
            if(BMI < 35f)
            {
                return "I stopień otyłości";
            }
            if(BMI < 40f)
            {
                return "II stopień otyłości";
            }
            return "Otyłość skrajna";
        }

        private void Center(Control control)
        {
            control.Location = new Point(control.Parent.Width / 2 - control.Size.Width / 2, control.Location.Y);
        }

        private void ButtonBMI_Click(object sender, EventArgs e)
        {
            if (!Calculator.CalculateBMI(users[currentUserIndex]))
            {
                panelProfiles.BringToFront();
                return;
            }

            UpdateArrow();

            labelBMI.Text = "Twoje BMI wynosi: " + users[currentUserIndex].BMI.ToString("0.##");
            labelBMIInterpretation.Text = GetInterpretation(users[currentUserIndex].BMI);

            Center(labelBMI);
            Center(labelBMIInterpretation);

            panelBMI.BringToFront();
        }

        private void ButtonActivity_Click(object sender, EventArgs e)
        {
            panelActivity.BringToFront();

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
            panelQuizMenu.BringToFront();
        }

        private void ButtonCalculator_Click(object sender, EventArgs e)
        {
            panelMacro.BringToFront();
            UpdateActivityLevel();
        }

        private void ButtonSurvey_Click(object sender, EventArgs e)
        {
            panelSurveyMenu.BringToFront();
        }

        private void ButtonProfile_Click(object sender, EventArgs e)
        {
            panelProfiles.BringToFront();

            buttonSaveChanges.Enabled = false;

            UpdateUserItems();

            UpdateArrowButtons();
            groupBoxEdit.Visible = false;

            if(users.Count == 0)
            {
                buttonEdit.Enabled = false;
            }
        }

        private int GetFirstVisibleUserItemIndex()
        {
            if (currentUserIndex <= 1)
            {
                return 0;
            }
            else if (currentUserIndex >= users.Count - 2)
            {
                return users.Count - 3;
            }
            else
            {
                return currentUserIndex - 1;
            }
        }

        private Image SetAvatar(Avatar color)
        {
            if (color == Avatar.Blue)
            {
                return Resources.profileBlue;
            }
            else if (color == Avatar.Red)
            {
                return Resources.profileRed;
            }
            else
            {
                return Resources.profileGray;
            }
        }

        private void UpdateUserItems()
        {
            userItemFirst.Visible = false;
            userItemSecond.Visible = false;
            userItemThird.Visible = false;

            if (users.Count > 0)
            {
                int firstVisibleUserItemIndex = GetFirstVisibleUserItemIndex();

                if (users.Count >= 1)
                {
                    userItemFirst.Visible = true;
                    userItemFirst.UserName = users[firstVisibleUserItemIndex].name;
                    userItemFirst.Avatar = SetAvatar(users[firstVisibleUserItemIndex].avatar);
                    if (users.Count >= 2)
                    {
                        userItemSecond.Visible = true;
                        userItemSecond.UserName = users[firstVisibleUserItemIndex + 1].name;
                        userItemSecond.Avatar = SetAvatar(users[firstVisibleUserItemIndex + 1].avatar);

                        if (users.Count >= 3)
                        {
                            userItemThird.Visible = true;
                            userItemThird.UserName = users[firstVisibleUserItemIndex + 2].name;
                            userItemThird.Avatar = SetAvatar(users[firstVisibleUserItemIndex + 2].avatar);
                        }
                    }
                }

                userItemFirst.BackColor = darkblue1;
                userItemSecond.BackColor = darkblue1;
                userItemThird.BackColor = darkblue1;

                if (currentUserIndex == firstVisibleUserItemIndex)
                {
                    userItemFirst.BackColor = Color.FromArgb(127, 143, 166);
                }
                else if (currentUserIndex == firstVisibleUserItemIndex + 1)
                {
                    userItemSecond.BackColor = Color.FromArgb(127, 143, 166);
                }
                else
                {
                    userItemThird.BackColor = Color.FromArgb(127, 143, 166);
                }
                string indexInfo = string.Format("{0}/{1}", currentUserIndex + 1, users.Count);
                labelIndexInfo.Text = indexInfo;
            }
            else
            {
                labelIndexInfo.Text = "0/0";
            }
        }

        private void UpdateArrowButtons()
        {
            buttonArrowUp.Enabled = (currentUserIndex > 0);
            buttonArrowDown.Enabled = (currentUserIndex < users.Count - 1);
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

            if (numericUpDownCurrentAge.Value != users[currentUserIndex].age)
            {
                buttonSaveChanges.Enabled = true;
            }
        }

        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text == "" || (radioButtonFemale.Checked == false && radioButtonMale.Checked == false) || (radioButtonAvatarBlue.Checked == false && radioButtonAvatarRed.Checked == false && radioButtonAvatarGray.Checked == false))
            {
                string missingInfo = "";
                if (radioButtonAvatarBlue.Checked == false && radioButtonAvatarRed.Checked == false && radioButtonAvatarGray.Checked == false)
                {
                    missingInfo += "\n• zdjęcie";
                }
                if (radioButtonFemale.Checked == false && radioButtonMale.Checked == false)
                {
                    missingInfo += "\n• płeć";
                }
                if (textBoxName.Text == "")
                {
                    missingInfo += "\n• imię";
                }

                string message = string.Format("Aby utworzyć profil musisz podać wszystkie dane!\nBrakujące dane: {0}", missingInfo);
                string caption = "Niepoprawnie wypełniony formularz";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
                if (result == DialogResult.Yes)
                {
                    Close();
                }

                currentUserIndex = users.Count - 1;
            }
            else
            {
                Gender gender = radioButtonMale.Checked == true ? Gender.Male : Gender.Female;

                Avatar avatar = Avatar.Gray;
                if (radioButtonAvatarBlue.Checked)
                {
                    avatar = Avatar.Blue;
                }
                else if (radioButtonAvatarRed.Checked)
                {
                    avatar = Avatar.Red;
                }

                User newUser = new User(textBoxName.Text, Convert.ToByte(numericUpDownAge.Value), Convert.ToSingle(numericUpDownWeight.Value), Convert.ToUInt16(numericUpDownHeight.Value), gender, avatar);

                users.Add(newUser);

                textBoxName.Text = "";

                currentUserIndex = users.Count - 1;

                if (users.Count > 0)
                {
                    buttonEdit.Enabled = true;
                }
            }

            groupBoxEdit.Visible = false;
            UpdateUserItems();
            UpdateArrowButtons();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            int indexToRemove = currentUserIndex;

            string message = string.Format("Czy na pewno chcesz usunąć profil {0}?", users[indexToRemove].name);
            string caption = "Potwierdzenie usunięcia profilu";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.Yes)
            {
                if (indexToRemove > 0)
                {
                    --currentUserIndex;
                }
                else
                {
                    ++currentUserIndex;
                }
                users.RemoveAt(indexToRemove);

                currentUserIndex = 0;

                UpdateArrowButtons();
                groupBoxEdit.Visible = false;
                UpdateUserItems();
                buttonSaveChanges.Enabled = false;
                if (users.Count == 0)
                {
                    buttonEdit.Enabled = false;
                }
            }
        }

        private void ButtonSaveChanges_Click(object sender, EventArgs e)
        {
            if (currentUserIndex != -1)
            {
                users[currentUserIndex].name = textBoxCurrentName.Text;
                users[currentUserIndex].age = Convert.ToByte(numericUpDownCurrentAge.Value);
                users[currentUserIndex].weight = Convert.ToSingle(numericUpDownCurrentWeight.Value);
                users[currentUserIndex].height = Convert.ToUInt16(numericUpDownCurrentHeight.Value);

                if (radioButtonCurrentMale.Checked)
                {
                    users[currentUserIndex].gender = Gender.Male;
                }
                else
                {
                    users[currentUserIndex].gender = Gender.Female;
                }
                groupBoxEdit.Visible = false;
                buttonSaveChanges.Enabled = false;
                UpdateUserItems();
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
            if (currentUserIndex > 0)
            {
                --currentUserIndex;
            }
            buttonSaveChanges.Enabled = false;
            groupBoxEdit.Visible = false;
            UpdateUserItems();
            UpdateArrowButtons();
        }

        private void ButtonArrowDown_Click(object sender, EventArgs e)
        {
            if (currentUserIndex < users.Count - 1)
            {
                ++currentUserIndex;
            }
            buttonSaveChanges.Enabled = false;
            groupBoxEdit.Visible = false;
            UpdateUserItems();
            UpdateArrowButtons();
        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            groupBoxEdit.Visible = true;
            textBoxCurrentName.Text = users[currentUserIndex].name;
            numericUpDownCurrentAge.Value = users[currentUserIndex].age;
            numericUpDownCurrentWeight.Value = Convert.ToDecimal(users[currentUserIndex].weight);
            numericUpDownCurrentHeight.Value = Convert.ToUInt16(users[currentUserIndex].height);
            radioButtonCurrentMale.Checked = users[currentUserIndex].gender == Gender.Male;
            radioButtonCurrentFemale.Checked = users[currentUserIndex].gender == Gender.Female;
        }

        private void TextBoxCurrentName_TextChanged(object sender, EventArgs e)
        {
            if (textBoxCurrentName.Text != users[currentUserIndex].name)
            {
                buttonSaveChanges.Enabled = true;
            }
        }

        private void NumericUpDownCurrentWeight_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownCurrentWeight.Value != Convert.ToDecimal(users[currentUserIndex].weight))
            {
                buttonSaveChanges.Enabled = true;
            }
        }

        private void NumericUpDownCurrentHeight_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownCurrentHeight.Value != users[currentUserIndex].height)
            {
                buttonSaveChanges.Enabled = true;
            }
        }

        private void RadioButtonCurrentMale_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCurrentMale.Checked && users[currentUserIndex].gender != Gender.Male)
            {
                buttonSaveChanges.Enabled = true;
            }
        }

        private void RadioButtonCurrentFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCurrentFemale.Checked && users[currentUserIndex].gender != Gender.Female)
            {
                buttonSaveChanges.Enabled = true;
            }
        }

        private void SetupQuiz()
        {
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
            } while(!(buttonAnswerA.Enabled && buttonAnswerB.Enabled && buttonAnswerC.Enabled && buttonAnswerD.Enabled));

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
            panelQuizFinished.BringToFront();
            labelQuizResult.Text = "Wynik: " + Quiz.score + "/5";
            Center(labelQuizResult);
        }

        private void ButtonStartQuiz_Click(object sender, EventArgs e)
        {
            panelQuiz.BringToFront();
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

        private void ButtonFinishQuiz_Click(object sender, EventArgs e)
        {
            panelQuizMenu.BringToFront();
        }

        private void UpdateActivityLevel()
        {
            users[currentUserIndex].activityLevel = 1.1f + 0.1625f * trackBarActivityLevel.Value;
            UpdateMacro();
        }

        private void ButtonSurveyTitle_Clicked(object sender, EventArgs e)
        {
            currentSurveyIndex = GetSurveyID((Button)sender);
            Survey.currentQuestionIndex = 0;
            panelSurvey.BringToFront();
            NextSurveyQuestion();
        }

        private void NextSurveyQuestion()
        {
            labelSurveyQuestionNumber.Text = "Pytanie " + (Survey.currentQuestionIndex + 1) + "/" + surveys[currentSurveyIndex].questions.Count.ToString();
            switch(surveys[currentSurveyIndex].questions[Survey.currentQuestionIndex].questionType)
            {
                case QuestionType.YES_OR_NO:
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
                case QuestionType.ABCD:
                    {
                        buttonSurveyA.Visible = true;
                        buttonSurveyB.Visible = true;
                        buttonSurveyYes.Visible = true;
                        buttonSurveyNo.Visible = true;
                        textBoxSurveyText.Visible = false;
                        buttonSurveyConfirm.Visible = false;

                        buttonSurveyNo.Text = surveys[currentSurveyIndex].questions[Survey.currentQuestionIndex].answersValues[0].Key;
                        buttonSurveyYes.Text = surveys[currentSurveyIndex].questions[Survey.currentQuestionIndex].answersValues[1].Key;
                        buttonSurveyA.Text = surveys[currentSurveyIndex].questions[Survey.currentQuestionIndex].answersValues[2].Key;
                        buttonSurveyB.Text = surveys[currentSurveyIndex].questions[Survey.currentQuestionIndex].answersValues[3].Key;
                        break;
                    } 
                case QuestionType.INPUT:
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
            labelSurveyQuestion.Text = surveys[currentSurveyIndex].questions[Survey.currentQuestionIndex].questionTitle;
            Center(labelSurveyQuestionNumber);
            Center(labelSurveyQuestion);
        }


        private void ButtonSurveyAnswer_Clicked(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            bool correctValue = true;

            if(clickedButton == buttonSurveyConfirm)
            {
                try
                {
                    if(Convert.ToInt32(textBoxSurveyText.Text) >= 0 && Convert.ToInt32(textBoxSurveyText.Text) <= surveys[currentSurveyIndex].questions[Survey.currentQuestionIndex].maxInputValue)
                    {
                        surveys[currentSurveyIndex].surveyAnswersInt.Add(Convert.ToUInt32(textBoxSurveyText.Text));
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
                foreach (KeyValuePair<string, uint> answer in surveys[currentSurveyIndex].questions[Survey.currentQuestionIndex].answersValues)
                {
                    if (clickedButton.Text == answer.Key)
                    {
                        surveys[currentSurveyIndex].surveyAnswersInt.Add(answer.Value);
                    }
                }
            }
            if(correctValue)
            {
                if(Survey.currentQuestionIndex + 1 < surveys[currentSurveyIndex].questions.Count)
                {
                    ++Survey.currentQuestionIndex;
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
            panelSurveyFinished.BringToFront();

            if(currentSurveyIndex == 0) //Poziom aktywnosci fizycznej
            {
                users[currentUserIndex].physicalJob = surveys[currentSurveyIndex].surveyAnswersInt[0] == 1;
                users[currentUserIndex].trainingsInWeek = surveys[currentSurveyIndex].surveyAnswersInt[1];
                users[currentUserIndex].dailyMovementLevel = surveys[currentSurveyIndex].surveyAnswersInt[2];

                Calculator.CalculateActivityLevel(users[currentUserIndex]);
                labelFinish.Text = "Poziom aktywności użytkownika został zaktualizowany.";
            }       
            
            Center(labelFinish);
        }

        private void ButtonSurveyFinished_Clicked(object sender, EventArgs e)
        {
            panelSurveyMenu.BringToFront();
        }

        private void TrackBarActivityLevel_Scroll(object sender, EventArgs e)
        {
            UpdateActivityLevel();
        }

        private void SetButtonAsUnclicked(Button button)
        {
            button.BackColor = darkblue1;
            button.ForeColor = Color.FromArgb(255, 255, 255);
            button.Enabled = true;
        }

        private void SetButtonAsClicked(Button button)
        {
            button.BackColor = yellow2;
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
                    if (weatherInfo.Item2)
                    {
                        weatherMessage = string.Format("Odczuwalna temperatura wynosi {0}°C.\nPogodę uznaliśmy za dobrą.", weatherInfo.Item1);
                        SetButtonAsUnclicked(buttonBadWeather);
                        SetButtonAsUnclicked(buttonAnyWeather);
                        SetButtonAsClicked(buttonGoodWeather);
                    }
                    else
                    { 
                        weatherMessage = string.Format("Odczuwalna temperatura wynosi {0}°C.\nPogodę uznaliśmy za niekorzystną\nze względu na inne warunki (np. opady).", weatherInfo.Item1);
                        SetButtonAsUnclicked(buttonGoodWeather);
                        SetButtonAsUnclicked(buttonAnyWeather);
                        SetButtonAsClicked(buttonBadWeather);
                    }
                }
                else
                {
                    weatherMessage = string.Format("Odczuwalna temperatura wynosi {0}°C.\nPogodę uznaliśmy za niekorzystną.", weatherInfo.Item1);
                    SetButtonAsUnclicked(buttonGoodWeather);
                    SetButtonAsUnclicked(buttonAnyWeather);
                    SetButtonAsClicked(buttonBadWeather);
                }

                labelWeatherInfo.Text = weatherMessage;
                Center(labelWeatherInfo);

                labelWeatherInfo.Visible = true;
            }
            catch (WebException exception)
            {
                string message = string.Format("Nie znaleziono takiego miejsca ({0}).\nUpewnij się czy nazwa jest wpisana poprawnie i spróbuj ponownie.", textBoxCity.Text);
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
            foreach (Button button in list)
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

            if (OneOfButtonsDisabled(activtyMatcherParticipantsButtons) && OneOfButtonsDisabled(activtyMatcherWeatherButtons) && OneOfButtonsDisabled(activtyMatcherEffortButtons))
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

                ActivityMatcher.Search(participants, weather, effortLevel);
                if (ActivityMatcher.currentSport == null)
                {
                    labelActivityResult.Text = "Nie znaleziono aktywności o podanych cechach.\nSpróbuj ponownie z innymi kryteriami.";
                }
                else
                {
                    labelActivityResult.Text = ActivityMatcher.currentSport.name;
                    pictureBoxSportResult.ImageLocation = string.Format("..\\..\\Resources\\Images\\{0}", ActivityMatcher.currentSport.imagePath);
                }
                Center(labelActivityResult);
                panelActivityResults.BringToFront();
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
            ActivityMatcher.Search();
            if (ActivityMatcher.currentSport == null)
            {
                labelActivityResult.Text = "Nie znaleziono aktywności o podanych cechach.\nSpróbuj ponownie z innymi kryteriami.";
            }     
            else
            {
                labelActivityResult.Text = ActivityMatcher.currentSport.name;
                pictureBoxSportResult.ImageLocation = string.Format("..\\..\\Resources\\Images\\{0}", ActivityMatcher.currentSport.imagePath);
            }
            Center(labelActivityResult);
        }

        private void Hackheroes_FormClosing(object sender, FormClosingEventArgs e)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            List<string> JSON = new List<string>();

            foreach (User user in users)
            {
                JSON.Add(JsonSerializer.Serialize(user, options));
            }

            File.WriteAllLines("..\\..\\users.json", JSON);
        }

        private void ButtonChangeSearchingData_Click(object sender, EventArgs e)
        {
            LoadSports();
            panelActivity.BringToFront();
        }
    }
}