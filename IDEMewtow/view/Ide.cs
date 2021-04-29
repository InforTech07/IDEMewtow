using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace IDEMewtow
{
    public partial class Ide : Form
    {
        public Ide(int id)
        {
            InitializeComponent();
            RequestProyectData(id);

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void RequestProyectData(int idpro)
        {
            var data = RequestDB.GetProyectId(idpro);
            var name = Convert.ToString(data.Tables[0].Rows[0][0]);
            Ltitleproyect.Text = name;

            string path = Environment.rootDir + name;
            treeView1.Nodes.Add(Helpers.GenerateTreeView(path));
        }

        private void MouseDownWindow()
        {
            MouseDownWindows.ReleaseCapture();
            MouseDownWindows.SendMessage(this.Handle, 0x112, 0xf012, 0);
        }



        private void Ide_Load(object sender, EventArgs e)
        {


        }



        private void btnopen_Click(object sender, EventArgs e)
        {


            string PathFile = Helpers.OpenFile();
            var Content = Helpers.ReadFile(PathFile);
            string NameFile = Path.GetFileName(PathFile);
            Components NewComponent = new Components();
            var NewTabPage = NewComponent.CreateTab(NameFile);
            var NewTextBox = NewComponent.CreateTextBox();
            NewTextBox.Text = Content;
            NewTabPage.Controls.Add(NewTextBox);
            tabprimary.Controls.Add(NewTabPage);
            tabprimary.SelectedTab = NewTabPage;

        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {

                var PathNodo = treeView1.SelectedNode.FullPath;
                var path = Path.Combine(Environment.rootDir, PathNodo);
                var NameFile = treeView1.SelectedNode.Text;
                var FileContent = Helpers.ReadFile(path);
                Components NewComponent = new Components();
                var NewTabpage = NewComponent.CreateTab(NameFile);
                var NewTextBox = NewComponent.CreateTextBox();
                NewTextBox.Text = FileContent;
                NewTabpage.Controls.Add(NewTextBox);
                tabprimary.Controls.Add(NewTabpage);
                tabprimary.SelectedTab = NewTabpage;
            }
            // If the file is not found, handle the exception and inform the user.
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("File not found.");
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDownWindow();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var tab = tabprimary.SelectedTab;
            var contenido = string.Empty;
            var namefile = tab.Text;
            string proy = Ltitleproyect.Text + @"\";
            string pathfile = Path.Combine(Environment.rootDir, proy, namefile);


            foreach (Control ctrl in tab.Controls)
                if (ctrl is TextBox)
                {
                    contenido = ctrl.Text;
                }

            //  Console.WriteLine(namefile);
            //  Console.WriteLine(contenido);
            Helpers.DeleteFile(pathfile);
            Helpers.SaveFile(pathfile, contenido);
            tab.Refresh();
            //  Console.WriteLine(index);

        }

        

        private void BtnLexico_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            var tab = tabprimary.SelectedTab;
            var contenido=string.Empty;

            var namefile = tab.Text;
            string proy = Ltitleproyect.Text + @"\";
            string pathfile = Path.Combine(Environment.rootDir, proy, namefile);

            contenido = Helpers.ReadFile(pathfile);
            var data = TokenGenerator.CreateTokens(contenido);
            dataGridView1.DataSource = data;

        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.proyectmanager.Show();
        }
    }

    

}

