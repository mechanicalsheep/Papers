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
            this.btn_Install = new System.Windows.Forms.Button();
            this.btn_browse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_output
            // 
            this.lb_output.FormattingEnabled = true;
            this.lb_output.Location = new System.Drawing.Point(378, 12);
            this.lb_output.Name = "lb_output";
            this.lb_output.Size = new System.Drawing.Size(323, 108);
            this.lb_output.TabIndex = 0;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(189, 207);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(75, 23);
            this.btn_send.TabIndex = 3;
            this.btn_send.Text = "Send";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // tb_IP
            // 
            this.tb_IP.Location = new System.Drawing.Point(177, 82);
            this.tb_IP.Name = "tb_IP";
            this.tb_IP.Size = new System.Drawing.Size(100, 20);
            this.tb_IP.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(139, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(139, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Port";
            // 
            // tb_Port
            // 
            this.tb_Port.Location = new System.Drawing.Point(177, 135);
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
            this.tb_installer_path.Location = new System.Drawing.Point(329, 186);
            this.tb_installer_path.Name = "tb_installer_path";
            this.tb_installer_path.Size = new System.Drawing.Size(262, 20);
            this.tb_installer_path.TabIndex = 7;
            // 
            // btn_Install
            // 
            this.btn_Install.Location = new System.Drawing.Point(448, 238);
            this.btn_Install.Name = "btn_Install";
            this.btn_Install.Size = new System.Drawing.Size(62, 23);
            this.btn_Install.TabIndex = 8;
            this.btn_Install.Text = "Install";
            this.btn_Install.UseVisualStyleBackColor = true;
            this.btn_Install.Click += new System.EventHandler(this.btn_Install_Click);
            // 
            // btn_browse
            // 
            this.btn_browse.Location = new System.Drawing.Point(597, 183);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(62, 23);
            this.btn_browse.TabIndex = 9;
            this.btn_browse.Text = "Browse";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Click += new System.EventHandler(this.button1_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 383);
            this.Controls.Add(this.btn_browse);
            this.Controls.Add(this.btn_Install);
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
        private System.Windows.Forms.Button btn_Install;
        private System.Windows.Forms.Button btn_browse;
    }
}

