using System;
using System.Web;
using System.Web.Security;

namespace LMS5
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is authenticated
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;

                    string username = ticket.Name;
                    string role = ticket.UserData; // we stored role in UserData when creating ticket

                    lblWelcome.Text = $"Welcome, {username}";
                    lblWelcome.Visible = true;
                    btnLogout.Visible = true;
                    lnkLogin.Visible = false;

                    // Show menu panels according to role
                    pnlAdmin.Visible = role == "Admin";
                    pnlLibrarian.Visible = role == "Librarian";
                    pnlMember.Visible = role == "Member";
                }
                else
                {
                    lblWelcome.Visible = false;
                    btnLogout.Visible = false;
                    lnkLogin.Visible = true;

                    pnlAdmin.Visible = false;
                    pnlLibrarian.Visible = false;
                    pnlMember.Visible = false;
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/Login.aspx");
        }
    }
}
