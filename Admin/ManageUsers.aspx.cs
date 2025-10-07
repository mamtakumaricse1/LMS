using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace LMS5.Admin
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDBConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindUsers();
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

        protected void GridViewUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            lblMsg.Text = ""; // hide message when editing
            GridViewUsers.EditIndex = e.NewEditIndex;
            BindUsers();
        }

        protected void GridViewUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewUsers.EditIndex = -1;
            BindUsers();
        }

        protected void GridViewUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(GridViewUsers.DataKeys[e.RowIndex].Value);
            GridViewRow row = GridViewUsers.Rows[e.RowIndex];

            TextBox txtUsername = (TextBox)row.FindControl("txtUsername");
            TextBox txtFullName = (TextBox)row.FindControl("txtFullName");
            DropDownList ddlRole = (DropDownList)row.FindControl("ddlRole");

            string username = txtUsername.Text.Trim();
            string fullName = txtFullName.Text.Trim();
            string role = ddlRole.SelectedValue;

            // Server-side validation
            if (!Regex.IsMatch(username, "^[A-Za-z]+$"))
            {
                lblMsg.Text = "Username must contain only letters";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }
            if (!Regex.IsMatch(fullName, "^[A-Za-z\\s]+$"))
            {
                lblMsg.Text = "Full Name must contain only letters";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }
            if (role != "Admin" && role != "Librarian" && role != "Member")
            {
                lblMsg.Text = "Invalid role selected";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

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

        protected void GridViewUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
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

        protected void GridViewUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Preselect the correct role in edit mode
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex == GridViewUsers.EditIndex)
            {
                DropDownList ddlRole = (DropDownList)e.Row.FindControl("ddlRole");
                if (ddlRole != null)
                {
                    string role = DataBinder.Eval(e.Row.DataItem, "Role").ToString();
                    ddlRole.SelectedValue = role;
                }
            }
        }
    }
}
