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
    public partial class ComputerInfoForm : Form
    {
        public ComputerInfoForm(Computer computer)
        {
            InitializeComponent();
            lbl_ComputerName.Text = computer.name;
            tb_IP.Text = computer.ip;
            tb_Group.Text = computer.group;

           
            foreach(var software in computer.softwares)
            {
                lb_Softwares.Items.Add(software);
            }

            lb_Softwares.Items.Add("=========Cholatey Installations=========");

            foreach (var chocolate in computer.chocoSoftwares)
            {
                lb_Softwares.Items.Add(chocolate);
            }
           
        }

        private void btn_plus_Click(object sender, EventArgs e)
        {
            lbl_ComputerName.Text = tb_IP.Text;
        }
    }
}
