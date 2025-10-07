using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;

namespace LMS5
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDBConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("Login.aspx");

            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                string role = ticket.UserData;
                string username = ticket.Name;

                if (role != "Admin")
                    Response.Redirect("Login.aspx");

               
            }

            if (!IsPostBack)
            {
                LoadDashboardStats();
                BindRecentActivity();
            }
        }

        private void LoadDashboardStats()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Total Books
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Books", conn))
                    lblBooks.Text = cmd.ExecuteScalar().ToString();

                // Total Users (members only)
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Role='Member'", conn))
                    lblUsers.Text = cmd.ExecuteScalar().ToString();

                // Books Issued
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM BorrowRecords WHERE ReturnDate IS NULL", conn))
                    lblIssued.Text = cmd.ExecuteScalar().ToString();

                // Overdue Books
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM BorrowRecords WHERE DueDate < GETDATE() AND ReturnDate IS NULL", conn))
                    lblOverdue.Text = cmd.ExecuteScalar().ToString();
            }
        }

        private void BindRecentActivity()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter(@"
                    SELECT TOP 5 u.FullName, b.Title, br.BorrowDate, br.DueDate
                    FROM BorrowRecords br
                    JOIN Books b ON br.BookID = b.BookID
                    JOIN Users u ON br.UserID = u.Id
                    ORDER BY br.BorrowDate DESC", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                GridViewRecent.DataSource = dt;
                GridViewRecent.DataBind();
            }
        }

        // Navigation
        protected void btnManageUsers_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageUsers.aspx");
        }

        protected void btnManageBooks_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Shared/ManageBooks.aspx");
        }

        protected void btnBookReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("BookReport.aspx");
        }

        // Logout
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}
