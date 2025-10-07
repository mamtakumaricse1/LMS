using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace LMS5.Member
{
    public partial class MyBooks : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDBConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null || Session["Role"]?.ToString() != "Member")
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
                BindMyBooks();
        }

        private void BindMyBooks()
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("sp_GetMyBorrowedBooks", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                if (!dt.Columns.Contains("DueDate"))
                    dt.Columns.Add("DueDate", typeof(DateTime));

                if (!dt.Columns.Contains("Status"))
                    dt.Columns.Add("Status", typeof(string));

                foreach (DataRow row in dt.Rows)
                {
                    DateTime borrowDate = Convert.ToDateTime(row["BorrowDate"]);
                    row["DueDate"] = borrowDate.AddDays(15);
                    row["Status"] = row["ReturnDate"] == DBNull.Value ? "Borrowed" : "Returned";
                }

                // ✅ Sort so that Borrowed books appear first, then Returned
                DataView dv = dt.DefaultView;
                dv.Sort = "Status ASC, BorrowDate DESC";
                // “Borrowed” < “Returned” alphabetically, so ASC works fine

                GridViewMyBooks.DataSource = dv;
                GridViewMyBooks.DataBind();
            }
        }

        protected void GridViewMyBooks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                Button btnReturn = (Button)e.Row.FindControl("btnReturn");

                bool returned = drv["ReturnDate"] != DBNull.Value;
                btnReturn.Enabled = !returned;
                btnReturn.CssClass = returned ? "btn btn-secondary btn-sm" : "btn btn-warning btn-sm";

                DateTime dueDate = Convert.ToDateTime(drv["DueDate"]);
                if (!returned && DateTime.Now > dueDate)
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.LightCoral;
                }
            }
        }

        protected void GridViewMyBooks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ReturnBook")
            {
                int borrowId = Convert.ToInt32(e.CommandArgument);

                try
                {
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        SqlCommand cmd = new SqlCommand("sp_ReturnBook", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BorrowId", borrowId);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }

                    lblMessage.Text = "Book returned successfully!";
                    lblMessage.CssClass = "text-success";
                }
                catch (SqlException ex)
                {
                    lblMessage.Text = ex.Message;
                    lblMessage.CssClass = "text-danger";
                }

                BindMyBooks();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Member/MemberDashboard.aspx");
        }
    }
}
