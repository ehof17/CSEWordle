using System.Web.UI;
using System;
using WordleWebApp.WordleLogicServiceReference;
using System.IO;

public partial class WordleLogicTryIt : Page
{
    private readonly Service1Client _client = new Service1Client();
    private string DefaultXmlPath => Server.MapPath("~/App_Data/words.xml");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Prefill the big XML box with the default file used in the Member page
            txtXmlInput.Text = File.ReadAllText(DefaultXmlPath);
        }
    }

    // 1. GenerateWord 
    protected void btnGenerateWord_Click(object sender, EventArgs e)
    {
        try
        {
            string xml = GetXmlFromBoxOrDefault();
            lblGenerateResult.Text = _client.GenerateWord(xml);
        }
        catch (Exception ex)
        {
            lblGenerateResult.Text = "Error: " + ex.Message;
        }
    }

    // 2. IsValidGuess 
    protected void btnIsValid_Click(object sender, EventArgs e)
    {
        try
        {

            string xml = GetXmlFromBoxOrDefault();
            var resp = _client.IsValidGuess(xml, txtGuess.Text.Trim());
            lblValidBool.Text = resp.isValidWord.ToString();
            lblValidMessage.Text = resp.Message;


    

         
        }
        catch (Exception ex)
        {
            lblIsValid.Text = "Error: " + ex.Message;
        }
    }

    // 3. WordGuessChecker ------------------------------------------------------
    protected void btnCheckGuess_Click(object sender, EventArgs e)
    {
        try
        {
            var results = _client.WordGuessChecker(
                              txtUserGuess.Text.Trim(),
                              txtActualWord.Text.Trim());

            gvWGC.DataSource = results;
            gvWGC.DataBind();
        }
        catch (Exception ex)
        {
            // Log or surface as needed
        }
    }

    // 4. SaveWordToList --------------------------------------------------------
    protected void btnSaveWord_Click(object sender, EventArgs e)
    {
        string xml = txtExistingXml.Text;              // leave blank? fine.
        string word = txtWordToAdd.Text.Trim();
        string username = txtUsername.Text.Trim();

        if (string.IsNullOrWhiteSpace(word))
        {
            txtSaveResult.Text = "Please enter a word to add.";
            return;
        }

        // If xml box is empty, fall back to file
        if (string.IsNullOrWhiteSpace(xml))
            xml = File.ReadAllText(DefaultXmlPath);

        try
        {
            string result = _client.SaveWordToList(xml, word, username);

            // Display result
            txtSaveResult.Text = result;

            // For convenience, also push the returned XML back into the box
            if (!result.StartsWith("Invalid") && !result.StartsWith("Word already"))
                txtExistingXml.Text = result;
        }
        catch (Exception ex)
        {
            txtSaveResult.Text = "Service call failed: " + ex.Message;
        }
    }

    // ---------- helper --------------------------------------------------------
    private string GetXmlFromBoxOrDefault()
    {
        return string.IsNullOrWhiteSpace(txtXmlInput.Text)
             ? File.ReadAllText(DefaultXmlPath)
             : txtXmlInput.Text;
    }
}
