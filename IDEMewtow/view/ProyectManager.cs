using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Data.SQLite;

namespace IDEMewtow
{
    public partial class ProyectManager : Form
    {
        private const string DirDb = Environment.DirDB;
        private bool StatusDB;
        private bool StatusKeyWords;
        private bool StatusGramatica;
        public ProyectManager()
        {
          //  Thread splash = new Thread(new ThreadStart(SplashScreenShow));
          //  splash.Start();
          //  Thread.Sleep(8000);
            InitializeComponent();
          //  splash.Abort();

        }

        private void SplashScreenShow()
        {
            Application.Run(new SplashScreen());      
        }


        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            Psettings.Visible = false;
            bool sta = StatusComponents();
            if (sta==true)
            {
                Console.WriteLine("Listo..");
                TxbNameProyect.Visible = true;
                TxtBSolution.Visible = true;
                BtnCreateProyect.Visible = true;
                LbNew.Visible = true;
                Lbproyects.Visible = true;
                LoadProyects();
            } 
            else
            {
                Console.WriteLine(sta);
                Console.WriteLine("Faltan componentes...!!!");
                LStatus.Visible = true;
                LStatus.ForeColor = Color.FromArgb(245, 83, 133);
                LStatus.Text = "😪 Faltan componenetes...!!";
                BtnCreateProyect.Visible = false;
                TxbNameProyect.Visible = false;
                TxtBSolution.Visible = false;
                LbNew.Visible = false;
                Lbproyects.Visible = false;
            }

        }

        private void StatusMewtowDB()
        {
            StatusDB = File.Exists(DirDb);
            if (StatusDB)
            {
                BtnDataBase.ForeColor = Color.FromArgb(0, 122, 204);
                LDataBase.Text = "😄 Habilitado..!!";
                LDataBase.ForeColor = Color.FromArgb(0, 122, 204);
            }
            else
            {
                BtnDataBase.ForeColor = Color.FromArgb(245, 83, 133);
                LDataBase.ForeColor = Color.FromArgb(245, 83, 133);
                LDataBase.Text = "😪 Faltan componenetes...!!";
            }
        }

        private void StatusKeyWord()
        {
            int Result = RequestDB.GetCountKeyWord();
            
            if (Result > 0)
            {

                LKeyWords.Text = "😄 Listo..!!";
                LKeyWords.ForeColor = Color.FromArgb(0, 122, 204);
                BtnKeyWords.ForeColor = Color.FromArgb(0, 122, 204);
                BtnKeyWords.FlatAppearance.BorderColor = Color.FromArgb(0, 122, 204);
                StatusKeyWords = true;
            }
            else
            {
                LKeyWords.Text = "😪 Falta ...!!";
                LKeyWords.ForeColor = Color.FromArgb(245, 83, 133);
                BtnKeyWords.ForeColor = Color.FromArgb(245, 83, 133);
                BtnKeyWords.FlatAppearance.BorderColor = Color.FromArgb(245, 83, 133);
            }

        }

        private void StatusGrammar()
        {
            int Result = RequestDB.GetCountGrammar();

            if (Result > 0)
            {
                
                LGramatica.Text = "😄 Listo..!!";
                LGramatica.ForeColor = Color.FromArgb(0, 122, 204);
                BtnGramatica.ForeColor = Color.FromArgb(0, 122, 204);
                BtnGramatica.FlatAppearance.BorderColor = Color.FromArgb(0, 122, 204);
                StatusGramatica = true;
            }
            else
            {
                LGramatica.Text = "😪 Falta ...!!";
                LGramatica.ForeColor = Color.FromArgb(245, 83, 133);
                BtnGramatica.ForeColor = Color.FromArgb(245, 83, 133);
                BtnGramatica.FlatAppearance.BorderColor = Color.FromArgb(245, 83, 133);
            }

        }
        private void LoadProyects()
        {
            
                dataGridView1.DataSource = RequestDB.GetProyects();

        }

        
        
        
        
        private bool StatusComponents()
        {
            StatusMewtowDB();
            StatusKeyWord();
            StatusGrammar();
            bool Status = false;
            if(StatusDB && StatusKeyWords && StatusGramatica)
            {
                Status = true;
              //  Console.WriteLine("Listo..");
              //  TxbNameProyect.Visible = true;
              //  TxtBSolution.Visible = true;
              //  BtnCreateProyect.Visible = true;
              //  LbNew.Visible = true;
              //  Lbproyects.Visible = true;
              //  Psettings.Visible = false;
              //  LStatus.Visible = true;
              //  LStatus.ForeColor = Color.FromArgb(0, 122, 204);
              //  LStatus.Text = "😄 Empecemos..!!";
            }

            Console.WriteLine(StatusDB);
            Console.WriteLine(StatusKeyWords);
            Console.WriteLine(StatusGramatica);
            return Status;
        }
        

