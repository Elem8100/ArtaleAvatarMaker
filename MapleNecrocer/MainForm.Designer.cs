
namespace MapleNecrocer
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            OpenFolderButton = new Button();
            panel1 = new Panel();
            EffectButton = new Button();
            ChatRingButton = new Button();
            EffectRingButton = new Button();
            SoulEffectButton = new Button();
            TotemEffectButton = new Button();
            ConsumeButton = new Button();
            PetButton = new Button();
            RingButton = new Button();
            TitleButton = new Button();
            MedalButton = new Button();
            MorphButton = new Button();
            CashEffectButton = new Button();
            MountButton = new Button();
            NpcButton = new Button();
            ChairButton = new Button();
            MobButton = new Button();
            ViewButton = new Button();
            label4 = new Label();
            label1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // OpenFolderButton
            // 
            OpenFolderButton.Font = new Font("Verdana", 7F);
            OpenFolderButton.Location = new Point(120, 8);
            OpenFolderButton.Name = "OpenFolderButton";
            OpenFolderButton.Size = new Size(41, 23);
            OpenFolderButton.TabIndex = 0;
            OpenFolderButton.Text = "...";
            OpenFolderButton.TextAlign = ContentAlignment.TopCenter;
            OpenFolderButton.UseVisualStyleBackColor = true;
            OpenFolderButton.Click += OpenFolderButton_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.AutoScroll = true;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(EffectButton);
            panel1.Controls.Add(ChatRingButton);
            panel1.Controls.Add(EffectRingButton);
            panel1.Controls.Add(SoulEffectButton);
            panel1.Controls.Add(TotemEffectButton);
            panel1.Controls.Add(ConsumeButton);
            panel1.Controls.Add(PetButton);
            panel1.Controls.Add(RingButton);
            panel1.Controls.Add(TitleButton);
            panel1.Controls.Add(MedalButton);
            panel1.Controls.Add(MorphButton);
            panel1.Controls.Add(CashEffectButton);
            panel1.Controls.Add(MountButton);
            panel1.Controls.Add(NpcButton);
            panel1.Controls.Add(ChairButton);
            panel1.Controls.Add(MobButton);
            panel1.Controls.Add(ViewButton);
            panel1.Location = new Point(176, 8);
            panel1.Name = "panel1";
            panel1.Size = new Size(953, 80);
            panel1.TabIndex = 11;
            // 
            // EffectButton
            // 
            EffectButton.AutoSize = true;
            EffectButton.Enabled = false;
            EffectButton.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            EffectButton.Image = (Image)resources.GetObject("EffectButton.Image");
            EffectButton.ImageAlign = ContentAlignment.TopCenter;
            EffectButton.Location = new Point(1237, 3);
            EffectButton.Name = "EffectButton";
            EffectButton.RightToLeft = RightToLeft.No;
            EffectButton.Size = new Size(78, 52);
            EffectButton.TabIndex = 29;
            EffectButton.Text = "Effect";
            EffectButton.TextAlign = ContentAlignment.BottomCenter;
            EffectButton.UseVisualStyleBackColor = true;
            EffectButton.Click += MobButton_Click;
            // 
            // ChatRingButton
            // 
            ChatRingButton.AutoSize = true;
            ChatRingButton.Enabled = false;
            ChatRingButton.Font = new Font("Microsoft JhengHei UI", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            ChatRingButton.Image = (Image)resources.GetObject("ChatRingButton.Image");
            ChatRingButton.ImageAlign = ContentAlignment.TopCenter;
            ChatRingButton.Location = new Point(1158, 3);
            ChatRingButton.Name = "ChatRingButton";
            ChatRingButton.RightToLeft = RightToLeft.No;
            ChatRingButton.Size = new Size(79, 52);
            ChatRingButton.TabIndex = 28;
            ChatRingButton.Text = "聊天戒指";
            ChatRingButton.TextAlign = ContentAlignment.BottomCenter;
            ChatRingButton.UseVisualStyleBackColor = true;
            ChatRingButton.Click += MobButton_Click;
            // 
            // EffectRingButton
            // 
            EffectRingButton.AutoSize = true;
            EffectRingButton.Enabled = false;
            EffectRingButton.Font = new Font("Microsoft JhengHei UI", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            EffectRingButton.Image = (Image)resources.GetObject("EffectRingButton.Image");
            EffectRingButton.ImageAlign = ContentAlignment.TopCenter;
            EffectRingButton.Location = new Point(850, 3);
            EffectRingButton.Name = "EffectRingButton";
            EffectRingButton.RightToLeft = RightToLeft.No;
            EffectRingButton.Size = new Size(79, 52);
            EffectRingButton.TabIndex = 27;
            EffectRingButton.Text = "特效戒指";
            EffectRingButton.TextAlign = ContentAlignment.BottomCenter;
            EffectRingButton.UseVisualStyleBackColor = true;
            EffectRingButton.Click += MobButton_Click;
            // 
            // SoulEffectButton
            // 
            SoulEffectButton.AutoSize = true;
            SoulEffectButton.Enabled = false;
            SoulEffectButton.Font = new Font("Microsoft JhengHei UI", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            SoulEffectButton.Image = (Image)resources.GetObject("SoulEffectButton.Image");
            SoulEffectButton.ImageAlign = ContentAlignment.TopCenter;
            SoulEffectButton.Location = new Point(930, 3);
            SoulEffectButton.Name = "SoulEffectButton";
            SoulEffectButton.RightToLeft = RightToLeft.No;
            SoulEffectButton.Size = new Size(79, 52);
            SoulEffectButton.TabIndex = 25;
            SoulEffectButton.Text = "靈魂珠寶";
            SoulEffectButton.TextAlign = ContentAlignment.BottomCenter;
            SoulEffectButton.UseVisualStyleBackColor = true;
            SoulEffectButton.Click += MobButton_Click;
            // 
            // TotemEffectButton
            // 
            TotemEffectButton.AutoSize = true;
            TotemEffectButton.Enabled = false;
            TotemEffectButton.Font = new Font("Microsoft JhengHei UI", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            TotemEffectButton.Image = (Image)resources.GetObject("TotemEffectButton.Image");
            TotemEffectButton.ImageAlign = ContentAlignment.TopCenter;
            TotemEffectButton.Location = new Point(377, 3);
            TotemEffectButton.Name = "TotemEffectButton";
            TotemEffectButton.RightToLeft = RightToLeft.No;
            TotemEffectButton.Size = new Size(85, 52);
            TotemEffectButton.TabIndex = 24;
            TotemEffectButton.Text = "圖騰";
            TotemEffectButton.TextAlign = ContentAlignment.BottomCenter;
            TotemEffectButton.UseVisualStyleBackColor = true;
            TotemEffectButton.Click += MobButton_Click;
            // 
            // ConsumeButton
            // 
            ConsumeButton.AutoSize = true;
            ConsumeButton.Enabled = false;
            ConsumeButton.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            ConsumeButton.Image = (Image)resources.GetObject("ConsumeButton.Image");
            ConsumeButton.ImageAlign = ContentAlignment.TopCenter;
            ConsumeButton.Location = new Point(1081, 3);
            ConsumeButton.Name = "ConsumeButton";
            ConsumeButton.RightToLeft = RightToLeft.No;
            ConsumeButton.Size = new Size(76, 52);
            ConsumeButton.TabIndex = 21;
            ConsumeButton.Text = "Consume";
            ConsumeButton.TextAlign = ContentAlignment.BottomCenter;
            ConsumeButton.UseVisualStyleBackColor = true;
            ConsumeButton.Click += MobButton_Click;
            // 
            // PetButton
            // 
            PetButton.AutoSize = true;
            PetButton.Enabled = false;
            PetButton.Font = new Font("Microsoft JhengHei UI", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            PetButton.Image = (Image)resources.GetObject("PetButton.Image");
            PetButton.ImageAlign = ContentAlignment.TopCenter;
            PetButton.Location = new Point(1010, 3);
            PetButton.Name = "PetButton";
            PetButton.RightToLeft = RightToLeft.No;
            PetButton.Size = new Size(70, 52);
            PetButton.TabIndex = 16;
            PetButton.Text = "寵物";
            PetButton.TextAlign = ContentAlignment.BottomCenter;
            PetButton.UseVisualStyleBackColor = true;
            PetButton.Click += MobButton_Click;
            // 
            // RingButton
            // 
            RingButton.AutoSize = true;
            RingButton.Enabled = false;
            RingButton.Font = new Font("Microsoft JhengHei UI", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            RingButton.Image = (Image)resources.GetObject("RingButton.Image");
            RingButton.ImageAlign = ContentAlignment.TopCenter;
            RingButton.Location = new Point(164, 3);
            RingButton.Name = "RingButton";
            RingButton.RightToLeft = RightToLeft.No;
            RingButton.Size = new Size(70, 52);
            RingButton.TabIndex = 15;
            RingButton.Text = "名牌";
            RingButton.TextAlign = ContentAlignment.BottomCenter;
            RingButton.UseVisualStyleBackColor = true;
            RingButton.Click += MobButton_Click;
            // 
            // TitleButton
            // 
            TitleButton.AutoSize = true;
            TitleButton.Enabled = false;
            TitleButton.Font = new Font("Microsoft JhengHei UI", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            TitleButton.Image = (Image)resources.GetObject("TitleButton.Image");
            TitleButton.ImageAlign = ContentAlignment.TopCenter;
            TitleButton.Location = new Point(306, 3);
            TitleButton.Name = "TitleButton";
            TitleButton.RightToLeft = RightToLeft.No;
            TitleButton.Size = new Size(70, 52);
            TitleButton.TabIndex = 14;
            TitleButton.Text = "稱號";
            TitleButton.TextAlign = ContentAlignment.BottomCenter;
            TitleButton.UseVisualStyleBackColor = true;
            TitleButton.Click += MobButton_Click;
            // 
            // MedalButton
            // 
            MedalButton.AutoSize = true;
            MedalButton.Enabled = false;
            MedalButton.Font = new Font("Microsoft JhengHei UI", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            MedalButton.Image = (Image)resources.GetObject("MedalButton.Image");
            MedalButton.ImageAlign = ContentAlignment.TopCenter;
            MedalButton.Location = new Point(235, 3);
            MedalButton.Name = "MedalButton";
            MedalButton.RightToLeft = RightToLeft.No;
            MedalButton.Size = new Size(70, 52);
            MedalButton.TabIndex = 13;
            MedalButton.Text = "勳章";
            MedalButton.TextAlign = ContentAlignment.BottomCenter;
            MedalButton.UseVisualStyleBackColor = true;
            MedalButton.Click += MobButton_Click;
            // 
            // MorphButton
            // 
            MorphButton.AutoSize = true;
            MorphButton.Enabled = false;
            MorphButton.Font = new Font("Microsoft JhengHei UI", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            MorphButton.Image = (Image)resources.GetObject("MorphButton.Image");
            MorphButton.ImageAlign = ContentAlignment.TopCenter;
            MorphButton.Location = new Point(775, 3);
            MorphButton.Name = "MorphButton";
            MorphButton.RightToLeft = RightToLeft.No;
            MorphButton.Size = new Size(74, 52);
            MorphButton.TabIndex = 10;
            MorphButton.Text = "變身";
            MorphButton.TextAlign = ContentAlignment.BottomCenter;
            MorphButton.UseVisualStyleBackColor = true;
            MorphButton.Click += MobButton_Click;
            // 
            // CashEffectButton
            // 
            CashEffectButton.AutoSize = true;
            CashEffectButton.Enabled = false;
            CashEffectButton.Font = new Font("Microsoft JhengHei UI", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            CashEffectButton.Image = (Image)resources.GetObject("CashEffectButton.Image");
            CashEffectButton.ImageAlign = ContentAlignment.TopCenter;
            CashEffectButton.Location = new Point(79, 3);
            CashEffectButton.Name = "CashEffectButton";
            CashEffectButton.RightToLeft = RightToLeft.No;
            CashEffectButton.Size = new Size(84, 52);
            CashEffectButton.TabIndex = 9;
            CashEffectButton.Text = "點商特效";
            CashEffectButton.TextAlign = ContentAlignment.BottomCenter;
            CashEffectButton.UseVisualStyleBackColor = true;
            CashEffectButton.Click += MobButton_Click;
            // 
            // MountButton
            // 
            MountButton.Enabled = false;
            MountButton.Font = new Font("Microsoft JhengHei UI", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            MountButton.Image = (Image)resources.GetObject("MountButton.Image");
            MountButton.ImageAlign = ContentAlignment.TopCenter;
            MountButton.Location = new Point(619, 3);
            MountButton.Name = "MountButton";
            MountButton.Size = new Size(77, 52);
            MountButton.TabIndex = 8;
            MountButton.Text = "騎寵";
            MountButton.TextAlign = ContentAlignment.BottomCenter;
            MountButton.UseVisualStyleBackColor = true;
            MountButton.Click += MobButton_Click;
            // 
            // NpcButton
            // 
            NpcButton.Enabled = false;
            NpcButton.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            NpcButton.Image = (Image)resources.GetObject("NpcButton.Image");
            NpcButton.ImageAlign = ContentAlignment.TopCenter;
            NpcButton.Location = new Point(541, 3);
            NpcButton.Name = "NpcButton";
            NpcButton.Size = new Size(77, 52);
            NpcButton.TabIndex = 7;
            NpcButton.Text = "Npc";
            NpcButton.TextAlign = ContentAlignment.BottomCenter;
            NpcButton.UseVisualStyleBackColor = true;
            NpcButton.Click += MobButton_Click;
            // 
            // ChairButton
            // 
            ChairButton.Enabled = false;
            ChairButton.Font = new Font("Microsoft JhengHei UI", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            ChairButton.Image = (Image)resources.GetObject("ChairButton.Image");
            ChairButton.ImageAlign = ContentAlignment.TopCenter;
            ChairButton.Location = new Point(697, 3);
            ChairButton.Name = "ChairButton";
            ChairButton.Size = new Size(77, 52);
            ChairButton.TabIndex = 4;
            ChairButton.Text = "椅子";
            ChairButton.TextAlign = ContentAlignment.BottomCenter;
            ChairButton.UseVisualStyleBackColor = true;
            ChairButton.Click += MobButton_Click;
            // 
            // MobButton
            // 
            MobButton.Enabled = false;
            MobButton.Font = new Font("Microsoft JhengHei UI", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            MobButton.Image = (Image)resources.GetObject("MobButton.Image");
            MobButton.ImageAlign = ContentAlignment.TopCenter;
            MobButton.Location = new Point(463, 3);
            MobButton.Name = "MobButton";
            MobButton.Size = new Size(77, 52);
            MobButton.TabIndex = 3;
            MobButton.Text = "怪物";
            MobButton.TextAlign = ContentAlignment.BottomCenter;
            MobButton.UseVisualStyleBackColor = true;
            MobButton.Click += MobButton_Click;
            // 
            // ViewButton
            // 
            ViewButton.Enabled = false;
            ViewButton.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            ViewButton.Image = (Image)resources.GetObject("ViewButton.Image");
            ViewButton.ImageAlign = ContentAlignment.TopCenter;
            ViewButton.Location = new Point(1, 3);
            ViewButton.Name = "ViewButton";
            ViewButton.Size = new Size(77, 52);
            ViewButton.TabIndex = 0;
            ViewButton.Text = "View";
            ViewButton.TextAlign = ContentAlignment.BottomCenter;
            ViewButton.UseVisualStyleBackColor = true;
            ViewButton.Click += MobButton_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("微軟正黑體", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            label4.Location = new Point(12, 11);
            label4.Name = "label4";
            label4.Size = new Size(105, 20);
            label4.TabIndex = 12;
            label4.Text = "楓之谷資料夾";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Regular, GraphicsUnit.Pixel);
            label1.Location = new Point(12, 45);
            label1.Name = "label1";
            label1.Size = new Size(0, 32);
            label1.TabIndex = 13;
            // 
            // MainForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1175, 913);
            Controls.Add(label1);
            Controls.Add(label4);
            Controls.Add(panel1);
            Controls.Add(OpenFolderButton);
            DoubleBuffered = true;
            Font = new Font("Microsoft JhengHei UI", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MaximizeBox = false;
            MaximumSize = new Size(5000, 5000);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ArtaleAvatarMaker";
            Load += MainForm_Load;
            KeyDown += MainForm_KeyDown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button OpenFolderButton;
        private Panel panel1;
        public Button ViewButton;
        public Button ChairButton;
        public Button MobButton;
        public Button NpcButton;
        public Label label4;
        public Button MountButton;
        public Button CashEffectButton;
        private Button MorphButton;
        public Button MedalButton;
        public Button TitleButton;
        public Button RingButton;
        private Button PetButton;
        private Button ConsumeButton;
        public Button TotemEffectButton;
        public Button SoulEffectButton;
        public Button EffectRingButton;
        public Button ChatRingButton;
        public Button EffectButton;
        public Label label1;
    }
}