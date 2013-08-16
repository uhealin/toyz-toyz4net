namespace ZDSL.App
{
    partial class ImageLocalForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbRootPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.btnSelectRootPath = new System.Windows.Forms.Button();
            this.btnImageLocal = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.tsslResult = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.btnImageLocal);
            this.groupBox1.Controls.Add(this.btnSelectRootPath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbRootPath);
            this.groupBox1.Location = new System.Drawing.Point(34, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(578, 110);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // tbRootPath
            // 
            this.tbRootPath.Location = new System.Drawing.Point(99, 44);
            this.tbRootPath.Name = "tbRootPath";
            this.tbRootPath.Size = new System.Drawing.Size(216, 21);
            this.tbRootPath.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "根目录";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslResult});
            this.statusStrip1.Location = new System.Drawing.Point(0, 353);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(619, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // btnSelectRootPath
            // 
            this.btnSelectRootPath.Location = new System.Drawing.Point(321, 42);
            this.btnSelectRootPath.Name = "btnSelectRootPath";
            this.btnSelectRootPath.Size = new System.Drawing.Size(56, 23);
            this.btnSelectRootPath.TabIndex = 2;
            this.btnSelectRootPath.Text = "选择";
            this.btnSelectRootPath.UseVisualStyleBackColor = true;
            this.btnSelectRootPath.Click += new System.EventHandler(this.btnSelectRootPath_Click);
            // 
            // btnImageLocal
            // 
            this.btnImageLocal.Location = new System.Drawing.Point(383, 42);
            this.btnImageLocal.Name = "btnImageLocal";
            this.btnImageLocal.Size = new System.Drawing.Size(75, 23);
            this.btnImageLocal.TabIndex = 3;
            this.btnImageLocal.Text = "开始导入";
            this.btnImageLocal.UseVisualStyleBackColor = true;
            this.btnImageLocal.Click += new System.EventHandler(this.btnImageLocal_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(464, 42);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // tsslResult
            // 
            this.tsslResult.Name = "tsslResult";
            this.tsslResult.Size = new System.Drawing.Size(0, 17);
            // 
            // ImageLocalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 375);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ImageLocalForm";
            this.Text = "ImageLocalForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbRootPath;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnImageLocal;
        private System.Windows.Forms.Button btnSelectRootPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslResult;
    }
}