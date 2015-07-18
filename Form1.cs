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
            lblPath.Text = "";


        }

        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Text Documents (*.txt) |*.txt|All Files (*.*) |*.*";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var fileName = ofd.FileName;
                file_name = Path.GetDirectoryName(fileName) + "\\" + ofd.SafeFileName;
                lblPath.Text = file_name;
                System.IO.StreamReader sr = new System.IO.StreamReader(fileName);
                txtNotepad.Text = sr.ReadToEnd();
                sr.Close();
            }
        }

        private void mnuFileSaveAs_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Text Documents (*.txt) |*.txt|All Files (*.*) |*.*";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var filename = sfd.FileName;
                System.IO.StreamWriter sw = new System.IO.StreamWriter(filename);
                sw.WriteLine(txtNotepad.Text);
                sw.Close(); // close the file
            }
        }

        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Text Documents (*.txt) |*.txt|All Files (*.*) |*.*";
            if (file_name == "")
            {
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var filename = sfd.FileName;
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(filename);
                    sw.WriteLine(txtNotepad.Text);
                    sw.Close();
                }
            }
            else
            {
                lblPath.Text = file_name;
                System.IO.StreamWriter sw2 = new System.IO.StreamWriter(file_name);
                sw2.WriteLine(txtNotepad.Text);
                sw2.Close(); // comment
            }
        }

        private void mnuPageSetUp_Click(object sender, EventArgs e)
        {
          
           
        }

        private void mnuPrint_Click(object sender, EventArgs e)
        {
            var pdf = new PrintDialog();
            if (pdf.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure you want to quit?", "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

  

        private void mnuPaste_Click(object sender, EventArgs e)
        {
            txtNotepad.Paste();
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            txtNotepad.Text = "";
        }

       

        private void mnuFont_Click(object sender, EventArgs e)
        {
            var fd = new FontDialog();
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

        private void txtNotepad_TextChanged(object sender, EventArgs e)
        {
            //int numLines = txtNotepad.Text.Split('\n').Length;
            int numLines = txtNotepad.Lines.Length;//count line numbers
            int numCols = txtNotepad.Text.Count(); // Count number of columns
            tssStatus.Text = "Ln" + numLines + ",";
            tssStatus.Text += "  Col " + txtNotepad.Text.Count(); 
        }

        private void mnuEdit_Click(object sender, EventArgs e)
        {
            if (txtNotepad.SelectedText != "")
            {
                mnuCopy.Enabled = true;
                mnuCut.Enabled = true;
                mnuDelete.Enabled = true;
            }
        }

        private void mnuStatusBar_Click(object sender, EventArgs e)
        {
            if (mnuStatusBar.Checked)
            {
                tssStatus.Visible = false;
            }
            else
            {
                tssStatus.Visible = true;
            }
        }

        private void mnuViewHelp_Click(object sender, EventArgs e)
        {
            
        }

        private void mnuAboutNotepad_Click(object sender, EventArgs e)
        {
            AboutBox1 abtBox = new AboutBox1();
            abtBox.Show();
        }

        private void mnuSelectAll_Click_1(object sender, EventArgs e)
        {
            txtNotepad.SelectAll();
        }

        private void mnuDateTime_Click(object sender, EventArgs e)
        {
            txtNotepad.Text += DateTime.Now + " ";
        }

        private void mnuWordWrap_Click(object sender, EventArgs e)
        {
            if (mnuWordWrap.Checked)
            {
                txtNotepad.WordWrap = true;
            }
            else
            {
                txtNotepad.WordWrap = true; 
            }
            
        }

        private void mnFind_Click(object sender, EventArgs e)
        {
            // find here 
        }

       

     

    }

     

    
}
