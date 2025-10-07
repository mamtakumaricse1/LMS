using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace LMS5.Admin
{
    public partial class Settings : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDBConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSettings();
            }
        }

        private void LoadSettings()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT TOP 1 LibraryName, AdminEmail, BorrowDays, FinePerDay FROM SystemSettings ORDER BY Id DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtLibraryName.Text = dr["LibraryName"].ToString();
                    txtAdminEmail.Text = dr["AdminEmail"].ToString();
                    txtBorrowDays.Text = dr["BorrowDays"].ToString();
                    txtFinePerDay.Text = dr["FinePerDay"].ToString();
                }
                dr.Close();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            lblMsg.CssClass = "";

            // Validation
            if (string.IsNullOrWhiteSpace(txtLibraryName.Text) ||
                string.IsNullOrWhiteSpace(txtAdminEmail.Text) ||
                string.IsNullOrWhiteSpace(txtBorrowDays.Text) ||
                string.IsNullOrWhiteSpace(txtFinePerDay.Text))
            {
                lblMsg.Text = "All fields are required!";
                lblMsg.CssClass = "text-danger";
                return;
            }

            if (!Regex.IsMatch(txtAdminEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                lblMsg.Text = "Invalid email format!";
                lblMsg.CssClass = "text-danger";
                return;
            }

            if (!int.TryParse(txtBorrowDays.Text, out int borrowDays) || borrowDays <= 0)
            {
                lblMsg.Text = "Borrow Days must be a positive number!";
                lblMsg.CssClass = "text-danger";
                return;
            }

            if (!decimal.TryParse(txtFinePerDay.Text, out decimal finePerDay) || finePerDay < 0)
            {
                lblMsg.Text = "Fine per day must be a non-negative number!";
                lblMsg.CssClass = "text-danger";
                return;
            }

            // Save to DB
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string insertQuery = @"INSERT INTO SystemSettings (LibraryName, AdminEmail, BorrowDays, FinePerDay)
                                       VALUES (@LibraryName, @AdminEmail, @BorrowDays, @FinePerDay)";
                SqlCommand cmd = new SqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@LibraryName", txtLibraryName.Text.Trim());
                cmd.Parameters.AddWithValue("@AdminEmail", txtAdminEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@BorrowDays", borrowDays);
                cmd.Parameters.AddWithValue("@FinePerDay", finePerDay);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            lblMsg.Text = "Settings saved successfully!";
            lblMsg.CssClass = "text-success";
        }
    }
}
