using System;
using System.Collections.Generic;
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
            Calculator.CalculateBMI(Program.users[Program.currentUserIndex]);
            updateArrow();
            labelBMI.Text = "Twoje BMI wynosi: " + Program.users[Program.currentUserIndex].BMI.ToString("0.##");
            labelBMIInterpretation.Text = getInterpretation(Program.users[Program.currentUserIndex].BMI);
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
            changePanel(4, true);
        }

        private void buttonSurvey_Click(object sender, EventArgs e)
        {
            changePanel(5, true);
        }

        private void buttonProfile_Click(object sender, EventArgs e)
        {
            changePanel(6, true);

            listBoxUsers.SelectedIndex = Program.currentUserIndex;
            textBoxCurrentName.Text = Program.users[Program.currentUserIndex].name;
            numericUpDownCurrentAge.Value = Program.users[Program.currentUserIndex].age;
            numericUpDownCurrentHeight.Value = Program.users[Program.currentUserIndex].height;
            numericUpDownCurrentWeight.Value = Convert.ToDecimal(Program.users[Program.currentUserIndex].weight);
            if(Program.users[Program.currentUserIndex].gender == Gender.Male)
            {
                radioButtonCurrentMale.Checked = true;
            }
            else
            {
                radioButtonCurrentFemale.Checked = true;
            }

            textBoxCurrentName.Visible = false;
            numericUpDownCurrentAge.Visible = false;
            numericUpDownCurrentHeight.Visible = false;
            numericUpDownCurrentWeight.Visible = false;
            radioButtonCurrentFemale.Visible = false;
            radioButtonCurrentMale.Visible = false;
            label18.Visible = false;
            label19.Visible = false;
            label20.Visible = false;
            label22.Visible = false;
            label23.Visible = false;
            buttonDelete.Visible = false;
            buttonSaveChanges.Visible = false;
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            changePanel(0, false);
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

                Program.currentUserIndex = listBoxUsers.Items.Count - 1;
                listBoxUsers.SelectedIndex = listBoxUsers.Items.Count - 1;
            }
            else
            {
                Gender gend;
                if (radioButtonFemale.Checked)
                {
                    gend = Gender.Female;
                }
                else
                {
                    gend = Gender.Male;
                }
                User newUser = new User(textBoxName.Text, Convert.ToByte(numericUpDownAge.Value), Convert.ToSingle(numericUpDownWeight.Value), Convert.ToUInt16(numericUpDownHeight.Value), gend);
                Program.users.Add(newUser);
                listBoxUsers.Items.Add(newUser.name);

                Program.currentUserIndex = Program.users.Count - 1; 
                listBoxUsers.SelectedIndex = Program.currentUserIndex;
            }

            updateArrowButtons();
        }

        private void listBoxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateArrowButtons();

            if(listBoxUsers.SelectedIndex != -1)
            {
                Program.currentUserIndex = listBoxUsers.SelectedIndex;

                textBoxCurrentName.Text = Program.users[Program.currentUserIndex].name;
                numericUpDownCurrentAge.Value = Program.users[Program.currentUserIndex].age;
                numericUpDownCurrentWeight.Value = Convert.ToDecimal(Program.users[Program.currentUserIndex].weight);
                numericUpDownCurrentHeight.Value = Convert.ToUInt16(Program.users[Program.currentUserIndex].height);
                if (Program.users[Program.currentUserIndex].gender == Gender.Male)
                {
                    radioButtonCurrentMale.Checked = true;
                }
                else
                {
                    radioButtonCurrentFemale.Checked = true;
                }
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
            }
        }

        private void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            if (listBoxUsers.SelectedIndex != -1)
            {
                Program.users[listBoxUsers.SelectedIndex].name = textBoxCurrentName.Text;
                Program.users[listBoxUsers.SelectedIndex].age = Convert.ToByte(numericUpDownCurrentAge.Value);
                Program.users[listBoxUsers.SelectedIndex].weight = Convert.ToSingle(numericUpDownCurrentWeight.Value);
                Program.users[listBoxUsers.SelectedIndex].height = Convert.ToUInt16(numericUpDownCurrentHeight.Value);

                if (radioButtonCurrentMale.Checked)
                {
                    Program.users[listBoxUsers.SelectedIndex].gender = Gender.Male;
                }
                else
                {
                    Program.users[listBoxUsers.SelectedIndex].gender = Gender.Female;
                }
                listBoxUsers.Items[listBoxUsers.SelectedIndex] = Program.users[listBoxUsers.SelectedIndex].name;
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
            textBoxCurrentName.Visible = true;
            numericUpDownCurrentAge.Visible = true;
            numericUpDownCurrentHeight.Visible = true;
            numericUpDownCurrentWeight.Visible = true;
            radioButtonCurrentFemale.Visible = true;
            radioButtonCurrentMale.Visible = true;
            label18.Visible = true;
            label19.Visible = true;
            label20.Visible = true;
            label22.Visible = true;
            label23.Visible = true;
            buttonDelete.Visible = true;
            buttonSaveChanges.Visible = true;
        }
    }
}
