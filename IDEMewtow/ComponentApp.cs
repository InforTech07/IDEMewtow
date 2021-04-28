using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDEMewtow
{
    public partial class ComponentApp : Component
    {
        public ComponentApp()
        {
            InitializeComponent();
        }

        public ComponentApp(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
