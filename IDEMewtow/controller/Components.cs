using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace IDEMewtow
{
    public class Components
    {
       
        public Components()
        {
           

        }
       
        public TabPage CreateTab(string title)
        {
            Button btn = new Button();
            TabPage newTab = new TabPage(title);
            newTab.BackColor = Color.FromArgb(28, 30, 38);
            return newTab;
        }

        public TextBox CreateTextBox()
        {
            TextBox tb = new TextBox();
            tb.Multiline = true;
            tb.Dock = DockStyle.Fill;
            tb.BackColor = Color.FromArgb(28, 30,38);
            tb.ForeColor = Color.White;
            return tb;
        }

        public DataGridView CreateDataGridView()
        {
            DataGridView dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            return dgv;
        }

    }
}
