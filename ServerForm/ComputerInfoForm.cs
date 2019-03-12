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
            lbl_snapshotDate.Text = computer.dateTime;
            lbl_ip.Text = computer.ip;
            lbl_Group.Text = computer.group;
            lbl_OS.Text = computer.OS;
            lbl_Model.Text = computer.model;
            lbl_RAM.Text = computer.ram;


           
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
           // lbl_ComputerName.Text = tb_IP.Text;
        }
    }
}
