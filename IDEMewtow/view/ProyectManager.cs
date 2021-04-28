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
               // BtnCreateDB.Visible = false;
                BtnCreateProyect.Visible = true;
                LoadProyects();


            } 
            else
            {
                Console.WriteLine(sta);
                Console.WriteLine("No existe la base datos..!");
                LStatus.Visible = true;
                LStatus.ForeColor = Color.FromArgb(245, 83, 133);
                LStatus.Text = "😪 Falta componenetes...!!";
                BtnCreateProyect.Visible = false; 
            }

        }

        private void LoadProyects()
        {
            
                dataGridView1.DataSource = RequestDB.GetProyects();

        }

        private bool StatusComponents()
        {
            bool Status = false;
            if(StatusDB && StatusKeyWords && StatusGramatica)
            {
                Status = true;
            }
      

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
             RequestDB.CreateNewProyect(ProyectName);
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
            bool ResultStatusDB = File.Exists(DirDb);
            if (ResultStatusDB==true)
            {
                LDataBase.Text = "😄 Listo..!!";
                LDataBase.ForeColor = Color.FromArgb(0, 122, 204);
                StatusDB = true;
            }
            else
            {
                DBMewtow.CreateDataBase();
                LDataBase.Text = "😄 Listo..!!";
                LDataBase.ForeColor = Color.FromArgb(0, 122, 204);
                StatusDB = true;
            }
        }

        private void BtnKeyWords_Click(object sender, EventArgs e)
        {
            bool ResultStatusKeyWords = true;
            if (ResultStatusKeyWords == true)
            {
                LKeyWords.Text = "😄 Listo..!!";
                LKeyWords.ForeColor = Color.FromArgb(0, 122, 204);
                StatusKeyWords = true;
            }
            else
            {
               // DBMewtow.CreateDataBase();
                LKeyWords.Text = "😄 Listo..!!";
                LKeyWords.ForeColor = Color.FromArgb(0, 122, 204);
                StatusKeyWords = true;
            }

        }

        private void BtnGramatica_Click(object sender, EventArgs e)
        {
            bool ResultStatusGramatica = true;
            if (ResultStatusGramatica == true)
            {
                LGramatica.Text = "😄 Listo..!!";
                LGramatica.ForeColor = Color.FromArgb(0, 122, 204);
                StatusGramatica = true;
            }
            else
            {
                // DBMewtow.CreateDataBase();
                LGramatica.Text = "😄 Listo..!!";
                LGramatica.ForeColor = Color.FromArgb(0, 122, 204);
                StatusGramatica = true;
            }
        }

        private void BtnSettingClose_Click_1(object sender, EventArgs e)
        {
            Psettings.Visible = false;
            Console.WriteLine("listo...!");
            LStatus.Visible = true;
            LStatus.ForeColor = Color.FromArgb(0, 122, 204);
            LStatus.Text = "😄 Empecemos..!!";
            BtnCreateProyect.Visible = true;
            LoadProyects();
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            Psettings.Visible = true;
        }
    }
}
