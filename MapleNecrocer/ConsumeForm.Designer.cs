﻿namespace MapleNecrocer
{
    partial class ConsumeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            textBox1 = new TextBox();
            tabPage3 = new TabPage();
            button1 = new Button();
            panel1 = new Panel();
            UseButton = new Button();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            tabControl1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Location = new Point(6, 58);
            tabControl1.Name = "tabControl1";
            tabControl1.Padding = new Point(6, 0);
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(420, 585);
            tabControl1.TabIndex = 7;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(412, 557);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Icons";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(textBox1);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3, 3, 3, 5);
            tabPage2.Size = new Size(412, 560);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Search";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(112, 6);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(162, 24);
            textBox1.TabIndex = 5;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(button1);
            tabPage3.Location = new Point(4, 26);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(412, 560);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Consume Effect";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(120, 3);
            button1.Name = "button1";
            button1.Size = new Size(116, 27);
            button1.TabIndex = 0;
            button1.Text = "Remove";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(UseButton);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(56, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(320, 49);
            panel1.TabIndex = 8;
            // 
            // UseButton
            // 
            UseButton.Location = new Point(263, 6);
            UseButton.Name = "UseButton";
            UseButton.Size = new Size(52, 35);
            UseButton.TabIndex = 3;
            UseButton.Text = "Drop";
            UseButton.TextAlign = ContentAlignment.MiddleLeft;
            UseButton.UseVisualStyleBackColor = true;
            UseButton.Click += UseButton_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(74, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(40, 40);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 16);
            label1.Name = "label1";
            label1.Size = new Size(0, 18);
            label1.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(110, 16);
            label2.Name = "label2";
            label2.Size = new Size(0, 18);
            label2.TabIndex = 2;
            // 
            // ConsumeForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(432, 650);
            Controls.Add(panel1);
            Controls.Add(tabControl1);
            Font = new Font("Tahoma", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            KeyPreview = true;
            Name = "ConsumeForm";
            Padding = new Padding(0, 0, 0, 5);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Consume";
            TopMost = true;
            FormClosing += ConsumeForm_FormClosing;
            Shown += ConsumeForm_Shown;
            KeyDown += ConsumeForm_KeyDown;
            tabControl1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TextBox textBox1;
        private TabPage tabPage3;
        private Panel panel1;
        private Button UseButton;
        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private Button button1;
    }
}