using System;

namespace LMS5.Member
{
    public partial class MemberDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnAvailableBooks_Click(object sender, EventArgs e)
        {
            Response.Redirect("AvailableBooks.aspx");
        }


        protected void btnBorrowBook_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Member/Borrow.aspx");
        }

        protected void btnMyBooks_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Member/MyBooks.aspx");
        }
    }
}
