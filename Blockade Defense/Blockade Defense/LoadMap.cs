using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Blockade_Defense
{
    public partial class LoadMap : Form
    {
        public string newMapFIleName;
        //public Map m1 = new Map();
        public string savedMapFile;
        bool isRunning = true;
        List<string> listOfMaps = new List<string>();

        public LoadMap()
        {
            InitializeComponent();
            CenterToScreen();

            string path = Directory.GetCurrentDirectory();
            string[] starray = Directory.GetFiles(path);

            string[] fileArray = new string[100];
            newMapFIleName = "";
            savedMapFile = "";
            int counter = 0;

            // this is for formatting the names of each map file
            foreach (string words in starray)
            {
                if (words.Contains("map") == true)
                {
                    fileArray[counter] = words;
                    string[] strArray = words.Split('\\');
                    textBox1.Text += (strArray[strArray.Length - 1]) + Environment.NewLine;
                    listOfMaps.Add(strArray[strArray.Length - 1]);
                    counter++;
                }
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            

        }

        // Load saved map
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                int i = 0;
                while ( i < listOfMaps.Count)
                {
                    if (textBox3.Text == listOfMaps[i])
                    {
                        savedMapFile = textBox3.Text;
                        isRunning = false;
                        this.Close();
                        return;
                    }
                    i++;              
                }
                textBox3.Text = "";
                MessageBox.Show("Must enter an existing map name to access a saved map");
            }
            else
            {
                MessageBox.Show("Must enter a name to access a saved map");
            }
        }

        // create new map
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                newMapFIleName = "map" + textBox2.Text + ".txt";
                isRunning = false;
                this.Close();
            }
            else
            {
                MessageBox.Show("Must enter a name for the new map");
            }
        }

        public string GetNewMap()
        {
            return newMapFIleName;
        }
        public string GetSavedMap()
        {
            return savedMapFile;
        }

        public void ShowErrorMessage()
        {
            MessageBox.Show("File was not found, try again");
        }

        public bool MapSelected()
        {
            if (newMapFIleName == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckIfRunning()
        {
            return isRunning;
        }


    }
}
