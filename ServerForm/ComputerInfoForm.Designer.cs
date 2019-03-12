namespace ServerForm
{
    partial class ComputerInfoForm
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
            this.lbl_ComputerName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tb_IP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_Group = new System.Windows.Forms.TextBox();
            this.lb_Softwares = new System.Windows.Forms.ListBox();
            this.btn_plus = new System.Windows.Forms.Button();
            this.btn_minus = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_ComputerName
            // 
            this.lbl_ComputerName.AutoSize = true;
            this.lbl_ComputerName.Location = new System.Drawing.Point(324, 30);
            this.lbl_ComputerName.Name = "lbl_ComputerName";
            this.lbl_ComputerName.Size = new System.Drawing.Size(42, 13);
            this.lbl_ComputerName.TabIndex = 0;
            this.lbl_ComputerName.Text = "HELLO";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(85, 97);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(86, 81);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // tb_IP
            // 
            this.tb_IP.Location = new System.Drawing.Point(327, 97);
            this.tb_IP.Name = "tb_IP";
            this.tb_IP.Size = new System.Drawing.Size(100, 20);
            this.tb_IP.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(276, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "IP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(276, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Group";
            // 
            // tb_Group
            // 
            this.tb_Group.Location = new System.Drawing.Point(327, 136);
            this.tb_Group.Name = "tb_Group";
            this.tb_Group.Size = new System.Drawing.Size(100, 20);
            this.tb_Group.TabIndex = 4;
            // 
            // lb_Softwares
            // 
            this.lb_Softwares.FormattingEnabled = true;
            this.lb_Softwares.Location = new System.Drawing.Point(493, 78);
            this.lb_Softwares.Name = "lb_Softwares";
            this.lb_Softwares.Size = new System.Drawing.Size(219, 238);
            this.lb_Softwares.TabIndex = 6;
            // 
            // btn_plus
            // 
            this.btn_plus.Location = new System.Drawing.Point(534, 343);
            this.btn_plus.Name = "btn_plus";
            this.btn_plus.Size = new System.Drawing.Size(48, 23);
            this.btn_plus.TabIndex = 7;
            this.btn_plus.Text = "+";
            this.btn_plus.UseVisualStyleBackColor = true;
            this.btn_plus.Click += new System.EventHandler(this.btn_plus_Click);
            // 
            // btn_minus
            // 
            this.btn_minus.Location = new System.Drawing.Point(626, 343);
            this.btn_minus.Name = "btn_minus";
            this.btn_minus.Size = new System.Drawing.Size(45, 23);
            this.btn_minus.TabIndex = 8;
            this.btn_minus.Text = "-";
            this.btn_minus.UseVisualStyleBackColor = true;
            // 
            // ComputerInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 452);
            this.Controls.Add(this.btn_minus);
            this.Controls.Add(this.btn_plus);
            this.Controls.Add(this.lb_Softwares);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_Group);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_IP);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbl_ComputerName);
            this.Name = "ComputerInfoForm";
            this.Text = "ComputerInfoForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_ComputerName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox tb_IP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_Group;
        private System.Windows.Forms.ListBox lb_Softwares;
        private System.Windows.Forms.Button btn_plus;
        private System.Windows.Forms.Button btn_minus;
    }
}