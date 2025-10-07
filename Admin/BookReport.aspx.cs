using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using NLog;

namespace LMS5.Admin
{
    public partial class BookReport : System.Web.UI.Page
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        string connStr = ConfigurationManager.ConnectionStrings["MyDBConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBooks();
                BindStats();
            }
        }

        private void BindBooks(string author = "", string category = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = @"
                        SELECT B.BookId, B.Title, B.ISBN,
                               A.Name AS AuthorName, C.Name AS CategoryName,
                               B.AddedDate,
                               CASE WHEN EXISTS(
                                   SELECT 1 FROM BorrowRecords BR
                                   WHERE BR.BookId = B.BookId AND BR.ReturnDate IS NULL
                               ) THEN 'Borrowed' ELSE 'Available' END AS Status
                        FROM Books B
                        INNER JOIN Authors A ON B.AuthorId = A.AuthorId
                        INNER JOIN Categories C ON B.CategoryId = C.CategoryId
                        WHERE (@AuthorName = '' OR A.Name LIKE '%' + @AuthorName + '%')
                          AND (@CategoryName = '' OR C.Name LIKE '%' + @CategoryName + '%')";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@AuthorName", author.Trim());
                    cmd.Parameters.AddWithValue("@CategoryName", category.Trim());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    GridViewBooks.DataSource = dt;
                    GridViewBooks.DataBind();

                    logger.Info($"BookReport viewed. Author filter: '{author}', Category filter: '{category}', Rows: {dt.Rows.Count}");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error loading books: " + ex.Message;
                lblMessage.CssClass = "text-danger";
                logger.Error(ex, "Error loading BookReport");
            }
        }

        private void BindStats()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmdTotal = new SqlCommand("SELECT COUNT(*) FROM Books", conn);
                    SqlCommand cmdAvailable = new SqlCommand("SELECT COUNT(*) FROM Books B WHERE NOT EXISTS (SELECT 1 FROM BorrowRecords BR WHERE BR.BookId = B.BookId AND BR.ReturnDate IS NULL)", conn);
                    SqlCommand cmdBorrowed = new SqlCommand("SELECT COUNT(*) FROM BorrowRecords WHERE ReturnDate IS NULL", conn);

                    lblTotalBooks.InnerText = cmdTotal.ExecuteScalar().ToString();
                    lblAvailableBooks.InnerText = cmdAvailable.ExecuteScalar().ToString();
                    lblBorrowedBooks.InnerText = cmdBorrowed.ExecuteScalar().ToString();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error loading stats");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindBooks(txtSearchAuthor.Text, txtSearchCategory.Text);
            BindStats();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSearchAuthor.Text = "";
            txtSearchCategory.Text = "";
            BindBooks();
            BindStats();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/AdminDashboard.aspx");
        }
    }
}
