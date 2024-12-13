namespace WindowsFormApp
{
    partial class Attendance
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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridViewAtt = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.TotalWorkingHours = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PresentDate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAtt)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(92, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 26);
            this.label1.TabIndex = 6;
            this.label1.Text = "ATTENDANCE";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(819, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 7;
            this.button1.Text = "Logout";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btn_Logout);
            // 
            // dataGridViewAtt
            // 
            this.dataGridViewAtt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAtt.Location = new System.Drawing.Point(97, 192);
            this.dataGridViewAtt.Name = "dataGridViewAtt";
            this.dataGridViewAtt.RowHeadersWidth = 40;
            this.dataGridViewAtt.Size = new System.Drawing.Size(341, 363);
            this.dataGridViewAtt.TabIndex = 8;
/*            this.dataGridViewAtt.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAtt_CellContentClick);
*/            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(694, 31);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 28);
            this.button2.TabIndex = 10;
            this.button2.Text = "Home";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btn_Home);
            // 
            // TotalWorkingHours
            // 
            this.TotalWorkingHours.AutoSize = true;
            this.TotalWorkingHours.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalWorkingHours.Location = new System.Drawing.Point(681, 369);
            this.TotalWorkingHours.Name = "TotalWorkingHours";
            this.TotalWorkingHours.Size = new System.Drawing.Size(56, 16);
            this.TotalWorkingHours.TabIndex = 13;
            this.TotalWorkingHours.Text = "Duration";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(628, 320);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 28);
            this.label2.TabIndex = 12;
            this.label2.Text = "Working Hours";
            // 
            // PresentDate
            // 
            this.PresentDate.AutoSize = true;
            this.PresentDate.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PresentDate.Location = new System.Drawing.Point(660, 281);
            this.PresentDate.Name = "PresentDate";
            this.PresentDate.Size = new System.Drawing.Size(34, 16);
            this.PresentDate.TabIndex = 14;
            this.PresentDate.Text = "Date";
            // 
            // Attendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(950, 630);
            this.Controls.Add(this.PresentDate);
            this.Controls.Add(this.TotalWorkingHours);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridViewAtt);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "Attendance";
            this.Text = "Attendance";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAtt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridViewAtt;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label TotalWorkingHours;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label PresentDate;
    }
}