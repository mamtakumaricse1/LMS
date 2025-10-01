using System;

namespace LMS5.Librarian
{
    public partial class LibrarianDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnManageBooks_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageBooks.aspx");
        }

        protected void btnCreateBook_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateBook.aspx");
        }

        protected void btnAvailableBooks_Click(object sender, EventArgs e)
        {
            Response.Redirect("AvailableBooks.aspx");
        }

        protected void btnBorrowRecords_Click(object sender, EventArgs e)
        {
            Response.Redirect("BorrowRecords.aspx");
        }
    }
}
