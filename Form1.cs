using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text.Json;

namespace app
{
    public partial class Hackheroes : Form
    {
        private List<Survey> surveys;
        private int currentSurveyIndex;

        private List<User> users;
        private int currentUserIndex;
        private readonly List<Button> surveyButtons = new List<Button>();

        private readonly List<Panel> panels = new List<Panel>();
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

            Color leftPanelBackColor = green2;
            flowLayoutPanel1.BackColor = leftPanelBackColor;
            panelProfileSetup.BackColor = leftPanelBackColor;

            Color leftPanelButtonsColor = green2;
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
            menuButtons.Add(buttonProfilePicture);
        }

        private void InitializeProfile()
        {
            buttonProfile.BackColor = green2;
            buttonProfile.Text = users[currentUserIndex].name;
        }

        private void DisableButton(object sender, EventArgs e, bool isProfile = false)
        {
            var clickedButton = (Button)sender;    
            foreach (Button _button in menuButtons)
            {
                _button.Enabled = true;
                _button.BackColor = green2;
                panelProfileSetup.BackColor = green2;
                if (_button.Text == clickedButton.Text)
                {
                    _button.Enabled = false;
                }
            }
         
            if (!isProfile)
            {
                clickedButton.BackColor = green1;
                panelPointer.Location = new Point(0, clickedButton.Location.Y + panelProfileSetup.Size.Height);
                panelPointer.Height = clickedButton.Height;
            }
            else
            {
                buttonProfilePicture.Enabled = false;
                buttonProfile.Enabled = false;
                buttonProfile.BackColor = green1;
                panelProfileSetup.BackColor = green1;
                buttonProfilePicture.BackColor = green1;
                panelPointer.Location = new Point(0, 0);
                panelPointer.Height = panelProfileSetup.Height;
            }
            panelPointer.Visible = true;   
        }

        private void ChangePanel(int index)
        {
            panels[index].BringToFront();
        }

