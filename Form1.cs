using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace app
{
    public partial class Hackheroes : Form
    {
        private readonly List<Panel> panels = new List<Panel>();

        public Hackheroes()
        {
            InitializeComponent();
        }

        public static class ModifyProgressBarColor
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
            static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
            public static void SetState(ProgressBar bar, int state)
            {
                SendMessage(bar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Hackheroes_Load(object sender, EventArgs e)
        {
            using(StreamReader loading = new StreamReader("..\\..\\users.dat"))
            {
                string name;
                byte age;
                float weight;
                uint height;
                Gender gender;

                string line;
                string[] arr = new string[4];

                while(!loading.EndOfStream)
                {
                    name = loading.ReadLine();

                    line = loading.ReadLine();
                    arr = line.Split(' ');

                    age = Convert.ToByte(arr[0]);
                    weight = Convert.ToSingle(arr[1]);
                    height = Convert.ToUInt32(arr[2]);
                    gender = arr[3] == "Female" ? Gender.Female : Gender.Male;

                    Program.users.Add(new User(name, age, weight, height, gender));
                    listBoxUsers.Items.Add(name);
                }
            }

            if(Program.users.Count == 0)
            {
                Program.users.Add(new User("User", 18, 80f, 180, Gender.Male));
            }

            panels.Add(panel0); //buttons
            panels.Add(panel1); //BMI
            panels.Add(panel2); //sport activity
            panels.Add(panel3); //quiz
            panels.Add(panel4); //calculator
            panels.Add(panel5); //surveys
            panels.Add(panel6); //profiles

            center(label6, 30); //BMI
        }

        private void changePanel(int index, bool visibility)
        {
            panels[index].BringToFront();
            buttonReturn.Visible = visibility;
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

        private void updateArrow()
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

        private string getInterpretation(float BMI)
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

        private void center(Control control, int h)
        {
            control.Location = new Point(1000 / 2 - control.Size.Width / 2, h);
        }

        private void buttonBMI_Click(object sender, EventArgs e)
        {
            int userIndex = Program.currentUserIndex;
            Calculator.CalculateBMI(Program.users[userIndex]);

            updateArrow();

            labelBMI.Text = "Twoje BMI wynosi: " + Program.users[userIndex].BMI.ToString("0.##");
            labelBMIInterpretation.Text = getInterpretation(Program.users[userIndex].BMI);

            center(labelBMI, 300);
            center(labelBMIInterpretation, 360);

            changePanel(1, true);
        }

        private void buttonActivity_Click(object sender, EventArgs e)
        {
            changePanel(2, true);
        }

        private void buttonQuiz_Click(object sender, EventArgs e)
        {
            changePanel(3, true);
        }

        private void buttonCalculator_Click(object sender, EventArgs e)
        {
            Calculator.CalculateMacro(Program.users[Program.currentUserIndex]);
            changePanel(4, true);
        }

        private void buttonSurvey_Click(object sender, EventArgs e)
        {
            changePanel(5, true);
        }

        private void buttonProfile_Click(object sender, EventArgs e)
        {
            changePanel(6, true);

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

            updateArrowButtons();
            setEditInfoVisibility(false);
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            changePanel(0, false);
        }

        private void setEditInfoVisibility(bool visibility)
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

        private void updateArrowButtons()
        {
            buttonArrowUp.Enabled = (listBoxUsers.SelectedIndex > 0);
            buttonArrowDown.Enabled = (listBoxUsers.SelectedIndex < listBoxUsers.Items.Count - 1);
        }

        private void updateAgeForm(Label lbl, NumericUpDown numericUD)
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

        private void numericUpDownAge_ValueChanged(object sender, EventArgs e)
        {
            updateAgeForm(label15, numericUpDownAge);
        }

        private void numericUpDownCurrentAge_ValueChanged(object sender, EventArgs e)
        {
            updateAgeForm(label17, numericUpDownCurrentAge);

            if(numericUpDownCurrentAge.Value != Program.users[Program.currentUserIndex].age)
            {
                buttonSaveChanges.Enabled = true;
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
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
                if (result == System.Windows.Forms.DialogResult.Yes)
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

                Program.currentUserIndex = listBoxUsers.SelectedIndex = Program.users.Count - 1; 
            }

            UpdateButtonDeleteEnabledStatus();
            setEditInfoVisibility(false);
            updateArrowButtons();
        }

        private void listBoxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateArrowButtons();
            setEditInfoVisibility(false);

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

        private void buttonDelete_Click_1(object sender, EventArgs e)
        {
            int indexToRemove = listBoxUsers.SelectedIndex;
            if (listBoxUsers.Items.Count <= 1)
            {
                string message = "Nie można usunąć jedynego istniejącego profilu!\nUtwórz nowy profil lub edytuj już istniejący.";
                string caption = "Nie można usunąć profilu";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
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
                updateArrowButtons();
                setEditInfoVisibility(false);
                buttonSaveChanges.Enabled = false;
            }
        }

        private void buttonSaveChanges_Click(object sender, EventArgs e)
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
                setEditInfoVisibility(false);
                buttonSaveChanges.Enabled = false;
            }
            else
            {
                string message = "Aby edytować dane, musisz najpierw zaznaczyć profil!";
                string caption = "Nie zaznaczono profilu";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    Close();
                }
            }
        }

        private void buttonArrowUp_Click(object sender, EventArgs e)
        {
            if (listBoxUsers.SelectedIndex > 0)
            {
                --listBoxUsers.SelectedIndex;
            }
        }

        private void buttonArrowDown_Click(object sender, EventArgs e)
        {
            if (listBoxUsers.SelectedIndex < listBoxUsers.Items.Count - 1)
            {
                ++listBoxUsers.SelectedIndex;
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            setEditInfoVisibility(true);
        }

        private void Hackheroes_FormClosing(object sender, FormClosingEventArgs e)
        {
            using(StreamWriter saving = new StreamWriter("..\\..\\users.dat"))
            {
                foreach (User user in Program.users)
                {
                    saving.WriteLine(user.name);
                    saving.WriteLine(user.getData());
                }
            }          
        }

        private void textBoxCurrentName_TextChanged(object sender, EventArgs e)
        {
            if (textBoxCurrentName.Text != Program.users[Program.currentUserIndex].name)
            {
                buttonSaveChanges.Enabled = true;
            }
        }

        private void numericUpDownCurrentWeight_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownCurrentWeight.Value != Convert.ToDecimal(Program.users[Program.currentUserIndex].weight))
            {
                buttonSaveChanges.Enabled = true;
            }
        }

        private void numericUpDownCurrentHeight_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownCurrentHeight.Value != Program.users[Program.currentUserIndex].height)
            {
                buttonSaveChanges.Enabled = true;
            }
        }

        private void radioButtonCurrentMale_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCurrentMale.Checked && Program.users[Program.currentUserIndex].gender != Gender.Male)
            {
                buttonSaveChanges.Enabled = true;
            }
        }

        private void radioButtonCurrentFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCurrentFemale.Checked && Program.users[Program.currentUserIndex].gender != Gender.Female)
            {
                buttonSaveChanges.Enabled = true;
            }
        }

        private void SetupQuiz()
        {
            ButtonStartQuiz.Visible = false;
            tableLayoutPanelAnswers.Visible = true;
            ButtonAnswerA.Visible = true;
            ButtonAnswerB.Visible = true;
            ButtonAnswerC.Visible = true;
            ButtonAnswerD.Visible = true;
            center(labelQuestion, 130);
        }

        private void FinishQuiz()
        {
            tableLayoutPanelAnswers.Visible = false;
            ButtonAnswerA.Visible = false;
            ButtonAnswerB.Visible = false;
            ButtonAnswerC.Visible = false;
            ButtonAnswerD.Visible = false;
            labelQuizResult.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetupQuiz();
        }
    }
}