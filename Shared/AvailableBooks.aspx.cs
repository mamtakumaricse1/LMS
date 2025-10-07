using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using NLog;

namespace LMS5.Shared
{
    public partial class AvailableBooks : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDBConn"].ConnectionString;

        // Logger for borrow operations
        private static readonly Logger BorrowLogger = LogManager.GetLogger("Borrow");

        protected void Page_Load(object sender, EventArgs e)
        {
            // Ensure user is logged in
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                BindAvailableBooks();
            }
        }

        /// <summary>
        /// Bind all books with their status
        /// </summary>
        private void BindAvailableBooks()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
            SELECT B.BookId, B.Title, B.ISBN, A.Name AS AuthorName, C.Name AS CategoryName,
                   CASE WHEN EXISTS(
                        SELECT 1 FROM BorrowRecords BR
                        WHERE BR.BookId = B.BookId AND BR.ReturnDate IS NULL
                   ) THEN 'Borrowed' ELSE 'Available' END AS Status
            FROM Books B
            INNER JOIN Authors A ON B.AuthorId = A.AuthorId
            INNER JOIN Categories C ON B.CategoryId = C.CategoryId
            ORDER BY 
                CASE 
                    WHEN EXISTS(SELECT 1 FROM BorrowRecords BR WHERE BR.BookId = B.BookId AND BR.ReturnDate IS NULL) 
                        THEN 1 ELSE 0 
                END, 
                B.Title ASC"; // sort by status first, then alphabetically by title

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridViewAvailableBooks.DataSource = dt;
                GridViewAvailableBooks.DataBind();
            }
        }


        /// <summary>
        /// Customize row appearance and control borrow button visibility
        /// </summary>
        protected void GridViewAvailableBooks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                string status = drv["Status"].ToString();

                // Status label
                var lblStatus = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Row.FindControl("lblStatus");
                if (lblStatus != null)
                {
                    lblStatus.InnerText = status;
                    if (status == "Available")
                        lblStatus.Attributes["class"] = "badge bg-success"; // green
                    else
                        lblStatus.Attributes["class"] = "badge bg-danger";   // red
                }

                // Borrow button
                Button btnBorrow = (Button)e.Row.FindControl("btnBorrow");
                string role = Session["Role"]?.ToString();

                if (btnBorrow != null)
                {
                    if (role == "Member")
                    {
                        if (status == "Borrowed")
                        {
                            btnBorrow.Text = "Borrowed";
                            btnBorrow.Enabled = false;
                            btnBorrow.CssClass = "btn btn-secondary btn-sm";
                        }
                        else
                        {
                            btnBorrow.Text = "Borrow";
                            btnBorrow.Enabled = true;
                            btnBorrow.CssClass = "btn btn-primary btn-sm";
                            btnBorrow.CommandName = "Borrow";
                            btnBorrow.CommandArgument = drv["BookId"].ToString();
                        }
                    }
                    else
                    {
                        btnBorrow.Visible = false; // hide for Admin/Librarian
                    }
                }
            }
        }

        /// <summary>
        /// Handle borrow command when member clicks Borrow button
        /// </summary>
        protected void GridViewAvailableBooks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Borrow")
            {
                int bookId = Convert.ToInt32(e.CommandArgument);
                int userId = Convert.ToInt32(Session["UserId"]);
                string memberName = Session["Username"]?.ToString() ?? "Unknown";

                try
                {
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        SqlCommand cmd = new SqlCommand("sp_BorrowBook", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@BookId", bookId);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }

                    lblMessage.Text = "Book borrowed successfully!";
                    lblMessage.CssClass = "text-success";

                    // Log borrow action
                    BorrowLogger.Info($"Member '{memberName}' (UserId={userId}) borrowed BookId={bookId} at {DateTime.Now}");
                }
                catch (SqlException ex)
                {
                    lblMessage.Text = ex.Message;
                    lblMessage.CssClass = "text-danger";

                    // Log error
                    BorrowLogger.Error(ex, $"Failed borrow attempt by Member '{memberName}' for BookId={bookId}");
                }

                BindAvailableBooks(); // Refresh grid to update status
            }
        }

        /// <summary>
        /// Navigate back to dashboard based on role
        /// </summary>
       
    }
}
