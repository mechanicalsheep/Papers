namespace Client
{
    partial class ClientForm
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
            this.btn_send = new System.Windows.Forms.Button();
            this.tb_IP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_Port = new System.Windows.Forms.TextBox();
            this.btn_shutdown = new System.Windows.Forms.Button();
            this.tb_installer_path = new System.Windows.Forms.TextBox();
            this.btn_Run = new System.Windows.Forms.Button();
            this.btn_browse = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_username = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_domain = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_command = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lb_output
            // 
            this.lb_output.FormattingEnabled = true;
            this.lb_output.Location = new System.Drawing.Point(21, 12);
            this.lb_output.Name = "lb_output";
            this.lb_output.Size = new System.Drawing.Size(680, 108);
            this.lb_output.TabIndex = 0;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(105, 225);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(75, 23);
            this.btn_send.TabIndex = 3;
            this.btn_send.Text = "Send";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // tb_IP
            // 
            this.tb_IP.Location = new System.Drawing.Point(92, 139);
            this.tb_IP.Name = "tb_IP";
            this.tb_IP.Size = new System.Drawing.Size(100, 20);
            this.tb_IP.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Port";
            // 
            // tb_Port
            // 
            this.tb_Port.Location = new System.Drawing.Point(92, 179);
            this.tb_Port.Name = "tb_Port";
            this.tb_Port.Size = new System.Drawing.Size(100, 20);
            this.tb_Port.TabIndex = 2;
            // 
            // btn_shutdown
            // 
            this.btn_shutdown.Location = new System.Drawing.Point(45, 332);
            this.btn_shutdown.Name = "btn_shutdown";
            this.btn_shutdown.Size = new System.Drawing.Size(75, 23);
            this.btn_shutdown.TabIndex = 6;
            this.btn_shutdown.Text = "Shutdown";
            this.btn_shutdown.UseVisualStyleBackColor = true;
            this.btn_shutdown.Click += new System.EventHandler(this.btn_shutdown_Click);
            // 
            // tb_installer_path
            // 
            this.tb_installer_path.Location = new System.Drawing.Point(326, 139);
            this.tb_installer_path.Name = "tb_installer_path";
            this.tb_installer_path.Size = new System.Drawing.Size(262, 20);
            this.tb_installer_path.TabIndex = 7;
            // 
            // btn_Run
            // 
            this.btn_Run.Location = new System.Drawing.Point(472, 290);
            this.btn_Run.Name = "btn_Run";
            this.btn_Run.Size = new System.Drawing.Size(62, 23);
            this.btn_Run.TabIndex = 8;
            this.btn_Run.Text = "Run";
            this.btn_Run.UseVisualStyleBackColor = true;
            this.btn_Run.Click += new System.EventHandler(this.btn_Install_Click);
            // 
            // btn_browse
            // 
            this.btn_browse.Location = new System.Drawing.Point(594, 136);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(62, 23);
            this.btn_browse.TabIndex = 9;
            this.btn_browse.Text = "Browse";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(369, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Password";
            // 
            // tb_password
            // 
            this.tb_password.Location = new System.Drawing.Point(434, 215);
            this.tb_password.Name = "tb_password";
            this.tb_password.Size = new System.Drawing.Size(100, 20);
            this.tb_password.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(367, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Username";
            // 
            // tb_username
            // 
            this.tb_username.Location = new System.Drawing.Point(434, 189);
            this.tb_username.Name = "tb_username";
            this.tb_username.Size = new System.Drawing.Size(100, 20);
            this.tb_username.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(369, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Domain";
            // 
            // tb_domain
            // 
            this.tb_domain.Location = new System.Drawing.Point(434, 241);
            this.tb_domain.Name = "tb_domain";
            this.tb_domain.Size = new System.Drawing.Size(100, 20);
            this.tb_domain.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(323, 269);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Command";
            // 
            // tb_command
            // 
            this.tb_command.Location = new System.Drawing.Point(388, 266);
            this.tb_command.Name = "tb_command";
            this.tb_command.Size = new System.Drawing.Size(297, 20);
            this.tb_command.TabIndex = 13;
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 383);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tb_command);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tb_domain);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_password);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_username);
            this.Controls.Add(this.btn_browse);
            this.Controls.Add(this.btn_Run);
            this.Controls.Add(this.tb_installer_path);
            this.Controls.Add(this.btn_shutdown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_Port);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_IP);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.lb_output);
            this.Name = "ClientForm";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lb_output;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.TextBox tb_IP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_Port;
        private System.Windows.Forms.Button btn_shutdown;
        private System.Windows.Forms.TextBox tb_installer_path;
        private System.Windows.Forms.Button btn_Run;
        private System.Windows.Forms.Button btn_browse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_username;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_domain;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_command;
    }
}

