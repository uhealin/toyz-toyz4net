namespace Toyz4net.App.Xcar
{
    partial class UploadCustomerForm
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.cobDrivercs = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cobBuytime = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cobShop = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cobCity = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cobProvince = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvCustomer = new System.Windows.Forms.DataGridView();
            this.xcarcustomerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataDataSet = new Toyz4net.App.dataDataSet();
            this.xcar_customerTableAdapter = new Toyz4net.App.dataDataSetTableAdapters.xcar_customerTableAdapter();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslStatue = new System.Windows.Forms.ToolStripStatusLabel();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mobile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUploadInd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUploadtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProvice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBuytime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDrivecs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnNext = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbTimer = new System.Windows.Forms.Label();
            this.btnTimer = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xcarcustomerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataDataSet)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnNext);
            this.groupBox1.Controls.Add(this.btnSubmit);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.cobDrivercs);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cobBuytime);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cobShop);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cobCity);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cobProvince);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtMobile);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(399, 199);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(308, 170);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 15;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(122, 170);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 14;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cobDrivercs
            // 
            this.cobDrivercs.FormattingEnabled = true;
            this.cobDrivercs.Location = new System.Drawing.Point(252, 135);
            this.cobDrivercs.Name = "cobDrivercs";
            this.cobDrivercs.Size = new System.Drawing.Size(121, 20);
            this.cobDrivercs.TabIndex = 13;
            this.cobDrivercs.SelectedIndexChanged += new System.EventHandler(this.cobDrivercs_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(193, 138);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "联系时间";
            // 
            // cobBuytime
            // 
            this.cobBuytime.FormattingEnabled = true;
            this.cobBuytime.Location = new System.Drawing.Point(65, 135);
            this.cobBuytime.Name = "cobBuytime";
            this.cobBuytime.Size = new System.Drawing.Size(121, 20);
            this.cobBuytime.TabIndex = 11;
            this.cobBuytime.SelectedIndexChanged += new System.EventHandler(this.cobBuytime_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "购车时间";
            // 
            // cobShop
            // 
            this.cobShop.FormattingEnabled = true;
            this.cobShop.Location = new System.Drawing.Point(65, 105);
            this.cobShop.Name = "cobShop";
            this.cobShop.Size = new System.Drawing.Size(308, 20);
            this.cobShop.TabIndex = 9;
            this.cobShop.SelectedIndexChanged += new System.EventHandler(this.cobShop_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "4S店";
            // 
            // cobCity
            // 
            this.cobCity.FormattingEnabled = true;
            this.cobCity.Location = new System.Drawing.Point(248, 70);
            this.cobCity.Name = "cobCity";
            this.cobCity.Size = new System.Drawing.Size(121, 20);
            this.cobCity.TabIndex = 7;
            this.cobCity.SelectedIndexChanged += new System.EventHandler(this.cobCity_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(201, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "城市";
            // 
            // cobProvince
            // 
            this.cobProvince.FormattingEnabled = true;
            this.cobProvince.Location = new System.Drawing.Point(65, 70);
            this.cobProvince.Name = "cobProvince";
            this.cobProvince.Size = new System.Drawing.Size(121, 20);
            this.cobProvince.TabIndex = 5;
            this.cobProvince.SelectedIndexChanged += new System.EventHandler(this.cobProvince_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "省份";
            // 
            // txtMobile
            // 
            this.txtMobile.Location = new System.Drawing.Point(248, 27);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(125, 21);
            this.txtMobile.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(201, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "手机";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(64, 27);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(122, 21);
            this.txtName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "姓名";
            // 
            // dgvCustomer
            // 
            this.dgvCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colName,
            this.mobile,
            this.colUploadInd,
            this.colUploadtime,
            this.colRemark,
            this.colProvice,
            this.colCity,
            this.colShop,
            this.colBuytime,
            this.colDrivecs});
            this.dgvCustomer.Location = new System.Drawing.Point(12, 226);
            this.dgvCustomer.Name = "dgvCustomer";
            this.dgvCustomer.RowTemplate.Height = 23;
            this.dgvCustomer.Size = new System.Drawing.Size(1021, 327);
            this.dgvCustomer.TabIndex = 1;
            this.dgvCustomer.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCustomer_CellClick);
            // 
            // xcarcustomerBindingSource
            // 
            this.xcarcustomerBindingSource.DataMember = "xcar_customer";
            this.xcarcustomerBindingSource.DataSource = this.dataDataSet;
            // 
            // dataDataSet
            // 
            this.dataDataSet.DataSetName = "dataDataSet";
            this.dataDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // xcar_customerTableAdapter
            // 
            this.xcar_customerTableAdapter.ClearBeforeFill = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatue});
            this.statusStrip1.Location = new System.Drawing.Point(0, 569);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1045, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslStatue
            // 
            this.tsslStatue.Name = "tsslStatue";
            this.tsslStatue.Size = new System.Drawing.Size(131, 17);
            this.tsslStatue.Text = "toolStripStatusLabel1";
            // 
            // colId
            // 
            this.colId.DataPropertyName = "id";
            this.colId.HeaderText = "id";
            this.colId.Name = "colId";
            // 
            // colName
            // 
            this.colName.DataPropertyName = "name";
            this.colName.HeaderText = "姓名";
            this.colName.Name = "colName";
            this.colName.Width = 80;
            // 
            // mobile
            // 
            this.mobile.DataPropertyName = "mobile";
            this.mobile.HeaderText = "电话";
            this.mobile.Name = "mobile";
            // 
            // colUploadInd
            // 
            this.colUploadInd.DataPropertyName = "upload_ind";
            this.colUploadInd.HeaderText = "已上传？";
            this.colUploadInd.Name = "colUploadInd";
            this.colUploadInd.Width = 60;
            // 
            // colUploadtime
            // 
            this.colUploadtime.DataPropertyName = "upload_time";
            this.colUploadtime.HeaderText = "上传时间";
            this.colUploadtime.Name = "colUploadtime";
            // 
            // colRemark
            // 
            this.colRemark.DataPropertyName = "remark";
            this.colRemark.HeaderText = "备注";
            this.colRemark.Name = "colRemark";
            // 
            // colProvice
            // 
            this.colProvice.DataPropertyName = "province";
            this.colProvice.HeaderText = "省";
            this.colProvice.Name = "colProvice";
            this.colProvice.Width = 50;
            // 
            // colCity
            // 
            this.colCity.DataPropertyName = "city";
            this.colCity.HeaderText = "市";
            this.colCity.Name = "colCity";
            this.colCity.Width = 50;
            // 
            // colShop
            // 
            this.colShop.DataPropertyName = "shop";
            this.colShop.HeaderText = "店";
            this.colShop.Name = "colShop";
            this.colShop.Width = 150;
            // 
            // colBuytime
            // 
            this.colBuytime.DataPropertyName = "buy_time";
            this.colBuytime.HeaderText = "购车时间";
            this.colBuytime.Name = "colBuytime";
            // 
            // colDrivecs
            // 
            this.colDrivecs.DataPropertyName = "drive_cs";
            this.colDrivecs.HeaderText = "联系时间";
            this.colDrivecs.Name = "colDrivecs";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(218, 170);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 16;
            this.btnNext.Text = "下一个";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnTimer);
            this.groupBox2.Controls.Add(this.lbTimer);
            this.groupBox2.Location = new System.Drawing.Point(435, 31);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(285, 183);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // lbTimer
            // 
            this.lbTimer.AutoSize = true;
            this.lbTimer.Location = new System.Drawing.Point(50, 43);
            this.lbTimer.Name = "lbTimer";
            this.lbTimer.Size = new System.Drawing.Size(41, 12);
            this.lbTimer.TabIndex = 0;
            this.lbTimer.Text = "label8";
            // 
            // btnTimer
            // 
            this.btnTimer.Location = new System.Drawing.Point(52, 147);
            this.btnTimer.Name = "btnTimer";
            this.btnTimer.Size = new System.Drawing.Size(75, 23);
            this.btnTimer.TabIndex = 1;
            this.btnTimer.Text = "开始";
            this.btnTimer.UseVisualStyleBackColor = true;
            this.btnTimer.Click += new System.EventHandler(this.btnTimer_Click);
            // 
            // UploadCustomerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 591);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dgvCustomer);
            this.Controls.Add(this.groupBox1);
            this.Name = "UploadCustomerForm";
            this.Text = "UploadCustomerForm";
            this.Load += new System.EventHandler(this.UploadCustomerForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xcarcustomerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataDataSet)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cobDrivercs;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cobBuytime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cobShop;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cobCity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cobProvince;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvCustomer;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnClear;
        private dataDataSet dataDataSet;
        private System.Windows.Forms.BindingSource xcarcustomerBindingSource;
        private Toyz4net.App.dataDataSetTableAdapters.xcar_customerTableAdapter xcar_customerTableAdapter;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn mobile;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUploadInd;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUploadtime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProvice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShop;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBuytime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDrivecs;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnTimer;
        private System.Windows.Forms.Label lbTimer;
    }
}