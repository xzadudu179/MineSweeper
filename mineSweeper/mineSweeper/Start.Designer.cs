namespace mineSweeper
{
    partial class Start
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Start));
            label1 = new Label();
            label2 = new Label();
            widthBox = new TextBox();
            heightBox = new TextBox();
            panel1 = new Panel();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            sizeShowText = new Label();
            label3 = new Label();
            panel2 = new Panel();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            spaceShowText = new Label();
            label5 = new Label();
            whNotice = new Label();
            label4 = new Label();
            minesCountTBox = new TextBox();
            exitButton = new Button();
            startButton = new Button();
            isErrorMessageShowBox = new CheckBox();
            MenuMain = new MenuStrip();
            MenuFile = new ToolStripMenuItem();
            MenuItemSave = new ToolStripMenuItem();
            loadMapStrip = new ToolStripMenuItem();
            difficultyMenu = new ToolStripMenuItem();
            ezMenu = new ToolStripMenuItem();
            medMenu = new ToolStripMenuItem();
            hardMenu = new ToolStripMenuItem();
            helpStrip = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            latestStat = new ToolStripStatusLabel();
            rankingListStrip = new ToolStripMenuItem();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            MenuMain.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(61, 46);
            label1.Name = "label1";
            label1.Size = new Size(20, 17);
            label1.TabIndex = 0;
            label1.Text = "宽";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(61, 76);
            label2.Name = "label2";
            label2.Size = new Size(20, 17);
            label2.TabIndex = 1;
            label2.Text = "高";
            // 
            // widthBox
            // 
            widthBox.Location = new Point(87, 43);
            widthBox.MaxLength = 9;
            widthBox.Name = "widthBox";
            widthBox.Size = new Size(100, 23);
            widthBox.TabIndex = 2;
            widthBox.KeyDown += WidthBox_KeyDown;
            widthBox.Leave += widthBox_Leave;
            // 
            // heightBox
            // 
            heightBox.Location = new Point(87, 73);
            heightBox.MaxLength = 9;
            heightBox.Name = "heightBox";
            heightBox.Size = new Size(100, 23);
            heightBox.TabIndex = 3;
            heightBox.KeyDown += HeightBox_KeyDown;
            heightBox.Leave += heightBox_Leave;
            // 
            // panel1
            // 
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(sizeShowText);
            panel1.Controls.Add(label3);
            panel1.Location = new Point(65, 117);
            panel1.Name = "panel1";
            panel1.Size = new Size(122, 164);
            panel1.TabIndex = 4;
            // 
            // button3
            // 
            button3.Location = new Point(23, 121);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 4;
            button3.Text = "大(45px)";
            button3.UseVisualStyleBackColor = true;
            button3.Click += buttonSizeLarge_Click;
            // 
            // button2
            // 
            button2.Location = new Point(23, 80);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 3;
            button2.Text = "中(35px)";
            button2.UseVisualStyleBackColor = true;
            button2.Click += buttonSizeMid_Click;
            // 
            // button1
            // 
            button1.Location = new Point(23, 40);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "小(25px)";
            button1.UseVisualStyleBackColor = true;
            button1.Click += buttonSizeSmall_Click;
            // 
            // sizeShowText
            // 
            sizeShowText.AutoSize = true;
            sizeShowText.Location = new Point(68, 11);
            sizeShowText.Name = "sizeShowText";
            sizeShowText.Size = new Size(15, 17);
            sizeShowText.TabIndex = 1;
            sizeShowText.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 11);
            label3.Name = "label3";
            label3.Size = new Size(59, 17);
            label3.TabIndex = 0;
            label3.Text = "格子大小:";
            // 
            // panel2
            // 
            panel2.Controls.Add(button4);
            panel2.Controls.Add(button5);
            panel2.Controls.Add(button6);
            panel2.Controls.Add(spaceShowText);
            panel2.Controls.Add(label5);
            panel2.Location = new Point(216, 117);
            panel2.Name = "panel2";
            panel2.Size = new Size(122, 164);
            panel2.TabIndex = 5;
            // 
            // button4
            // 
            button4.Location = new Point(23, 121);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 4;
            button4.Text = "4px";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button1_Click;
            // 
            // button5
            // 
            button5.Location = new Point(23, 80);
            button5.Name = "button5";
            button5.Size = new Size(75, 23);
            button5.TabIndex = 3;
            button5.Text = "2px";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button3_Click;
            // 
            // button6
            // 
            button6.Location = new Point(23, 40);
            button6.Name = "button6";
            button6.Size = new Size(75, 23);
            button6.TabIndex = 2;
            button6.Text = "无";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button2_Click;
            // 
            // spaceShowText
            // 
            spaceShowText.AutoSize = true;
            spaceShowText.Location = new Point(68, 11);
            spaceShowText.Name = "spaceShowText";
            spaceShowText.Size = new Size(15, 17);
            spaceShowText.TabIndex = 1;
            spaceShowText.Text = "0";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 11);
            label5.Name = "label5";
            label5.Size = new Size(59, 17);
            label5.TabIndex = 0;
            label5.Text = "格子间距:";
            // 
            // whNotice
            // 
            whNotice.AutoSize = true;
            whNotice.Location = new Point(213, 46);
            whNotice.Name = "whNotice";
            whNotice.Size = new Size(101, 17);
            whNotice.TabIndex = 6;
            whNotice.Text = " 注意:宽高最高40";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(57, 299);
            label4.Name = "label4";
            label4.Size = new Size(35, 17);
            label4.TabIndex = 7;
            label4.Text = "雷数:";
            // 
            // minesCountTBox
            // 
            minesCountTBox.Location = new Point(98, 296);
            minesCountTBox.MaxLength = 9;
            minesCountTBox.Name = "minesCountTBox";
            minesCountTBox.Size = new Size(100, 23);
            minesCountTBox.TabIndex = 8;
            minesCountTBox.KeyDown += minesCountTBox_KeyDown;
            minesCountTBox.Leave += minesCountTBox_Leave;
            // 
            // exitButton
            // 
            exitButton.Location = new Point(208, 296);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(62, 23);
            exitButton.TabIndex = 9;
            exitButton.Text = "退出";
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += exitButton_Click;
            // 
            // startButton
            // 
            startButton.Location = new Point(276, 296);
            startButton.Name = "startButton";
            startButton.Size = new Size(62, 23);
            startButton.TabIndex = 10;
            startButton.Text = "开始!";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // isErrorMessageShowBox
            // 
            isErrorMessageShowBox.AutoSize = true;
            isErrorMessageShowBox.Location = new Point(215, 75);
            isErrorMessageShowBox.Name = "isErrorMessageShowBox";
            isErrorMessageShowBox.Size = new Size(99, 21);
            isErrorMessageShowBox.TabIndex = 11;
            isErrorMessageShowBox.Text = "提示错误输入";
            isErrorMessageShowBox.UseVisualStyleBackColor = true;
            isErrorMessageShowBox.CheckedChanged += isErrorMessageShowBox_CheckedChanged;
            // 
            // MenuMain
            // 
            MenuMain.Items.AddRange(new ToolStripItem[] { MenuFile, difficultyMenu, helpStrip, rankingListStrip });
            MenuMain.Location = new Point(0, 0);
            MenuMain.Name = "MenuMain";
            MenuMain.Size = new Size(406, 25);
            MenuMain.TabIndex = 12;
            MenuMain.Text = "menuStrip1";
            // 
            // MenuFile
            // 
            MenuFile.DropDownItems.AddRange(new ToolStripItem[] { MenuItemSave, loadMapStrip });
            MenuFile.Name = "MenuFile";
            MenuFile.Size = new Size(44, 21);
            MenuFile.Text = "文件";
            // 
            // MenuItemSave
            // 
            MenuItemSave.Name = "MenuItemSave";
            MenuItemSave.ShortcutKeyDisplayString = "Ctrl+S";
            MenuItemSave.ShortcutKeys = Keys.Control | Keys.S;
            MenuItemSave.Size = new Size(171, 22);
            MenuItemSave.Text = "保存";
            MenuItemSave.Click += MenuItemSave_Click;
            // 
            // loadMapStrip
            // 
            loadMapStrip.Name = "loadMapStrip";
            loadMapStrip.ShortcutKeys = Keys.Control | Keys.Q;
            loadMapStrip.Size = new Size(171, 22);
            loadMapStrip.Text = "打开地图";
            loadMapStrip.Click += loadMapStrip_Click;
            // 
            // difficultyMenu
            // 
            difficultyMenu.DropDownItems.AddRange(new ToolStripItem[] { ezMenu, medMenu, hardMenu });
            difficultyMenu.Name = "difficultyMenu";
            difficultyMenu.ShortcutKeyDisplayString = "";
            difficultyMenu.Size = new Size(44, 21);
            difficultyMenu.Text = "难度";
            // 
            // ezMenu
            // 
            ezMenu.Name = "ezMenu";
            ezMenu.ShortcutKeys = Keys.Control | Keys.E;
            ezMenu.Size = new Size(205, 22);
            ezMenu.Text = "初级(8x8,10)";
            ezMenu.Click += ezMenu_Click;
            // 
            // medMenu
            // 
            medMenu.Name = "medMenu";
            medMenu.ShortcutKeys = Keys.Control | Keys.D;
            medMenu.Size = new Size(205, 22);
            medMenu.Text = "中极(16x16,40)";
            medMenu.Click += medMenu_Click;
            // 
            // hardMenu
            // 
            hardMenu.Name = "hardMenu";
            hardMenu.ShortcutKeys = Keys.Control | Keys.F;
            hardMenu.Size = new Size(205, 22);
            hardMenu.Text = "高级(16x30,99)";
            hardMenu.Click += hardMenu_Click;
            // 
            // helpStrip
            // 
            helpStrip.Name = "helpStrip";
            helpStrip.Size = new Size(44, 21);
            helpStrip.Text = "帮助";
            helpStrip.Click += helpStrip_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { latestStat });
            statusStrip1.Location = new Point(0, 352);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(406, 22);
            statusStrip1.TabIndex = 13;
            statusStrip1.Text = "statusStrip1";
            // 
            // latestStat
            // 
            latestStat.Name = "latestStat";
            latestStat.Size = new Size(0, 17);
            // 
            // rankingListStrip
            // 
            rankingListStrip.Name = "rankingListStrip";
            rankingListStrip.Size = new Size(56, 21);
            rankingListStrip.Text = "排行榜";
            rankingListStrip.Click += rankingListStrip_Click;
            // 
            // Start
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(406, 374);
            ControlBox = false;
            Controls.Add(statusStrip1);
            Controls.Add(isErrorMessageShowBox);
            Controls.Add(startButton);
            Controls.Add(exitButton);
            Controls.Add(minesCountTBox);
            Controls.Add(label4);
            Controls.Add(whNotice);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(heightBox);
            Controls.Add(widthBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(MenuMain);
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = MenuMain;
            MaximizeBox = false;
            MaximumSize = new Size(422, 430);
            MinimumSize = new Size(422, 390);
            Name = "Start";
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Show;
            Text = "扫雷设置";
            TopMost = true;
            Load += Start_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            MenuMain.ResumeLayout(false);
            MenuMain.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox widthBox;
        private TextBox heightBox;
        private Panel panel1;
        private Label sizeShowText;
        private Label label3;
        private Button button1;
        private Button button3;
        private Button button2;
        private Panel panel2;
        private Button button4;
        private Button button5;
        private Button button6;
        private Label spaceShowText;
        private Label label5;
        private Label whNotice;
        private Label label4;
        private TextBox minesCountTBox;
        private Button exitButton;
        private Button startButton;
        private CheckBox isErrorMessageShowBox;
        private MenuStrip MenuMain;
        private ToolStripMenuItem MenuFile;
        private ToolStripMenuItem MenuItemSave;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel latestStat;
        private ToolStripMenuItem difficultyMenu;
        private ToolStripMenuItem ezMenu;
        private ToolStripMenuItem medMenu;
        private ToolStripMenuItem hardMenu;
        private ToolStripMenuItem loadMapStrip;
        private ToolStripMenuItem helpStrip;
        private ToolStripMenuItem rankingListStrip;
    }
}