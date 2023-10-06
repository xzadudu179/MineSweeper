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
            //��ͼ         

            //�������������С
            public int mapSizeMax = 40;

            //ÿ������Ĵ�С
            public int buttonSize = 30;

            //����֮��ļ��
            public int buttonSpace = 0;

            public bool isDead = false;

            //�Ƿ����˵�һ��
            public bool isStart = false;

            //�Ƿ�ʤ��
            public bool isWin = false;

            //��
            public int width = 8;
            //��
            public int height = 10;
            //����
            public int minesCount = 10;
            public int realMinesCount = 0;
            //���ڵĴ�С
            public int windowsSpace = 60;
            //��갴���Ƿ���
            public bool isLeftDown = false;
            public bool isRightDown = false;

            //ʹ�õ�ʱ��
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
        /// �����¼�
        /// </summary>
        /// <param name="clickButton"></param>
        private void ClickMapButton(ButtonMap clickButton)
        {
            //���������
            if (clickButton.IsFlaged) return;
            //�������
            if (clickButton.IsHasMine)
            {
                clickButton.MapButton!.BackColor = Color.Red;
                clickButton.boom = true;
                //clickButton.MapButton!.Text = "��";
                ShowMines(settings.buttons!);
                timerT.Stop();
                settings.isDead = true;
                gameStatText.Text = "ʧ��";
                MessageBox.Show("boom");
                return;
            }
            //����
            if (clickButton.IsSweeped) return;
            Sweep(clickButton.IndexX, clickButton.IndexY, settings.buttons!);
        }

        /// <summary>
        /// �ж��Ƿ�ʤ��
        /// </summary> 
        private void IsOrNotWin(ButtonMap[,] buttons)
        {
            //����
            //������и��Ӳ����ѱ�ɨ���������� �������׶������� ��ô��ʤ��
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
            gameStatText.Text = "ʤ��";
            InputDialog winDialog = new InputDialog($"ʤ��~ ��ʱ{timeStat.Text}��\n" +
                $"����Ϊ:{(float)(settings.width * settings.height / 100f) * (float)((float)(settings.minesCount) / (settings.width * settings.height * 1f)) / (float)(settings.timeSpends > 0f ? settings.timeSpends : 0.01f) * 100}" +
                $"\n\n�������û���:", " ", startForm!);
            winDialog.ShowDialog();
            //MessageBox.Show($"ʤ��~ ��ʱ{timeStat.Text}��", "ʤ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// �������ʱ
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
                //����
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
        /// ����
        /// </summary>
        /// <param name="bSize">��ť��С</param>
        /// <param name="mWidth">��</param>
        /// <param name="mHeight">��</param>
        /// <param name="bButtonSpace">��ť���</param>
        /// <param name="mMinesCount">��������</param>
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
        /// ���ɵ�ͼ
        /// </summary>
        /// <param name="height">��ͼ��</param>
        /// <param name="width">��ͼ��</param>
        private void GenerateMap(int width, int height)
        {
            //�ж��Ƿ����
            height = height > settings.mapSizeMax ? settings.mapSizeMax : height <= 1 ? 2 : height;
            width = width > settings.mapSizeMax ? settings.mapSizeMax : width <= 1 ? 2 : width;
            settings.buttons = new ButtonMap[width, height];
            //���ɵ�ͼ
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    settings.buttons[i, j] = new ButtonMap(new Button(), i, j);
                    settings.buttons[i, j].MapButton!.Name = i.ToString() + "," + j.ToString();
                }
            }
            //��һ����ť��λ����60,60
            //�ڶ�����ť��90,60
            //���ɰ�ť
            foreach (ButtonMap button in settings.buttons)
            {
                if (button.MapButton != null)
                {
                    button.MapButton.Size = new System.Drawing.Size(settings.buttonSize, settings.buttonSize);
                    //λ��
                    button.MapButton.Location = new System.Drawing.Point(button.IndexX * (settings.buttonSize + settings.buttonSpace) + settings.windowsSpace,
                        button.IndexY * (settings.buttonSize + settings.buttonSpace) + settings.windowsSpace);
                    button.MapButton.MouseDown += MapButtonClick;
                    button.MapButton.MouseUp += MapMouseUp;
                    //button.MapButton.BackColor = System.Drawing.Color.WhiteSmoke;
                    //button.MapButton.FlatStyle = FlatStyle.Popup;
                    //����
                    button.MapButton.Font = new Font(button.MapButton!.Font.FontFamily, (float)(settings.buttonSize * 0.4f), FontStyle.Regular);

                    this.Controls.Add(button.MapButton);
                }
            }
            //�ı䴰�ڴ�С 150Ϊ���ڼ��(��һ����ť���*2
            //(buttonSize / 2)�Ƿ�ֹ��ť��СӰ�촰��
            this.Size = new System.Drawing.Size(settings.buttons.Length / height * (settings.buttonSize + settings.buttonSpace) +
                settings.windowsSpace * 2 + ((settings.buttonSize + settings.buttonSpace) / 2),
                settings.buttons.Length / width * (settings.buttonSize + settings.buttonSpace) + settings.windowsSpace * 2 + ((settings.buttonSize + settings.buttonSpace) / 2));
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="minesCount">����</param>
        /// <param name="buttons">����</param>
        /// <param name="buttonIndexNow">��ǰ�����ť</param>
        private void GenerateMine(int minesCount, ButtonMap[,] buttons, int[] buttonIndexNow, bool synchMines)
        {
            if (minesCount >= buttons.Length - 0) minesCount = buttons.Length - 2;
            if (minesCount <= 0) minesCount = 1;
            //����ͬ��
            if (synchMines)
            {
                settings.realMinesCount = minesCount;
            }
            //minesLeftCount.Text = settings.realMinesCount.ToString();
            int[,] mines = new int[buttons.GetLength(0), buttons.GetLength(1)];
            Random random = new Random();
            //ʣ����Ҫ���õ�����
            int minesLeft = settings.realMinesCount;
            //���ѡ���ӷ���
            while (minesLeft > 0)
            {
                for (int i = 0; i < mines.GetLength(0); i++)
                {
                    for (int j = 0; j < mines.GetLength(1); j++)
                    {
                        if (minesLeft <= 0) break;
                        //�ж��Ƿ����
                        //����: ������� �������û���� ���겻���ڵ�ǰ��ť
                        if (i == buttonIndexNow[0] && j == buttonIndexNow[1]) continue;
                        if (random.Next(0, mines.Length) == 1 && mines[i, j] != 1)
                        {
                            mines[i, j] = 1;
                            minesLeft--;
                        }
                    }
                }
            }

            //��������
            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    //������
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
        /// �����ͼ��ťʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapButtonClick(object? sender, MouseEventArgs e)
        {
            try
            {
                //�ж�����
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
            //�ж�
            if (sender == null) return;
            Button button = (Button)sender;
            //���������¼�
            //�ָ��ַ���
            char[] chs = { ',' }; //�ָ���Щ����
            string[] str = button.Name.ToString().Split(chs, StringSplitOptions.RemoveEmptyEntries);
            ButtonMap clickButton = ButtonMap.ReadButton(Convert.ToInt32(str[0]), Convert.ToInt32(str[1]), settings.buttons!);
            //��һ�ΰ���ʱ
            if (!settings.isStart && e.Button == MouseButtons.Left && !settings.isRightDown)
            {
                //������
                GenerateMine(settings.minesCount, settings.buttons!, new int[] { Convert.ToInt32(str[0]), Convert.ToInt32(str[1]) }, true);
                //������ʱ��
                timerT.Enabled = true;
                settings.isStart = true;
            }
            //�ж�����            
            //���
            if (e.Button == MouseButtons.Left && !settings.isRightDown)
            {
                ClickMapButton(clickButton);
                settings.isLeftDown = true;
            }
            //�Ҽ�
            if (e.Button == MouseButtons.Right && !settings.isLeftDown)
            {
                settings.isRightDown = true;
                if (clickButton.IsSweeped) return;
                if (clickButton.IsFlaged == true)
                {
                    clickButton.IsFlaged = false;
                    //clickButton.MapButton!.Text = "";
                    //������ʾ����
                    minesLeftCount.Text = (Convert.ToInt32(minesLeftCount.Text) + 1).ToString();
                }
                else
                {
                    clickButton.IsFlaged = true;
                    //clickButton.MapButton!.Text = "��";
                    //������ʾ����
                    minesLeftCount.Text = (Convert.ToInt32(minesLeftCount.Text) - 1).ToString();
                }
            }
            //���Ҽ�
            if (e.Button == MouseButtons.Right && settings.isLeftDown)
            {
                //�Զ�������׸����ĸ�
                if (clickButton.OtherMineCount <= 0 && !clickButton.IsSweeped) return;
                QuickCheck(clickButton.IndexX, clickButton.IndexY, settings.buttons!);
            }
            else if (e.Button == MouseButtons.Left && settings.isRightDown)
            {
                //�Զ�������׸����ĸ�
                if (clickButton.OtherMineCount <= 0 && !clickButton.IsSweeped) return;
                QuickCheck(clickButton.IndexX, clickButton.IndexY, settings.buttons!);
            }
            //�ж��Ƿ�ʤ��
            IsOrNotWin(settings.buttons!);
        }

        /// <summary>
        /// ��ʾ���е���
        /// </summary>
        /// <param name="buttons"></param>
        private static void ShowMines(ButtonMap[,] buttons)
        {
            foreach (ButtonMap button in buttons)
            {
                if (button.IsHasMine)
                {
                    button.MapButton!.Text = "��";
                    button.MapButton!.ForeColor = Color.Black;
                }
            }
        }

        /// <summary>
        /// ��ݵ��
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="buttons"></param>
        private void QuickCheck(int x, int y, ButtonMap[,] buttons)
        {
            ButtonMap thisButton = ButtonMap.ReadButton(x, y, buttons);
            //if (thisButton.IsSweeped || thisButton.OtherMineCount <= 0) return;
            //������Χ�ĸ���
            List<ButtonMap> searchButtons = new List<ButtonMap>();
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i >= buttons.GetLength(0) || j >= buttons.GetLength(1) || i < 0 || j < 0) continue;
                    if (ButtonMap.ReadButton(i, j, buttons).IsSweeped || buttons[i, j] == thisButton) continue;
                    //��ӽ��б�
                    searchButtons.Add(ButtonMap.ReadButton(i, j, buttons));
                }
            }
            //�ж���Χ�ĸ����Ƿ��б��� �ҵ�ǰ����İ�ť��ʾ�����Ƿ�����������һ��
            //���һ�� ������Χ����û���첢��û��ɨ��ķ���
            int flag = 0;
            foreach (ButtonMap button in searchButtons)
            {
                //����
                if (button.IsFlaged)
                {
                    flag++;
                }
            }
            if (flag == thisButton.OtherMineCount)
            {
                //������Χ����û�������û��ɨ��ķ���
                foreach (ButtonMap button in searchButtons)
                {
                    //����
                    if (button.IsFlaged || button.IsSweeped) continue;
                    ClickMapButton(button);
                }
            }
        }

        /// <summary>
        /// �ж���Χ��û����
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private void Sweep(int x, int y, ButtonMap[,] buttons)
        {
            ButtonMap thisButton = ButtonMap.ReadButton(x, y, buttons);
            //���Ϊ������
            thisButton.IsSweeped = true;
            //����������ť
            //thisButton.MapButton!.Enabled = false;
            //bool hasFlagedButton = false;
            //����
            thisButton.OtherMineCount = 0;
            //������Χ�ĸ���
            List<ButtonMap> searchButtons = new List<ButtonMap>();
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i >= buttons.GetLength(0) || j >= buttons.GetLength(1) || i < 0 || j < 0) continue;
                    if (ButtonMap.ReadButton(i, j, buttons).IsSweeped || buttons[i, j] == thisButton) continue;
                    //��ӽ��б�
                    searchButtons.Add(ButtonMap.ReadButton(i, j, buttons));
                }
            }
            //�ж��Ƿ�����
            int fori = 0;
            foreach (ButtonMap button in searchButtons)
            {
                //����
                if (button.IsHasMine)
                {
                    //��������ʾ������
                    thisButton.OtherMineCount += 1;
                    //��ӽ�otherMine�б�
                    thisButton.otherMine![fori] = button;
                }
                fori++;
            }
            //��ʾ����
            if (thisButton.OtherMineCount > 0 /*|| hasFlagedButton*/)
            {
                thisButton.MapButton!.Text = thisButton.OtherMineCount.ToString();
                thisButton.MapButton!.Font = new Font(thisButton.MapButton!.Font.FontFamily, thisButton.MapButton!.Font.Size, FontStyle.Bold);
            }
            //û����
            else
            {
                foreach (ButtonMap button in searchButtons)
                {
                    //������
                    if (button.IsFlaged)
                    {
                        button.IsFlaged = false;
                        minesLeftCount.Text = (Convert.ToInt32(minesLeftCount.Text) + 1).ToString();
                    }
                    //��������
                    Sweep(ButtonMap.ReadIndex(button)[0], ButtonMap.ReadIndex(button)[1], buttons);
                }
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveStrip_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("�˹�����δ����~");
            //�洢����mapbuttons
            timerT.Stop();
            SaveFileDialog sfd = new SaveFileDialog
            {
                Title = "�����ͼ�ļ�",
                //Filter = "ɨ�׵�ͼ�ļ�|*.xml",
                Filter = "ɨ�׵�ͼ�ļ�|*.swmap",
                RestoreDirectory = true
            };
            sfd.ShowDialog();
            string path = sfd.FileName;
            if (path == "")
            {
                return;
            }
            //��¼���ѵ�ʱ��
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

            ////���л�Ϊxml
            //XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            //TextWriter writer = new StreamWriter(path);
            //serializer.Serialize(writer, settings);
            //writer.Close();

            //���л�Ϊbf
            using (FileStream fsWrite = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                //���л�
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fsWrite, settings);
            }



            if (!settings.isDead && !settings.isWin)
            {
                timerT.Start();
            }


            MessageBox.Show($"�ɹ����浽{path}");
        }

        /// <summary>
        /// ����
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
                ofd.Title = "�򿪵�ͼ�ļ�";
                ofd.Multiselect = false;
                //ofd.Filter = "ɨ�׵�ͼ�ļ�|*.xml";
                ofd.Filter = "ɨ�׵�ͼ�ļ�|*.swmap";
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

                //����
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
                MessageBox.Show($"���س��ִ���:{ex.Message}\n{ex.InnerException}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void LoadSweepMap(string path)
        {
            try
            {
                Settings newSettings = new Settings();

                ////xml�����л�
                //FileStream fs = File.Open(path, FileMode.Open);
                //using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                //{
                //    XmlSerializer xs = new XmlSerializer(typeof(Settings));
                //    newSettings = (Settings)xs.Deserialize(sr)!;
                //}

                //bf�����л�
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
                //����һ���ĵ�ͼ
                GenerateMap(settings.width, settings.height);
                //minesLeftCount.Text = newSettings.minesCount.ToString();
                //���� �ı����а�ť��Ϣ
                for (int i = 0; i < newSettings.buttonsSeri.Count; i++)
                {
                    for (int j = 0; j < newSettings.buttonsSeri[i].Count; j++)
                    {
                        //settings.buttonsSeri[i][j].MapButton = newSettings.buttons[i, j].MapButton;
                        //ͬ������
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
                            settings.buttons![i, j].MapButton!.Text = "��";
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
                    gameStatText.Text = "ʧ��";
                }
                else if (settings.isWin)
                {
                    gameStatText.Text = "ʤ��";
                }
                else
                {
                    gameStatText.Text = "������";
                    timerT.Start();
                }
                minesLeftCount.Text = settings.realMinesCount.ToString();
                //�ж�����
                if (settings.realMinesCount >= 0)
                {
                    mineProg.Value = Convert.ToInt32(Math.Abs(1f - (float)Convert.ToInt32(minesLeftCount.Text) / (float)settings.minesCount) * 100);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"���س��ִ���:{ex.Message}\n{ex.InnerException}\n{ex}��ȷ���Ƿ�Ϊɨ�׵�ͼ�ļ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// ʱ��
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
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void retryStrip_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Process.GetCurrentProcess()?.Kill();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpStrip_Click(object sender, EventArgs e)
        {
            string helpString =
                "��ͼ�����ɸ������\n" +
                "�����ڿ���ʱ ���������Ե㿪��\n" +
                "�㿪�Ŀ�������ʾ���֣����������Χ��8��������[����ֵ]������" +
                "����㿪�Ŀ麬�е��� ����Ϸʧ��\n" +
                "�Ҽ����δ�㿪�Ŀ�ɽ�����Ϊ���ף��ٴε����ȡ�����(���Ϊ���׺��������ÿ鲻�����κη�Ӧ)\n" +
                "���Ҽ�ͬʱ����ѵ㿪�Ҵ������ֵĿ�ʱ������ÿ���Χ8�����б���������ڿ����������ʾ�����������Զ��㿪��Χ8�����г������������п飬�����޷��㿪"
                ;
            MessageBox.Show(helpString, "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// �Ե�ǰ�������¿�ʼ
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
            gameStatText.Text = "������";
        }
    }
    /// <summary>
    /// ��ͼ���
    /// </summary>
    [Serializable]
    public class ButtonMap
    {
        //��Χ����
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

        //X����
        public int IndexX
        { get; set; }

        //Y����
        public int IndexY
        { get; set; }

        //�Ƿ񱻱��Ϊ��
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
                        this.MapButton!.Text = "��";
                        this.MapButton!.ForeColor = Color.Red;
                    }

                }
                _isFlaged = value;
            }
        }

        //�Ƿ�����
        public bool IsHasMine
        { get; set; }

        //�Ƿ��ѱ�ɨ��
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

        //����
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

        //������Χ�׵�����
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
        /// ����������ȡ���
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
        /// ��ȡ����
        /// </summary>
        /// <param name="mapButton"></param>
        /// <returns></returns>
        public static int[] ReadIndex(ButtonMap mapButton)
        {
            return new int[] { mapButton.IndexX, mapButton.IndexY };
        }
    }
}