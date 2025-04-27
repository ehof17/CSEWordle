using System;
using System.Collections.Generic;
using System.Web.UI;

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

            if (!IsPostBack)
            {

                if (Session["Username"] != null)
                {
                    lblUsername.Text = Session["Username"].ToString();
                }

                // …inside Page_Load
                List<ServiceEntry> entries = new List<ServiceEntry>
{
    new ServiceEntry
    {
        Provider       = "Alex Alvarado",
        ComponentType  = "Cookies",
        Operation      = "Saves username",
        Parameters     = "N/A",
        ReturnType     = "N/A",
        Description    = "When a user registers they can opt-in to cookies; the username will be pre-filled next time.",
        TryItLink      = "Login.aspx"
    },
    new ServiceEntry
    {
        Provider       = "Alex Alvarado",
        ComponentType  = "User Control",
        Operation      = "Staff page",
        Parameters     = "N/A",
        ReturnType     = "N/A",
        Description    = "Allows staff to enter new words and add staff users.",
        TryItLink      = "Staff.aspx"
    },
    new ServiceEntry
    {
        Provider       = "Alex Alvarado",
        ComponentType  = "Web Service (WSDL)",
        Operation      = "GenerateWord",
        Parameters     = "string gameWordsXml",
        ReturnType     = "string",
        Description    = "Returns a random word from the supplied XML word list.",
        TryItLink      = "WordleLogicTryIt.aspx"
    },
    new ServiceEntry
    {
        Provider       = "Alex Alvarado",
        ComponentType  = "Web Service (WSDL)",
        Operation      = "WordGuessChecker",
        Parameters     = "string userGuess, string actualWord",
        ReturnType     = "List<WordLetter>",
        Description    = "Compares a guess to the actual word and returns letter-by-letter status objects.",
        TryItLink      = "WordleLogicTryIt.aspx"
    },
    new ServiceEntry
    {
        Provider       = "Alex Alvarado",
        ComponentType  = "Web Service (WSDL)",
        Operation      = "IsValidGuess",
        Parameters     = "string wordsXml - Filepath to words xml file, string guess",
        ReturnType     = "ValidResponse (isValidWord bool, Message string)",
        Description    = "Checks whether a guess exists in the XML list **or** an online dictionary.",
        TryItLink      = "Staff.aspx"
    },
    new ServiceEntry
    {
        Provider       = "Eli Hoffman",
        ComponentType  = "DLL Function",
        Operation      = "HashPassword",
        Parameters     = "string password",
        ReturnType     = "string",
        Description    = "Hashes the password using SHA-256.",
        TryItLink      = "HashPassword.aspx"
    },
    new ServiceEntry
    {
        Provider       = "Eli Hoffman",
        ComponentType  = "Web Service (WSDL)",
        Operation      = "Login",
        Parameters     = "string username, string hashedPassword, string xmlUserFile, string xmlLogAttemptFile",
        ReturnType     = "AuthResult (Success bool, Message string)",
        Description    = "Validates credentials against XML records.",
        TryItLink      = "Login.aspx"
    },
    new ServiceEntry
    {
        Provider       = "Eli Hoffman",
        ComponentType  = "Web Service (WSDL)",
        Operation      = "Register",
        Parameters     = "string username, string hashedPassword, string xmlFile",
        ReturnType     = "AuthResult (Success bool, Message string)",
        Description    = "Adds a username/password pair to an XML file.",
        TryItLink      = "Login.aspx"
    },
    new ServiceEntry
    {
        Provider       = "Eli Hoffman",
        ComponentType  = "User Control",
        Operation      = "Login window",
        Parameters     = "N/A",
        ReturnType     = "N/A",
        Description    = "UI for login and registration.",
        TryItLink      = "Login.aspx"
    },
    new ServiceEntry
    {
        Provider       = "Eli Hoffman",
        ComponentType  = "User Control",
        Operation      = "Member page",
        Parameters     = "N/A",
        ReturnType     = "N/A",
        Description    = "Game-play page for Wordle.",
        TryItLink      = "Member.aspx"
    }
};

                ServiceDirectoryGrid.DataSource = entries;
                ServiceDirectoryGrid.DataBind();


                ServiceDirectoryGrid.DataSource = entries;
                ServiceDirectoryGrid.DataBind();
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
