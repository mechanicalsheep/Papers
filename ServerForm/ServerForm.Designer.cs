namespace ServerForm
{
    partial class ServerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lb_output = new System.Windows.Forms.ListBox();
            this.btn_Scan = new System.Windows.Forms.Button();
            this.btn_send = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.tb_domain = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_command = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_username = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_getComputer = new System.Windows.Forms.Button();
            this.lv_computers = new System.Windows.Forms.ListView();
            this.button1 = new System.Windows.Forms.Button();
            this.cb_groups = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lb_output
            // 
            this.lb_output.BackColor = System.Drawing.Color.Black;
            this.lb_output.ForeColor = System.Drawing.Color.LimeGreen;
            this.lb_output.FormattingEnabled = true;
            this.lb_output.Location = new System.Drawing.Point(66, 319);
            this.lb_output.Name = "lb_output";
            this.lb_output.Size = new System.Drawing.Size(617, 186);
            this.lb_output.TabIndex = 0;
            // 
            // btn_Scan
            // 
            this.btn_Scan.Location = new System.Drawing.Point(331, 290);
            this.btn_Scan.Name = "btn_Scan";
            this.btn_Scan.Size = new System.Drawing.Size(75, 23);
            this.btn_Scan.TabIndex = 1;
            this.btn_Scan.Text = "Scan";
            this.btn_Scan.UseVisualStyleBackColor = true;
            this.btn_Scan.Click += new System.EventHandler(this.btn_Scan_Click);
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(542, 209);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(75, 23);
            this.btn_send.TabIndex = 3;
            this.btn_send.Text = "Send";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(472, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Password";
            // 
            // tb_password
            // 
            this.tb_password.Location = new System.Drawing.Point(531, 95);
            this.tb_password.Name = "tb_password";
            this.tb_password.PasswordChar = '*';
            this.tb_password.Size = new System.Drawing.Size(100, 20);
            this.tb_password.TabIndex = 5;
            // 
            // tb_domain
            // 
            this.tb_domain.Location = new System.Drawing.Point(531, 134);
            this.tb_domain.Name = "tb_domain";
            this.tb_domain.Size = new System.Drawing.Size(100, 20);
            this.tb_domain.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(477, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Domain";
            // 
            // tb_command
            // 
            this.tb_command.Location = new System.Drawing.Point(531, 169);
            this.tb_command.Name = "tb_command";
            this.tb_command.Size = new System.Drawing.Size(100, 20);
            this.tb_command.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(471, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Command";
            // 
            // tb_username
            // 
            this.tb_username.Location = new System.Drawing.Point(531, 58);
            this.tb_username.Name = "tb_username";
            this.tb_username.Size = new System.Drawing.Size(100, 20);
            this.tb_username.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(465, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Username";
            // 
            // btn_getComputer
            // 
            this.btn_getComputer.Location = new System.Drawing.Point(120, 290);
            this.btn_getComputer.Name = "btn_getComputer";
            this.btn_getComputer.Size = new System.Drawing.Size(146, 23);
            this.btn_getComputer.TabIndex = 11;
            this.btn_getComputer.Text = "Get Computer Snapshot";
            this.btn_getComputer.UseVisualStyleBackColor = true;
            this.btn_getComputer.Click += new System.EventHandler(this.btn_getComputer_Click);
            // 
            // lv_computers
            // 
            this.lv_computers.Location = new System.Drawing.Point(33, 58);
            this.lv_computers.Name = "lv_computers";
            this.lv_computers.Size = new System.Drawing.Size(400, 224);
            this.lv_computers.TabIndex = 12;
            this.lv_computers.UseCompatibleStateImageBehavior = false;
            this.lv_computers.View = System.Windows.Forms.View.Details;
            this.lv_computers.SelectedIndexChanged += new System.EventHandler(this.lv_computers_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(468, 280);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "OPENCOMPUTER";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cb_groups
            // 
            this.cb_groups.FormattingEnabled = true;
            this.cb_groups.Items.AddRange(new object[] {
            "All",
            "Domain",
            "BYOD",
            "Server",
            "Computer Labs"});
            this.cb_groups.Location = new System.Drawing.Point(120, 31);
            this.cb_groups.Name = "cb_groups";
            this.cb_groups.Size = new System.Drawing.Size(175, 21);
            this.cb_groups.TabIndex = 14;
            this.cb_groups.Text = "Filter Group";
            this.cb_groups.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 517);
            this.Controls.Add(this.cb_groups);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lv_computers);
            this.Controls.Add(this.btn_getComputer);
            this.Controls.Add(this.tb_username);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_command);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_domain);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_password);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.btn_Scan);
            this.Controls.Add(this.lb_output);
            this.Name = "ServerForm";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lb_output;
        private System.Windows.Forms.Button btn_Scan;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.TextBox tb_domain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_command;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_username;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_getComputer;
        private System.Windows.Forms.ListView lv_computers;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cb_groups;
    }
}

