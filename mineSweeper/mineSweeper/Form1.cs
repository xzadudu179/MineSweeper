using System;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;
#pragma warning disable SYSLIB0011

namespace mineSweeper
{
    [Serializable]
    public partial class Form1 : Form
    {
        [Serializable]
        public class Settings
        {
            //地图         

            //最大可设置网格大小
            public int mapSizeMax = 40;

            //每个网格的大小
            public int buttonSize = 30;

            //网格之间的间距
            public int buttonSpace = 0;

            public bool isDead = false;

            //是否点击了第一下
            public bool isStart = false;

            //是否胜利
            public bool isWin = false;

            //宽
            public int width = 8;
            //高
            public int height = 10;
            //雷数
            public int minesCount = 10;
            public int realMinesCount = 0;
            //窗口的大小
            public int windowsSpace = 60;
            //鼠标按键是否按下
            public bool isLeftDown = false;
            public bool isRightDown = false;

            //使用的时间
            public float timeSpends = 0;

            public List<List<ButtonMap>> buttonsSeri = new List<List<ButtonMap>>();

            [XmlIgnore]
            [NonSerialized]
            public ButtonMap[,]? buttons;

            [XmlIgnore]
            [NonSerialized]
            public bool isLoad = false;
            [XmlIgnore]
            [NonSerialized]
            public string loadPath = "";
        }
        internal Settings settings = new Settings();
        private Start? startForm;
        public Form1(bool isload, string path)
        {
            settings.isLoad = isload;
            settings.loadPath = path;
            InitializeComponent();
        }

        /// <summary>
        /// 单击事件
        /// </summary>
        /// <param name="clickButton"></param>
        private void ClickMapButton(ButtonMap clickButton)
        {
            //如果被标棋
            if (clickButton.IsFlaged) return;
            //如果有雷
            if (clickButton.IsHasMine)
            {
                clickButton.MapButton!.BackColor = Color.Red;
                clickButton.boom = true;
                //clickButton.MapButton!.Text = "●";
                ShowMines(settings.buttons!);
                timerT.Stop();
                settings.isDead = true;
                gameStatText.Text = "失败";
                MessageBox.Show("boom");
                return;
            }
            //搜索
            if (clickButton.IsSweeped) return;
            Sweep(clickButton.IndexX, clickButton.IndexY, settings.buttons!);
        }

