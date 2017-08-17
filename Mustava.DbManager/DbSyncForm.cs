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
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using Mustava.Ado;
using Mustava.DbManager.Properties;
using Mustava.Extensions;
using Newtonsoft.Json;

namespace Mustava.DbManager
{
    public partial class DbSyncForm : Form
    {
        private SqlConnectionStringBuilder _connectionString1;
        private SqlConnectionStringBuilder _connectionString2;

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
                    try
                    {
                        cbeServer1.Properties.Items.Add(JsonConvert.DeserializeObject<SqlConnectionStringBuilder>(item));
                    }
                    catch (Exception exception)
                    {
                        
                    }
                }
            }

            if (Settings.Default.Server2List != null)
            {
                foreach (var item in Settings.Default.Server2List)
                {
                    try
                    {
                        cbeServer2.Properties.Items.Add(JsonConvert.DeserializeObject<SqlConnectionStringBuilder>(item));
                    }
                    catch (Exception exception)
                    {
                        
                    }
                }
            }
        }

        private void DbSyncForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Server1List = new StringCollection();
            foreach (var item in cbeServer1.Properties.Items)
            {
                Settings.Default.Server1List.Add(JsonConvert.SerializeObject(item));
            }

            Settings.Default.Server2List = new StringCollection();
            foreach (var item in cbeServer2.Properties.Items)
            {
                Settings.Default.Server2List.Add(JsonConvert.SerializeObject(item));
            }

            Settings.Default.Save();
        }

        private void hlcConnect1_Click(object sender, EventArgs e)
        {
            var connectionString = new SqlConnectionStringBuilder
            {
                DataSource = cbeServer1.EditValue.ExToStringOrEmpty(),
                UserID = txtUsername1.EditValue.ExToStringOrEmpty(),
                Password = txtPassword1.EditValue.ExToStringOrEmpty()
            };

            try
            {
                Services.GetAllDatabases(connectionString.ToString())
                    .ForEach(i => cbeSchema1.Properties.Items.Add(i));

                if (!cbeServer1.Properties.Items.Contains(connectionString))
                {
                    cbeServer1.Properties.Items.Add(connectionString);
                }

                _connectionString1 = connectionString;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
        }

        private void hlcConnect2_Click(object sender, EventArgs e)
        {
            var connectionString = new SqlConnectionStringBuilder
            {
                DataSource = cbeServer2.EditValue.ExToStringOrEmpty(),
                UserID = txtUsername2.EditValue.ExToStringOrEmpty(),
                Password = txtPassword2.EditValue.ExToStringOrEmpty()
            };

            try
            {
                Services.GetAllDatabases(connectionString.ToString())
                    .ForEach(i => cbeSchema2.Properties.Items.Add(i));

                if (!cbeServer2.Properties.Items.Contains(connectionString))
                {
                    cbeServer2.Properties.Items.Add(connectionString);
                }

                _connectionString2 = connectionString;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
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
            if (_connectionString1 == null)
            {
                return;
            }

            var objects = new List<object>();
            if (ceShowTables.Checked)
                objects.Add(ceShowTables.Tag);
            if (ceShowProcedures.Checked)
                objects.Add(ceShowProcedures.Tag);
            if (ceShowFunctions.Checked)
                objects.Add(ceShowFunctions.Tag);

            var schema = cbeSchema1.EditValue.ExToStringOrEmpty();
            if (schema.ExIsNullOrEmpty())
            {
                XtraMessageBox.Show("Şema seç");
                return;
            }
            
            var list = Services.GetAllObjects(_connectionString1.ToString(), objects.ToArray());
            gObjects1.DataSource = new BindingSource(list, "");
        }

        private void hlcRefresh2_Click(object sender, EventArgs e)
        {
            if (_connectionString2 == null)
            {
                return;
            }

            var objects = new List<object>();
            if (ceShowTables2.Checked)
                objects.Add(ceShowTables2.Tag);
            if (ceShowProcedures2.Checked)
                objects.Add(ceShowProcedures2.Tag);
            if (ceShowFunctions2.Checked)
                objects.Add(ceShowFunctions2.Tag);

            var schema = cbeSchema2.EditValue.ExToStringOrEmpty();
            if (schema.ExIsNullOrEmpty())
            {
                XtraMessageBox.Show("Şema seç");
                return;
            }
            
            var list = Services.GetAllObjects(_connectionString2.ToString(), objects.ToArray());
            gObjects2.DataSource = new BindingSource(list, "");
        }

        private void cbeSchema1_EditValueChanged(object sender, EventArgs e)
        {
            if (cbeSchema1.EditValue.ExToStringOrEmpty() != string.Empty)
            {
                _connectionString1.InitialCatalog = cbeSchema1.EditValue.ExToStringOrEmpty();
            }
        }

        private void cbeSchema2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbeSchema2.EditValue.ExToStringOrEmpty() != string.Empty)
            {
                _connectionString2.InitialCatalog = cbeSchema2.EditValue.ExToStringOrEmpty();
            }
        }

        private void cbeServer1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var connectionString = cbeServer1.SelectedItem as SqlConnectionStringBuilder;
            if (connectionString == null)
            {
                return;
            }

            _connectionString1 = connectionString;
            cbeServer1.EditValue = _connectionString1.DataSource;
            txtUsername1.EditValue = _connectionString1.UserID;
            txtPassword1.EditValue = _connectionString1.Password;
        }

        private void cbeServer2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var connectionString = cbeServer2.SelectedItem as SqlConnectionStringBuilder;
            if (connectionString == null)
            {
                return;
            }

            _connectionString2 = connectionString;
            cbeServer2.EditValue = _connectionString2.DataSource;
            txtUsername2.EditValue = _connectionString2.UserID;
            txtPassword2.EditValue = _connectionString2.Password;
        }
    }
}
