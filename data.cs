using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace studentreg
{
    public partial class data : Form
    {
        public data()
        {
            InitializeComponent();
        }

        private void data_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'gcbtDataSet.student' table. You can move, or remove it, as needed.
            this.studentTableAdapter.Fill(this.gcbtDataSet.student);

            this.reportViewer1.RefreshReport();
        }
    }
}