        private void PlaceholderE(TextBox btn,string text)
        {
            Helpers e = new Helpers();
            e.PlaceholderEnter(btn, text);

        }
        private void PlaceholderL(TextBox btn, string text)
        {
            Helpers l = new Helpers();
            l.PlaceholderLeave(btn, text);

        }

      

        private void TxbNameProyect_Enter(object sender, EventArgs e)
        {
            PlaceholderE(TxbNameProyect, "Nombre");
            
        }

        private void TxbNameProyect_Leave(object sender, EventArgs e)
        {
            PlaceholderL(TxbNameProyect, "Nombre");
            
        }

        private void TxtBSolution_Enter(object sender, EventArgs e)
        {
            PlaceholderE(TxtBSolution, "Solucion");
        }

        private void TxtBSolution_Leave(object sender, EventArgs e)
        {
            PlaceholderL(TxtBSolution, "Solucion");
        }
        private void MouseDownWindow()
        {
            MouseDownWindows.ReleaseCapture();
            MouseDownWindows.SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void ProyectManager_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDownWindow();
            
        }

        private void BtnCreateProyect_Click(object sender, EventArgs e)
        {
             string ProyectName = TxbNameProyect.Text;
             string SolutionName = TxtBSolution.Text;
             RequestDB.CreateNewProyect(ProyectName,SolutionName);
             CreateFile.NewFolder(ProyectName);
             CreateFile.NewFile(ProyectName,SolutionName);
             int lastId = RequestDB.GetLastId();
             Ide ide = new Ide(lastId);
             ide.Show();
             this.Hide();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string celid = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            int id = Convert.ToInt32(celid);
            Ide idepro = new Ide(id);
            idepro.Show();
            this.Hide();
        }

        

        

        private void BtnSettingClose_Click(object sender, EventArgs e)
        {
            Psettings.Visible = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void BtnDataBase_Click(object sender, EventArgs e)
        {
            
            if (StatusDB)
            {
                Helpers.MewtowMessage("😄 BD Habilitado!","OK");  
            }
            else
            {
                DBMewtow.CreateDataBase();
                StatusMewtowDB();
                StatusDB = true;
            }
        }

        private void BtnKeyWords_Click(object sender, EventArgs e)
        {
            if (StatusKeyWords)
            {
                Helpers.MewtowMessage("😄 Ya hay palabras reservadas..!", "OK");
            }
            else
            {
                Console.WriteLine("presionado para crear keywords");
                bool result = KeyWord.LoadKeyword();
                StatusKeyWords = result;
                StatusKeyWord();
            }
            
        }

        private void BtnGramatica_Click(object sender, EventArgs e)
        {
            if (StatusGramatica)
            {
                Helpers.MewtowMessage("😄 Ya hay una gramatica..!!", "OK");
            }
            else
            {
                Grammar g = new Grammar();
                bool result = g.LoadGrammar();
                 StatusGramatica = result;
                 StatusGrammar();
            }

        }

        private void BtnSettingClose_Click_1(object sender, EventArgs e)
        {
            if (StatusDB && StatusKeyWords && StatusGramatica)
            {
                StatusComponents();
                Console.WriteLine("Listo..");
                TxbNameProyect.Visible = true;
                TxtBSolution.Visible = true;
                BtnCreateProyect.Visible = true;
                LbNew.Visible = true;
                Lbproyects.Visible = true;
                Psettings.Visible = false;
                LStatus.Visible = true;
                LStatus.ForeColor = Color.FromArgb(0, 122, 204);
                LStatus.Text = "😄 Empecemos..!!";
                LoadProyects();
            }
            else
            {
                Helpers.MewtowMessage("😪 Falta componenetes...!!", "Error");
            }
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            Psettings.Visible = true;
        }

        private void BtnDeleteKeyWord_Click(object sender, EventArgs e)
        {
            RequestDB.DeleteKeyWord();
            StatusKeyWords = false;
            StatusKeyWord();
        }

        private void BtnDeleteGrammar_Click(object sender, EventArgs e)
        {
            RequestDB.DeleteGrammar();
            StatusGramatica = false;
            StatusGrammar();
        }
    }
}
