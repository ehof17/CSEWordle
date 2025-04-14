using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using WordleLogic;

namespace WordleWebApp
{
    public partial class Member : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gameGeneratorBtn_Click(object sender, EventArgs e)
        {
            string word = Logic.GenerateWord(Server.MapPath("~/App_Data/words.txt"));
            testLbl.Text = word;
        }
    }
}