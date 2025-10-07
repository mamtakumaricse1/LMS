using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMS5.Admin
{
    public partial class BorrowRecords : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDBConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBorrowRecords();
            }
        }

        private void BindBorrowRecords()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("EXEC sp_GetBorrowRecords", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridViewBorrow.DataSource = dt;
                GridViewBorrow.DataBind();
            }
        }

        // Highlight overdue books
        protected void GridViewBorrow_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = DataBinder.Eval(e.Row.DataItem, "Status").ToString();
                if (status == "Overdue")
                {
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                }
                else if (status == "Borrowed")
                {
                    e.Row.BackColor = System.Drawing.Color.LightYellow;
                }
            }
        }
    }
}
