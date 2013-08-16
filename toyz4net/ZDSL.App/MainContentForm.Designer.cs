namespace ZDSL.App
{
    partial class MainContentForm
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tslStaticPage = new System.Windows.Forms.ToolStripLabel();
            this.tslHotelDetailImport = new System.Windows.Forms.ToolStripLabel();
            this.tslImageLocal = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslStaticPage,
            this.tslHotelDetailImport,
            this.tslImageLocal});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1022, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tslStaticPage
            // 
            this.tslStaticPage.Name = "tslStaticPage";
            this.tslStaticPage.Size = new System.Drawing.Size(101, 22);
            this.tslStaticPage.Text = "静态页面脚本生成";
            this.tslStaticPage.Click += new System.EventHandler(this.tslStaticPage_Click);
            // 
            // tslHotelDetailImport
            // 
            this.tslHotelDetailImport.Name = "tslHotelDetailImport";
            this.tslHotelDetailImport.Size = new System.Drawing.Size(101, 22);
            this.tslHotelDetailImport.Text = "酒店详细信息导入";
            this.tslHotelDetailImport.Click += new System.EventHandler(this.tslHotelDetailImport_Click);
            // 
            // tslImageLocal
            // 
            this.tslImageLocal.Name = "tslImageLocal";
            this.tslImageLocal.Size = new System.Drawing.Size(65, 22);
            this.tslImageLocal.Text = "图片本地化";
            this.tslImageLocal.Click += new System.EventHandler(this.tslImageLocal_Click);
            // 
            // MainContentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 553);
            this.Controls.Add(this.toolStrip1);
            this.IsMdiContainer = true;
            this.Name = "MainContentForm";
            this.Text = "MainContentForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel tslStaticPage;
        private System.Windows.Forms.ToolStripLabel tslHotelDetailImport;
        private System.Windows.Forms.ToolStripLabel tslImageLocal;
    }
}