using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMS5.Shared
{
    public partial class ManageBooks : System.Web.UI.Page
    {
        private readonly string connStr = ConfigurationManager.ConnectionStrings["MyDBConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBooks();
                string role = Session["Role"]?.ToString();
                if (role == "Librarian")
                    btnAddBook.Visible = true; // Librarian can add books
            }
        }

        private void BindBooks()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"
            SELECT b.BookId, b.Title, b.ISBN,
                   a.Name AS AuthorName,
                   c.Name AS CategoryName
            FROM Books b
            JOIN Authors a ON b.AuthorId = a.AuthorId
            JOIN Categories c ON b.CategoryId = c.CategoryId
        ";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridViewBooks.DataSource = dt;
                GridViewBooks.DataBind();
            }
        }


        protected void btnAddBook_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Shared/AddBook.aspx");
        }

        protected void GridViewBooks_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewBooks.EditIndex = e.NewEditIndex;
            BindBooks();
        }

        protected void GridViewBooks_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewBooks.EditIndex = -1;
            BindBooks();
        }

        protected void GridViewBooks_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int bookId = Convert.ToInt32(GridViewBooks.DataKeys[e.RowIndex].Value);
            GridViewRow row = GridViewBooks.Rows[e.RowIndex];
            string isbn = ((TextBox)row.FindControl("txtISBN")).Text;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Books SET ISBN=@ISBN WHERE BookId=@BookId", con);
                cmd.Parameters.AddWithValue("@ISBN", isbn);
                cmd.Parameters.AddWithValue("@BookId", bookId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            GridViewBooks.EditIndex = -1;
            lblMessage.Text = "Book updated successfully!";
            lblMessage.CssClass = "text-success";
            BindBooks();
        }

        protected void GridViewBooks_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int bookId = Convert.ToInt32(GridViewBooks.DataKeys[e.RowIndex].Value);
            string role = Session["Role"]?.ToString();

            if (role == "Librarian")
            {
                lblMessage.Text = "Librarians are not allowed to delete books.";
                lblMessage.CssClass = "text-danger";
                return;
            }

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Books WHERE BookId=@BookId", con);
                cmd.Parameters.AddWithValue("@BookId", bookId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            lblMessage.Text = "Book deleted successfully!";
            lblMessage.CssClass = "text-success";
            BindBooks();
        }

        protected void GridViewBooks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string role = Session["Role"]?.ToString();

                // Hide Delete button for Librarian
                if (role == "Librarian")
                {
                    foreach (Control ctrl in e.Row.Cells[e.Row.Cells.Count - 1].Controls)
                    {
                        if (ctrl is LinkButton btn && btn.CommandName == "Delete")
                            btn.Visible = false;
                    }
                }
            }
        }
    }
}
