using Microsoft.Ajax.Utilities;
using System;
using System.Web;
using System.Net;
using WordleWebApp.AuthServiceReferenceAsuHosted;
using Security;
using System.Web.Security;
using System.Collections.Generic;
using System.Security.Principal;
namespace WordleWebApp
{

    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie myCookies = Request.Cookies["myCookieId"];

            if ((myCookies == null) || myCookies["username"] == "")
            {
                // new user or user didn't save username
            }
            else
            {
                txtUsername.Text = myCookies["username"];
            }
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsernameRegister.Text;
            string password = txtPasswordRegister.Text;
            string confirmPassword = txtPasswordConfirm.Text;

            if (password != confirmPassword)
            {
                lblRegisterError.Text = "Passwords don't match";
                return;
            }

            //create cookie object
            HttpCookie myCookies = new HttpCookie("myCookieId");

            string hashed = PasswordHasher.HashPassword(password);
            Service1Client authClient = new Service1Client();



            AuthResult res = authClient.Register(username, hashed, Server.MapPath("~/App_Data/Users.xml"));
            if (res.Success)
            {
                if (saveUsernameCB.Checked)
                {
                    //Storing username and hashed password as cookies if the checkbox is checked written by Alex 4/12
                    myCookies["username"] = username;
                    myCookies["hashedPassword"] = hashed;
                    myCookies.Expires = DateTime.Now.AddMonths(6);
                    Response.Cookies.Add(myCookies);
                }

                // Store the username so we can display it on Default
                Session["Username"] = username;
                // Users register, they go to Member page
                Response.Redirect("Member.aspx");
            }
            else
            {
                lblRegisterError.Text = res.Message;
            }
        }

        //Login either for Member or Staff
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            string hashed = PasswordHasher.HashPassword(password);
            Service1Client authClient = new Service1Client();

            AuthResult memberRes = authClient.Login(username, hashed,
                           Server.MapPath("~/App_Data/Users.xml"),
                           Server.MapPath("~/App_Data/LoginAttempts.xml"));

            AuthResult staffRes = authClient.Login(username, hashed,
                                 Server.MapPath("~/App_Data/Staff.xml"),
                                 Server.MapPath("~/App_Data/LoginAttempts.xml"));

            // Build a role list
            var roles = new List<string>();
            if (memberRes.Success) roles.Add("Member");
            if (staffRes.Success) roles.Add("Staff");

            if (roles.Count == 0)
            {
                lblError.Text = memberRes.Message;
                return;
            }

            string userData = string.Join(",", roles);

            var ticket = new FormsAuthenticationTicket(
                            1,
                            username,
                            DateTime.Now,
                            DateTime.Now.AddMinutes(30),
                            false,
                            userData,          // will contain 1 or multiple roles
                            FormsAuthentication.FormsCookiePath);

            string enc = FormsAuthentication.Encrypt(ticket);
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, enc));


            Session["Username"] = username;

            // First check the returnURL
            // So if the GoToStaff/Member button is clicked, the return url will be preserved and the button would go to the correct page
            string returnUrl = Request.QueryString["ReturnUrl"];
            if (!string.IsNullOrEmpty(returnUrl) &&
                UrlAuthorizationModule.CheckUrlAccessForPrincipal(returnUrl, new GenericPrincipal(new FormsIdentity(ticket),roles.ToArray()),"GET")) 
            {
                Response.Redirect(returnUrl);
                return;
            }

            // If theres no returnUrl, then staff can go to either staff or member page, but default to staff

            if (roles.Contains("Staff"))
                Response.Redirect("Staff.aspx");
            else
                Response.Redirect("Member.aspx");
        }
    }
}
