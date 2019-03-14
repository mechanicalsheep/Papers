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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lb_Softwares = new System.Windows.Forms.ListBox();
            this.btn_plus = new System.Windows.Forms.Button();
            this.btn_minus = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_Model = new System.Windows.Forms.Label();
            this.lbl_RAM = new System.Windows.Forms.Label();
            this.lbl_ip = new System.Windows.Forms.Label();
            this.lbl_OS = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_snapshotDate = new System.Windows.Forms.Label();
            this.btn_EditGroup = new System.Windows.Forms.Button();
            this.tb_group = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.pictureBox1.Location = new System.Drawing.Point(12, 36);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(86, 81);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "IP :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Group :";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "OS :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_group);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lbl_Model);
            this.groupBox1.Controls.Add(this.lbl_RAM);
            this.groupBox1.Controls.Add(this.lbl_ip);
            this.groupBox1.Controls.Add(this.lbl_OS);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(141, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 191);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Model :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "RAM :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(262, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Snapshot Date :";
            // 
            // lbl_Model
            // 
            this.lbl_Model.AutoSize = true;
            this.lbl_Model.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Model.Location = new System.Drawing.Point(57, 96);
            this.lbl_Model.Name = "lbl_Model";
            this.lbl_Model.Size = new System.Drawing.Size(49, 13);
            this.lbl_Model.TabIndex = 17;
            this.lbl_Model.Text = "Model :";
            // 
            // lbl_RAM
            // 
            this.lbl_RAM.AutoSize = true;
            this.lbl_RAM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_RAM.Location = new System.Drawing.Point(57, 124);
            this.lbl_RAM.Name = "lbl_RAM";
            this.lbl_RAM.Size = new System.Drawing.Size(16, 13);
            this.lbl_RAM.TabIndex = 18;
            this.lbl_RAM.Text = "R";
            // 
            // lbl_ip
            // 
            this.lbl_ip.AutoSize = true;
            this.lbl_ip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ip.Location = new System.Drawing.Point(57, 26);
            this.lbl_ip.Name = "lbl_ip";
            this.lbl_ip.Size = new System.Drawing.Size(27, 13);
            this.lbl_ip.TabIndex = 14;
            this.lbl_ip.Text = "IP :";
            // 
            // lbl_OS
            // 
            this.lbl_OS.AutoSize = true;
            this.lbl_OS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_OS.Location = new System.Drawing.Point(62, 74);
            this.lbl_OS.Name = "lbl_OS";
            this.lbl_OS.Size = new System.Drawing.Size(32, 13);
            this.lbl_OS.TabIndex = 16;
            this.lbl_OS.Text = "OS :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(73, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "GB";
            // 
            // lbl_snapshotDate
            // 
            this.lbl_snapshotDate.AutoSize = true;
            this.lbl_snapshotDate.Location = new System.Drawing.Point(346, 52);
            this.lbl_snapshotDate.Name = "lbl_snapshotDate";
            this.lbl_snapshotDate.Size = new System.Drawing.Size(78, 13);
            this.lbl_snapshotDate.TabIndex = 20;
            this.lbl_snapshotDate.Text = "Snapshot Date";
            // 
            // btn_EditGroup
            // 
            this.btn_EditGroup.Location = new System.Drawing.Point(149, 293);
            this.btn_EditGroup.Name = "btn_EditGroup";
            this.btn_EditGroup.Size = new System.Drawing.Size(76, 23);
            this.btn_EditGroup.TabIndex = 21;
            this.btn_EditGroup.Text = "Edit Group";
            this.btn_EditGroup.UseVisualStyleBackColor = true;
            this.btn_EditGroup.Click += new System.EventHandler(this.btn_EditGroup_Click);
            // 
            // tb_group
            // 
            this.tb_group.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_group.Enabled = false;
            this.tb_group.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_group.Location = new System.Drawing.Point(60, 52);
            this.tb_group.Name = "tb_group";
            this.tb_group.Size = new System.Drawing.Size(100, 13);
            this.tb_group.TabIndex = 20;
            // 
            // ComputerInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 452);
            this.Controls.Add(this.btn_EditGroup);
            this.Controls.Add(this.lbl_snapshotDate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_minus);
            this.Controls.Add(this.btn_plus);
            this.Controls.Add(this.lb_Softwares);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbl_ComputerName);
            this.Controls.Add(this.label6);
            this.Name = "ComputerInfoForm";
            this.Text = "ComputerInfoForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_ComputerName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lb_Softwares;
        private System.Windows.Forms.Button btn_plus;
        private System.Windows.Forms.Button btn_minus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_snapshotDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_Model;
        private System.Windows.Forms.Label lbl_RAM;
        private System.Windows.Forms.Label lbl_ip;
        private System.Windows.Forms.Label lbl_OS;
        private System.Windows.Forms.TextBox tb_group;
        private System.Windows.Forms.Button btn_EditGroup;
    }
}