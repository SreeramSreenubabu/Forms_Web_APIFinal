namespace WindowsFormApp
{
    partial class Dashboard
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
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridViewProfilesDash = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewAttDash = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProfilesDash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAttDash)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(819, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "Logout";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btn_Logout);
            // 
            // dataGridViewProfilesDash
            // 
            this.dataGridViewProfilesDash.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProfilesDash.Location = new System.Drawing.Point(104, 245);
            this.dataGridViewProfilesDash.Name = "dataGridViewProfilesDash";
            this.dataGridViewProfilesDash.Size = new System.Drawing.Size(327, 180);
            this.dataGridViewProfilesDash.TabIndex = 1;
            this.dataGridViewProfilesDash.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_Profile);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "DASHBOARD";
/*            this.label1.Click += new System.EventHandler(this.lbl_Dashboard);
*/            // 
            // dataGridViewAttDash
            // 
            this.dataGridViewAttDash.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAttDash.Location = new System.Drawing.Point(519, 245);
            this.dataGridViewAttDash.Name = "dataGridViewAttDash";
            this.dataGridViewAttDash.Size = new System.Drawing.Size(327, 180);
            this.dataGridViewAttDash.TabIndex = 5;
            this.dataGridViewAttDash.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAtt_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(100, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "Profile";
/*            this.label2.Click += new System.EventHandler(this.lbl_Profile);
*/            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(515, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 19);
            this.label3.TabIndex = 7;
            this.label3.Text = "Attendance";
/*            this.label3.Click += new System.EventHandler(this.lbl_Attendance);
*/            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(950, 630);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridViewAttDash);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewProfilesDash);
            this.Controls.Add(this.button1);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.Dashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProfilesDash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAttDash)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridViewProfilesDash;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewAttDash;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}