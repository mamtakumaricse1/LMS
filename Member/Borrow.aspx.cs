using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using NLog;

namespace LMS5.Member
{
    public partial class Borrow : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDBConn"].ConnectionString;

        // ✅ Use class-based logger — NLog will automatically detect it as LMS5.Member.Borrow
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Only members can access
            if (Session["UserId"] == null || Session["Role"]?.ToString() != "Member")
            {
                logger.Warn("Unauthorized access attempt to Borrow page. Redirecting to Login.aspx");
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                try
                {
                    BindBooks();
                    logger.Info($"Borrow page loaded successfully for user '{Session["Username"]}'.");
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Error while loading Borrow page.");
                }
            }
        }

        /// <summary>
        /// Bind all books with their availability status
        /// </summary>
        private void BindBooks()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT B.BookId, B.Title, A.Name AS AuthorName, C.Name AS CategoryName, B.ISBN,
                           CASE WHEN EXISTS(
                               SELECT 1 FROM BorrowRecords BR
                               WHERE BR.BookId = B.BookId AND BR.ReturnDate IS NULL
                           ) THEN 'Borrowed' ELSE 'Available' END AS Status
                    FROM Books B
                    INNER JOIN Authors A ON B.AuthorId = A.AuthorId
                    INNER JOIN Categories C ON B.CategoryId = C.CategoryId";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridViewBooks.DataSource = dt;
                GridViewBooks.DataBind();
            }
        }

        /// <summary>
        /// Customize row appearance and borrow button
        /// </summary>
        protected void GridViewBooks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                string status = drv["Status"].ToString();

                Button btnBorrow = (Button)e.Row.FindControl("btnBorrow");
                if (btnBorrow != null)
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
                        btnBorrow.CssClass = "btn btn-success btn-sm";
                        btnBorrow.CommandName = "Borrow";
                        btnBorrow.CommandArgument = drv["BookId"].ToString();
                    }
                }
            }
        }

        /// <summary>
        /// Handle borrow command and log the action
        /// </summary>
        protected void GridViewBooks_RowCommand(object sender, GridViewCommandEventArgs e)
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

                    lblMessage.Text = "✅ Book borrowed successfully!";
                    lblMessage.CssClass = "text-success";

                    // ✅ Log success
                    logger.Info($"Member '{memberName}' (UserId={userId}) borrowed BookId={bookId} successfully on {DateTime.Now:yyyy-MM-dd HH:mm:ss}.");

                    BindBooks();
                }
                catch (SqlException ex)
                {
                    lblMessage.Text = "⚠ Database Error: " + ex.Message;
                    lblMessage.CssClass = "text-danger";

                    // ❌ Log SQL error
                    logger.Error(ex, $"SQL error while member '{memberName}' (UserId={userId}) tried to borrow BookId={bookId}.");
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "⚠ Unexpected Error: " + ex.Message;
                    lblMessage.CssClass = "text-danger";

                    // 🚨 Log unhandled errors
                    logger.Fatal(ex, $"Unexpected error during borrow attempt by member '{memberName}' (UserId={userId}) for BookId={bookId}.");
                }
            }
        }
    }
}
