using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using WordleLogic;

namespace WordleWebApp
{
    public partial class Staff : System.Web.UI.Page
    {
        public static readonly object StaffLock = new object();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Staff"))
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void addWordBtn_Click(object sender, EventArgs e)
        {
            string word = enterWordHereTB.Text;
            try
            {
                if((!Logic.IsValidGuess(Server.MapPath("~/App_Data/words.txt"), word)) && (word.Length == 5))
                {
                    using (StreamWriter sw = new StreamWriter(Server.MapPath("~/App_Data/words.txt"), append: true))
                    {
                        sw.WriteLine(word);
                    }
                    WordAddedLbl.Text = "Word added";
                }
                else if (Logic.IsValidGuess(Server.MapPath("~/App_Data/words.txt"), word))
                {
                    WordAddedLbl.Text = "Word exists!";
                }
                else
                {
                    WordAddedLbl.Text = "Invalid word!";
                }
            }catch (Exception error)
            {
                Console.WriteLine(error);

            }
            
        }

        protected void backButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void addUserBtn_Click(object sender, EventArgs e)
        {
            string staffXMLPath = Server.MapPath("~/App_Data/Staff.xml");
            string usersXMLPath = Server.MapPath("~/App_Data/Users.xml");
            try
            {
                //grab username from text box
                string username = enterUsernameTB.Text;

                //create 2 XML docs one for users and one for staff
                XmlDocument userDoc = new XmlDocument();
                userDoc.Load(usersXMLPath);

                XmlDocument staffDoc = new XmlDocument();
                staffDoc.Load(staffXMLPath);

                //Enter root of users
                XmlNode root = userDoc.DocumentElement;
                XmlNodeList children = root.ChildNodes;
                bool userInUsers = false;
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(userDoc.NameTable);
                nsmgr.AddNamespace("ns", "http://schemas.datacontract.org/2004/07/AuthenticationService");

                foreach (XmlNode child in children) //check each node in the users file to find user
                {
                    XmlNode usernameNode = child.SelectSingleNode("ns:Username",nsmgr);
                    if (usernameNode != null && usernameNode.InnerText == username) //user is found
                    {
                        userInUsers = true;
                        bool userExistsInStaff = false;
                        foreach (XmlNode staffUser in staffDoc.DocumentElement.ChildNodes) //check if user is already a staff member
                        {
                            XmlNode staffUsernameNode = staffUser.SelectSingleNode("ns:Username",nsmgr);
                            if (staffUsernameNode != null && staffUsernameNode.InnerText == username) //user is found in staff 
                            {
                                userExistsInStaff = true;
                                successLbl.Text = "User is already in staff.";
                                break;
                            }
                        }
                        if (!userExistsInStaff) //user does not exist in staff yet
                        {
                            userInUsers = true;
                            lock (StaffLock) //acquire lock for staff
                            {
                                //Update staff to hold the user in xml file and add staff to user role
                                XmlNode importedNode = staffDoc.ImportNode(child, true);
                                staffDoc.DocumentElement.AppendChild(importedNode);
                                XmlNode roleNode = child.SelectSingleNode("ns:Role", nsmgr);
                                if (roleNode != null)
                                {
                                    roleNode.InnerText = "Staff";

                                }
                                else
                                {
                                    XmlElement newRole = userDoc.CreateElement("Role","http://schemas.datacontract.org/2004/07/AuthenticationService");
                                    newRole.InnerText = "Staff";
                                    child.AppendChild(newRole);
                                    
                                }
                                //save both documents
                                userDoc.Save(usersXMLPath);
                                staffDoc.Save(staffXMLPath);
                            }
                            successLbl.Text = "User has been upgraded to staff.";
                            break;
                        }
                    }
                }
                if (!userInUsers) //user was not found
                {
                    successLbl.Text = "User not found.";
                }
            }
            catch (Exception error) //invalid text
            {
                Console.WriteLine(error);
                successLbl.Text = "Invalid text.";
            }
        }
    }
}