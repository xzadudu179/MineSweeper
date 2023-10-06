namespace mineSweeper
{
    partial class InputDialog
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
            textBox1 = new TextBox();
            labelMain = new Label();
            OKButton = new Button();
            cancelButton = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(12, 112);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(218, 23);
            textBox1.TabIndex = 0;
            // 
            // labelMain
            // 
            labelMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            labelMain.Location = new Point(12, 9);
            labelMain.Name = "labelMain";
            labelMain.Size = new Size(218, 100);
            labelMain.TabIndex = 1;
            labelMain.Text = "-";
            labelMain.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // OKButton
            // 
            OKButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            OKButton.Location = new Point(155, 141);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(75, 27);
            OKButton.TabIndex = 2;
            OKButton.Text = "确定";
            OKButton.UseVisualStyleBackColor = true;
            OKButton.Click += OKButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.Location = new Point(74, 141);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 27);
            cancelButton.TabIndex = 3;
            cancelButton.Text = "取消";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // InputDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(242, 180);
            Controls.Add(cancelButton);
            Controls.Add(OKButton);
            Controls.Add(labelMain);
            Controls.Add(textBox1);
            Name = "InputDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Form2";
            Load += inputDialog_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Label labelMain;
        private Button OKButton;
        private Button cancelButton;
    }
}