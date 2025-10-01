using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;

namespace LMS5.Admin
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDBConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUsers();
            }
        }

        private void BindUsers()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT Id, Username, FullName, Role, CreatedDate FROM Users";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridViewUsers.DataSource = dt;
                GridViewUsers.DataBind();
            }
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }

        protected void GridViewUsers_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            GridViewUsers.EditIndex = e.NewEditIndex;
            BindUsers();
        }

        protected void GridViewUsers_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            GridViewUsers.EditIndex = -1;
            BindUsers();
        }

        protected void GridViewUsers_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(GridViewUsers.DataKeys[e.RowIndex].Value);
            GridViewRow row = GridViewUsers.Rows[e.RowIndex];

            string username = ((System.Web.UI.WebControls.TextBox)row.Cells[1].Controls[0]).Text.Trim();
            string fullName = ((System.Web.UI.WebControls.TextBox)row.Cells[2].Controls[0]).Text.Trim();
            string role = ((System.Web.UI.WebControls.TextBox)row.Cells[3].Controls[0]).Text.Trim();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "UPDATE Users SET Username=@Username, FullName=@FullName, Role=@Role WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            GridViewUsers.EditIndex = -1;
            BindUsers();
            lblMsg.Text = "User updated successfully!";
            lblMsg.ForeColor = System.Drawing.Color.Green;
        }

        protected void GridViewUsers_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GridViewUsers.DataKeys[e.RowIndex].Value);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "DELETE FROM Users WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            BindUsers();
            lblMsg.Text = "User deleted successfully!";
            lblMsg.ForeColor = System.Drawing.Color.Red;
        }
    }
}
