using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LMS5.Librarian
{
    public partial class BorrowRecords : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDBConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindGrid();
        }

        private void BindGrid()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT br.Id, u.Username, b.Title AS BookTitle,
                           br.BorrowDate, br.ReturnDate
                    FROM BorrowRecords br
                    INNER JOIN Users u ON br.UserId = u.Id
                    INNER JOIN Books b ON br.BookId = b.BookId
                    ORDER BY br.BorrowDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvBorrowRecords.DataSource = dt;
                gvBorrowRecords.DataBind();
            }
        }

        protected void gvBorrowRecords_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ReturnBook")
            {
                int recordId = Convert.ToInt32(e.CommandArgument);

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string update = "UPDATE BorrowRecords SET ReturnDate = GETDATE() WHERE Id = @Id";
                    SqlCommand cmd = new SqlCommand(update, conn);
                    cmd.Parameters.AddWithValue("@Id", recordId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMessage.Text = "✅ Book marked as returned!";
                BindGrid();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("LibrarianDashboard.aspx");
        }
    }
}
