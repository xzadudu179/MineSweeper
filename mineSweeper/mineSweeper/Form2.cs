using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mineSweeper
{
    public partial class InputDialog : Form
    {
        private Start startForm;
        private string title;
        private string labelText;
        public InputDialog(string labelText, string title, Start startForm)
        {
            this.startForm = startForm;
            this.labelText = labelText;
            this.title = title;
            InitializeComponent();
        }

        private void inputDialog_Load(object sender, EventArgs e)
        {
            this.Text = title;
            labelMain.Text = labelText;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, EventArgs e)
        {
            //startForm.userName = InputOK();
            //InputOK();
            this.Close();
        }

        private void InputOK()
        {
            startForm.RecordResult(textBox1.Text);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
