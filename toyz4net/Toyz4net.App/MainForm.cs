using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Toyz4net.App.Xcar;

namespace Toyz4net.App
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void tsmiImportCustomer_Click(object sender, EventArgs e)
        {
            ImportCustomerForm form = new ImportCustomerForm();
            form.MdiParent = this;
            form.Show();
        }

        private void tsmiUploadCustomer_Click(object sender, EventArgs e)
        {
            UploadCustomerForm form = new UploadCustomerForm();
            form.MdiParent = this;
            form.Show();
        }
    }
}
