using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.DataFormats;

namespace mineSweeper
{
    public partial class Start : Form
    {
        private readonly Form1 mainForm;
        private int buttonSize = 0;
        private int height = 0;
        private int width = 0;
        private int buttonSpace = 0;
        private int minesCount = 0;
        private bool isErrorInputMessageShow = false;
        public const int rankingListMaxCount = 15;
        public List<string> rankingList = new List<string>();

        //public string userName = "";
        public Start(Form1 form1)
        {
            InitializeComponent();
            mainForm = form1;
            // mainForm.Hide();
        }
        private void Start_Load(object sender, EventArgs e)
        {
            LoadItems();
            whNotice.Text = "注意:宽高最高为" + mainForm.settings.mapSizeMax;
        }

        /// <summary>
        /// 结束设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startButton_Click(object sender, EventArgs e)
        {
            if (width <= 1 || height <= 1 || buttonSize < 20 || minesCount < 1)
            {
                MessageBox.Show("请完整填写设置", "缺少设置", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            mainForm.Set(buttonSize, width, height, buttonSpace, minesCount, this);
            this.Close();
        }

        /// <summary>
        /// 设置宽度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WidthBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SizeCheck(widthBox);
                width = Convert.ToInt32(widthBox.Text);
            }
        }

        private void SizeCheck(TextBox textBox)
        {
            //进行判断是否是合法的值
            try
            {
                if (textBox.Text == "") return;
                Convert.ToInt32(textBox.Text);
            }
            catch
            {
                if (isErrorInputMessageShow)
                    MessageBox.Show("请输入有效的值！(纯数字)", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Text = System.Text.RegularExpressions.Regex.Replace(textBox.Text, @"[^\d]*", "");
                textBox.Text = "0";
            }
            if (Convert.ToInt32(textBox.Text) > mainForm.settings.mapSizeMax)
            {
                if (isErrorInputMessageShow)
                    MessageBox.Show($"最大值为{mainForm.settings.mapSizeMax}!", "属性值错误", MessageBoxButtons.OK);
                textBox.Text = mainForm.settings.mapSizeMax.ToString();
            }
            if (Convert.ToInt32(textBox.Text) <= 2)
            {
                if (isErrorInputMessageShow)
                    MessageBox.Show("最小值为2!", "属性值错误", MessageBoxButtons.OK);
                textBox.Text = "2";
            }
        }

        /// <summary>
        /// 设置高度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HeightBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SizeCheck(heightBox);
                height = Convert.ToInt32(heightBox.Text);
            }
        }

        //protected override void OnClosing(CancelEventArgs e)
        //{
        //    WillExit(e, true, true);
        //}

        private void exitButton_Click(object sender, EventArgs e)
        {
            WillExit(true, true);
        }

        private void WillExit(bool hasWarn, bool isAllExit)
        {
            if (hasWarn)
            {
                DialogResult dr = MessageBox.Show("是否在退出时保存? ", "确认", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    SaveItems(false);
                    if (!isAllExit)
                    {
                        this.Close();
                        return;
                    }
                    mainForm.Close();
                }
                else if (dr == DialogResult.No)
                {
                    if (!isAllExit)
                    {
                        this.Close();
                        return;
                    }
                    mainForm.Close();
                }
            }
        }

        private void buttonSizeSmall_Click(object sender, EventArgs e)
        {
            buttonSize = 25;
            sizeShowText.Text = buttonSize.ToString();
        }

        private void buttonSizeMid_Click(object sender, EventArgs e)
        {
            buttonSize = 35;
            sizeShowText.Text = buttonSize.ToString();
        }

        private void buttonSizeLarge_Click(object sender, EventArgs e)
        {
            buttonSize = 45;
            sizeShowText.Text = buttonSize.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            buttonSpace = 0;
            spaceShowText.Text = buttonSpace.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            buttonSpace = 2;
            spaceShowText.Text = buttonSpace.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buttonSpace = 4;
            spaceShowText.Text = buttonSpace.ToString();
        }

