using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;

namespace LMS5.Shared
{
    public partial class BorrowRecords : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDBConn"].ConnectionString;
        string role = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;
                    role = ticket.UserData;
                }

                BindRecords();
            }
        }

        private void BindRecords()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "";

                    if (role == "Admin")
                    {
                        // Admin sees all records, calculate DueDate = BorrowDate + 15 days
                        query = @"
                            SELECT 
                                br.Id AS BorrowId, 
                                b.Title AS BookTitle, 
                                u.FullName AS MemberName,
                                br.BorrowDate,
                                DATEADD(DAY, 15, br.BorrowDate) AS DueDate,
                                br.ReturnDate
                            FROM BorrowRecords br
                            INNER JOIN Books b ON br.BookId = b.BookId
                            INNER JOIN Users u ON br.UserId = u.Id
                            ORDER BY br.BorrowDate DESC";
                    }
                    else if (role == "Librarian")
                    {
                        // Librarian sees only active/overdue borrowings
                        query = @"
                            SELECT 
                                br.Id AS BorrowId, 
                                b.Title AS BookTitle, 
                                u.FullName AS MemberName,
                                br.BorrowDate,
                                DATEADD(DAY, 15, br.BorrowDate) AS DueDate,
                                br.ReturnDate
                            FROM BorrowRecords br
                            INNER JOIN Books b ON br.BookId = b.BookId
                            INNER JOIN Users u ON br.UserId = u.Id
                            WHERE br.ReturnDate IS NULL
                            ORDER BY DATEADD(DAY, 15, br.BorrowDate) ASC";
                    }

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    GridViewRecords.DataSource = dt;
                    GridViewRecords.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error loading records: " + ex.Message;
            }
        }
    }
}
