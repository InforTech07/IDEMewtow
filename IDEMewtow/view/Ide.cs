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

        public static string Gnameproyect;
        public Ide(int id)
        {
            InitializeComponent();
            RequestProyectData(id);
            LoadKeyWords();
            LoadGrammar();

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
            Gnameproyect = name;

            string path = Environment.rootDir + name;
            treeView1.Nodes.Add(Helpers.GenerateTreeView(path));
            PrintLogProcess("-> Cargando proyecto: " + name);
        }

        private void MouseDownWindow()
        {
            MouseDownWindows.ReleaseCapture();
            MouseDownWindows.SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        private void LoadKeyWords()
        {
            KeyWord keywords = new KeyWord();
            keywords.CreateKeyWords();
            PrintLogProcess("-> Cargando palabras reservadas..!");
        }

        private void LoadGrammar()
        {
            Grammar loadgrammar = new Grammar();
            loadgrammar.CreateWordCsharp();
            PrintLogProcess("-> Cargando gramatica");
        }
        
        public void PrintLogProcess(string v_process)
        {
            ProcessLog.AddProcess(v_process);
            var dataprocess = ProcessLog.Getprocess();
            var lprocess = string.Empty;
            foreach(var p in dataprocess)
            {
                lprocess += p + "\r\n";
            }

            TbxLogProcess.Text = lprocess;
            TbxLogProcess.SelectionStart = TbxLogProcess.Text.Length;
            TbxLogProcess.ScrollToCaret();
            TbxLogProcess.Refresh();
        }


        private void PrintLogError()
        {
            var dataerror = ErrorLog.GetError();
            var lError = string.Empty;
            foreach(var e in dataerror)
            {
                lError += e + "\r\n";
            }
            TbxLogError.Text = lError;
            TbxLogError.SelectionStart = TbxLogError.Text.Length;
            TbxLogError.ScrollToCaret();
            TbxLogError.Refresh();
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
            PrintLogProcess("-> Abriendo archivo: " + NameFile);

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
                PrintLogProcess("-> Abriendo archivo: " + NameFile);
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
            {
                if (ctrl is TextBox)
                {
                    contenido = ctrl.Text;
                }

            }
                
            Helpers.DeleteFile(pathfile);
            Helpers.SaveFile(pathfile, contenido);
            tab.Refresh();
            PrintLogProcess("-> Guardando cambios de:" + namefile);

        }

        

        private void BtnLexico_Click(object sender, EventArgs e)
        {
            Components NewComp = new Components();
            var NTabPage = NewComp.CreateTab("Lexico");
            var NDataGridView = NewComp.CreateDataGridView();
            TokenMewtow MTok = new TokenMewtow();
            var DataTokens = MTok.GetTokens();
            if(DataTokens != null)
            {
                NDataGridView.DataSource = DataTokens;
                NTabPage.Controls.Add(NDataGridView);
                tabprimary.Controls.Add(NTabPage);
                tabprimary.SelectedTab = NTabPage;
                NDataGridView.Columns[0].HeaderText = "Token";
                NDataGridView.Columns[1].HeaderText = "Indice X";
                NDataGridView.Columns[2].HeaderText = "Indice Y";
                NDataGridView.Columns[3].HeaderText = "Clasificacion";
                NDataGridView.Columns[4].HeaderText = "Fase Lexico";
                NDataGridView.Columns[5].Visible = false;
                NDataGridView.Columns[6].Visible = false;
                NDataGridView.AutoResizeColumns();
                NDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                PrintLogProcess("-> Ejecutando Proceso lexico");

            }
            else
            {
                Helpers.MewtowMessage("No ha compilado. !Hagalo!", "Error");
                PrintLogProcess("-> Fallo en ejecucion lexico...");
                return;
            }
            
            
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.proyectmanager.Show();
        }

        private void BtnCompiler_Click(object sender, EventArgs e)
        {
            ClearDataGrid();
            ErrorLog.ResetErrorList();
            var tab = tabprimary.SelectedTab;
            var ContentFile = string.Empty;

            var namefile = tab.Text;
            string proy = Ltitleproyect.Text + @"\";
            string pathfile = Path.Combine(Environment.rootDir, proy, namefile);

            ContentFile = Helpers.ReadFile(pathfile);
            PrintLogProcess("-> Compilando archivo: " + namefile);
            TokenMewtow MewtowTok = new TokenMewtow();
            bool Result = MewtowTok.CompilerFile(ContentFile);
            if (Result)
            {
                var DataToken = MewtowTok.GetTokens();
                DataGridViewSymbol.DataSource = DataToken;
                DataGridViewSymbol.Columns[0].HeaderText = "Token";
                DataGridViewSymbol.Columns[1].HeaderText = "X";
                DataGridViewSymbol.Columns[2].HeaderText = "Y";
                DataGridViewSymbol.Columns[3].HeaderText = "Tipo";
                DataGridViewSymbol.Columns[4].Visible = false;
                DataGridViewSymbol.Columns[5].Visible = false;
                DataGridViewSymbol.Columns[6].Visible = false;
                DataGridViewSymbol.AutoResizeColumns();
                DataGridViewSymbol.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                PrintLogProcess("-> Imprimiendo Tabla Simbolo..");
            }

            PrintLogProcess("-> Fin de compilacion: Error(" + ErrorLog.ContError + ")");
            PrintLogError();
        }

        private void BtnSintactico_Click(object sender, EventArgs e)
        {
            Components NewCompp = new Components();
            var NTabPagee = NewCompp.CreateTab("Sintactico");
            TreeView treesintactic = new TreeView();

            treesintactic.Nodes.Add(TokenMewtow.GenerateTreeSyntactic());
            treesintactic.Dock = DockStyle.Fill;
            treesintactic.BackColor = Color.FromArgb(28, 30, 38);
            treesintactic.ForeColor = Color.White;

            NTabPagee.Controls.Add(treesintactic);
            tabprimary.Controls.Add(NTabPagee);
            tabprimary.SelectedTab = NTabPagee;
            PrintLogProcess("-> Ejecutantdo Fase Sintactico");
        }

        private void BtnSemantics_Click(object sender, EventArgs e)
        {
            Components NewCompps = new Components();
            var NTabPagees = NewCompps.CreateTab("Semantica");
            TreeView treesemantic = new TreeView();

            treesemantic.Nodes.Add(Grammar.GenerateTreeVar());
            treesemantic.Nodes.Add(Grammar.GenerateTreeMethod());
            treesemantic.Nodes.Add(Grammar.GenerateTreeClass());
            treesemantic.Dock = DockStyle.Fill;
            treesemantic.BackColor = Color.FromArgb(28, 30, 38);
            treesemantic.ForeColor = Color.White;

            NTabPagees.Controls.Add(treesemantic);
            tabprimary.Controls.Add(NTabPagees);
            tabprimary.SelectedTab = NTabPagees;
            PrintLogProcess("-> Ejecutantdo Fase semantico");
        }

        private void ClearDataGrid()
        {
            DataGridViewSymbol.DataSource = null;
        }

        

        private void  BtnNewFile_Click(object sender, EventArgs e)
        {
            
            TxNameFile.Visible = true;
            BtnNewFile.Text = "crear";
            if(TxNameFile.Text != "nombre")
            {
                CreateFile.NewFile(Ltitleproyect.Text, TxNameFile.Text);
                string pat = Environment.rootDir + Ltitleproyect.Text;
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(Helpers.GenerateTreeView(pat));
                PrintLogProcess("-> Se creo un nuevo archivo [" + TxNameFile.Text + "]");
                BtnNewFile.Text = "Nuevo Archivo";
                TxNameFile.Visible = false;
            }

            return;
        }

        private void TxNameFile_Enter(object sender, EventArgs e)
        {
            Helpers h = new Helpers();
            h.PlaceholderEnter(TxNameFile, "nombre");
        }

        private void TxNameFile_Leave(object sender, EventArgs e)
        {
            Helpers hh = new Helpers();
            hh.PlaceholderLeave(TxNameFile, "nombre");
        }

        private void BtnMinMaxWindows_Click(object sender, EventArgs e)
        {
            if (this.WindowState == 0)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void btnCodeCs_Click(object sender, EventArgs e)
        {
            PrintLogProcess("-> Inciando Traduccion a csharp");
            Components NewCompp = new Components();
            var NTabPagee = NewCompp.CreateTab("Codigo Csharp");
            var NTextBox = NewCompp.CreateTextBox();
            Grammar.clearCodeSharp();
            NTextBox.Text = "// Programa Escrito En Mewtow: Traducido a Csharp" + "\r\n";
            var MewtowSentence = TokenMewtow.GetListSentence();
            foreach (var Msent in MewtowSentence)
            {
                NTextBox.Text += Grammar.TranslatorToCsharp(Msent[2]) + "\r\n";
            }

            NTabPagee.Controls.Add(NTextBox);
            tabprimary.Controls.Add(NTabPagee);
            tabprimary.SelectedTab = NTabPagee;
            PrintLogProcess("-> Codigo Csharp Generado");

        }
    }

    

}

