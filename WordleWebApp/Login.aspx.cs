using Microsoft.Ajax.Utilities;
using System;
using System.Web;
using System.Net;
using WordleWebApp.AuthServiceReferenceAsuHosted;
using Security;
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
                if(saveUsernameCB.Checked)
                {
                    //Storing username and hashed password as cookies if the checkbox is checked written by Alex 4/12
                    myCookies["username"] = username;
                    myCookies["hashedPassword"] = hashed; 
                    myCookies.Expires = DateTime.Now.AddMonths(6);
                    Response.Cookies.Add(myCookies);
                }

                // Store the username so we can display it on Default
                Session["Username"] = username;
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblRegisterError.Text = res.Message;
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            string hashed = PasswordHasher.HashPassword(password);
            Service1Client authClient = new Service1Client();

            AuthResult res = authClient.Login(username, hashed, Server.MapPath("~/App_Data/Users.xml"), Server.MapPath("~/App_Data/LoginAttempts.xml"));
            
            if (res.Success)
            {
                // Store the username so we can display it on Default
                Session["Username"] = username;
               // add this for 6
               // FormsAuthentication
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblError.Text = res.Message;
            }
        }
    }
}
