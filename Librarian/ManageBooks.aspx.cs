using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMS5.Librarian
{
    public partial class ManageBooks : System.Web.UI.Page
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
                BindBooks();
            }
        }

        private void BindBooks()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT B.BookId, B.Title, B.ISBN, 
                           A.AuthorId, A.Name AS AuthorName, 
                           C.CategoryId, C.Name AS CategoryName
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

        protected void btnAddBook_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Librarian/CreateBook.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Librarian/LibrarianDashboard.aspx");
        }

        protected void GridViewBooks_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewBooks.EditIndex = e.NewEditIndex;
            BindBooks();

            GridViewRow row = GridViewBooks.Rows[e.NewEditIndex];

            DropDownList ddlAuthors = (DropDownList)row.FindControl("ddlAuthors");
            DropDownList ddlCategories = (DropDownList)row.FindControl("ddlCategories");

            BindDropdowns(ddlAuthors, ddlCategories);

            DataRowView drv = (DataRowView)row.DataItem;
        }

        private void BindDropdowns(DropDownList ddlAuthors, DropDownList ddlCategories)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Authors
                SqlDataAdapter daAuthors = new SqlDataAdapter("SELECT AuthorId, Name FROM Authors", conn);
                DataTable dtAuthors = new DataTable();
                daAuthors.Fill(dtAuthors);

                ddlAuthors.DataSource = dtAuthors;
                ddlAuthors.DataTextField = "Name";
                ddlAuthors.DataValueField = "AuthorId";
                ddlAuthors.DataBind();

                // Categories
                SqlDataAdapter daCategories = new SqlDataAdapter("SELECT CategoryId, Name FROM Categories", conn);
                DataTable dtCategories = new DataTable();
                daCategories.Fill(dtCategories);

                ddlCategories.DataSource = dtCategories;
                ddlCategories.DataTextField = "Name";
                ddlCategories.DataValueField = "CategoryId";
                ddlCategories.DataBind();
            }
        }

        protected void GridViewBooks_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridViewBooks.Rows[e.RowIndex];
                int bookId = Convert.ToInt32(GridViewBooks.DataKeys[e.RowIndex].Value);

                string title = ((TextBox)row.Cells[1].Controls[0]).Text.Trim();
                string isbn = ((TextBox)row.FindControl("txtISBN")).Text.Trim();

                DropDownList ddlAuthors = (DropDownList)row.FindControl("ddlAuthors");
                DropDownList ddlCategories = (DropDownList)row.FindControl("ddlCategories");

                // ✅ Server-side ISBN validation
                if (!Regex.IsMatch(isbn, @"^\d{9,13}$"))
                {
                    lblMessage.Text = "Error: ISBN must be 9–13 digits.";
                    lblMessage.CssClass = "text-danger mb-3";
                    return;
                }

                int authorId = Convert.ToInt32(ddlAuthors.SelectedValue);
                int categoryId = Convert.ToInt32(ddlCategories.SelectedValue);

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = @"UPDATE Books SET Title=@Title, AuthorId=@AuthorId, CategoryId=@CategoryId, ISBN=@ISBN WHERE BookId=@BookId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@AuthorId", authorId);
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    cmd.Parameters.AddWithValue("@ISBN", isbn);
                    cmd.Parameters.AddWithValue("@BookId", bookId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMessage.Text = "Book updated successfully!";
                lblMessage.CssClass = "text-success mb-3";
                GridViewBooks.EditIndex = -1;
                BindBooks();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.CssClass = "text-danger mb-3";
            }
        }

        protected void GridViewBooks_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewBooks.EditIndex = -1;
            BindBooks();
        }

        protected void GridViewBooks_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int bookId = Convert.ToInt32(GridViewBooks.DataKeys[e.RowIndex].Value);

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // Check references in BorrowRecords
                    string checkQuery = "SELECT COUNT(*) FROM BorrowRecords WHERE BookId=@BookId";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@BookId", bookId);

                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        lblMessage.Text = "⚠️ Cannot delete this book because it is referenced in BorrowRecords.";
                        lblMessage.CssClass = "text-danger mb-3";
                        return;
                    }

                    // No references → safe to delete
                    string query = "DELETE FROM Books WHERE BookId=@BookId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    cmd.ExecuteNonQuery();

                    lblMessage.Text = "✅ Book deleted successfully!";
                    lblMessage.CssClass = "text-success mb-3";
                    BindBooks();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.CssClass = "text-danger mb-3";
            }
        }

    }
}
