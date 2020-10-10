namespace app
{
    partial class Hackheroes
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonMinimize = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.panel0 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonBMI = new System.Windows.Forms.Button();
            this.buttonActivity = new System.Windows.Forms.Button();
            this.buttonQuiz = new System.Windows.Forms.Button();
            this.buttonCalculator = new System.Windows.Forms.Button();
            this.buttonSurvey = new System.Windows.Forms.Button();
            this.buttonProfile = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonReturn = new System.Windows.Forms.Button();
            this.panel0.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonMinimize
            // 
            this.buttonMinimize.BackColor = System.Drawing.Color.Transparent;
            this.buttonMinimize.BackgroundImage = global::app.Properties.Resources.minimizeIcon;
            this.buttonMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMinimize.FlatAppearance.BorderSize = 0;
            this.buttonMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMinimize.Location = new System.Drawing.Point(1122, -1);
            this.buttonMinimize.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMinimize.Name = "buttonMinimize";
            this.buttonMinimize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonMinimize.Size = new System.Drawing.Size(31, 30);
            this.buttonMinimize.TabIndex = 11;
            this.buttonMinimize.UseVisualStyleBackColor = false;
            this.buttonMinimize.Click += new System.EventHandler(this.button4_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.Color.Transparent;
            this.buttonClose.BackgroundImage = global::app.Properties.Resources.closeIcon;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location = new System.Drawing.Point(1153, -1);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(0);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonClose.Size = new System.Drawing.Size(31, 30);
            this.buttonClose.TabIndex = 10;
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel0
            // 
            this.panel0.Controls.Add(this.flowLayoutPanel1);
            this.panel0.Location = new System.Drawing.Point(100, 100);
            this.panel0.Name = "panel0";
            this.panel0.Size = new System.Drawing.Size(1000, 500);
            this.panel0.TabIndex = 13;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.buttonBMI);
            this.flowLayoutPanel1.Controls.Add(this.buttonActivity);
            this.flowLayoutPanel1.Controls.Add(this.buttonQuiz);
            this.flowLayoutPanel1.Controls.Add(this.buttonCalculator);
            this.flowLayoutPanel1.Controls.Add(this.buttonSurvey);
            this.flowLayoutPanel1.Controls.Add(this.buttonProfile);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1000, 500);
            this.flowLayoutPanel1.TabIndex = 13;
            // 
            // buttonBMI
            // 
            this.buttonBMI.BackColor = System.Drawing.Color.Red;
            this.buttonBMI.FlatAppearance.BorderSize = 0;
            this.buttonBMI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBMI.Font = new System.Drawing.Font("Poppins", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonBMI.Location = new System.Drawing.Point(0, 0);
            this.buttonBMI.Margin = new System.Windows.Forms.Padding(0);
            this.buttonBMI.Name = "buttonBMI";
            this.buttonBMI.Size = new System.Drawing.Size(333, 250);
            this.buttonBMI.TabIndex = 0;
            this.buttonBMI.Text = "BMI";
            this.buttonBMI.UseVisualStyleBackColor = false;
            this.buttonBMI.Click += new System.EventHandler(this.buttonBMI_Click);
            // 
            // buttonActivity
            // 
            this.buttonActivity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonActivity.FlatAppearance.BorderSize = 0;
            this.buttonActivity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonActivity.Font = new System.Drawing.Font("Poppins", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonActivity.Location = new System.Drawing.Point(333, 0);
            this.buttonActivity.Margin = new System.Windows.Forms.Padding(0);
            this.buttonActivity.Name = "buttonActivity";
            this.buttonActivity.Size = new System.Drawing.Size(333, 250);
            this.buttonActivity.TabIndex = 1;
            this.buttonActivity.Text = "Aktywność na dzisiaj";
            this.buttonActivity.UseVisualStyleBackColor = false;
            this.buttonActivity.Click += new System.EventHandler(this.buttonActivity_Click);
            // 
            // buttonQuiz
            // 
            this.buttonQuiz.BackColor = System.Drawing.Color.Yellow;
            this.buttonQuiz.FlatAppearance.BorderSize = 0;
            this.buttonQuiz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonQuiz.Font = new System.Drawing.Font("Poppins", 48F);
            this.buttonQuiz.Location = new System.Drawing.Point(666, 0);
            this.buttonQuiz.Margin = new System.Windows.Forms.Padding(0);
            this.buttonQuiz.Name = "buttonQuiz";
            this.buttonQuiz.Size = new System.Drawing.Size(333, 250);
            this.buttonQuiz.TabIndex = 2;
            this.buttonQuiz.Text = "Quizy";
            this.buttonQuiz.UseVisualStyleBackColor = false;
            this.buttonQuiz.Click += new System.EventHandler(this.buttonQuiz_Click);
            // 
            // buttonCalculator
            // 
            this.buttonCalculator.BackColor = System.Drawing.Color.Lime;
            this.buttonCalculator.FlatAppearance.BorderSize = 0;
            this.buttonCalculator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCalculator.Font = new System.Drawing.Font("Poppins", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonCalculator.Location = new System.Drawing.Point(0, 250);
            this.buttonCalculator.Margin = new System.Windows.Forms.Padding(0);
            this.buttonCalculator.Name = "buttonCalculator";
            this.buttonCalculator.Size = new System.Drawing.Size(333, 250);
            this.buttonCalculator.TabIndex = 3;
            this.buttonCalculator.Text = "Kalkulator kalorii";
            this.buttonCalculator.UseVisualStyleBackColor = false;
            this.buttonCalculator.Click += new System.EventHandler(this.buttonCalculator_Click);
            // 
            // buttonSurvey
            // 
            this.buttonSurvey.BackColor = System.Drawing.Color.Aqua;
            this.buttonSurvey.FlatAppearance.BorderSize = 0;
            this.buttonSurvey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSurvey.Font = new System.Drawing.Font("Poppins", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonSurvey.Location = new System.Drawing.Point(333, 250);
            this.buttonSurvey.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSurvey.Name = "buttonSurvey";
            this.buttonSurvey.Size = new System.Drawing.Size(333, 250);
            this.buttonSurvey.TabIndex = 4;
            this.buttonSurvey.Text = "Ankiety diagnostyczne";
            this.buttonSurvey.UseVisualStyleBackColor = false;
            this.buttonSurvey.Click += new System.EventHandler(this.buttonSurvey_Click);
            // 
            // buttonProfile
            // 
            this.buttonProfile.BackColor = System.Drawing.Color.Blue;
            this.buttonProfile.FlatAppearance.BorderSize = 0;
            this.buttonProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonProfile.Font = new System.Drawing.Font("Poppins", 48F);
            this.buttonProfile.Location = new System.Drawing.Point(666, 250);
            this.buttonProfile.Margin = new System.Windows.Forms.Padding(0);
            this.buttonProfile.Name = "buttonProfile";
            this.buttonProfile.Size = new System.Drawing.Size(333, 250);
            this.buttonProfile.TabIndex = 5;
            this.buttonProfile.Text = "Profile";
            this.buttonProfile.UseVisualStyleBackColor = false;
            this.buttonProfile.Click += new System.EventHandler(this.buttonProfile_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(100, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 500);
            this.panel1.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Poppins", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(458, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 65);
            this.label6.TabIndex = 1;
            this.label6.Text = "BMI";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(100, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1000, 500);
            this.panel2.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Poppins", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(391, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(226, 65);
            this.label5.TabIndex = 1;
            this.label5.Text = "Aktywność";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(100, 100);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1000, 500);
            this.panel3.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Poppins", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(443, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 65);
            this.label4.TabIndex = 1;
            this.label4.Text = "Quiz";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label3);
            this.panel4.Location = new System.Drawing.Point(100, 100);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1000, 500);
            this.panel4.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Poppins", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(418, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 65);
            this.label3.TabIndex = 1;
            this.label3.Text = "Makro";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label2);
            this.panel5.Location = new System.Drawing.Point(100, 100);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1000, 500);
            this.panel5.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Poppins", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(418, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 65);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ankiety";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label1);
            this.panel6.Location = new System.Drawing.Point(100, 100);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1000, 500);
            this.panel6.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Poppins", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(418, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 65);
            this.label1.TabIndex = 0;
            this.label1.Text = "Profile";
            // 
            // buttonReturn
            // 
            this.buttonReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReturn.Font = new System.Drawing.Font("Poppins", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonReturn.Location = new System.Drawing.Point(492, 606);
            this.buttonReturn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonReturn.Name = "buttonReturn";
            this.buttonReturn.Size = new System.Drawing.Size(225, 46);
            this.buttonReturn.TabIndex = 20;
            this.buttonReturn.Text = "Powrót";
            this.buttonReturn.UseVisualStyleBackColor = true;
            this.buttonReturn.Visible = false;
            this.buttonReturn.Click += new System.EventHandler(this.buttonReturn_Click);
            // 
            // Hackheroes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.ControlBox = false;
            this.Controls.Add(this.panel0);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.buttonReturn);
            this.Controls.Add(this.buttonMinimize);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Hackheroes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hackheroes";
            this.Load += new System.EventHandler(this.Hackheroes_Load);
            this.panel0.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonMinimize;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel panel0;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonBMI;
        private System.Windows.Forms.Button buttonActivity;
        private System.Windows.Forms.Button buttonQuiz;
        private System.Windows.Forms.Button buttonCalculator;
        private System.Windows.Forms.Button buttonSurvey;
        private System.Windows.Forms.Button buttonProfile;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonReturn;
    }
}

