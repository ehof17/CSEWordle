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
    public partial class WordleLogicTryit : System.Web.UI.Page
    {
        private Service1Client _client = new Service1Client();

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
                lblIsValid.Text = ok ? "✔ Valid" : "✖ Invalid";
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

        protected void btnConvert_Click(object sender, EventArgs e)
        {
            try
            {
                
                var serializer = new JavaScriptSerializer();
                var list = serializer.Deserialize<List<WordLetter>>(txtConvertJson.Text);
                WordLetter[] wl = new WordLetter[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    wl[i] = list[i];
                }

                var display = _client.ConvertToDisplay(wl);
                gvDisplay.DataSource = display;
                gvDisplay.DataBind();
            }
            catch (Exception ex)
            {
               
            }
        }
    }
}
}