        private void ChangePanel(Panel panel)
        {
            panel.BringToFront();
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

                for (int i = 0; i < JSON.Length; i += 6)
                {
                    sportLine = string.Empty;

                    for (int line = 0; line < 6; line++)
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

                for (int i = 0; i < JSON.Length; i += 7)
                {
                    userLine = string.Empty;
                    for (int line = 0; line < 7; line++)
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
                users.Add(new User("User", 18, 80f, 180, Gender.Male));
            }
            finally
            {
                foreach (User user in users)
                {
                    listBoxUsers.Items.Add(user.name);
                }
            }
        }

        private void Hackheroes_Load(object sender, EventArgs e)
        {
            panels.Add(panel0); //buttons
            panels.Add(panel1); //BMI
            panels.Add(panel2); //sport activity
            panels.Add(panel3); //quiz
            panels.Add(panel4); //calculator
            panels.Add(panel5); //surveys
            panels.Add(panel6); //profiles

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

            LoadQuestions();
            LoadSports();
            LoadSurveys();
            LoadUsers();

            InitializeProfile();
            Center(buttonProfile);

            ChangePanel(0);
        }

        private void Disable(Button button)
        {
            button.Enabled = false;
            button.BackColor = Color.FromArgb(127, 143, 166);
        }

        private int GetSurveyID(Button button)
        {
            for(int i = 0; i < surveyButtons.Count; i++)
            {
                if(button == surveyButtons[i])
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

        private void UpdateResultOfMatching()
        {
            Participants participants = Participants.Any;
            if (radioButtonIndividual.Checked)
            {
                participants = Participants.One;
            }
            else if (radioButtonPair.Checked)
            {
                participants = Participants.Two;
            }
            else if (radioButtonTeam.Checked)
            {
                participants = Participants.More;
            }
            // TO DO: getting weather from API and decide if good or bad
            /*
             * if(checkBoxChooseAutomatically.Checked)
             * {
             *      if(weather == rainy || weather == snowy || weather == windy || temperature < 15)
             *      {
             *          goodWeather = Weather.Bad;
             *      }
             *      else
             *      {
             *          goodWeather = Weather.Good;
             *      }
             * }
             */

            Weather weather = Weather.Any;
            if (radioButtonGoodWeather.Checked)
            {
                weather = Weather.Good;
            }
            else if (radioButtonBadWeather.Checked)
            {
                weather = Weather.Bad;
            }

            EffortLevel effortLevel = EffortLevel.Any;
            if (!radioButtonAllEffortLevels.Checked)
            {
                switch (trackBarEffortLevel.Value)
                {
                    case 0:
                        effortLevel = EffortLevel.Low;
                        break;
                    case 1:
                        effortLevel = EffortLevel.Medium;
                        break;
                    case 2:
                        effortLevel = EffortLevel.High;
                        break;
                }
            }
            
            labelActivityResult.Text = ActivityMatcher.Search(participants, weather, effortLevel);
            Center(labelActivityResult);

            if (labelActivityResult.Text == "")
            {
                string message = "Nie udało się znaleźć aktywności o takich kryteriach. Wprowadź inne dane i spróbuj ponownie.";
                string caption = "Nie znaleziono aktywności";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    Close();
                }
            }
        }

        private void UpdateMacro()
        {
            Calculator.CalculateMacro(users[currentUserIndex]);
            labelKcal.Text = (users[currentUserIndex].calories).ToString();
            labelFats.Text = (users[currentUserIndex].fat).ToString();
            labelCarbohydrates.Text = (users[currentUserIndex].carbohydrates).ToString();
            labelProtein.Text = (users[currentUserIndex].protein).ToString();
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
            float BMI = users[currentUserIndex].BMI;
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
            if(BMI < 16f)
            {
                return "Wygłodzenie";
            }
            else if(BMI<17f)
            {
                return "Wychudzenie";
            }
            else if(BMI<18.5f)
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
            else if(BMI<35f)
            {
                return "I stopień otyłości";
            }
            else if(BMI<40f)
            {
                return "II stopień otyłości";
            }
            else
            {
                return "Otyłość skrajna";
            }
        }

        private void Center(Control control)
        {
            control.Location = new Point(control.Parent.Size.Width / 2 - control.Size.Width / 2, control.Location.Y);
        }

        private void ButtonBMI_Click(object sender, EventArgs e)
        {
            DisableButton(sender, e);
            int userIndex = currentUserIndex;

            if(!Calculator.CalculateBMI(users[userIndex]))
            {
                ChangePanel(6);
                return;
            }

            UpdateArrow();

            labelBMI.Text = "Twoje BMI wynosi: " + users[userIndex].BMI.ToString("0.##");
            labelBMIInterpretation.Text = GetInterpretation(users[userIndex].BMI);

            Center(labelBMI);
            Center(labelBMIInterpretation);

            ChangePanel(1);
        }

        private void ButtonActivity_Click(object sender, EventArgs e)
        {
            DisableButton(sender, e);
            ChangePanel(2);

            radioButtonAllParticipants.Checked = true;
            radioButtonAllWeatherConditions.Checked = true;
            radioButtonAllEffortLevels.Checked = true;
            groupBoxWeather.Enabled = !checkBoxChooseAutomatically.Checked;
            UpdateResultOfMatching();
        }

        private void ButtonQuiz_Click(object sender, EventArgs e)
        {
            DisableButton(sender, e);
            ChangePanel(3);
        }

        private void ButtonCalculator_Click(object sender, EventArgs e)
        {
            DisableButton(sender, e);
            ChangePanel(4);
            UpdateActivityLevel();
        }

        private void ButtonSurvey_Click(object sender, EventArgs e)
        {
            DisableButton(sender, e);

            ChangePanel(5);
        }

        private void ButtonProfile_Click(object sender, EventArgs e)
        {
            DisableButton(sender, e, true);
            ChangePanel(6);

            UpdateButtonDeleteEnabledStatus();
            buttonSaveChanges.Enabled = false;

            int userIndex = listBoxUsers.SelectedIndex = currentUserIndex;

            textBoxCurrentName.Text = users[userIndex].name;
            numericUpDownCurrentAge.Value = users[userIndex].age;
            numericUpDownCurrentHeight.Value = users[userIndex].height;
            numericUpDownCurrentWeight.Value = Convert.ToDecimal(users[userIndex].weight);
            
            if(users[userIndex].gender == Gender.Male)
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

            if(numericUpDownCurrentAge.Value != users[currentUserIndex].age)
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

                currentUserIndex = listBoxUsers.SelectedIndex = listBoxUsers.Items.Count - 1;
            }
            else
            {
                Gender gender = radioButtonMale.Checked == true ? Gender.Male : Gender.Female;

                User newUser = new User(textBoxName.Text, Convert.ToByte(numericUpDownAge.Value), Convert.ToSingle(numericUpDownWeight.Value), Convert.ToUInt16(numericUpDownHeight.Value), gender);

                users.Add(newUser);
                listBoxUsers.Items.Add(newUser.name);

                textBoxName.Text = "";

                currentUserIndex = listBoxUsers.SelectedIndex = users.Count - 1; 
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
                int userIndex = currentUserIndex = listBoxUsers.SelectedIndex;

                textBoxCurrentName.Text = users[userIndex].name;
                numericUpDownCurrentAge.Value = users[userIndex].age;
                numericUpDownCurrentWeight.Value = Convert.ToDecimal(users[userIndex].weight);
                numericUpDownCurrentHeight.Value = Convert.ToUInt16(users[userIndex].height);
                radioButtonCurrentMale.Checked = users[userIndex].gender == Gender.Male;
                radioButtonCurrentFemale.Checked = users[userIndex].gender == Gender.Female;
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
                users.RemoveAt(indexToRemove);
                listBoxUsers.Items.RemoveAt(indexToRemove);

                listBoxUsers.SelectedIndex = currentUserIndex = 0;

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

                users[userIndex].name = textBoxCurrentName.Text;
                users[userIndex].age = Convert.ToByte(numericUpDownCurrentAge.Value);
                users[userIndex].weight = Convert.ToSingle(numericUpDownCurrentWeight.Value);
                users[userIndex].height = Convert.ToUInt16(numericUpDownCurrentHeight.Value);

                if (radioButtonCurrentMale.Checked)
                {
                    users[userIndex].gender = Gender.Male;
                }
                else
                {
                    users[userIndex].gender = Gender.Female;
                }
                listBoxUsers.Items[userIndex] = users[userIndex].name;
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
            ButtonStartQuiz.Visible = false;
            labelNumber.Visible = true;
            pictureBoxTime.Visible = true;
            pictureBoxTimeBorder.Visible = true;
            labelQuestion.Visible = true;
            tableLayoutPanelAnswers.Visible = true;
            buttonAnswerA.Visible = true;
            buttonAnswerB.Visible = true;
            buttonAnswerC.Visible = true;
            buttonAnswerD.Visible = true;

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
            labelQuizResult.Text = "Wynik: " + Quiz.score + "/5";
            Center(labelQuizResult);
            labelQuestion.Visible = false;
            tableLayoutPanelAnswers.Visible = false;
            buttonAnswerA.Visible = false;
            buttonAnswerB.Visible = false;
            buttonAnswerC.Visible = false;
            buttonAnswerD.Visible = false;
            labelQuizResult.Visible = true;
            buttonFinishQuiz.Visible = true;
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

        private void ResetQuiz()
        {
            labelQuizResult.Visible = false;
            buttonFinishQuiz.Visible = false;
            ButtonStartQuiz.Visible = true;
        }

        private void ButtonFinishQuiz_Click(object sender, EventArgs e)
        {
            ResetQuiz();
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
            ChangePanel(panelSurvey);
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

                        buttonSurveyA.Text = surveys[currentSurveyIndex].questions[Survey.currentQuestionIndex].answersValues[0].Key;
                        buttonSurveyB.Text = surveys[currentSurveyIndex].questions[Survey.currentQuestionIndex].answersValues[1].Key;
                        buttonSurveyYes.Text = surveys[currentSurveyIndex].questions[Survey.currentQuestionIndex].answersValues[2].Key;
                        buttonSurveyNo.Text = surveys[currentSurveyIndex].questions[Survey.currentQuestionIndex].answersValues[3].Key;
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
                Console.WriteLine("index = " + Survey.currentQuestionIndex);
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
            ChangePanel(panelSurveyFinished);

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

        private void TrackBarActivityLevel_Scroll(object sender, EventArgs e)
        {
            UpdateActivityLevel();
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            UpdateResultOfMatching();
        }

        private void TrackBarEffortLevel_Scroll(object sender, EventArgs e)
        {
            radioButtonAllEffortLevels.Checked = false;
            ActivityMatcher.approvedSports.Clear();
            UpdateResultOfMatching();
        }
        private void TrackBarEffortLevel_Click(object sender, EventArgs e)
        {
            radioButtonAllEffortLevels.Checked = false;
            ActivityMatcher.approvedSports.Clear();
            UpdateResultOfMatching();
        }

        private void RadioButtonTeam_CheckedChanged(object sender, EventArgs e)
        {
            ActivityMatcher.approvedSports.Clear();
            UpdateResultOfMatching();
        }

        private void RadioButtonAllParticipants_CheckedChanged(object sender, EventArgs e)
        {
            ActivityMatcher.approvedSports.Clear();
            UpdateResultOfMatching();
        }

        private void RadioButtonGoodWeather_CheckedChanged(object sender, EventArgs e)
        {
            ActivityMatcher.approvedSports.Clear();
            UpdateResultOfMatching();
        }

        private void RadioButtonBadWeather_CheckedChanged(object sender, EventArgs e)
        {
            ActivityMatcher.approvedSports.Clear();
            UpdateResultOfMatching();
        }

        private void RadioButtonAllWeatherConditions_CheckedChanged(object sender, EventArgs e)
        {
            ActivityMatcher.approvedSports.Clear();
            UpdateResultOfMatching();
        }

        private void RadioButtonAllEffortLevels_CheckedChanged(object sender, EventArgs e)
        {
            ActivityMatcher.approvedSports.Clear();
            UpdateResultOfMatching();
        }

        private void RadioButtonIndividual_CheckedChanged(object sender, EventArgs e)
        {
            ActivityMatcher.approvedSports.Clear();
            UpdateResultOfMatching();
        }

        private void CheckBoxChooseAutomatically_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxWeather.Enabled = !checkBoxChooseAutomatically.Checked;
            UpdateResultOfMatching();
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
    }
}