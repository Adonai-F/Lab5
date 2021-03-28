using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab_5
{
    public partial class Lab5 : Form
    {
        public Lab5()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxEdit.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create new variable. 
            string filePath = String.Empty;
            // Create OpenFileDialog object.
            OpenFileDialog openFile = new OpenFileDialog();
            // Set open location to filePath.
            openFile.FileName = filePath;
            // Set allowed file types.
            openFile.Filter = "Text files (*.txt)|*.txt";

            // If user clicks okay to open a file. 
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                // Set filename to open location.
                filePath = openFile.FileName;
                // Create new fileStream object. Allow open and read privileges.
                FileStream fileToOpen = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                // Create streamReader object.
                StreamReader reader = new StreamReader(fileToOpen);
                // Read contents of text file.
                reader.ReadToEnd();
                // Close reader box.
                reader.Close();
            }

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = ".txt";
            Saves(filePath);
        }



        #region Functions

        private void Saves(string filePath) 
        {
            // Set default text in save box to .txt file.
            string path = filePath;
            // Create new saveFileDialog object.
            SaveFileDialog saveLocation = new SaveFileDialog();
            // Set the save location to the file path.
            saveLocation.FileName = path;
            // Assign save file types that user can choose from.
            saveLocation.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            // If user clicks okay on the dialog box to save file.
            if (saveLocation.ShowDialog() == DialogResult.OK)
            {
                // Set filepath to the filename save location.
                path = saveLocation.FileName;

                // Set access privileges to create and write (save). 
                FileStream fileToAccess = new FileStream(path, FileMode.Create, FileAccess.Write);

                // Create streamWriter object.
                StreamWriter writer = new StreamWriter(fileToAccess);
                // Close the write box
                writer.Close();
            }
        }



        #endregion
    }
}
