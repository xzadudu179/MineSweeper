namespace mineSweeper
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            minesLeftCount = new ToolStripStatusLabel();
            mineProg = new ToolStripProgressBar();
            timeStatText = new ToolStripStatusLabel();
            timeStat = new ToolStripStatusLabel();
            Second = new ToolStripStatusLabel();
            gameStatText = new ToolStripStatusLabel();
            menuStrip1 = new MenuStrip();
            saveStrip = new ToolStripMenuItem();
            loadStrip = new ToolStripMenuItem();
            retryStrip = new ToolStripMenuItem();
            helpStrip = new ToolStripMenuItem();
            reStartNow = new ToolStripMenuItem();
            timerT = new System.Windows.Forms.Timer(components);
            statusStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, minesLeftCount, mineProg, timeStatText, timeStat, Second, gameStatText });
            statusStrip1.Location = new Point(0, 188);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(334, 22);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(59, 17);
            toolStripStatusLabel1.Text = "剩余雷数:";
            // 
            // minesLeftCount
            // 
            minesLeftCount.Name = "minesLeftCount";
            minesLeftCount.Size = new Size(15, 17);
            minesLeftCount.Text = "0";
            // 
            // mineProg
            // 
            mineProg.Name = "mineProg";
            mineProg.Size = new Size(100, 16);
            // 
            // timeStatText
            // 
            timeStatText.Name = "timeStatText";
            timeStatText.Size = new Size(59, 17);
            timeStatText.Text = "消耗时间:";
            // 
            // timeStat
            // 
            timeStat.Name = "timeStat";
            timeStat.Size = new Size(15, 17);
            timeStat.Text = "0";
            // 
            // Second
            // 
            Second.Name = "Second";
            Second.Size = new Size(20, 17);
            Second.Text = "秒";
            // 
            // gameStatText
            // 
            gameStatText.Name = "gameStatText";
            gameStatText.Size = new Size(44, 17);
            gameStatText.Text = "游玩中";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { saveStrip, loadStrip, retryStrip, helpStrip, reStartNow });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(334, 25);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // saveStrip
            // 
            saveStrip.Name = "saveStrip";
            saveStrip.ShortcutKeys = Keys.Control | Keys.S;
            saveStrip.Size = new Size(44, 21);
            saveStrip.Text = "保存";
            saveStrip.Click += SaveStrip_Click;
            // 
            // loadStrip
            // 
            loadStrip.Name = "loadStrip";
            loadStrip.ShortcutKeys = Keys.Control | Keys.W;
            loadStrip.Size = new Size(44, 21);
            loadStrip.Text = "加载";
            loadStrip.Click += loadStrip_Click;
            // 
            // retryStrip
            // 
            retryStrip.Name = "retryStrip";
            retryStrip.ShortcutKeys = Keys.Control | Keys.R;
            retryStrip.Size = new Size(68, 21);
            retryStrip.Text = "重新启动";
            retryStrip.Click += retryStrip_Click;
            // 
            // helpStrip
            // 
            helpStrip.Name = "helpStrip";
            helpStrip.ShortcutKeys = Keys.Control | Keys.H;
            helpStrip.Size = new Size(44, 21);
            helpStrip.Text = "帮助";
            helpStrip.Click += helpStrip_Click;
            // 
            // reStartNow
            // 
            reStartNow.Name = "reStartNow";
            reStartNow.ShortcutKeys = Keys.Control | Keys.Q;
            reStartNow.Size = new Size(44, 21);
            reStartNow.Text = "重试";
            reStartNow.Click += reStartNow_Click;
            // 
            // timerT
            // 
            timerT.Tick += TimerT_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 210);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(303, 226);
            Name = "Form1";
            Text = "MineSweeper";
            Load += Form1_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel minesLeftCount;
        private ToolStripProgressBar mineProg;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem saveStrip;
        private ToolStripMenuItem loadStrip;
        private ToolStripStatusLabel timeStatText;
        private ToolStripStatusLabel timeStat;
        private ToolStripStatusLabel Second;
        private System.Windows.Forms.Timer timerT;
        private ToolStripMenuItem retryStrip;
        private ToolStripStatusLabel gameStatText;
        private ToolStripMenuItem helpStrip;
        private ToolStripMenuItem reStartNow;
    }
}