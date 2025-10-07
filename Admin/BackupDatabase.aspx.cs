using System;
using System.Configuration;
using System.Data.SqlClient;

namespace LMS5.Admin
{
    public partial class BackupDatabase : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDBConn"].ConnectionString;

        protected void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string backupPath = @"C:\SQLBackups\LMS5_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".bak";
                    string query = $"BACKUP DATABASE LMS5 TO DISK = '{backupPath}' WITH INIT";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    lblStatus.Text = $"Backup completed successfully! Saved to: {backupPath}";
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error: " + ex.Message;
            }
        }
    }
}
