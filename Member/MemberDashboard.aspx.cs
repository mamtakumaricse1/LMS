using System;
using System.Data.SqlClient;
using System.Configuration;

namespace LMS5.Member
{
    public partial class MemberDashboard : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDBConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] != null)
                {
                    BindDashboardCounts();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        private void BindDashboardCounts()
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Available Books
                SqlCommand cmdAvailable = new SqlCommand(@"
                    SELECT COUNT(*) FROM Books b
                    WHERE NOT EXISTS (
                        SELECT 1 FROM BorrowRecords br
                        WHERE br.BookId = b.BookId AND br.ReturnDate IS NULL
                    )", conn);
                lblAvailableCount.Text = cmdAvailable.ExecuteScalar().ToString();

                // Borrowed Books (all borrowed books for this user)
                SqlCommand cmdBorrowed = new SqlCommand(@"
                    SELECT COUNT(*) FROM BorrowRecords
                    WHERE UserId=@UserId AND ReturnDate IS NULL", conn);
                cmdBorrowed.Parameters.AddWithValue("@UserId", userId);
                lblBorrowedCount.Text = cmdBorrowed.ExecuteScalar().ToString();

                // My Borrowed Books
                lblMyBooksCount.Text = lblBorrowedCount.Text; // same as borrowed

                // Overdue Books
                SqlCommand cmdOverdue = new SqlCommand(@"
                    SELECT COUNT(*) FROM BorrowRecords
                    WHERE UserId=@UserId AND ReturnDate IS NULL AND DueDate < GETDATE()", conn);
                cmdOverdue.Parameters.AddWithValue("@UserId", userId);
                lblOverdueCount.Text = cmdOverdue.ExecuteScalar().ToString();
            }
        }
    }
}
