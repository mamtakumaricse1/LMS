using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMS5.Admin
{
    public partial class ManageBooks : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDBConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBooks();
            }
        }

        private void BindBooks()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"SELECT B.BookId, B.Title, B.ISBN, 
                                 A.Name AS AuthorName, C.Name AS CategoryName, 
                                 B.AuthorId, B.CategoryId
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

        protected void GridViewBooks_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewBooks.EditIndex = e.NewEditIndex;
            BindBooks();
            BindDropdowns();
        }

        protected void GridViewBooks_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewBooks.EditIndex = -1;
            BindBooks();
        }

        protected void GridViewBooks_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridViewBooks.Rows[e.RowIndex];
                int bookId = Convert.ToInt32(GridViewBooks.DataKeys[e.RowIndex].Value);

                string title = ((TextBox)row.Cells[1].Controls[0]).Text.Trim();
                string isbn = ((TextBox)row.Cells[4].Controls[0]).Text.Trim();

                DropDownList ddlAuthors = (DropDownList)row.FindControl("ddlAuthors");
                DropDownList ddlCategories = (DropDownList)row.FindControl("ddlCategories");

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

        protected void GridViewBooks_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int bookId = Convert.ToInt32(GridViewBooks.DataKeys[e.RowIndex].Value);

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "DELETE FROM Books WHERE BookId=@BookId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@BookId", bookId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMessage.Text = "Book deleted successfully!";
                lblMessage.CssClass = "text-success mb-3";
                BindBooks();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.CssClass = "text-danger mb-3";
            }
        }

        private void BindDropdowns()
        {
            foreach (GridViewRow row in GridViewBooks.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow && row.RowIndex == GridViewBooks.EditIndex)
                {
                    DropDownList ddlAuthors = (DropDownList)row.FindControl("ddlAuthors");
                    DropDownList ddlCategories = (DropDownList)row.FindControl("ddlCategories");

                    // Bind Authors
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT AuthorId, Name FROM Authors", conn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlAuthors.DataSource = dt;
                        ddlAuthors.DataTextField = "Name";
                        ddlAuthors.DataValueField = "AuthorId";
                        ddlAuthors.DataBind();

                        ddlAuthors.SelectedValue = row.Cells[1].Text == "" ? "1" : DataBinder.Eval(row.DataItem, "AuthorId").ToString();
                    }

                    // Bind Categories
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT CategoryId, Name FROM Categories", conn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlCategories.DataSource = dt;
                        ddlCategories.DataTextField = "Name";
                        ddlCategories.DataValueField = "CategoryId";
                        ddlCategories.DataBind();

                        ddlCategories.SelectedValue = row.Cells[1].Text == "" ? "1" : DataBinder.Eval(row.DataItem, "CategoryId").ToString();
                    }
                }
            }
        }

        protected void btnAddBook_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateBook.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/AdminDashboard.aspx");
        }
    }
}
