using System;

namespace LMS5.Librarian
{
    public partial class LibrarianDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // You can add future stats or summary code here
        }

        protected void btnManageBooks_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Shared/ManageBooks.aspx");
        }


        protected void btnAvailableBooks_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Shared/AvailableBooks.aspx");
        }

        protected void btnBorrowRecords_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Shared/BorrowRecords.aspx");
        }

        protected void btnOverdueBooks_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Shared/OverdueBooks.aspx");
        }
    }
}
