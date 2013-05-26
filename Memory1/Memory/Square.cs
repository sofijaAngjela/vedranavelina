using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;


namespace Memory
{
    public class Square
    {
        public bool isSelected { get; set; }
        public bool isGuessed { get; set; }
        public Button button { get; set; }
        public Image image { get; set; }

        public Square() { }
        public Square(Button b1, Image i, string tag)
        {
            isSelected = false;
            isGuessed = false;
            button = b1;
            image = i;
            image.Tag = tag;
            button.BackgroundImage = Properties.Resources.conce;
        }

        public void open(Graphics g)
        {
            button.Visible = false;
            g.DrawImage(this.image, button.Location);
        }

        public void close() {
            button.Visible = true;
        }
    }
}