        private void minesCountTBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MinesCheck();
                minesCount = Convert.ToInt32(minesCountTBox.Text);
            }
        }

        private void MinesCheck()
        {
            //进行判断是否是合法的值
            try
            {
                if (minesCountTBox.Text == "") return;
                Convert.ToInt32(minesCountTBox.Text);
            }
            catch
            {
                if (isErrorInputMessageShow)
                    MessageBox.Show("请输入有效的值！(纯数字)", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                minesCountTBox.Text = System.Text.RegularExpressions.Regex.Replace(minesCountTBox.Text, @"[^\d]*", "");
                minesCountTBox.Text = "0";
            }
            if (Convert.ToInt32(minesCountTBox.Text) > width * height - 1)
            {
                if (isErrorInputMessageShow)
                    MessageBox.Show($"最大值为{width * height - 2}!", "属性值错误", MessageBoxButtons.OK);
                minesCountTBox.Text = (width * height - 2).ToString();
            }
            if (Convert.ToInt32(minesCountTBox.Text) <= 1)
            {
                if (isErrorInputMessageShow)
                    MessageBox.Show("最小值为1!", "属性值错误", MessageBoxButtons.OK);
                minesCountTBox.Text = "1";
            }
        }

        private void widthBox_Leave(object sender, EventArgs e)
        {
            SizeCheck(widthBox);
            if (widthBox.Text == "") return;
            width = Convert.ToInt32(widthBox.Text);
        }

        private void heightBox_Leave(object sender, EventArgs e)
        {
            SizeCheck(heightBox);
            if (heightBox.Text == "") return;
            height = Convert.ToInt32(heightBox.Text);
        }

        private void minesCountTBox_Leave(object sender, EventArgs e)
        {
            MinesCheck();
            if (minesCountTBox.Text == "") return;
            minesCount = Convert.ToInt32(minesCountTBox.Text);
        }

        /// <summary>
        /// 是否提示错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void isErrorMessageShowBox_CheckedChanged(object sender, EventArgs e)
        {
            isErrorInputMessageShow = isErrorMessageShowBox.Checked;
        }

        private void sortRankList()
        {

        }

        /// <summary>
        /// 加载
        /// </summary>
        private void LoadItems()
        {
            XmlDocument xmlDoc = new XmlDocument();
            //XmlDocument xmldoc2 = new XmlDocument();
            //加载要读取的xml
            try
            {
                xmlDoc.Load("MineSweeperSettings.xml");
                //xmldoc2.Load("Rankings.xml");
            }
            catch
            {
                return;
            }

            //获得根节点
            XmlElement books = xmlDoc.DocumentElement!;

            //获得子节点
            XmlNodeList xnl = books!.ChildNodes;

            //排行榜相关
            //XmlNodeList rankXnl = xmldoc2.DocumentElement!.ChildNodes;
            //foreach (XmlNode xnlx in rankXnl)
            //{
            //    for (int i = 0; i < xnlx.Attributes!.Count; i++)
            //    {
            //        rankingList.Add(xnlx.Attributes[i].Value);
            //    }
            //}
            //排序排行榜

            string test = "";
            //加载所有元素
            foreach (XmlNode node in xnl)
            {
                for (int i = 0; i < node.Attributes!.Count; i++)
                {
                    test += $"{node.Name}[{i}]:{node.Attributes[i].Name} = {node.Attributes[i].Value}\n";
                    if (node.Name == "vars")
                    {
                        switch (i)
                        {
                            case 0:
                                buttonSize = Convert.ToInt32(node.Attributes[i].Value);
                                sizeShowText.Text = buttonSize.ToString();
                                break;
                            case 1:
                                height = Convert.ToInt32(node.Attributes[i].Value);
                                break;
                            case 2:
                                width = Convert.ToInt32(node.Attributes[i].Value);
                                break;
                            case 3:
                                buttonSpace = Convert.ToInt32(node.Attributes[i].Value);
                                spaceShowText.Text = buttonSpace.ToString();
                                break;
                            case 4:
                                minesCount = Convert.ToInt32(node.Attributes[i].Value);
                                break;
                            case 5:
                                isErrorInputMessageShow = Convert.ToBoolean(node.Attributes[i].Value);
                                break;
                        }
                    }
                    else if (node.Name == "checks")
                    {
                        switch (i)
                        {
                            case 0:
                                isErrorMessageShowBox.Checked = Convert.ToBoolean(node.Attributes[i].Value);
                                break;
                        }
                    }
                    widthBox.Text = width.ToString();
                    heightBox.Text = height.ToString();
                    minesCountTBox.Text = minesCount.ToString();
                }
            }
            //MessageBox.Show(test,"Debug");
        }

        /// <summary>
        /// 保存按键按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemSave_Click(object sender, EventArgs e)
        {
            SaveItems(false);
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void SaveItems(bool isShowMessagebox)
        {
            //使用XML保存变量值
            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "UTF-8", null));
            //根节点
            XmlElement root = xml.CreateElement("root");
            xml.AppendChild(root);
            //保存变量值
            XmlElement basicVars = xml.CreateElement("vars");
            basicVars.SetAttribute("buttonSize", buttonSize.ToString());
            basicVars.SetAttribute("height", height.ToString());
            basicVars.SetAttribute("width", width.ToString());
            basicVars.SetAttribute("buttonSpace", buttonSpace.ToString());
            basicVars.SetAttribute("minesCount", minesCount.ToString());
            basicVars.SetAttribute("isErrorInputMessageShow", isErrorInputMessageShow.ToString());
            root.AppendChild(basicVars);
            //保存文本框等值
            //XmlElement basicTexts = xml.CreateElement("texts");
            //basicTexts.SetAttribute("widthBox", widthBox.Text);
            //basicTexts.SetAttribute("heightBox", heightBox.Text);
            //basicTexts.SetAttribute("minesCountTBox", minesCountTBox.Text);
            //root.AppendChild(basicTexts);
            //保存复选框等值
            XmlElement basicChecks = xml.CreateElement("checks");
            basicChecks.SetAttribute("isErrorMessageShowBox", isErrorMessageShowBox.Checked.ToString());
            root.AppendChild(basicChecks);

            //string path = "";
            //SaveFileDialog saveDialog = new SaveFileDialog();
            //saveDialog.Title = "保存自定义属性";
            //saveDialog.Filter = "XML文档|*.xml";
            //saveDialog.ShowDialog();

            //path = saveDialog.FileName;

            //if (path == "")
            //{
            //    return;
            //}
            xml.Save("MineSweeperSettings.xml");
            if (isShowMessagebox)
            {
                MessageBox.Show("保存成功", "保存状态", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                latestStat.Text = "保存成功";
            }
        }

        /// <summary>
        /// 初级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ezMenu_Click(object sender, EventArgs e)
        {
            //8x8 10雷
            QuickSetting(8, 8, 10);
        }

        /// <summary>
        /// 快速设置大小和雷数
        /// </summary>
        /// <param name="w">宽</param>
        /// <param name="h">高</param>
        /// <param name="mines">雷数</param>
        private void QuickSetting(int h, int w, int mines)
        {
            widthBox.Text = w.ToString();
            heightBox.Text = h.ToString();
            width = w;
            height = h;
            minesCount = mines;
            minesCountTBox.Text = mines.ToString();
        }

        /// <summary>
        /// 中级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void medMenu_Click(object sender, EventArgs e)
        {
            QuickSetting(16, 16, 40);
        }

        /// <summary>
        /// 高级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hardMenu_Click(object sender, EventArgs e)
        {
            QuickSetting(16, 30, 99);
        }

        /// <summary>
        /// 加载地图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadMapStrip_Click(object sender, EventArgs e)
        {
            this.Hide();
            mainForm.LoadMap();
            this.Dispose();
        }

        /// <summary>
        /// 帮助
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpStrip_Click(object sender, EventArgs e)
        {
            string helpString =
                "宽:地图的宽度\n" +
                "高:地图的高度\n" +
                "提示错误输入:当输入不合法时弹出对话框提示\n" +
                "格子大小:地图中每一个格子的大小(像素)\n" +
                "格子间距:地图中每一个格子的间隔(像素)\n" +
                "雷数:地雷的数量";
            MessageBox.Show(helpString, "帮助", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 排行榜
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rankingListStrip_Click(object sender, EventArgs e)
        {
            MessageBox.Show("此功能暂不可用!");
            return;
            ////雷密度 花费时间 大小
            ////总分 = 雷密度 / (花费时间 > 0 ? 花费时间 : 0.01f) * (长 * 宽 / 10)
            ////如果雷只有一个那么总分为0
            //string ranks = "";
            //foreach (var ranking in rankingList)
            //{
            //    ranks = $"{ranks}\n{ranking}";
            //}
            //MessageBox.Show(ranks, "排行榜", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void RecordResult(string userName)
        {
            if (rankingList.Count > rankingListMaxCount) return;
            float rankScore = (mainForm.settings.width * mainForm.settings.height / mainForm.settings.minesCount) / (mainForm.settings.timeSpends > 0 ?
                mainForm.settings.timeSpends : 0.01f) * (mainForm.settings.width * mainForm.settings.height / 10);
            if(minesCount == 0) rankScore = 0;
            rankingList.Add($"{userName}: 总分:{rankScore},设置:宽{mainForm.settings.width},高:{mainForm.settings.height}," +
                $"雷密度:{mainForm.settings.minesCount / mainForm.settings.width * mainForm.settings.height * 100}%");
            //rankingList.Add()
            //使用XML保存变量值
            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "UTF-8", null));
            //根节点
            XmlElement root = xml.CreateElement("RankingList");
            xml.AppendChild(root);
            //保存变量值
            XmlElement ranking = xml.CreateElement("ranking");
            for (int i = 0; i < rankingList.Count; i++)
            {
                ranking.SetAttribute(rankScore.ToString(), rankingList[i]);
            }
            root.AppendChild(ranking);
            xml.Save("Rankings.xml");
        }
    }
}
