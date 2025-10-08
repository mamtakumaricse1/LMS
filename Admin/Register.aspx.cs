using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using NLog; // ✅ Import NLog

namespace LMS5
{
    public partial class Register : System.Web.UI.Page
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger(); // ✅ NLog logger
        string connStr = ConfigurationManager.ConnectionStrings["MyDBConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            string fullName = txtFullName.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string role = ddlRole.SelectedValue;

            string passwordHash = ComputeSha256Hash(password);

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "INSERT INTO Users (Username, FullName, PasswordHash, Role) VALUES (@Username, @FullName, @PasswordHash, @Role)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    cmd.Parameters.AddWithValue("@Role", role);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMsg.ForeColor = System.Drawing.Color.Green;
                lblMsg.Text = "User registered successfully!";
                ClearFields();

                // ✅ Log success
                logger.Info($"New user registered successfully: Username={username}, Role={role}, FullName={fullName}");
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    lblMsg.Text = "Username already exists. Choose another.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;

                    // ✅ Log duplicate username attempt
                    logger.Warn($"Attempted to register with existing username: {username}");
                }
                else
                {
                    lblMsg.Text = "Database error: " + ex.Message;
                    lblMsg.ForeColor = System.Drawing.Color.Red;

                    // ✅ Log SQL error details
                    logger.Error(ex, $"Database error while registering user: {username}");
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Unexpected error occurred.";
                lblMsg.ForeColor = System.Drawing.Color.Red;

                // ✅ Log any other unhandled exceptions
                logger.Fatal(ex, $"Unexpected error during user registration for: {username}");
            }
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }

        private void ClearFields()
        {
            txtFullName.Text = txtUsername.Text = txtPassword.Text = "";
            ddlRole.SelectedIndex = 0;
        }
    }
}
