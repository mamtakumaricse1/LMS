using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LMS5.Shared
{
    public partial class OverdueBooks : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDBConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
                BindOverdueBooks();
        }

        private void BindOverdueBooks()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Get fine per day from SystemSettings (default = 0.50)
                decimal finePerDay = 0.50m;
                SqlCommand cmdSetting = new SqlCommand("SELECT TOP 1 FinePerDay FROM SystemSettings ORDER BY Id DESC", conn);
                var result = cmdSetting.ExecuteScalar();
                if (result != null)
                    finePerDay = Convert.ToDecimal(result);

                // Get user details
                // Get user details
                int userId = 0;
                string role = "";

                SqlCommand cmdUser = new SqlCommand("SELECT Id, Role FROM Users WHERE Username=@Username", conn);
                cmdUser.Parameters.AddWithValue("@Username", User.Identity.Name);
                using (SqlDataReader rdr = cmdUser.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        userId = Convert.ToInt32(rdr["Id"]);
                        role = rdr["Role"].ToString();
                    }
                }
                // Build query
                string query = $@"
                    SELECT 
                        br.Id AS BorrowId,
                        u.Username,
                        b.Title AS BookTitle,
                        br.BorrowDate,
                        br.DueDate,
                        DATEDIFF(DAY, br.DueDate, GETDATE()) AS DaysOverdue,
                        DATEDIFF(DAY, br.DueDate, GETDATE()) * {finePerDay} AS FineAmount
                    FROM BorrowRecords br
                    INNER JOIN Users u ON br.UserId = u.Id
                    INNER JOIN Books b ON br.BookId = b.BookId
                    WHERE br.ReturnDate IS NULL
                      AND br.DueDate < GETDATE()
                      AND br.Id = (
                            SELECT MAX(Id) 
                            FROM BorrowRecords 
                            WHERE BookId = br.BookId AND ReturnDate IS NULL
                      )";

                // Restrict results if user is a Member
                if (role.Equals("Member", StringComparison.OrdinalIgnoreCase))
                    query += " AND br.UserId = @UserId";

                query += " ORDER BY DaysOverdue DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                if (role.Equals("Member", StringComparison.OrdinalIgnoreCase))
                    cmd.Parameters.AddWithValue("@UserId", userId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            GridViewOverdue.DataSource = dt;
            GridViewOverdue.DataBind();

            // Show message if no records
            lblMessage.Text = dt.Rows.Count == 0
                ? "No overdue books found."
                : "";
        }
    }
}