        /// <summary>
        /// 判断是否胜利
        /// </summary> 
        private void IsOrNotWin(ButtonMap[,] buttons)
        {
            //遍历
            //如果所有格子不是已被扫除就是有雷 或所有雷都被标旗 那么就胜利
            foreach (ButtonMap button in buttons)
            {
                if (button.IsHasMine || button.IsSweeped)
                {
                    continue;
                }
                else
                {
                    return;
                }
            }
            minesLeftCount.Text = "0";
            timerT.Enabled = false;
            settings.isWin = true;
            mineProg.Value = mineProg.Maximum;
            gameStatText.Text = "胜利";
            InputDialog winDialog = new InputDialog($"胜利~ 耗时{timeStat.Text}秒\n" +
                $"分数为:{(float)(settings.width * settings.height / 100f) * (float)((float)(settings.minesCount) / (settings.width * settings.height * 1f)) / (float)(settings.timeSpends > 0f ? settings.timeSpends : 0.01f) * 100}" +
                $"\n\n请输入用户名:", " ", startForm!);
            winDialog.ShowDialog();
            //MessageBox.Show($"胜利~ 耗时{timeStat.Text}秒", "胜利", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            if (settings.isLoad)
            {
                LoadSweepMap(settings.loadPath);
            }
            else
            {
                menuStrip1.Hide();
                statusStrip1.Hide();
                //this.Hide();
                Start start = new Start(this);
                //设置
                start.Show();
                //if (start.isOK)
                //{
                //    GenerateMap(12, 12);
                //    minesLeftCount.Text = minesCount.ToString();
                //    this.Visible = true;
                //    timerT.Enabled = true;
                //}
            }

        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="bSize">按钮大小</param>
        /// <param name="mWidth">宽</param>
        /// <param name="mHeight">高</param>
        /// <param name="bButtonSpace">按钮间距</param>
        /// <param name="mMinesCount">地雷数量</param>
        public void Set(int bSize, int mWidth, int mHeight, int bButtonSpace, int mMinesCount, Start startForm)
        {
            this.startForm = startForm;
            settings.buttonSize = bSize;
            settings.width = mWidth;
            settings.height = mHeight;
            settings.buttonSpace = bButtonSpace;
            settings.minesCount = mMinesCount;
            //this.Show();

            GenerateMap(settings.width, settings.height);
            minesLeftCount.Text = settings.minesCount.ToString();
            menuStrip1.Show();
            statusStrip1.Show();
            //timerT.Enabled = true;
        }

        /// <summary>
        /// 生成地图
        /// </summary>
        /// <param name="height">地图高</param>
        /// <param name="width">地图宽</param>
        private void GenerateMap(int width, int height)
        {
            //判断是否过大
            height = height > settings.mapSizeMax ? settings.mapSizeMax : height <= 1 ? 2 : height;
            width = width > settings.mapSizeMax ? settings.mapSizeMax : width <= 1 ? 2 : width;
            settings.buttons = new ButtonMap[width, height];
            //生成地图
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    settings.buttons[i, j] = new ButtonMap(new Button(), i, j);
                    settings.buttons[i, j].MapButton!.Name = i.ToString() + "," + j.ToString();
                }
            }
            //第一个按钮的位置在60,60
            //第二个按钮是90,60
            //生成按钮
            foreach (ButtonMap button in settings.buttons)
            {
                if (button.MapButton != null)
                {
                    button.MapButton.Size = new System.Drawing.Size(settings.buttonSize, settings.buttonSize);
                    //位置
                    button.MapButton.Location = new System.Drawing.Point(button.IndexX * (settings.buttonSize + settings.buttonSpace) + settings.windowsSpace,
                        button.IndexY * (settings.buttonSize + settings.buttonSpace) + settings.windowsSpace);
                    button.MapButton.MouseDown += MapButtonClick;
                    button.MapButton.MouseUp += MapMouseUp;
                    //button.MapButton.BackColor = System.Drawing.Color.WhiteSmoke;
                    //button.MapButton.FlatStyle = FlatStyle.Popup;
                    //字体
                    button.MapButton.Font = new Font(button.MapButton!.Font.FontFamily, (float)(settings.buttonSize * 0.4f), FontStyle.Regular);

                    this.Controls.Add(button.MapButton);
                }
            }
            //改变窗口大小 150为窗口间距(第一个按钮间距*2
            //(buttonSize / 2)是防止按钮大小影响窗口
            this.Size = new System.Drawing.Size(settings.buttons.Length / height * (settings.buttonSize + settings.buttonSpace) +
                settings.windowsSpace * 2 + ((settings.buttonSize + settings.buttonSpace) / 2),
                settings.buttons.Length / width * (settings.buttonSize + settings.buttonSpace) + settings.windowsSpace * 2 + ((settings.buttonSize + settings.buttonSpace) / 2));
        }

