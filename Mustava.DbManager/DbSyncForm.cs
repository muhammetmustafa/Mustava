using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mustava.Ado;
using Mustava.Extensions;

namespace Mustava.DbManager
{
    public partial class DbSyncForm : Form
    {
        public DbSyncForm()
        {
            InitializeComponent();
        }

        private void hlcConnect1_Click(object sender, EventArgs e)
        {
            var connectionString = new SqlConnectionStringBuilder
            {
                DataSource = cbeServer1.EditValue.ToStringOrEmpty(),
                UserID = txtUsername1.EditValue.ToStringOrEmpty(),
                Password = txtPassword1.EditValue.ToStringOrEmpty()
            };

            Services.GetAllDatabases(connectionString.ToString())
                .ForEach(i => cbeSchema1.Properties.Items.Add(i));
        }
    }
}
