using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace app
{
    public partial class Hackheroes : Form
    {
        private readonly List<Panel> panels = new List<Panel>();

        public Hackheroes()
        {
            InitializeComponent();
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
        }

        private void changePanel(int index, bool visibility)
        {
            panels[index].BringToFront();
            buttonReturn.Visible = visibility;
        }

        private void buttonBMI_Click(object sender, EventArgs e)
        {
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
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            changePanel(0, false);
        }

        private void numericUpDownAge_ValueChanged(object sender, EventArgs e)
        {
            uint val = Convert.ToUInt16(numericUpDownAge.Value);
            if(val == 1)
            {
                label15.Text = "rok";
            }
            else if (val >= 2 && val <= 4)
            {
                label15.Text = "lata";
            }
            else if (val >= 5 && val <= 21)
            {
                label15.Text = "lat";
            }
            else
            {
                uint lastDigit = val % 10;
                switch (lastDigit)
                {
                    case 2:
                    case 3:
                    case 4:
                        label15.Text = "lata";
                        break;
                    default:
                        label15.Text = "lat";
                        break;
                }
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            Program.users.Add(new User(textBoxName.Text, Convert.ToByte(numericUpDownAge.Value), Convert.ToSingle(numericUpDownWeight.Value), Convert.ToByte(numericUpDownHeight.Value), Convert.ToBoolean(radioButtonFemale.Checked);
        }
    }
}
