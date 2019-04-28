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
        bool toggle = false;
        Computer computer;
        ServerForm form;
        ServerDataHandler data;
        public ComputerInfoForm(ServerForm form, Computer computer)
        {
            data = new ServerDataHandler();
            InitializeComponent();
            this.form = form;
            this.computer = computer;
            lbl_ComputerName.Text = computer.name;

            lbl_snapshotDate.Text = computer.dateTime;
            lbl_ip.Text = computer.ip;
            tb_group.Text = computer.group;
            lbl_OS.Text = computer.OS;
            lbl_Model.Text = computer.model;
            lbl_RAM.Text = computer.ram;
            lbl_username.Text = computer.username;


            Console.WriteLine("computer.port= " + computer.port);
            Console.WriteLine("computer.group= " + computer.group);
            foreach(var software in computer.softwares)
            {
                lb_Softwares.Items.Add(software);
            }

            if (computer.chocoSoftwares != null)
            {

                lb_Softwares.Items.Add("=========Cholatey Installations=========");

                foreach (var chocolate in computer.chocoSoftwares)
                {
                    lb_Softwares.Items.Add(chocolate);
                }

            }
           
        }

        private void btn_plus_Click(object sender, EventArgs e)
        {
           // lbl_ComputerName.Text = tb_IP.Text;

        }

        private void btn_EditGroup_Click(object sender, EventArgs e)
        {
            toggle = !toggle;
            tb_group.Enabled = toggle;
            if (toggle)
                btn_EditGroup.Text = "Save";

            else
               if (btn_EditGroup.Text == "Save")
            {
                computer.group = tb_group.Text;
                data.SaveObjectData(computer,computer.uniqueKey,"Computers");
               form.serverNet.setGroup(computer.ip,Convert.ToInt32(computer.port),tb_group.Text);
                btn_EditGroup.Text = "Edit Group";

            }
            //Console.WriteLine("toggle is: "+toggle);
        }
    }
}
