using System.Diagnostics.Eventing.Reader;
using System.Net.Sockets;
using System.IO;
using System;
using System.Text.RegularExpressions;

namespace _2048Tetris
{
    public partial class Form1 : Form
    {
        public int[,] map = new int[8, 4];
        public Label[,] labels = new Label[8, 4];
        public PictureBox[,] pics = new PictureBox[8, 4];
        private int score = 0;
        public int step = 1;
        private int duration = 2;
        private int gametime = 60;
        public int max,maxnum=2,time=1000;
        public int set = 1;
        public Form1()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(_keyboardEvent);
            map[0, 0] = 1;
            StreamReader str = new StreamReader(Application.StartupPath + "bestscore.txt");
            string st1 = str.ReadLine();
            str.Close();
            StreamReader str2 = new StreamReader(Application.StartupPath + "bestnum.txt");
            string st2 = str2.ReadLine();
            str2.Close();
            max = Convert.ToInt32(st2);
            timer1.Tick += new EventHandler(count_down);
            label3.Text = "最佳成績:" + st1;
            label1.Text = "Score:" + score;
            label4.Text = "倒數時間:" + duration.ToString();
            label5.Text = "遊戲剩餘時間" + gametime.ToString();
            label6.Text = "最大合成數字:" + st2;
            timer1.Interval = time;
            timer1.Start();
            timer2.Start();
            createMap();
            createPics();
        }
        private void createMap()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    PictureBox pic = new PictureBox();
                    pic.Location = new Point(32 + 56 * j, 111 + 56 * i);
                    pic.Size = new Size(50, 50);
                    pic.BackColor = Color.Gray;
                    this.Controls.Add(pic);
                }
            }
        }
        private void generateNewPic()
        {
            Random rnd = new Random();
            int a = 0;
            int b = 0;
            int sum = 0, r;
            if (maxnum<16)
            {
                sum = 2;
                set = 1;
            }
            else if (maxnum==16)
            {
                int[] list = new int[] { 2, 4 };
                r = rnd.Next(list.Length);
                sum = list[r];
                set = 1;
            }
            else if (maxnum==32)
            {
                int[] list = new int[] { 2, 4, 8 };
                r = rnd.Next(list.Length);
                sum = list[r];
                set = 1;
            }
            else if (maxnum == 64)
            {
                int[] list = new int[] { 4, 8, 16 };
                r = rnd.Next(list.Length);
                sum = list[r];
                set = 2;
            }
            else if (maxnum == 128)
            {
                int[] list = new int[] { 8, 16, 32 };
                r = rnd.Next(list.Length);
                sum = list[r];
                set = 2;
            }
            else if (maxnum == 256)
            {
                int[] list = new int[] { 16, 32, 64 };
                r = rnd.Next(list.Length);
                sum = list[r];
                set = 2;
            }
            else if (maxnum == 512)
            {
                int[] list = new int[] { 16, 32, 64 };
                r = rnd.Next(list.Length);
                sum = list[r];
                set = 3;
            }
            else if (maxnum == 1024)
            {
                int[] list = new int[] { 32, 64, 128 };
                r = rnd.Next(list.Length);
                sum = list[r];
                set = 3;
            }
            else if (maxnum == 2048)
            {
                int[] list = new int[] { 64, 128, 256 };
                r = rnd.Next(list.Length);
                sum = list[r];
                set = 3;
            }
            map[a, b] = 1;
            pics[a, b] = new PictureBox();
            labels[a, b] = new Label();
            labels[a, b].Text = Convert.ToString(sum);
            labels[a, b].Size = new Size(50, 50);
            labels[a, b].TextAlign = ContentAlignment.MiddleCenter;
            labels[a, b].Font = new Font(new FontFamily("Microsoft Sans Serif"), 12);
            pics[a, b].Controls.Add(labels[a, b]);
            pics[a, b].Location = new Point(32 + b * 56, 111 + 56 * a);
            pics[a, b].Size = new Size(50, 50);
            changeColor(sum, a, b);
            this.Controls.Add(pics[a, b]);
            pics[a, b].BringToFront();
        }
        private void createPics()
        {
            pics[0, 0] = new PictureBox();
            labels[0, 0] = new Label();
            labels[0, 0].Text = "2";
            labels[0, 0].Size = new Size(50, 50);
            labels[0, 0].TextAlign = ContentAlignment.MiddleCenter;
            labels[0, 0].Font = new Font(new FontFamily("Microsoft Sans Serif"), 12);
            pics[0, 0].Controls.Add(labels[0, 0]);
            pics[0, 0].Location = new Point(32, 111);
            pics[0, 0].Size = new Size(50, 50);
            pics[0, 0].BackColor = Color.LightSteelBlue;
            this.Controls.Add(pics[0, 0]);
            pics[0, 0].BringToFront();
        }
        private void changeColor(int sum, int k, int j)
        {
            if (sum % 2048 == 0) pics[k, j].BackColor = Color.LimeGreen;
            else if (sum % 1024 == 0) pics[k, j].BackColor = Color.Pink;
            else if (sum % 512 == 0) pics[k, j].BackColor = Color.Red;
            else if (sum % 256 == 0) pics[k, j].BackColor = Color.DarkViolet;
            else if (sum % 128 == 0) pics[k, j].BackColor = Color.AliceBlue;
            else if (sum % 64 == 0) pics[k, j].BackColor = Color.GreenYellow;
            else if (sum % 32 == 0) pics[k, j].BackColor = Color.PeachPuff;
            else if (sum % 16 == 0) pics[k, j].BackColor = Color.Aqua;
            else if (sum % 8 == 0) pics[k, j].BackColor = Color.Orange;
            else if (sum % 4 == 0) pics[k, j].BackColor = Color.Sienna;
            else pics[k, j].BackColor = Color.LightSteelBlue;
        }

        private void _keyboardEvent(object sender, KeyEventArgs e)
        {
            bool ifPicsWasMoved = false;
            switch (e.KeyCode.ToString())
            {
                case "Right":
                    if (step == 2)
                    {
                        step = 1;
                        duration = 2;
                        timer1.Stop();
                        timer1.Start();
                        label4.Text = "倒數時間:" + duration.ToString();
                        ifPicsWasMoved = true;
                        for (int k = 0; k < 8; k++)
                        {
                            for (int l = 2; l >= 0; l--)
                            {
                                if (map[k, l] == 1)
                                {
                                    for (int j = l + 1; j < 4; j++)
                                    {
                                        if (map[k, j] == 0)
                                        {
                                            map[k, j - 1] = 0;
                                            map[k, j] = 1;
                                            pics[k, j] = pics[k, j - 1];
                                            pics[k, j - 1] = null;
                                            labels[k, j] = labels[k, j - 1];
                                            labels[k, j - 1] = null;
                                            pics[k, j].Location = new Point(pics[k, j].Location.X + 56, pics[k, j].Location.Y);
                                        }
                                        else
                                        {
                                            int a = int.Parse(labels[k, j].Text);
                                            int b = int.Parse(labels[k, j - 1].Text);
                                            step = 1;
                                            duration = 2;
                                            timer1.Stop();
                                            timer1.Start();
                                            label4.Text = "倒數時間:" + duration.ToString();
                                            if (a == b)
                                            {
                                                labels[k, j].Text = (a + b).ToString();
                                                score += (a + b);
                                                if(set==1)
                                                {
                                                    if (a + b >= 64)
                                                        gametime += 8;
                                                }
                                                else if (set == 2)
                                                {
                                                    if (a + b >= 512)
                                                        gametime += 12;
                                                }
                                                else if (set == 3)
                                                {
                                                    if (a + b >= 2048)
                                                        gametime += 15;
                                                }
                                                if (a + b > Convert.ToInt32(max))
                                                    max = (a + b);
                                                if (a + b > Convert.ToInt32(maxnum))
                                                {
                                                    maxnum = (a + b);
                                                    time = time - 70;
                                                }     
                                                label5.Text = "遊戲剩餘時間:" + gametime.ToString();
                                                changeColor(a + b, k, j);
                                                label1.Text = "Score:" + score;
                                                map[k, j - 1] = 0;
                                                this.Controls.Remove(pics[k, j - 1]);
                                                this.Controls.Remove(labels[k, j - 1]);
                                                pics[k, j - 1] = null;
                                                labels[k, j - 1] = null;

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (step == 1)
                    {
                        for (int k = 0; k < 1; k++)
                        {
                            for (int l = 2; l >= 0; l--)
                            {
                                if (map[k, l] == 1)
                                {
                                    int j = l + 1;
                                    if (map[k, j] == 0)
                                    {
                                        map[k, j - 1] = 0;
                                        map[k, j] = 1;
                                        pics[k, j] = pics[k, j - 1];
                                        pics[k, j - 1] = null;
                                        labels[k, j] = labels[k, j - 1];
                                        labels[k, j - 1] = null;
                                        pics[k, j].Location = new Point(pics[k, j].Location.X + 56, pics[k, j].Location.Y);
                                        j++;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "Left":
                    if (step == 2)
                    {
                        step = 1;
                        duration = 2;
                        timer1.Stop();
                        timer1.Start();
                        label4.Text = "倒數時間:" + duration.ToString();
                        ifPicsWasMoved = true;
                        for (int k = 0; k < 8; k++)
                        {
                            for (int l = 1; l < 4; l++)
                            {
                                if (map[k, l] == 1)
                                {
                                    for (int j = l - 1; j >= 0; j--)
                                    {
                                        if (map[k, j] == 0)
                                        {

                                            map[k, j + 1] = 0;
                                            map[k, j] = 1;
                                            pics[k, j] = pics[k, j + 1];
                                            pics[k, j + 1] = null;
                                            labels[k, j] = labels[k, j + 1];
                                            labels[k, j + 1] = null;
                                            pics[k, j].Location = new Point(pics[k, j].Location.X - 56, pics[k, j].Location.Y);
                                        }
                                        else
                                        {
                                            int a = int.Parse(labels[k, j].Text);
                                            int b = int.Parse(labels[k, j + 1].Text);
                                            step = 1;
                                            duration = 2;
                                            timer1.Stop();
                                            timer1.Start();
                                            label4.Text = "倒數時間:" + duration.ToString();
                                            if (a == b)
                                            {
                                                labels[k, j].Text = (a + b).ToString();
                                                score += (a + b);
                                                if (set == 1)
                                                {
                                                    if (a + b >= 64)
                                                        gametime += 8;
                                                }
                                                else if (set == 2)
                                                {
                                                    if (a + b >= 512)
                                                        gametime += 12;
                                                }
                                                else if (set == 3)
                                                {
                                                    if (a + b >= 2048)
                                                        gametime += 15;
                                                }
                                                if (a + b > Convert.ToInt32(max))
                                                    max = (a + b);
                                                if (a + b > Convert.ToInt32(maxnum))
                                                {
                                                    maxnum = (a + b);
                                                    time = time - 70;
                                                }
                                                label5.Text = "遊戲剩餘時間:" + gametime.ToString();
                                                changeColor(a + b, k, j);
                                                label1.Text = "Score:" + score;
                                                map[k, j + 1] = 0;
                                                this.Controls.Remove(pics[k, j + 1]);
                                                this.Controls.Remove(labels[k, j + 1]);
                                                pics[k, j + 1] = null;
                                                labels[k, j + 1] = null;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (step == 1)
                    {
                        for (int k = 0; k < 1; k++)
                        {
                            for (int l = 1; l < 4; l++)
                            {
                                if (map[k, l] == 1)
                                {
                                    int j = l - 1;
                                    if (map[k, j] == 0)
                                    {
                                        map[k, j + 1] = 0;
                                        map[k, j] = 1;
                                        pics[k, j] = pics[k, j + 1];
                                        pics[k, j + 1] = null;
                                        labels[k, j] = labels[k, j + 1];
                                        labels[k, j + 1] = null;
                                        pics[k, j].Location = new Point(pics[k, j].Location.X - 56, pics[k, j].Location.Y);
                                        j--;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "Down":
                    if (step == 1)
                    {
                        for (int k = 7; k >= 0; k--)
                        {
                            for (int l = 0; l < 4; l++)
                            {
                                if (map[k, l] == 1)
                                {
                                    for (int j = k + 1; j < 8; j++)
                                    {
                                        if (map[j, l] == 0)
                                        {
                                            step = 2;
                                            duration = 2;
                                            timer1.Stop();
                                            timer1.Start();
                                            label4.Text = "倒數時間:" + duration.ToString();
                                            map[j - 1, l] = 0;
                                            map[j, l] = 1;
                                            pics[j, l] = pics[j - 1, l];
                                            pics[j - 1, l] = null;
                                            labels[j, l] = labels[j - 1, l];
                                            labels[j - 1, l] = null;
                                            pics[j, l].Location = new Point(pics[j, l].Location.X, pics[j, l].Location.Y + 56);
                                        }
                                        else
                                        {
                                            int a = int.Parse(labels[j, l].Text);
                                            int b = int.Parse(labels[j - 1, l].Text);
                                            step = 2;
                                            duration = 2;
                                            timer1.Stop();
                                            timer1.Start();
                                            label4.Text = "倒數時間:" + duration.ToString();
                                            if (a == b)
                                            {
                                                labels[j, l].Text = (a + b).ToString();
                                                score += (a + b);
                                                if (set == 1)
                                                {
                                                    if (a + b >= 64)
                                                        gametime += 8;
                                                }
                                                else if (set == 2)
                                                {
                                                    if (a + b >= 512)
                                                        gametime += 12;
                                                }
                                                else if (set == 3)
                                                {
                                                    if (a + b >= 2048)
                                                        gametime += 15;
                                                }
                                                if (a + b > Convert.ToInt32(max))
                                                    max = (a + b);
                                                if (a + b > Convert.ToInt32(maxnum))
                                                {
                                                    maxnum = (a + b);
                                                    time = time - 70;
                                                }
                                                label5.Text = "遊戲剩餘時間:" + gametime.ToString();
                                                changeColor(a + b, j, l);
                                                label1.Text = "Score:" + score;
                                                map[j - 1, l] = 0;
                                                this.Controls.Remove(pics[j - 1, l]);
                                                this.Controls.Remove(labels[j - 1, l]);
                                                pics[j - 1, l] = null;
                                                labels[j - 1, l] = null;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
            if (ifPicsWasMoved)
            {
                generateNewPic();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int k = 0; k < 8; k++)
            {
                for (int j = 0; j < 4; j++)
                {
                    map[k, j] = 0;
                    this.Controls.Remove(pics[k, j]);
                    this.Controls.Remove(labels[k, j]);
                    pics[k, j] = null;
                    labels[k, j] = null;
                }
            }
            createMap();
            map[0, 0] = 1;
            createPics();
            step = 1;
            duration = 2;
            gametime = 60;
            StreamReader str = new StreamReader(Application.StartupPath + "bestscore.txt");
            string st1 = str.ReadLine();
            str.Close();
            StreamReader str2 = new StreamReader(Application.StartupPath + "bestnum.txt");
            string st2 = str2.ReadLine();
            str2.Close();

            if (score > Convert.ToInt32(st1))
            {
                StreamWriter stw = new StreamWriter(Application.StartupPath + "bestscore.txt");
                string stscore = Convert.ToString(score);
                stw.WriteLine(stscore);
                stw.Close();
                label3.Text = ("最佳成績:" + Convert.ToString(score));
            }
            if (max > Convert.ToInt32(st2))
            {
                StreamWriter stw = new StreamWriter(Application.StartupPath + "bestnum.txt");
                string stnum = Convert.ToString(max);
                stw.WriteLine(stnum);
                stw.Close();
                label6.Text = ("最大合成數字:" + Convert.ToString(max));
            }
            score = 0;
            label1.Text = "Score:" + score;
            label4.Text = "倒數時間:" + duration.ToString();
            label5.Text = "遊戲剩餘時間" + gametime.ToString();
            timer1.Start();
            timer2.Start();
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Right || keyData == Keys.Left)
                return false;
            return base.ProcessDialogKey(keyData);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
        private void count_down(object sender, EventArgs e)
        {
            if (duration == 0)
            {
                label4.Text = "倒數時間:" + duration.ToString();
                if (step == 1)
                {
                    for (int k = 7; k >= 0; k--)
                    {
                        for (int l = 0; l < 4; l++)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int j = k + 1; j < 8; j++)
                                {
                                    if (map[j, l] == 0)
                                    {
                                        step = 2;
                                        duration = 2;
                                        timer1.Stop();
                                        timer1.Start();
                                        label4.Text = "倒數時間:" + duration.ToString();
                                        map[j - 1, l] = 0;
                                        map[j, l] = 1;
                                        pics[j, l] = pics[j - 1, l];
                                        pics[j - 1, l] = null;
                                        labels[j, l] = labels[j - 1, l];
                                        labels[j - 1, l] = null;
                                        pics[j, l].Location = new Point(pics[j, l].Location.X, pics[j, l].Location.Y + 56);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[j, l].Text);
                                        int b = int.Parse(labels[j - 1, l].Text);
                                        if (a == b)
                                        {
                                            step = 2;
                                            duration = 2;
                                            timer1.Stop();
                                            timer1.Start();
                                            label4.Text = "倒數時間:" + duration.ToString();
                                            labels[j, l].Text = (a + b).ToString();
                                            score += (a + b);
                                            if (set == 1)
                                            {
                                                if (a + b >= 64)
                                                    gametime += 8;
                                            }
                                            else if (set == 2)
                                            {
                                                if (a + b >= 512)
                                                    gametime += 12;
                                            }
                                            else if (set == 3)
                                            {
                                                if (a + b >= 2048)
                                                    gametime += 15;
                                            }
                                            if (a + b > Convert.ToInt32(max))
                                                max = (a + b);
                                            if (a + b > Convert.ToInt32(maxnum))
                                            {
                                                maxnum = (a + b);
                                                time = time - 70;
                                            }
                                            label5.Text = "遊戲剩餘時間:" + gametime.ToString();
                                            changeColor(a + b, j, l);
                                            label1.Text = "Score:" + score;
                                            map[j - 1, l] = 0;
                                            this.Controls.Remove(pics[j - 1, l]);
                                            this.Controls.Remove(labels[j - 1, l]);
                                            pics[j - 1, l] = null;
                                            labels[j - 1, l] = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (step == 2)
                {
                    Random rnd = new Random();
                    int num = rnd.Next(1, 3);
                    if (num == 1)
                    {
                        step = 1;
                        duration = 2;
                        timer1.Stop();
                        timer1.Start();
                        label4.Text = "倒數時間:" + duration.ToString();
                        generateNewPic();
                        for (int k = 0; k < 8; k++)
                        {
                            for (int l = 2; l >= 0; l--)
                            {
                                if (map[k, l] == 1)
                                {
                                    for (int j = l + 1; j < 4; j++)
                                    {
                                        if (map[k, j] == 0)
                                        {
                                            map[k, j - 1] = 0;
                                            map[k, j] = 1;
                                            pics[k, j] = pics[k, j - 1];
                                            pics[k, j - 1] = null;
                                            labels[k, j] = labels[k, j - 1];
                                            labels[k, j - 1] = null;
                                            pics[k, j].Location = new Point(pics[k, j].Location.X + 56, pics[k, j].Location.Y);
                                        }
                                        else
                                        {
                                            int a = int.Parse(labels[k, j].Text);
                                            int b = int.Parse(labels[k, j - 1].Text);
                                            step = 1;
                                            duration = 2;
                                            timer1.Stop();
                                            timer1.Start();
                                            label4.Text = "倒數時間:" + duration.ToString();
                                            if (a == b)
                                            {
                                                labels[k, j].Text = (a + b).ToString();
                                                score += (a + b);
                                                if (set == 1)
                                                {
                                                    if (a + b >= 64)
                                                        gametime += 8;
                                                }
                                                else if (set == 2)
                                                {
                                                    if (a + b >= 512)
                                                        gametime += 12;
                                                }
                                                else if (set == 3)
                                                {
                                                    if (a + b >= 2048)
                                                        gametime += 15;
                                                }
                                                if (a + b > Convert.ToInt32(max))
                                                    max = (a + b);
                                                if (a + b > Convert.ToInt32(maxnum))
                                                {
                                                    maxnum = (a + b);
                                                    time = time - 70;
                                                }
                                                label5.Text = "遊戲剩餘時間:" + gametime.ToString();
                                                changeColor(a + b, k, j);
                                                label1.Text = "Score:" + score;
                                                map[k, j - 1] = 0;
                                                this.Controls.Remove(pics[k, j - 1]);
                                                this.Controls.Remove(labels[k, j - 1]);
                                                pics[k, j - 1] = null;
                                                labels[k, j - 1] = null;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (num == 2)
                    {
                        step = 1;
                        duration = 2;
                        timer1.Stop();
                        timer1.Start();
                        generateNewPic();
                        label4.Text = "倒數時間:" + duration.ToString();
                        for (int k = 0; k < 8; k++)
                        {
                            for (int l = 1; l < 4; l++)
                            {
                                if (map[k, l] == 1)
                                {
                                    for (int j = l - 1; j >= 0; j--)
                                    {
                                        if (map[k, j] == 0)
                                        {

                                            map[k, j + 1] = 0;
                                            map[k, j] = 1;
                                            pics[k, j] = pics[k, j + 1];
                                            pics[k, j + 1] = null;
                                            labels[k, j] = labels[k, j + 1];
                                            labels[k, j + 1] = null;
                                            pics[k, j].Location = new Point(pics[k, j].Location.X - 56, pics[k, j].Location.Y);
                                        }
                                        else
                                        {
                                            int a = int.Parse(labels[k, j].Text);
                                            int b = int.Parse(labels[k, j + 1].Text);
                                            step = 1;
                                            duration = 2;
                                            timer1.Stop();
                                            timer1.Start();
                                            label4.Text = "倒數時間:" + duration.ToString();
                                            if (a == b)
                                            {
                                                labels[k, j].Text = (a + b).ToString();
                                                score += (a + b);
                                                if (set == 1)
                                                {
                                                    if (a + b >= 64)
                                                        gametime += 8;
                                                }
                                                else if (set == 2)
                                                {
                                                    if (a + b >= 512)
                                                        gametime += 12;
                                                }
                                                else if (set == 3)
                                                {
                                                    if (a + b >= 2048)
                                                        gametime += 15;
                                                }
                                                if (a + b > Convert.ToInt32(max))
                                                    max = (a + b);
                                                if (a + b > Convert.ToInt32(maxnum))
                                                {
                                                    maxnum = (a + b);
                                                    time = time - 70;
                                                }
                                                label5.Text = "遊戲剩餘時間:" + gametime.ToString();
                                                changeColor(a + b, k, j);
                                                label1.Text = "Score:" + score;
                                                map[k, j + 1] = 0;
                                                this.Controls.Remove(pics[k, j + 1]);
                                                this.Controls.Remove(labels[k, j + 1]);
                                                pics[k, j + 1] = null;
                                                labels[k, j + 1] = null;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                duration = 2;
                timer1.Stop();
                timer1.Start();
                label4.Text = "倒數時間:" + duration.ToString();
            }
            else if (duration >= 1)
            {
                duration--;
                label4.Text = "倒數時間:" + duration.ToString();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (gametime == 0)
            {
                label5.Text = "遊戲剩餘時間:" + gametime.ToString();
                timer1.Stop();
                timer2.Stop();
                MessageBox.Show("Game Over!");
                StreamReader str = new StreamReader(Application.StartupPath + "bestscore.txt");
                string st1 = str.ReadLine();
                str.Close();
                StreamReader str2 = new StreamReader(Application.StartupPath + "bestnum.txt");
                string st2 = str2.ReadLine();
                str2.Close();

                if (score > Convert.ToInt32(st1))
                {
                    StreamWriter stw = new StreamWriter(Application.StartupPath + "bestscore.txt");
                    string stscore = Convert.ToString(score);
                    stw.WriteLine(stscore);
                    stw.Close();
                    label3.Text = ("最佳成績:" + Convert.ToString(score));
                }
                if (max > Convert.ToInt32(st2))
                {
                    StreamWriter stw = new StreamWriter(Application.StartupPath + "bestnum.txt");
                    string stnum = Convert.ToString(max);
                    stw.WriteLine(stnum);
                    stw.Close();
                    label6.Text = ("最大合成數字:" + Convert.ToString(max));
                }
                score = 0;
                label1.Text = "Score:" + score;
            }
            else if (gametime >= 1)
            {
                gametime--;
                label5.Text = "遊戲剩餘時間:" + gametime.ToString();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}