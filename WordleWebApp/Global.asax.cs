using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace WordleWebApp
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

         
        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null) return;                          

            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            string[] roles = ticket.UserData.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var id = new FormsIdentity(ticket);
            var prn = new System.Security.Principal.GenericPrincipal(id, roles);
            Context.User = prn;
        }
    }
    }