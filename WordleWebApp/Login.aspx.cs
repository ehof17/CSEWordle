using Microsoft.Ajax.Utilities;
using System;
using System.Web;
using WordleWebApp.AuthServiceReference;
using Security;
namespace WordleWebApp
{

    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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


            string hashed = PasswordHasher.HashPassword(password);
            Service1Client authClient = new Service1Client();

            // Todo: Handle this better. Maybe return an object with an error message prop
            // instead of just checking the string
            string success = authClient.Register(username, hashed, "Member.xml");
            if (success == "Registration successful.")
            {
                // Store the username so we can display it on Default
                Session["Username"] = username;
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblRegisterError.Text = success;
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            string hashed = PasswordHasher.HashPassword(password);
            bool bruh = PasswordHasher.VerifyPassword(password, hashed);

            Service1Client authClient = new Service1Client();


            // Todo: Handle this better. Maybe return an object with an error message prop
            // instead of just checking the string
            string success = authClient.Login(username, hashed, "Member.xml");
            if (success == "Login successful.")
            {
                // Store the username so we can display it on Default
                Session["Username"] = username;
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblError.Text = success;
            }
        }
    }
}
