using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManifestCreatorC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Clear();
                textBox2.Clear();
                string path = folderBrowserDialog1.SelectedPath;
                textBox1.Text = path;
                string directorymainname = Path.GetFullPath(folderBrowserDialog1.SelectedPath);
                var directorylength = directorymainname.Length;
                string[] allfiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
                int nbofrun = 0;
                foreach (var file in allfiles)
                {

                    FileInfo info = new FileInfo(file);


                    //DateTime modification = File.GetLastWriteTime(info.FullName);
                    //CultureInfo culture = new CultureInfo("en-US");

                    //DateTime modification = File.GetLastWriteTime(@"" + info.FullName);

                    
                    // Do something with the Folder or just add them to a list via nameoflist.add();
                    
                    var removebeforeroot = info.FullName;

                    removebeforeroot = removebeforeroot.Substring((directorylength+1), removebeforeroot.Length - (directorylength+1));
                    //textBox2.AppendText(removebeforeroot + " | " +modification.ToString("d",culture) + " | "+info.Length+Environment.NewLine);
                    if (checkBox1.Checked == true)
                    {
                        if (nbofrun < allfiles.Length)
                        {
                            textBox2.AppendText(removebeforeroot +",0"+Environment.NewLine);
                        }
                        else
                        {
                            textBox2.AppendText(removebeforeroot + ",0" + info.Name);
                        }
                        nbofrun += 1;

                    }
                    else
                    {
                        if (nbofrun < allfiles.Length)
                        {
                            textBox2.AppendText(removebeforeroot + ",0," + info.Length+Environment.NewLine);
                        }
                        else
                        {
                            textBox2.AppendText(removebeforeroot + ",0," + info.Length);
                        }
                        nbofrun += 1;
                    }
                  
                    


                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string directorymainname = Path.GetFileName(folderBrowserDialog1.SelectedPath);
            saveFileDialog1.Filter = "txt files (*.txt*)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.Title = "save the manifest";
            saveFileDialog1.FileName = directorymainname+" -- Manifest.txt";
            if (saveFileDialog1.ShowDialog()== DialogResult.OK)
            {
                string path = saveFileDialog1.FileName;
                StreamWriter sw = new StreamWriter(File.Create(path));
                sw.Write(textBox2.Text);
                sw.Dispose();
            }
        }
    }
}
