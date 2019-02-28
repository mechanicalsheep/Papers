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
            this.lb_onlineComp.Location = new System.Drawing.Point(40, 24);
            this.lb_onlineComp.Name = "lb_onlineComp";
            this.lb_onlineComp.Size = new System.Drawing.Size(250, 251);
            this.lb_onlineComp.TabIndex = 2;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(313, 34);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(75, 23);
            this.btn_send.TabIndex = 3;
            this.btn_send.Text = "Send";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 517);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.lb_onlineComp);
            this.Controls.Add(this.btn_Scan);
            this.Controls.Add(this.lb_output);
            this.Name = "ServerForm";
            this.Text = "Server";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lb_output;
        private System.Windows.Forms.Button btn_Scan;
        private System.Windows.Forms.ListBox lb_onlineComp;
        private System.Windows.Forms.Button btn_send;
    }
}

