extern alias newt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerForm
{
    public partial class ServerForm : Form
    {
        public ServerForm()
        {
            InitializeComponent();
            ServerNet serverNet = new ServerNet(this);
        }

        public void writeline(string message)
        {
            lb_output.Items.Add(message);
        }
    }
}
