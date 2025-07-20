namespace MapleNecrocer
{
    partial class RingForm
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
            panel1 = new Panel();
            button1 = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            hScrollBar1 = new HScrollBar();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Location = new Point(6, 78);
            panel1.Name = "panel1";
            panel1.Size = new Size(345, 489);
            panel1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Font = new Font("Tahoma", 13F, FontStyle.Regular, GraphicsUnit.Pixel);
            button1.Location = new Point(226, 12);
            button1.Name = "button1";
            button1.Size = new Size(95, 28);
            button1.TabIndex = 6;
            button1.Text = "刪除";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Tahoma", 13F, FontStyle.Regular, GraphicsUnit.Pixel);
            textBox1.Location = new Point(62, 15);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(116, 23);
            textBox1.TabIndex = 5;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            label1.Location = new Point(12, 17);
            label1.Name = "label1";
            label1.Size = new Size(52, 18);
            label1.TabIndex = 4;
            label1.Text = "Search";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("微軟正黑體", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            label2.Location = new Point(8, 46);
            label2.Name = "label2";
            label2.Size = new Size(42, 19);
            label2.TabIndex = 7;
            label2.Text = "長度:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Tahoma", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            label3.Location = new Point(52, 47);
            label3.Name = "label3";
            label3.Size = new Size(24, 18);
            label3.TabIndex = 8;
            label3.Text = "25";
            // 
            // hScrollBar1
            // 
            hScrollBar1.Location = new Point(90, 46);
            hScrollBar1.Maximum = 60;
            hScrollBar1.Name = "hScrollBar1";
            hScrollBar1.Size = new Size(220, 20);
            hScrollBar1.TabIndex = 9;
            hScrollBar1.Value = 25;
            hScrollBar1.Scroll += hScrollBar1_Scroll;
            // 
            // RingForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(359, 574);
            Controls.Add(hScrollBar1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(panel1);
            Font = new Font("Tahoma", 13F);
            KeyPreview = true;
            MaximumSize = new Size(377, 900);
            Name = "RingForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ring";
            TopMost = true;
            FormClosing += RingForm_FormClosing;
            Shown += RingForm_Shown;
            KeyDown += RingForm_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button button1;
        private TextBox textBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private HScrollBar hScrollBar1;
    }
}