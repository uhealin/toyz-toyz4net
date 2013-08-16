namespace ZDSL.App
{
    partial class HotelDetailImportForm
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
            this.btnStop = new System.Windows.Forms.Button();
            this.cbSubInfo = new System.Windows.Forms.CheckBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.tbKeyword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslResult = new System.Windows.Forms.ToolStripStatusLabel();
            this.colHotelId = new System.Windows.Forms.ColumnHeader();
            this.colHotelName = new System.Windows.Forms.ColumnHeader();
            this.colUpdateTime = new System.Windows.Forms.ColumnHeader();
            this.colCount = new System.Windows.Forms.ColumnHeader();
            this.colRemark = new System.Windows.Forms.ColumnHeader();
            this.lvImportHotels = new System.Windows.Forms.ListView();
            this.nudMin = new System.Windows.Forms.NumericUpDown();
            this.nudMax = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMax)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nudMax);
            this.groupBox1.Controls.Add(this.nudMin);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.cbSubInfo);
            this.groupBox1.Controls.Add(this.btnImport);
            this.groupBox1.Controls.Add(this.tbKeyword);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(43, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(742, 105);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(573, 27);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // cbSubInfo
            // 
            this.cbSubInfo.AutoSize = true;
            this.cbSubInfo.Checked = true;
            this.cbSubInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSubInfo.Location = new System.Drawing.Point(395, 32);
            this.cbSubInfo.Name = "cbSubInfo";
            this.cbSubInfo.Size = new System.Drawing.Size(84, 16);
            this.cbSubInfo.TabIndex = 3;
            this.cbSubInfo.Text = "导入子数据";
            this.cbSubInfo.UseVisualStyleBackColor = true;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(485, 27);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(73, 23);
            this.btnImport.TabIndex = 2;
            this.btnImport.Text = "开始导入";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // tbKeyword
            // 
            this.tbKeyword.Location = new System.Drawing.Point(137, 29);
            this.tbKeyword.Name = "tbKeyword";
            this.tbKeyword.Size = new System.Drawing.Size(238, 21);
            this.tbKeyword.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "过滤条件（酒店名）";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslResult});
            this.statusStrip1.Location = new System.Drawing.Point(0, 567);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(894, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslResult
            // 
            this.tsslResult.Name = "tsslResult";
            this.tsslResult.Size = new System.Drawing.Size(0, 17);
            // 
            // colHotelId
            // 
            this.colHotelId.Text = "酒店ID";
            this.colHotelId.Width = 115;
            // 
            // colHotelName
            // 
            this.colHotelName.Text = "酒店名";
            this.colHotelName.Width = 303;
            // 
            // colUpdateTime
            // 
            this.colUpdateTime.Text = "更新时间";
            this.colUpdateTime.Width = 83;
            // 
            // colCount
            // 
            this.colCount.Text = "数据列数";
            this.colCount.Width = 97;
            // 
            // colRemark
            // 
            this.colRemark.Text = "备注";
            this.colRemark.Width = 106;
            // 
            // lvImportHotels
            // 
            this.lvImportHotels.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colHotelId,
            this.colHotelName,
            this.colUpdateTime,
            this.colCount,
            this.colRemark});
            this.lvImportHotels.Location = new System.Drawing.Point(43, 159);
            this.lvImportHotels.Name = "lvImportHotels";
            this.lvImportHotels.Size = new System.Drawing.Size(742, 402);
            this.lvImportHotels.TabIndex = 1;
            this.lvImportHotels.UseCompatibleStateImageBehavior = false;
            this.lvImportHotels.View = System.Windows.Forms.View.Details;
            // 
            // nudMin
            // 
            this.nudMin.Location = new System.Drawing.Point(137, 72);
            this.nudMin.Name = "nudMin";
            this.nudMin.Size = new System.Drawing.Size(95, 21);
            this.nudMin.TabIndex = 5;
            // 
            // nudMax
            // 
            this.nudMax.Location = new System.Drawing.Point(266, 72);
            this.nudMax.Name = "nudMax";
            this.nudMax.Size = new System.Drawing.Size(109, 21);
            this.nudMax.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(238, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "~";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "查询范围";
            // 
            // HotelDetailImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 589);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lvImportHotels);
            this.Controls.Add(this.groupBox1);
            this.Name = "HotelDetailImportForm";
            this.Text = "HotelDetailImportForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.TextBox tbKeyword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbSubInfo;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslResult;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ColumnHeader colHotelId;
        private System.Windows.Forms.ColumnHeader colHotelName;
        private System.Windows.Forms.ColumnHeader colUpdateTime;
        private System.Windows.Forms.ColumnHeader colCount;
        private System.Windows.Forms.ColumnHeader colRemark;
        private System.Windows.Forms.ListView lvImportHotels;
        private System.Windows.Forms.NumericUpDown nudMax;
        private System.Windows.Forms.NumericUpDown nudMin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

    }
}