using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace LMS5.Admin
{
    public partial class MonthlyReport : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDBConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMonthlyReport();
            }
        }

        private void BindMonthlyReport()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT 
                        FORMAT(BorrowDate,'yyyy-MM') AS [Month],
                        COUNT(*) AS TotalBorrowed,
                        SUM(CASE WHEN ReturnDate IS NULL THEN 1 ELSE 0 END) AS CurrentlyBorrowed
                    FROM BorrowRecords
                    GROUP BY FORMAT(BorrowDate,'yyyy-MM')
                    ORDER BY [Month] DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }
            GridViewMonthly.DataSource = dt;
            GridViewMonthly.DataBind();
        }
    }
}
