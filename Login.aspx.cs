using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using NLog;

namespace LMS5
{
    public partial class Login : System.Web.UI.Page
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        string connStr = ConfigurationManager.ConnectionStrings["MyDBConn"].ConnectionString;

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUser.Text.Trim();
                string password = txtPass.Text.Trim();
                string role = ddlRole.SelectedValue;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
                {
                    lblMsg.Text = "Please enter username, password, and select role.";
                    logger.Info("login failed for:" + username);
                    return;
                }

                string passwordHash = ComputeSha256Hash(password);

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string getUserQ = @"SELECT Id FROM Users 
                                     WHERE Username=@Username 
                                       AND PasswordHash=@PasswordHash 
                                       AND Role=@Role";

                    string loginLoggingSucessQ = @"INSERT INTO UsersLoginInfo 
                                                (Username, PlainPassword, PasswordHash, Role)
                                                VALUES (@UserName, @PlainPassword, @PasswordHash, @Role)";

                    using (SqlCommand cmd = new SqlCommand(getUserQ, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        cmd.Parameters.AddWithValue("@Role", role);

                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            int userId = Convert.ToInt32(result);

                            // store user id in session
                            Session["UserId"] = userId;
                            Session["Username"] = username;
                            Session["Role"] = role;

                            // create FormsAuthentication ticket
                            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                                1, username, DateTime.Now, DateTime.Now.AddMinutes(30), false, role
                            );

                            string encTicket = FormsAuthentication.Encrypt(ticket);
                            Response.Cookies.Add(new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                            // inserting to logging db
                            // complete the code
                            using (SqlCommand logCmd = new SqlCommand(loginLoggingSucessQ, conn))
                            {
                                logCmd.Parameters.AddWithValue("@UserName", username);
                                logCmd.Parameters.AddWithValue("@PlainPassword", ""); // not keeping bcz valid creds
                                logCmd.Parameters.AddWithValue("@PasswordHash", passwordHash); // storing hashed pwd
                                logCmd.Parameters.AddWithValue("@Role", role);
                                logCmd.ExecuteNonQuery();
                            }
                            logger.Info("Login successfull for user in mode: " + role);

                            // redirect by role
                            if (role == "Admin") {
                                Response.Redirect("~/Admin/AdminDashboard.aspx");
                            }
                            else if (role == "Librarian")
                                Response.Redirect("~/Librarian/LibrarianDashboard.aspx");
                            else
                                Response.Redirect("~/Member/MemberDashboard.aspx");
                        }
                        else
                        {
                            lblMsg.Text = "Invalid username, password, or role.";
                            using (SqlCommand logCmd = new SqlCommand(loginLoggingSucessQ, conn))
                            {
                                logCmd.Parameters.AddWithValue("@UserName", username);
                                logCmd.Parameters.AddWithValue("@PlainPassword", password); // storing hashed pwd
                                logCmd.Parameters.AddWithValue("@PasswordHash", "");
                                logCmd.Parameters.AddWithValue("@Role", role);
                                logCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error during login for user: " + txtUser.Text.Trim());
                lblMsg.Text = "Error during login: " + ex.Message;
            }
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }
    }
}
