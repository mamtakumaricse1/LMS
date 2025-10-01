using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;

namespace LMS5
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDBConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("Login.aspx");
            }

            // Get role from FormsAuthenticationTicket
            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                string role = ticket.UserData; // UserData stores role

                if (role != "Admin")
                {
                    Response.Redirect("Login.aspx");
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                BindUsers();
            }
        }

        private void BindUsers()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT Id, Username, FullName, Role, CreatedDate FROM Users", conn);
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

        protected void btnEditUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageUsers.aspx");
        }

        protected void btnBookReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("BookReport.aspx");
        }
    }
}
