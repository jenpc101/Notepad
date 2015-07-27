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
using System.Drawing.Printing;
using System.Text.RegularExpressions;


namespace createsamplenotepad
{
    public partial class frmNotepad : Form
    {
        string file_name = "";

        public frmNotepad()
        {
            InitializeComponent();
          
        }


        private void mnuFileNew_Click(object sender, EventArgs e)
        {
            txtNotepad.Text = "";
            file_name = "";
            this.Text = "Notepad";            
        }

        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Text Documents (*.txt) |*.txt|All Files (*.*) |*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                file_name = ofd.FileName; 
                Text = Path.GetFileName(file_name);
                StreamReader sr = new StreamReader(file_name);
                txtNotepad.Text = sr.ReadToEnd();
                sr.Close();
            }
        }

        private void mnuFileSaveAs_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        public void mnuFileSave_Click(object sender, EventArgs e)
        {
           
            if (file_name == "")
            {
                saveFile();
            }
            else
            {
                Text = Path.GetFileName(file_name);
                streamWriter(file_name);
            }
        }

        private void mnuFilePageSetUp_Click(object sender, EventArgs e)
        {
          
           
        }
        
        private void mnuFilePrint_Click(object sender, EventArgs e)
        {
            Font printFont = new Font("Arial", 12);
            StreamReader Printfile = new StreamReader(file_name);
            
                try
                {
                    PrintDocument docToPrint = new PrintDocument();
                    docToPrint.DocumentName = "Password"; 
                    docToPrint.PrintPage += (s, ev) =>
                    {
                        float linesPerPage = 0;
                        float yPos = 0;
                        int count = 0;
                        float leftMargin = ev.MarginBounds.Left;
                        float topMargin = ev.MarginBounds.Top;
                        string line = null;

                        linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics);

                        while (count < linesPerPage && ((line = Printfile.ReadLine()) != null))
                        {
                            yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                            ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                            count++;
                        }

                        if (line != null)
                            ev.HasMorePages = true;
                        else
                            ev.HasMorePages = false;
                    };
                    docToPrint.Print();
                }
                catch (System.Exception f)
                {
                    MessageBox.Show(f.Message);
                }

            
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {    //Change the okcancel to yesno

            if (file_name != "" || txtNotepad.Text == "")
            {
                Application.Exit();
            }

            else
            {
                 DialogResult dr = MessageBox.Show("Do you want to save change to unname file?", "Exit", MessageBoxButtons.YesNoCancel ,MessageBoxIcon.Question); 
                if (dr == DialogResult.Yes)
                {
                    saveFile();
                    Application.Exit();
                }
                else if (dr == DialogResult.No)
                {
                    Application.Exit();
                }
                else if (dr == DialogResult.Cancel)
                {
                    return;
                }
                
               
            }
                        
        }

        private void mnuEditPaste_Click(object sender, EventArgs e)
        {
            txtNotepad.Paste();
           // txtNotepad.SelectedText = Clipboard.GetText();
        }

        private void mnuEditDelete_Click(object sender, EventArgs e)
        {
            txtNotepad.Text = "";
        }

        private void mnuFormatFont_Click(object sender, EventArgs e)
        {
            var fd = new FontDialog();
            fd.Font  = txtNotepad.Font;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                txtNotepad.Font = fd.Font;
            }
        }


        private void mnuCopy_Click(object sender, EventArgs e)
        {
             txtNotepad.Copy();   
        }

        private void mnuCut_Click(object sender, EventArgs e)
        {
            txtNotepad.Cut();

        }

        private void mnuSelectAll_Click(object sender, EventArgs e)
        {
            txtNotepad.SelectAll();
        }

        private void mnuUndo_Click(object sender, EventArgs e)
        {
            txtNotepad.Undo();
        }

        private void mnuEdit_Click(object sender, EventArgs e)
        {
            //mnuCopy.Enabled = mnuDelete.Enabled = mnuCut.Enabled = (txtNotepad.SelectedText != "");
            var b = txtNotepad.SelectedText != "";
            mnuCopy.Enabled = b;
            mnuDelete.Enabled = b;
            mnuCut.Enabled = b;
        }


        private void mnuAboutNotepad_Click(object sender, EventArgs e)
        {
            AboutBox1 abtBox = new AboutBox1();
            abtBox.Show();
        }

        private void mnuDateTime_Click(object sender, EventArgs e)
        {
            string dateTime = DateTime.Now.ToString();
            //int lenDateTime = dateTime.Length;

            //var selectionIndex = txtNotepad.SelectionStart;
            //txtNotepad.Text = txtNotepad.Text.Insert(selectionIndex, DateTime.Now.ToString());
            //txtNotepad.SelectionStart = selectionIndex + lenDateTime;
            txtNotepad.SelectedText = dateTime;
        }   

        private void mnuWordWrap_Click(object sender, EventArgs e)
        {
            txtNotepad.WordWrap = mnuWordWrap.Checked;
        }

        private void mnFind_Click(object sender, EventArgs e)
        {
            // find here 
        }

        private void saveFile()
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Text Documents (*.txt) |*.txt|All Files (*.*) |*.*";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var filename = sfd.FileName;
                streamWriter(filename);
            }
        }

        private void streamWriter(string fileName)
        {
            StreamWriter sw = new StreamWriter(fileName);
            sw.WriteLine(txtNotepad.Text);
            sw.Close();
        }

        private void txtNotepad_TextChanged(object sender, EventArgs e)
        {
            int numLines = txtNotepad.Lines.Length;//count line numbers
            int numCols = txtNotepad.Text.Count(); // Count number of columns
            tssStatusLabel.Text = "Ln " + numLines + ",";
            tssStatusLabel.Text += " Col " + txtNotepad.Text.Count(); 

        }

        private void mnuViewStatusBar_Click(object sender, EventArgs e)
        {
            ssStatusStrip.Visible = mnuViewStatusBar.Checked;  
        }

    }   
}
