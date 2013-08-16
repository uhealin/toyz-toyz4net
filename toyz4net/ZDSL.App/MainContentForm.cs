using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZDSL.App
{
    public partial class MainContentForm : Form
    {
        public MainContentForm()
        {
            InitializeComponent();
        }

        private void tslStaticPage_Click(object sender, EventArgs e)
        {
            StaticPageForm form = new StaticPageForm();
            form.MdiParent = this;
            form.Show();
        }

        private void tslHotelDetailImport_Click(object sender, EventArgs e)
        {
            HotelDetailImportForm form = new HotelDetailImportForm();
            form.MdiParent = this;
            form.Show();
        }

        private void tslImageLocal_Click(object sender, EventArgs e)
        {
            ImageLocalForm form= new ImageLocalForm();
            form.MdiParent = this;
            form.Show();
        }
    }
}
