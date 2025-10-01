using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace LMS5.Admin
{
    public partial class CreateBook : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDBConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAuthors();
                BindCategories();
                txtAddedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        private void BindAuthors()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT AuthorId, Name FROM Authors", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlAuthors.DataSource = dt;
                ddlAuthors.DataTextField = "Name";
                ddlAuthors.DataValueField = "AuthorId";
                ddlAuthors.DataBind();
                ddlAuthors.Items.Insert(0, new ListItem("--Select Author--", ""));
            }
        }

        private void BindCategories()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT CategoryId, Name FROM Categories", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlCategories.DataSource = dt;
                ddlCategories.DataTextField = "Name";
                ddlCategories.DataValueField = "CategoryId";
                ddlCategories.DataBind();
                ddlCategories.Items.Insert(0, new ListItem("--Select Category--", ""));
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            string title = txtTitle.Text.Trim();
            string isbn = txtISBN.Text.Trim();
            int authorId = Convert.ToInt32(ddlAuthors.SelectedValue);
            int categoryId = Convert.ToInt32(ddlCategories.SelectedValue);
            DateTime addedDate;

            if (!DateTime.TryParse(txtAddedDate.Text.Trim(), out addedDate))
            {
                lblMessage.Text = "Invalid Added Date!";
                lblMessage.CssClass = "text-danger";
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "INSERT INTO Books (Title, AuthorId, CategoryId, ISBN, AddedDate) " +
                                   "VALUES (@Title, @AuthorId, @CategoryId, @ISBN, @AddedDate)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@AuthorId", authorId);
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    cmd.Parameters.AddWithValue("@ISBN", isbn);
                    cmd.Parameters.AddWithValue("@AddedDate", addedDate);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMessage.Text = "Book added successfully!";
                lblMessage.CssClass = "text-success";
                ClearForm();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.CssClass = "text-danger";
            }
        }

        private void ClearForm()
        {
            txtTitle.Text = "";
            txtISBN.Text = "";
            ddlAuthors.SelectedIndex = 0;
            ddlCategories.SelectedIndex = 0;
            txtAddedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            // Check query string to determine origin
            string from = Request.QueryString["from"];
            if (!string.IsNullOrEmpty(from) && from.ToLower() == "librarian")
                Response.Redirect("~/Librarian/LibrarianDashboard.aspx");
            else
                Response.Redirect("~/Admin/ManageBooks.aspx");
        }
    }
}
