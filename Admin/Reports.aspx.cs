using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace LMS5.Admin
{
    public partial class Reports : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDBConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindReport();
            }
        }

        private void BindReport()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT b.BookId, b.Title, a.Name AS AuthorName, c.Name AS CategoryName,
                        COUNT(br.Id) AS TimesBorrowed
                    FROM Books b
                    LEFT JOIN BorrowRecords br ON b.BookId = br.BookId
                    LEFT JOIN Authors a ON b.AuthorId = a.AuthorId
                    LEFT JOIN Categories c ON b.CategoryId = c.CategoryId
                    GROUP BY b.BookId, b.Title, a.Name, c.Name
                    ORDER BY TimesBorrowed DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }
            GridViewReport.DataSource = dt;
            GridViewReport.DataBind();
        }
    }
}
