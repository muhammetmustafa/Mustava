using System.Data;
using System.Data.OleDb;

namespace Mustava.Helper
{
    public class ExcelHelpers
    {
        public static DataSet Parse(string fileName)
        {
            var connectionString =
                string.Format("provider=Microsoft.Jet.OLEDB.4.0; data source={0};Extended Properties=Excel 8.0;",
                    fileName);

            var data = new DataSet();

            foreach (var sheetName in GetExcelSheetNames(connectionString))
            {
                using (var con = new OleDbConnection(connectionString))
                {
                    var dataTable = new DataTable();
                    var query = string.Format("SELECT * FROM [{0}]", sheetName);
                    con.Open();
                    var adapter = new OleDbDataAdapter(query, con);
                    adapter.Fill(dataTable);
                    data.Tables.Add(dataTable);
                }
            }

            return data;
        }

        public static string[] GetExcelSheetNames(string connectionString)
        {
            OleDbConnection con = null;
            DataTable dt = null;
            con = new OleDbConnection(connectionString);
            con.Open();
            dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (dt == null)
            {
                return null;
            }

            var excelSheetNames = new string[dt.Rows.Count];
            var i = 0;

            foreach (DataRow row in dt.Rows)
            {
                excelSheetNames[i] = row["TABLE_NAME"].ToString();
                i++;
            }

            return excelSheetNames;
        }
    }
}