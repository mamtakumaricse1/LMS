using System;
using System.Web;
using System.Web.Security;
using System.Web.Routing;
using System.Web.UI; // Needed for ScriptManager

namespace LMS5
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);

            // Register jQuery for ScriptManager references
            ScriptManager.ScriptResourceMapping.AddDefinition(
                "jquery",
                new ScriptResourceDefinition
                {
                    Path = "~/Scripts/jquery-3.6.0.min.js",     // make sure this file exists in your project
                    DebugPath = "~/Scripts/jquery-3.6.0.js",    // optional debug version
                    CdnPath = "https://code.jquery.com/jquery-3.6.0.min.js",
                    CdnDebugPath = "https://code.jquery.com/jquery-3.6.0.js"
                });
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            // Map clean URLs to your WebForms pages
            routes.MapPageRoute("LoginRoute", "Login", "~/Login.aspx");
            routes.MapPageRoute("BooksRoute", "Books", "~/Books.aspx");
            routes.MapPageRoute("BorrowRoute", "Borrow", "~/Borrow.aspx");
            // Add more routes as needed
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            // This fires for every request after authentication
            if (HttpContext.Current.User != null &&
                HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                FormsAuthenticationTicket ticket = id.Ticket;

                // Role comes from the UserData field of the ticket (set in Login.aspx.cs)
                string[] roles = ticket.UserData.Split(',');

                // Assign the role(s) to the current user context
                HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(id, roles);
            }
        }
    }
}
