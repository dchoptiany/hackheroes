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
        private readonly List<Panel> panels = new List<Panel>();
        private List<Button> answerButtons = new List<Button>();

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

                answerButtons.Add(ButtonAnswerA);
                answerButtons.Add(ButtonAnswerB);
                answerButtons.Add(ButtonAnswerC);
                answerButtons.Add(ButtonAnswerD);

                ChangePanel(0);
            }
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
            Center(labelActivityResult, 370);

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

        private void Center(Control control, int h)
        {
            control.Location = new Point(1000 / 2 - control.Size.Width / 2, h);
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

            Center(labelBMI, 300);
            Center(labelBMIInterpretation, 360);

            ChangePanel(1);
        }

        private void ButtonActivity_Click(object sender, EventArgs e)
        {
            ChangePanel(2);
            ActivityMatcher.LoadSports();

            radioButtonAllParticipants.Checked = true;
            radioButtonAllWeatherConditions.Checked = true;
            radioButtonAllEffortLevels.Checked = true;
            groupBoxWeather.Enabled = !checkBoxChooseAutomatically.Checked;
            UpdateResultOfMatching();
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
            Center(labelQuestion, 110);

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
            Center(labelQuizResult, 200);
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

        private void TrackBarActivityLevel_Scroll(object sender, EventArgs e)
        {
            UpdateActivityLevel();
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
    }
}