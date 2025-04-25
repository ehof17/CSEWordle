using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using WordleWebApp.WordleLogicServiceReference;
namespace WordleWebApp
{
    public partial class WordleLogicTryIt : Page
    {
        private Service1Client _client = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var defaultPath = Server.MapPath("~/App_Data/words.txt");
                txtGenerateFilePath.Text = defaultPath;
                txtValidFilePath.Text = defaultPath;
            }
        }
        protected void btnGenerateWord_Click(object sender, EventArgs e)
        {
            try
            {
                lblGenerateResult.Text = _client.GenerateWord(txtGenerateFilePath.Text);
            }
            catch (Exception ex)
            {
                lblGenerateResult.Text = "Error: " + ex.Message;
            }
        }

        protected void btnIsValid_Click(object sender, EventArgs e)
        {
            try
            {
                bool ok = _client.IsValidGuess(txtValidFilePath.Text, txtGuess.Text);
                lblIsValid.Text = ok ? "Valid" : "Invalid";
            }
            catch (Exception ex)
            {
                lblIsValid.Text = "Error: " + ex.Message;
            }
        }

        protected void btnCheckGuess_Click(object sender, EventArgs e)
        {
            try
            {
                var results = _client.WordGuessChecker(txtUserGuess.Text, txtActualWord.Text);
                gvGuessResults.DataSource = results;
                gvGuessResults.DataBind();
            }
            catch (Exception ex)
            {
                
            }
        }

    }
}