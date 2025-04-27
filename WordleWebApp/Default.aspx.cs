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
            if (Session["Username"] != null)
            {
                lblUsername.Text = Session["Username"].ToString();
                if (!IsPostBack)
                {
                    List<ServiceEntry> entries = new List<ServiceEntry>
        {

                new ServiceEntry
            {
                Provider = "Alex Alvarado",
                ComponentType = "Cookies",
                Operation = "Saves username",
                Parameters = "N/A",
                ReturnType = "N/A",
                Description = "When a user registers they can opt in to using cookies and their username will be saved next time they log in",
                TryItLink = "Login.aspx"
            },

            new ServiceEntry
            {
                 Provider = "Alex Alvarado",
                ComponentType = "User Control",
                Operation = "Staff page",
                Parameters = "NA",
                ReturnType = "NA",
                Description = "Created the Staff page, a page where staff can enter new words to be used by the game and where staff can add new staff users",
                TryItLink = "Staff.aspx"
            },

                  new ServiceEntry
            {
                Provider = "Alex Alvarado",
                ComponentType = "Web Service (WSDL)",
                Operation = "GenerateWord",
                Parameters = "string filepath",
                ReturnType = "string",
                Description = "This function generates a random word from the list of words in word.txt in app data",
                TryItLink = "WordleLogicTryIt.aspx"
            },

              new ServiceEntry
            {
                Provider = "Alex Alvarado",
                ComponentType = "Web Service (WSDL)",
                Operation = "WordGuessChecker",
                Parameters = "string userGuess, string actualWord",
                ReturnType = "WordLetter[]",
                Description = "Takes a users guess and the actaul generated word and compares them and creates a list that holds WordLetters that have the status of each letter",
                TryItLink = "WordleLogicTryIt.aspx"
            },
            new ServiceEntry
            {
                Provider = "Alex Alvarado",
                ComponentType = "Web Service (WSDL)",
                Operation = "IsValidGuess",
                Parameters = "string filePath, string guess",
                ReturnType = "bool",
                Description = "takes a users guess and checks whether it is in the list of guessable words in words.txt",
                TryItLink = "Staff.aspx"
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


            new ServiceEntry
            {
                Provider = "Eli Hoffman",
                ComponentType = "Web Service (WSDL)",
                Operation = "Login",
                Parameters = "string username, string hashedPassword, string xmlUserFile, string xmlLogAttemptFile",
                ReturnType = "AuthResult: Success(bool) Message(string)",
                Description = "Validates a user's credentials against XML file records",
                TryItLink = "Login.aspx"
            },
            new ServiceEntry
            {
                Provider = "Eli Hoffman",
                ComponentType = "Web Service (WSDL)",
                Operation = "Register",
                Parameters = "string username, string hashedPassword, string xmlFile",
                ReturnType = "AuthResult: Success(bool) Message(string)",
                Description = "Adds a username and password to an XML file, to be logged into later",
                TryItLink = "Login.aspx"
            },

             new ServiceEntry
            {
                Provider = "Eli Hoffman",
                ComponentType = "User control",
                Operation = "Login window",
                Parameters = "N/A",
                ReturnType = "N/A",
                Description = "Allows user to log in and register",
                TryItLink = "Login.aspx"
            },

             new ServiceEntry
             {

                 Provider= "Eli Hoffman",
                 ComponentType= "User Control",
                 Operation = "Member page",
                Parameters = "N/A",
                ReturnType = "N/A",
                Description = "Allows user to play Wordle",
                TryItLink = "Member.aspx"

             },


        };

                    ServiceDirectoryGrid.DataSource = entries;
                    ServiceDirectoryGrid.DataBind();
                }
            }
            else
            {
                // Not logged in - redirect to login
                // Response.Redirect("Login.aspx");

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
