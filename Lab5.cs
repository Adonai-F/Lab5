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
        // Create new filePath variable. 
        string filePath = String.Empty;

        public Lab5()
        {
            InitializeComponent();
        }

        #region Event Handlers
        /// <summary>
        /// Clears file (make new file).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileNewClick(object sender, EventArgs e)
        {
            // Confirm close.
            bool result = ConfirmClose();
            if (result)
            {
                filePath = string.Empty;
                textBoxEdit.Clear();
                UpdateTitle();
            }
        }

        /// <summary>
        /// Exit form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileExitClick(object sender, EventArgs e)
        {
            // Confirm close.
            bool result = ConfirmClose();
            if (result)
            {
                Close();
            }
        }

        /// <summary>
        /// Open an existing file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileOpenClick(object sender, EventArgs e)
        {
            // Create OpenFileDialog object.
            OpenFileDialog openFile = new OpenFileDialog();
            // Set open location to filePath.
            openFile.FileName = filePath;
            // Set allowed file types.
            openFile.Filter = "Text files (*.txt)|*.txt";
            // Set title.
            openFile.Title = "Open File: ";

            // Confirm close.
            bool result = ConfirmClose();
            if (result)
            {
                // If user clicks okay to open a file. 
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    // Set filename to open location.
                    filePath = openFile.FileName;
                    UpdateTitle();
                    // Create new fileStream object. Allow open and read privileges.
                    FileStream fileToOpen = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                    // Create streamReader object.
                    StreamReader reader = new StreamReader(fileToOpen);
                    // Read contents of text file.
                    textBoxEdit.Text = reader.ReadToEnd();

                    // Close reader box.
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// Save the elements in the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileSaveClick(object sender, EventArgs e)
        {
            // If it is a new file.
            if (filePath == String.Empty)
            {
                menuFileSaveAsClick(sender, e);
            }
            // Overwrite existing information.
            else
            {
                // Perform function call.
                SaveFile(filePath);
            }
        }

        /// <summary>
        /// Save dialouge box appears.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileSaveAsClick(object sender, EventArgs e)
        {
            // Perform function call.
            SaveAs(filePath);
        }

        /// <summary>
        /// Cut out the selected text and copy to clipboard.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuEditCutClick(object sender, EventArgs e)
        {
            // If textbox is not empty.
            if (textBoxEdit.Text != null)
            {
                // Grab selected text and remove it.
                Clipboard.SetText(textBoxEdit.SelectedText);
                textBoxEdit.SelectedText = "";
            }
        }

        /// <summary>
        /// Copy selected text.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuEditCopyClick(object sender, EventArgs e)
        {
            // If textbox is not empty.
            if (textBoxEdit.Text != null)
            {
                Clipboard.SetText(textBoxEdit.SelectedText);
            }
        }

        /// <summary>
        /// Paste the copied items.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuEditPasteClick(object sender, EventArgs e)
        {
            // Paste
            textBoxEdit.Paste(Clipboard.GetText());
        }

        /// <summary>
        /// Information about the program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuHelpAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Adonai's Text Editor.\n" + "Lab 5\n" + "NETD 2202");
        }

        #endregion



        #region Functions
        /// <summary>
        /// Save as function to give saveDialog box.
        /// </summary>
        /// <param name="path"></param>
        private void SaveAs(string path)
        { 
            // Check to see if file exists.
            if (!File.Exists(path))
            {
                // Create new saveFileDialog object.
                SaveFileDialog saveLocation = new SaveFileDialog();
                // Set the save location to the file path.
                saveLocation.FileName = path;
                // Assign save file types that user can choose from.
                saveLocation.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                // Set title.
                saveLocation.Title = "Save File: ";

                // If user clicks okay on the dialog box to save file.
                if (saveLocation.ShowDialog() == DialogResult.OK)
                {
                    // Set filepath to the filename save location and update title.
                    path = saveLocation.FileName;
                    UpdateTitle();

                    // Set access privileges to create and write (save). 
                    FileStream fileToAccess = new FileStream(path, FileMode.Create, FileAccess.Write);

                    // Create streamWriter object.
                    StreamWriter writer = new StreamWriter(fileToAccess);

                    // Add contents.
                    writer.WriteLine(textBoxEdit.Text);

                    // Close write box.
                    writer.Close();
                }
            }
        
            
        }
        /// <summary>
        /// Save file without dialoug box. Only if file already exists
        /// </summary>
        /// <param name="path"></param>
        public void SaveFile(string path)
        {
            // Set access privileges to create and write (save). 
            FileStream fileToAccess = new FileStream(path, FileMode.Create, FileAccess.Write);

            // Create streamWriter object.
            StreamWriter writer = new StreamWriter(fileToAccess);

            // Add contents.
            writer.WriteLine(textBoxEdit.Text);

            // Close write box.
            writer.Close();
        }

        /// <summary>
        /// Update the title of the text editor to reflect the new items.
        /// </summary>
        public void UpdateTitle()
        {
            this.Text = "Adonai's Notepad";
            if(filePath != String.Empty)
            {
                this.Text += " - " + filePath;
            }
        }

        /// <summary>
        /// Confirming that the user wants to leave their work. Bonus marks!
        /// </summary>
        /// <returns></returns>
        private bool ConfirmClose()
        {
            const string message = "Are you sure you that you are done with this file?";
            var results = MessageBox.Show(message, "", MessageBoxButtons.YesNo);
            bool yes;

            if(results == DialogResult.No)
            {
                yes = false;
                return yes;
            }
            else
            {
                yes = true;
                return yes;
            }
        }

        #endregion     
    }
}
