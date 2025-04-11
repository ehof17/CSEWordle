using System;
using System.Collections.Generic;

namespace WordleWebApp
{
    public class ServiceEntry
    {
        public string Provider { get; set; }
        public string ComponentType { get; set; }
        public string Operation { get; set; }
        public string Parameters { get; set; }
        public string ReturnType { get; set; }
        public string Description { get; set; }
        public string TryItLink { get; set; }
    }
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
            {
                lblUsername.Text = Session["Username"].ToString();
                if (!IsPostBack)
                {
                    List<ServiceEntry> entries = new List<ServiceEntry>
        {
            new ServiceEntry
            {
                Provider = "Eli Hoffman",
                ComponentType = "Web Service (WSDL)",
                Operation = "Login",
                Parameters = "string username, string hashedPassword, string xmlFile",
                ReturnType = "string",
                Description = "Validates a user's credentials against XML file records",
                TryItLink = "Login.aspx"
            },
            new ServiceEntry
            {
                Provider = "Eli Hoffman",
                ComponentType = "Web Service (WSDL)",
                Operation = "Register",
                Parameters = "string username, string hashedPassword, string xmlFile",
                ReturnType = "string",
                Description = "Adds a username and password to an XML file, to be logged into later",
                TryItLink = "Login.aspx"
            },
            new ServiceEntry
            {
                Provider = "Eli Hoffman",
                ComponentType = "DLL Function",
                Operation = "HashPassword",
                Parameters = "string password",
                ReturnType = "string",
                Description = "Hashes the password using SHA-256",
                TryItLink = "HashPassword.aspx"
            },
           
        };

                    ServiceDirectoryGrid.DataSource = entries;
                    ServiceDirectoryGrid.DataBind();
                }
            }
            else
            {
                // Not logged in - redirect to login
                Response.Redirect("Login.aspx");

                //lblUsername.Text = "Not Logged in";
            }
        }

        protected void btnGoToMember_Click(object sender, EventArgs e)
        {
            Response.Redirect("Member.aspx");
        }

        protected void btnGoToStaff_Click(object sender, EventArgs e)
        {
            Response.Redirect("Staff.aspx");
        }
    }
}
