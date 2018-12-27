using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Blockade_Defense
{
    public partial class Name : Form
    {
        // attributes
        string name = "";
        public FormStartPosition startPos;
        public Name()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //name = "";
            name = textBox1.Text;
            if (name != "")
            {
                textBox1.Clear();
                this.Close();
            }
        }

        public string GetName()
        {
            if (name.Length > 8)
            {
                name = name.Substring(0, 8);
            }
            
            return name;
        }

    }
}
