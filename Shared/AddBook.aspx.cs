using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using NLog; // ✅ NLog namespace

namespace LMS5.Shared
{
    public partial class AddBook : System.Web.UI.Page
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger(); // ✅ Create logger

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
                SqlDataAdapter da = new SqlDataAdapter("SELECT AuthorId, Name FROM Authors ORDER BY Name", conn);
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
                SqlDataAdapter da = new SqlDataAdapter("SELECT CategoryId, Name FROM Categories ORDER BY Name", conn);
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
            int authorId;
            int categoryId;
            DateTime addedDate;

            if (!int.TryParse(ddlAuthors.SelectedValue, out authorId) || authorId <= 0)
            {
                lblMessage.Text = "Please select a valid Author.";
                lblMessage.CssClass = "text-danger";

                // ⚠️ Log invalid author selection
                logger.Warn("Invalid Author selection while adding book. Title='{0}', ISBN='{1}'", title, isbn);
                return;
            }

            if (!int.TryParse(ddlCategories.SelectedValue, out categoryId) || categoryId <= 0)
            {
                lblMessage.Text = "Please select a valid Category.";
                lblMessage.CssClass = "text-danger";

                // ⚠️ Log invalid category selection
                logger.Warn("Invalid Category selection while adding book. Title='{0}', ISBN='{1}'", title, isbn);
                return;
            }

            if (!DateTime.TryParse(txtAddedDate.Text.Trim(), out addedDate))
            {
                lblMessage.Text = "Invalid Added Date!";
                lblMessage.CssClass = "text-danger";

                // ⚠️ Log invalid date
                logger.Warn("Invalid date entered while adding book. Title='{0}', ISBN='{1}'", title, isbn);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = @"INSERT INTO Books (Title, AuthorId, CategoryId, ISBN, AddedDate) 
                                     VALUES (@Title, @AuthorId, @CategoryId, @ISBN, @AddedDate)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@AuthorId", authorId);
                        cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                        cmd.Parameters.AddWithValue("@ISBN", isbn);
                        cmd.Parameters.AddWithValue("@AddedDate", addedDate);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                lblMessage.Text = "✅ Book added successfully!";
                lblMessage.CssClass = "text-success";

                // ✅ Log success in business log
                logger.Info($"Book added successfully. Title='{title}', ISBN='{isbn}', AuthorId={authorId}, CategoryId={categoryId}");

                ClearForm();
            }
            catch (SqlException ex)
            {
                lblMessage.Text = "⚠ Database Error: " + ex.Message;
                lblMessage.CssClass = "text-danger";

                // ❌ Log SQL exception details
                logger.Error(ex, $"SQL error while adding book. Title='{title}', ISBN='{isbn}'");
            }
            catch (Exception ex)
            {
                lblMessage.Text = "⚠ Unexpected Error: " + ex.Message;
                lblMessage.CssClass = "text-danger";

                // 🚨 Log unexpected errors
                logger.Fatal(ex, $"Unexpected error while adding book. Title='{title}', ISBN='{isbn}'");
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
            string from = Request.QueryString["from"];
            if (!string.IsNullOrEmpty(from) && from.ToLower() == "librarian")
                Response.Redirect("~/Librarian/LibrarianDashboard.aspx");
            else
                Response.Redirect("~/Admin/ManageBooks.aspx");
        }
    }
}
