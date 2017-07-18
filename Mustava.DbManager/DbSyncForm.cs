using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mustava.Ado;
using Mustava.DbManager.Properties;
using Mustava.Extensions;

namespace Mustava.DbManager
{
    public partial class DbSyncForm : Form
    {
        public DbSyncForm()
        {
            InitializeComponent();
        }

        private void DbSyncForm_Load(object sender, EventArgs e)
        {
            if (Settings.Default.Server1List != null)
            {
                foreach (var item in Settings.Default.Server1List)
                {
                    cbeServer1.Properties.Items.Add(item);
                }
            }

            if (Settings.Default.Server2List != null)
            {
                foreach (var item in Settings.Default.Server2List)
                {
                    cbeServer2.Properties.Items.Add(item);
                }
            }
        }

        private void DbSyncForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Server1List = new StringCollection();
            foreach (var item in cbeServer1.Properties.Items)
            {
                Settings.Default.Server1List.Add(item.ToString());
            }

            Settings.Default.Server2List = new StringCollection();
            foreach (var item in cbeServer2.Properties.Items)
            {
                Settings.Default.Server2List.Add(item.ToString());
            }

            Settings.Default.Save();
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

            cbeServer1.Properties.Items.Add(cbeServer1.EditValue);
        }

        private void hlcConnect2_Click(object sender, EventArgs e)
        {
            var connectionString = new SqlConnectionStringBuilder
            {
                DataSource = cbeServer2.EditValue.ToStringOrEmpty(),
                UserID = txtUsername2.EditValue.ToStringOrEmpty(),
                Password = txtPassword2.EditValue.ToStringOrEmpty()
            };

            Services.GetAllDatabases(connectionString.ToString())
                .ForEach(i => cbeSchema2.Properties.Items.Add(i));

            cbeServer2.Properties.Items.Add(cbeServer1.EditValue);
        }

        private void btnCopyInfo1to2_Click(object sender, EventArgs e)
        {
            cbeServer2.EditValue = cbeServer1.EditValue;
            txtUsername2.EditValue = txtUsername1.EditValue;
            txtPassword2.EditValue = txtPassword1.EditValue;
        }

        private void btnCopyInfo2to1_Click(object sender, EventArgs e)
        {
            cbeServer1.EditValue = cbeServer2.EditValue;
            txtUsername1.EditValue = txtUsername2.EditValue;
            txtPassword1.EditValue = txtPassword2.EditValue;
        }

        private void bwWorker_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void bwWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void bwWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void hlcRefresh1_Click(object sender, EventArgs e)
        {

        }
    }
}
