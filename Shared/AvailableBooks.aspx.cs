using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace LMS5.Shared
{
    public partial class AvailableBooks : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDBConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
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
                    INNER JOIN Categories C ON B.CategoryId = C.CategoryId";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridViewAvailableBooks.DataSource = dt;
                GridViewAvailableBooks.DataBind();
            }
        }

        protected void GridViewAvailableBooks_RowDataBound(object sender, GridViewRowEventArgs e)
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
                        btnBorrow.CssClass = "btn btn-primary btn-sm";
                        btnBorrow.CommandName = "Borrow";
                        btnBorrow.CommandArgument = drv["BookId"].ToString();
                    }
                }
            }
        }

        protected void GridViewAvailableBooks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Borrow")
            {
                int bookId = Convert.ToInt32(e.CommandArgument);
                int userId = Convert.ToInt32(Session["UserId"]);

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
                }
                catch (SqlException ex)
                {
                    lblMessage.Text = ex.Message;
                    lblMessage.CssClass = "text-danger";
                }

                BindAvailableBooks(); // Refresh grid to update borrow status
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string role = Session["Role"]?.ToString();
            if (role == "Admin")
                Response.Redirect("~/Admin/AdminDashboard.aspx");
            else if (role == "Librarian")
                Response.Redirect("~/Librarian/LibrarianDashboard.aspx");
            else
                Response.Redirect("~/Member/MemberDashboard.aspx");
        }
    }
}
