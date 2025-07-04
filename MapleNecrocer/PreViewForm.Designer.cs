namespace MapleNecrocer;

partial class PreViewForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreViewForm));
        pictureBox1 = new PictureBox();
        button1 = new Button();
        button2 = new Button();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        SuspendLayout();
        // 
        // pictureBox1
        // 
        pictureBox1.Dock = DockStyle.Fill;
        pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
        pictureBox1.Location = new Point(0, 0);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(970, 2806);
        pictureBox1.TabIndex = 0;
        pictureBox1.TabStop = false;
        // 
        // button1
        // 
        button1.Font = new Font("Microsoft JhengHei UI", 12F);
        button1.Location = new Point(780, 513);
        button1.Name = "button1";
        button1.Size = new Size(170, 40);
        button1.TabIndex = 1;
        button1.Text = "儲存PSD";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // button2
        // 
        button2.Font = new Font("Microsoft JhengHei UI", 12F);
        button2.Location = new Point(800, 2766);
        button2.Name = "button2";
        button2.Size = new Size(170, 40);
        button2.TabIndex = 2;
        button2.Text = "儲存PSD";
        button2.UseVisualStyleBackColor = true;
        button2.Click += button2_Click;
        // 
        // PreViewForm
        // 
        AutoScaleDimensions = new SizeF(9F, 19F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoScroll = true;
        ClientSize = new Size(800, 450);
        Controls.Add(button2);
        Controls.Add(button1);
        Controls.Add(pictureBox1);
        Name = "PreViewForm";
        Text = "PreView";
        WindowState = FormWindowState.Maximized;
        FormClosing += PreViewForm_FormClosing;
        Load += PreViewForm_Load;
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private PictureBox pictureBox1;
    private Button button1;
    private Button button2;
}
