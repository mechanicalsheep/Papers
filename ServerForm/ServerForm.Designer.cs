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
            this.lb_onlineComp = new System.Windows.Forms.ListBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.tb_domain = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_command = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lb_output
            // 
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
            // lb_onlineComp
            // 
            this.lb_onlineComp.FormattingEnabled = true;
            this.lb_onlineComp.Location = new System.Drawing.Point(12, 24);
            this.lb_onlineComp.Name = "lb_onlineComp";
            this.lb_onlineComp.Size = new System.Drawing.Size(394, 251);
            this.lb_onlineComp.TabIndex = 2;
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
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 517);
            this.Controls.Add(this.tb_command);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_domain);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_password);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.lb_onlineComp);
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
        private System.Windows.Forms.ListBox lb_onlineComp;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.TextBox tb_domain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_command;
        private System.Windows.Forms.Label label3;
    }
}

