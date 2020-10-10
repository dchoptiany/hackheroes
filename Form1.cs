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
            panels.Add(panel0);
            panels.Add(panel1);
            panels.Add(panel2);
            panels.Add(panel3);
            panels.Add(panel4);
            panels.Add(panel5);
            panels.Add(panel6);
        }

        private void changePanel(int index, bool visibility)
        {
            panels[index].BringToFront();
            button9.Visible = visibility;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            changePanel(1, true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            changePanel(2, true);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            changePanel(3, true);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            changePanel(4, true);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            changePanel(5, true);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            changePanel(6, true);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            changePanel(0, false);
        }
    }
}