        /// <summary>
        /// 生成雷
        /// </summary>
        /// <param name="minesCount">雷数</param>
        /// <param name="buttons">网格</param>
        /// <param name="buttonIndexNow">当前点击按钮</param>
        private void GenerateMine(int minesCount, ButtonMap[,] buttons, int[] buttonIndexNow, bool synchMines)
        {
            if (minesCount >= buttons.Length - 0) minesCount = buttons.Length - 2;
            if (minesCount <= 0) minesCount = 1;
            //雷数同步
            if (synchMines)
            {
                settings.realMinesCount = minesCount;
            }
            //minesLeftCount.Text = settings.realMinesCount.ToString();
            int[,] mines = new int[buttons.GetLength(0), buttons.GetLength(1)];
            Random random = new Random();
            //剩余需要放置的雷数
            int minesLeft = settings.realMinesCount;
            //随机选格子放雷
            while (minesLeft > 0)
            {
                for (int i = 0; i < mines.GetLength(0); i++)
                {
                    for (int j = 0; j < mines.GetLength(1); j++)
                    {
                        if (minesLeft <= 0) break;
                        //判断是否放雷
                        //条件: 随机到雷 这个格子没有雷 坐标不等于当前按钮
                        if (i == buttonIndexNow[0] && j == buttonIndexNow[1]) continue;
                        if (random.Next(0, mines.Length) == 1 && mines[i, j] != 1)
                        {
                            mines[i, j] = 1;
                            minesLeft--;
                        }
                    }
                }
            }

            //遍历网格
            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    //设置雷
                    buttons[i, j].IsHasMine = mines[i, j] == 1 ? true : false;
                }
            }
        }

        private void MapMouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                settings.isLeftDown = false;
            }
            else if (e.Button == MouseButtons.Right)
            {
                settings.isRightDown = false;
            }
        }

        /// <summary>
        /// 点击地图按钮时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapButtonClick(object? sender, MouseEventArgs e)
        {
            try
            {
                //判断雷数
                if (Convert.ToInt32(minesLeftCount.Text) >= 0)
                {
                    mineProg.Value = Convert.ToInt32(Math.Abs(1f - (float)Convert.ToInt32(minesLeftCount.Text) / (float)settings.minesCount) * 100);
                }
            }
            catch
            {
                mineProg.Value = mineProg.Maximum;
            }
            if (settings.isWin) return;
            if (settings.isDead) return;
            //判断
            if (sender == null) return;
            Button button = (Button)sender;
            //获得鼠标点击事件
            //分割字符串
            char[] chs = { ',' }; //分割这些符号
            string[] str = button.Name.ToString().Split(chs, StringSplitOptions.RemoveEmptyEntries);
            ButtonMap clickButton = ButtonMap.ReadButton(Convert.ToInt32(str[0]), Convert.ToInt32(str[1]), settings.buttons!);
            //第一次按下时
            if (!settings.isStart && e.Button == MouseButtons.Left && !settings.isRightDown)
            {
                //生成雷
                GenerateMine(settings.minesCount, settings.buttons!, new int[] { Convert.ToInt32(str[0]), Convert.ToInt32(str[1]) }, true);
                //启动计时器
                timerT.Enabled = true;
                settings.isStart = true;
            }
            //判断输入            
            //左键
            if (e.Button == MouseButtons.Left && !settings.isRightDown)
            {
                ClickMapButton(clickButton);
                settings.isLeftDown = true;
            }
            //右键
            if (e.Button == MouseButtons.Right && !settings.isLeftDown)
            {
                settings.isRightDown = true;
                if (clickButton.IsSweeped) return;
                if (clickButton.IsFlaged == true)
                {
                    clickButton.IsFlaged = false;
                    //clickButton.MapButton!.Text = "";
                    //更改显示雷数
                    minesLeftCount.Text = (Convert.ToInt32(minesLeftCount.Text) + 1).ToString();
                }
                else
                {
                    clickButton.IsFlaged = true;
                    //clickButton.MapButton!.Text = "▽";
                    //更改显示雷数
                    minesLeftCount.Text = (Convert.ToInt32(minesLeftCount.Text) - 1).ToString();
                }
            }
            //左右键
            if (e.Button == MouseButtons.Right && settings.isLeftDown)
            {
                //自动清除标雷附近的格
                if (clickButton.OtherMineCount <= 0 && !clickButton.IsSweeped) return;
                QuickCheck(clickButton.IndexX, clickButton.IndexY, settings.buttons!);
            }
            else if (e.Button == MouseButtons.Left && settings.isRightDown)
            {
                //自动清除标雷附近的格
                if (clickButton.OtherMineCount <= 0 && !clickButton.IsSweeped) return;
                QuickCheck(clickButton.IndexX, clickButton.IndexY, settings.buttons!);
            }
            //判断是否胜利
            IsOrNotWin(settings.buttons!);
        }

        /// <summary>
        /// 显示所有地雷
        /// </summary>
        /// <param name="buttons"></param>
        private static void ShowMines(ButtonMap[,] buttons)
        {
            foreach (ButtonMap button in buttons)
            {
                if (button.IsHasMine)
                {
                    button.MapButton!.Text = "●";
                    button.MapButton!.ForeColor = Color.Black;
                }
            }
        }

        /// <summary>
        /// 快捷点击
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="buttons"></param>
        private void QuickCheck(int x, int y, ButtonMap[,] buttons)
        {
            ButtonMap thisButton = ButtonMap.ReadButton(x, y, buttons);
            //if (thisButton.IsSweeped || thisButton.OtherMineCount <= 0) return;
            //搜索周围的格子
            List<ButtonMap> searchButtons = new List<ButtonMap>();
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i >= buttons.GetLength(0) || j >= buttons.GetLength(1) || i < 0 || j < 0) continue;
                    if (ButtonMap.ReadButton(i, j, buttons).IsSweeped || buttons[i, j] == thisButton) continue;
                    //添加进列表
                    searchButtons.Add(ButtonMap.ReadButton(i, j, buttons));
                }
            }
            //判断周围的格子是否有标旗 且当前点击的按钮显示数字是否与旗帜数量一致
            //如果一致 消除周围所有没标旗并且没被扫描的方块
            int flag = 0;
            foreach (ButtonMap button in searchButtons)
            {
                //有雷
                if (button.IsFlaged)
                {
                    flag++;
                }
            }
            if (flag == thisButton.OtherMineCount)
            {
                //消除周围所有没被标棋和没被扫描的方块
                foreach (ButtonMap button in searchButtons)
                {
                    //消除
                    if (button.IsFlaged || button.IsSweeped) continue;
                    ClickMapButton(button);
                }
            }
        }

        /// <summary>
        /// 判断周围有没有雷
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private void Sweep(int x, int y, ButtonMap[,] buttons)
        {
            ButtonMap thisButton = ButtonMap.ReadButton(x, y, buttons);
            //标记为已搜索
            thisButton.IsSweeped = true;
            //不允许点击按钮
            //thisButton.MapButton!.Enabled = false;
            //bool hasFlagedButton = false;
            //归零
            thisButton.OtherMineCount = 0;
            //搜索周围的格子
            List<ButtonMap> searchButtons = new List<ButtonMap>();
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i >= buttons.GetLength(0) || j >= buttons.GetLength(1) || i < 0 || j < 0) continue;
                    if (ButtonMap.ReadButton(i, j, buttons).IsSweeped || buttons[i, j] == thisButton) continue;
                    //添加进列表
                    searchButtons.Add(ButtonMap.ReadButton(i, j, buttons));
                }
            }
            //判断是否有雷
            int fori = 0;
            foreach (ButtonMap button in searchButtons)
            {
                //有雷
                if (button.IsHasMine)
                {
                    //增加所显示的雷数
                    thisButton.OtherMineCount += 1;
                    //添加进otherMine列表
                    thisButton.otherMine![fori] = button;
                }
                fori++;
            }
            //显示雷数
            if (thisButton.OtherMineCount > 0 /*|| hasFlagedButton*/)
            {
                thisButton.MapButton!.Text = thisButton.OtherMineCount.ToString();
                thisButton.MapButton!.Font = new Font(thisButton.MapButton!.Font.FontFamily, thisButton.MapButton!.Font.Size, FontStyle.Bold);
            }
            //没有雷
            else
            {
                foreach (ButtonMap button in searchButtons)
                {
                    //被标旗
                    if (button.IsFlaged)
                    {
                        button.IsFlaged = false;
                        minesLeftCount.Text = (Convert.ToInt32(minesLeftCount.Text) + 1).ToString();
                    }
                    //继续搜索
                    Sweep(ButtonMap.ReadIndex(button)[0], ButtonMap.ReadIndex(button)[1], buttons);
                }
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveStrip_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("此功能暂未开启~");
            //存储所有mapbuttons
            timerT.Stop();
            SaveFileDialog sfd = new SaveFileDialog
            {
                Title = "保存地图文件",
                //Filter = "扫雷地图文件|*.xml",
                Filter = "扫雷地图文件|*.swmap",
                RestoreDirectory = true
            };
            sfd.ShowDialog();
            string path = sfd.FileName;
            if (path == "")
            {
                return;
            }
            //记录花费的时间
            settings.timeSpends = float.Parse(timeStat.Text);
            settings.realMinesCount = Convert.ToInt32(minesLeftCount.Text);
            settings.buttonsSeri.Clear();

            for (int i = 0; i < settings.buttons!.GetLength(0); i++)
            {
                settings.buttonsSeri.Add(new List<ButtonMap>());
                for (int j = 0; j < settings.buttons!.GetLength(1); j++)
                {
                    settings.buttonsSeri[i].Add(settings.buttons![i, j]);
                }
            }

            ////序列化为xml
            //XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            //TextWriter writer = new StreamWriter(path);
            //serializer.Serialize(writer, settings);
            //writer.Close();

            //序列化为bf
            using (FileStream fsWrite = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                //序列化
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fsWrite, settings);
            }



            if (!settings.isDead && !settings.isWin)
            {
                timerT.Start();
            }


            MessageBox.Show($"成功保存到{path}");
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadStrip_Click(object sender, EventArgs e)
        {
            LoadMap();
        }

        //public async void DeleteMap()
        //{
        //    await DoDeleteMap();
        //}
        //public async Task DoDeleteMap()
        //{
        //    Task t = Task.Run(() =>
        //    {
        //        foreach (ButtonMap button in settings.buttons!)
        //        {
        //            this.Controls.Remove(button.MapButton);
        //            //button.MapButton!.Visible = false;
        //        }
        //    });
        //    await t;
        //}

        public void LoadMap()
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "打开地图文件";
                ofd.Multiselect = false;
                //ofd.Filter = "扫雷地图文件|*.xml";
                ofd.Filter = "扫雷地图文件|*.swmap";
                ofd.ShowDialog();
                string path = ofd.FileName;
                if (path == "") return;
                //settings.loadPath = path;
                timerT.Stop();
                //if (settings.buttons != null)
                //{
                //    //foreach (ButtonMap button in settings.buttons!)
                //    //{
                //    //    button.MapButton!.Visible = false;
                //    //}

                //    //DeleteMap();
                //}

                //重启
                //Application.Restart();
                //Thread th = new Thread(() =>
                //{
                //    Form1 f1 = new Form1(true, path);
                //    f1.ShowDialog();
                //});
                //th.Start();               
                //this.Close();
                this.Hide();
                Form1 f1 = new Form1(true, path);
                f1.ShowDialog();
                this.Dispose();

                //LoadSweepMap(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载出现错误:{ex.Message}\n{ex.InnerException}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void LoadSweepMap(string path)
        {
            try
            {
                Settings newSettings = new Settings();

                ////xml反序列化
                //FileStream fs = File.Open(path, FileMode.Open);
                //using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                //{
                //    XmlSerializer xs = new XmlSerializer(typeof(Settings));
                //    newSettings = (Settings)xs.Deserialize(sr)!;
                //}

                //bf反序列化
                using (FileStream fsRead = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    newSettings = (Settings)bf.Deserialize(fsRead);
                }

                settings.width = newSettings.width;
                settings.height = newSettings.height;
                settings.isStart = newSettings.isStart;
                settings.isWin = newSettings.isWin;
                settings.isLeftDown = newSettings.isLeftDown;
                settings.isRightDown = newSettings.isRightDown;
                settings.buttonSize = newSettings.buttonSize;
                settings.buttonSpace = newSettings.buttonSpace;
                settings.isDead = newSettings.isDead;
                settings.mapSizeMax = newSettings.mapSizeMax;
                settings.minesCount = newSettings.minesCount;
                settings.realMinesCount = newSettings.realMinesCount;
                settings.windowsSpace = newSettings.windowsSpace;
                settings.timeSpends = newSettings.timeSpends;
                //生成一样的地图
                GenerateMap(settings.width, settings.height);
                //minesLeftCount.Text = newSettings.minesCount.ToString();
                //遍历 改变所有按钮信息
                for (int i = 0; i < newSettings.buttonsSeri.Count; i++)
                {
                    for (int j = 0; j < newSettings.buttonsSeri[i].Count; j++)
                    {
                        //settings.buttonsSeri[i][j].MapButton = newSettings.buttons[i, j].MapButton;
                        //同步设置
                        settings.buttons![i, j].IsHasMine = newSettings.buttonsSeri[i][j].IsHasMine;

                        //settings.buttons![i, j].IsSweeped = newSettings.buttonsSeri[i][j].IsSweeped;
                        if (newSettings.buttonsSeri[i][j].IsSweeped == true)
                        {
                            settings.buttons[i, j].MapButton!.BackColor = Color.WhiteSmoke;
                            settings.buttons[i, j].MapButton!.FlatStyle = FlatStyle.Flat;
                            settings.buttons[i, j].MapButton!.FlatAppearance.BorderColor = Color.LightGray;
                            settings.buttons[i, j].MapButton!.FlatAppearance.MouseOverBackColor = Color.WhiteSmoke;
                            settings.buttons[i, j].MapButton!.FlatAppearance.MouseDownBackColor = Color.WhiteSmoke;
                            settings.buttons[i, j].IsSweeped = true;
                        }

                        settings.buttons![i, j].IsFlaged = newSettings.buttonsSeri[i][j].IsFlaged;
                        if (settings.buttons![i, j].IsFlaged == false)
                        {
                            settings.buttons![i, j].MapButton!.Text = "";
                        }
                        else
                        {
                            settings.buttons![i, j].MapButton!.Text = "◆";
                            settings.buttons![i, j].MapButton!.ForeColor = Color.Red;
                        }

                        //settings.buttons[i, j].OtherMineCount = newSettings.buttonsSeri[i][j].OtherMineCount;
                        if (newSettings.buttonsSeri[i][j].OtherMineCount > 0)
                        {
                            settings.buttons[i, j].OtherMineCount = newSettings.buttonsSeri[i][j].OtherMineCount;
                            settings.buttons![i, j].MapButton!.Text = settings.buttons![i, j].OtherMineCount.ToString();
                            settings.buttons![i, j].MapButton!.Font = new Font(settings.buttons![i, j].MapButton!.Font.FontFamily, settings.buttons![i, j].MapButton!.Font.Size, FontStyle.Bold);

                            settings.buttons[i, j].MapButton!.ForeColor = settings.buttons![i, j].OtherMineCount switch
                            {
                                1 => Color.Blue,
                                2 => Color.Green,
                                3 => Color.Red,
                                4 => Color.Purple,
                                5 => Color.BlanchedAlmond,
                                6 => Color.Magenta,
                                7 => Color.Brown,
                                8 => Color.Black,
                                _ => Color.Cyan,
                            };
                        }

                        if (newSettings.buttonsSeri[i][j].boom == true)
                        {
                            settings.buttons![i, j].MapButton!.BackColor = Color.Red;
                        }

                    }
                }
                timeStat.Text = settings.timeSpends.ToString();
                if (settings.isDead)
                {
                    ShowMines(settings.buttons!);
                    gameStatText.Text = "失败";
                }
                else if (settings.isWin)
                {
                    gameStatText.Text = "胜利";
                }
                else
                {
                    gameStatText.Text = "游玩中";
                    timerT.Start();
                }
                minesLeftCount.Text = settings.realMinesCount.ToString();
                //判断雷数
                if (settings.realMinesCount >= 0)
                {
                    mineProg.Value = Convert.ToInt32(Math.Abs(1f - (float)Convert.ToInt32(minesLeftCount.Text) / (float)settings.minesCount) * 100);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载出现错误:{ex.Message}\n{ex.InnerException}\n{ex}请确认是否为扫雷地图文件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerT_Tick(object sender, EventArgs e)
        {
            if (timerT.Enabled)
            {
                timeStat.Text = $"{(float.Parse(timeStat.Text) + 0.1f):0.0}";
            }
            else
            {
                timeStat.Text = timeStat.Text;
            }
        }


        /// <summary>
        /// 重试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void retryStrip_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Process.GetCurrentProcess()?.Kill();
        }

        /// <summary>
        /// 帮助
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpStrip_Click(object sender, EventArgs e)
        {
            string helpString =
                "地图由若干个块组成\n" +
                "鼠标放在块上时 左键点击可以点开块\n" +
                "点开的块若有显示数字，则表明它周围的8个块中有[数字值]个地雷" +
                "如果点开的块含有地雷 则游戏失败\n" +
                "右键点击未点开的块可将其标记为地雷，再次点击以取消标记(标记为地雷后左键点击该块不会有任何反应)\n" +
                "左右键同时点击已点开且带有数字的块时，如果该块周围8个块中标记数量等于块的数字所显示的数量，则自动点开周围8个块中除带标记外的所有块，否则无法点开"
                ;
            MessageBox.Show(helpString, "帮助", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 以当前设置重新开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reStartNow_Click(object sender, EventArgs e)
        {
            foreach (ButtonMap button in settings.buttons!)
            {
                button.IsSweeped = false;
                button.IsFlaged = false;
                button.otherMine = new ButtonMap[8];
                button.IsHasMine = false;
                button.boom = false;
                button.OtherMineCount = 0;
                button.MapButton!.FlatStyle = FlatStyle.Standard;
                button.MapButton!.BackColor = Color.FromArgb(0xE1E1E1);
            }
            settings.timeSpends = 0;
            timeStat.Text = "0";
            settings.isDead = false;
            settings.isWin = false;
            settings.isStart = false;
            settings.realMinesCount = settings.minesCount;
            minesLeftCount.Text = settings.minesCount.ToString();
            mineProg.Value = 0;
            gameStatText.Text = "游玩中";
        }
    }
    /// <summary>
    /// 地图组件
    /// </summary>
    [Serializable]
    public class ButtonMap
    {
        //周围的雷
        [XmlIgnore]
        //[JsonIgnore]
        [NonSerialized]
        public ButtonMap[]? otherMine = new ButtonMap[8];

        public ButtonMap(Button mapButton, int x, int y)
        {
            MapButton = mapButton;
            IndexX = x;
            IndexY = y;
        }
        public ButtonMap()
        {

        }
        public bool boom = false;

        //X坐标
        public int IndexX
        { get; set; }

        //Y坐标
        public int IndexY
        { get; set; }

        //是否被标记为雷
        private bool _isFlaged;
        public bool IsFlaged
        {
            get
            {
                return _isFlaged;
            }
            set
            {
                if (value == false)
                {
                    if (this.MapButton != null)
                    {
                        this.MapButton.Text = "";
                    }

                }
                else
                {
                    if (this.MapButton != null)
                    {
                        this.MapButton!.Text = "◆";
                        this.MapButton!.ForeColor = Color.Red;
                    }

                }
                _isFlaged = value;
            }
        }

        //是否有雷
        public bool IsHasMine
        { get; set; }

        //是否已被扫描
        private bool _isSweeped;
        public bool IsSweeped
        {
            get
            {
                return _isSweeped;
            }
            set
            {
                if (this.MapButton != null && value == true)
                {
                    this.MapButton!.BackColor = Color.WhiteSmoke;
                    this.MapButton!.FlatStyle = FlatStyle.Flat;
                    this.MapButton!.FlatAppearance.BorderColor = Color.LightGray;
                    this.MapButton!.FlatAppearance.MouseOverBackColor = Color.WhiteSmoke;
                    this.MapButton!.FlatAppearance.MouseDownBackColor = Color.WhiteSmoke;
                }
                _isSweeped = value;
            }
        }

        //网格
        [XmlIgnore]
        //[JsonIgnore]
        [NonSerialized]
        private Button? _mapButton;
        public Button? MapButton
        {
            get
            {
                return this._mapButton;
            }
            set
            {
                this._mapButton = value;
            }
        }

        //格子周围雷的数量
        private int _otherMineCount;
        public int OtherMineCount
        {
            get
            {
                return _otherMineCount;
            }
            set
            {
                _otherMineCount = value;
                if (this.MapButton != null)
                {
                    this.MapButton!.ForeColor = this._otherMineCount switch
                    {
                        1 => Color.Blue,
                        2 => Color.Green,
                        3 => Color.Red,
                        4 => Color.Purple,
                        5 => Color.BlanchedAlmond,
                        6 => Color.Magenta,
                        7 => Color.Brown,
                        8 => Color.Black,
                        _ => Color.Black,
                    };
                }
            }
        }

        /// <summary>
        /// 根据索引读取组件
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="buttons"></param>
        /// <returns></returns>
        public static ButtonMap ReadButton(int x, int y, ButtonMap[,] buttons)
        {
            return buttons[x, y];
        }

        /// <summary>
        /// 读取索引
        /// </summary>
        /// <param name="mapButton"></param>
        /// <returns></returns>
        public static int[] ReadIndex(ButtonMap mapButton)
        {
            return new int[] { mapButton.IndexX, mapButton.IndexY };
        }
    }
}