using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Media;

namespace Memory
{
    public partial class Form1 : Form
    {
        private int timeElapsed;
        private int hits;
        private int opened;
        private static readonly int TIME = 120;
        List<Square> squares = new List<Square>();
        private bool can;

        public Form1()
        {
            InitializeComponent();
            #region Initialize Squares and add to list
            squares.Add(new Square(this.button1, Properties.Resources.DejanGorgievik, "DejanGorgievik"));
            squares.Add(new Square(this.button2, Properties.Resources.DejanGorgievik, "DejanGorgievik"));
            squares.Add(new Square(this.button3, Properties.Resources.DimitarTrajanov, "DimitarTrajanov"));
            squares.Add(new Square(this.button4, Properties.Resources.DimitarTrajanov, "DimitarTrajanov"));
            squares.Add(new Square(this.button5, Properties.Resources.GjorgiMadzarov, "GjorgiMadzarov"));
            squares.Add(new Square(this.button6, Properties.Resources.GjorgiMadzarov, "GjorgiMadzarov"));
            squares.Add(new Square(this.button7, Properties.Resources.TomceDelev, "TomceDelev"));
            squares.Add(new Square(this.button8, Properties.Resources.TomceDelev, "TomceDelev"));
            squares.Add(new Square(this.button9, Properties.Resources.AnastasMisev, "AnastasMisev"));
            squares.Add(new Square(this.button10, Properties.Resources.AnastasMisev, "AnastasMisev"));
            squares.Add(new Square(this.button11, Properties.Resources.BoroJakimovski, "BoroJakimovski"));
            squares.Add(new Square(this.button12, Properties.Resources.BoroJakimovski, "BoroJakimovski"));
            squares.Add(new Square(this.button13, Properties.Resources.VericaBakeva, "VericaBakeva"));
            squares.Add(new Square(this.button14, Properties.Resources.VericaBakeva, "VericaBakeva"));
            squares.Add(new Square(this.button15, Properties.Resources.VladimirTrajkovik, "VladimirTrajkovik"));
            squares.Add(new Square(this.button16, Properties.Resources.VladimirTrajkovik, "VladimirTrajkovik"));

            foreach (Square s in squares)
            {
                s.button.Click += new System.EventHandler(this.click);
            }
            opened = 0;
            #endregion
            newGame();
        }

        private void newGame()
        {
            foreach (Square s in squares)
            {
                s.isSelected = false;
                s.isGuessed = false;
                s.button.Visible = true;
            }


            //Random r = new Random();
            //for (int i = 0; i < 200; i++)
            //{
            //    int index = r.Next(16);
            //    Square temp = new Square();
            //    temp.button = squares[i % 16].button;
            //    squares[i % 16].button = squares[index].button;
            //    squares[index].button = temp.button;
            //}

            can = true;
            hits = 0;
            opened = 0;
            timeElapsed = 0;
            timer1.Start();
            updateTime();
            Invalidate();
        }

        private void click(object sender, EventArgs e)
        {
            if (can == false) return;
            Button tmp = sender as Button;
            foreach (Square s in squares)
            {
                if (s.button == tmp)
                {
                    if (opened == 0)
                    {
                        s.isSelected = true;
                        opened++;
                        Invalidate();
                    }
                    else if (opened == 1)
                    {
                        s.isSelected = true;
                        opened = 0;
                        can = false;
                        Invalidate();
                        validateGuess();
                    }
                }
            }
        }

        public void validateGuess()
        {
            Square s1 = new Square();
            Square s2 = new Square();
            bool tmp = true;
            foreach (Square s in squares)
            {
                if (s.isSelected)
                {
                    if (tmp)
                    { s1 = s; tmp = false; }
                    else { s2 = s; }
                }
            }
            if (s1.image.Tag == s2.image.Tag)
            {
                SoundPlayer sound = new SoundPlayer(Properties.Resources.beep_04);
                sound.Play();
                s1.isGuessed = true;
                s2.isGuessed = true;
                s1.isSelected = false;
                s2.isSelected = false;
                hits++;
                can = true;
               
            }
            else
            {
                first = s1;
                second = s2;
                Timer t1 = new Timer();
                t1.Interval = 1000;
                t1.Tick += new EventHandler(t1_Tick);
                t1.Start();
            }
        }
        Square first;
        Square second;

        private void t1_Tick(object sender, EventArgs e)
        {
            first.isSelected = false;
            second.isSelected = false;
            
            can = true;
            ((Timer)sender).Stop();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            timeElapsed++;
            updateTime();
            if (timeElapsed == TIME)
            {
                timer1.Stop();
                SoundPlayer sound = new SoundPlayer(Properties.Resources.aah_01);
                sound.Play();
                if (MessageBox.Show("Си за уште една партија?", "Тапа сиииииииииииии!!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    newGame();
                }
                else
                    Close();
            }
            Invalidate();
            validateWin();
        }

        private void updateTime()
        {
            int left = TIME - timeElapsed;
            int min = left / 60;
            int sec = left % 60;
            lblTimeLeft.Text = string.Format("{0:00}:{1:00}", min, sec);
        }

        private void новаИграToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Нова игра?", "Нова игра", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                newGame();
            }
        }

        private void крајToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Square s in squares)
            {
                if (s != null)
                {
                    if (s.isSelected || s.isGuessed)
                    {
                        s.open(e.Graphics);
                    }
                    else
                    {
                        s.close();
                    }
                }
            }
        }

      

        private void validateWin() {
            if (hits == (squares.Count) / 2)
            {
                timer1.Stop();
                SoundPlayer sound = new SoundPlayer(Properties.Resources.cheer_01);
                sound.Play();
                MessageBox.Show("Честитки!!! Добитник сте на 10ка по Визуелно Програмирање :)");
                if (MessageBox.Show("Нова игра?", "Нова игра", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    newGame();
                }
                else
                    Close();
            }

        }

     
    }
}
