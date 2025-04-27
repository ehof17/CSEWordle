<%@ Page Language="C#" AutoEventWireup="true"
         CodeBehind="WordleLogicTryIt.aspx.cs"
         Inherits="WordleWebApp.WordleLogicTryIt" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WordleLogic Service – Try It</title>
    <style>
      body { font-family: Arial; margin: 20px; }
      h2 { margin-bottom: 5px; }
      .section { margin-bottom: 30px; }
      .description { font-size: 0.9em; color: #555; margin-bottom: 8px; }
      .meta { font-size: 0.85em; color: #333; margin-left: 20px; margin-bottom: 12px; }
      input[type=text] { width: 300px; }
      .result { margin-top: 10px; font-weight: bold; }
    </style>
</head>
<body>
    <form id="form1" runat="server">

    <h1>
        Try It page for Wordle Logic Service
    </h1>
      <!-- 1. GenerateWord -->
      <div class="section">
        <h2>1. GenerateWord</h2>
        <div class="description">
          Chooses a random word from a provided xml file of words.
        </div>
        <div class="meta">
          <strong>Arguments:</strong><br />
          <code>filePath</code> (string): physical path to the word list file.<br />
          <strong>Return Type:</strong> <code>string</code> — the randomly selected word.
        </div>
        File path:
        <asp:TextBox ID="txtGenerateFilePath" runat="server" />
        <asp:Button ID="btnGenerateWord"
                    runat="server"
                    Text="Generate"
                    OnClick="btnGenerateWord_Click" />
        <br /><span class="result">Result:</span> <asp:Label ID="lblGenerateResult" runat="server" />
      </div>

      <!-- 2. IsValidGuess -->
      <div class="section">
        <h2>2. IsValidGuess</h2>
        <div class="description">
          Checks whether the provided word is valid. Will check if it exists in the provided XML file, and if not it will check if it exists in a dictionary
        </div>
        <div class="meta">
          <strong>Arguments:</strong><br />
          <code>filePath</code> (string): physical path to the word list file.<br />
          <code>guess</code>    (string): the word to validate.<br />
          <strong>Return Type:</strong> <code>bool</code> — true if the guess is in the list.
        </div>
        File path: <asp:TextBox ID="txtValidFilePath" runat="server" /><br />
        Guess:     <asp:TextBox ID="txtGuess" runat="server" />
        <asp:Button ID="btnIsValid"
                    runat="server"
                    Text="Check"
                    OnClick="btnIsValid_Click" />
        <br /><span class="result">Result:</span> <asp:Label ID="lblIsValid" runat="server" />
      </div>

      <!-- 3. WordGuessChecker -->
      <div class="section">
        <h2>3. WordGuessChecker</h2>
        <div class="description">
          Compares a user guess to the actual word and returns per‑letter statuses.
        </div>
        <div class="meta">
          <strong>Arguments:</strong><br />
          <code>userGuess</code>  (string): the guessed word.<br />
          <code>actualWord</code> (string): the target word to compare against.<br />
          <strong>Return Type:</strong> <code>WordLetter[]</code> — an array of objects where each <code>WordLetter</code> has:<br />
          &nbsp;&nbsp;<code>Letter</code> (char)<br />
          &nbsp;&nbsp;<code>Status</code> (enum: Unknown, CorrectLetter, CorrectLetterWrongSpot, IncorrectLetter)<br />
        </div>
        Your Guess:  <asp:TextBox ID="txtUserGuess" runat="server" /><br />
        Actual Word: <asp:TextBox ID="txtActualWord" runat="server" />
        <asp:Button ID="btnCheckGuess"
                    runat="server"
                    Text="Run Checker"
                    OnClick="btnCheckGuess_Click" />
        <br />
        <asp:GridView ID="gvGuessResults"
                      runat="server"
                      AutoGenerateColumns="false">
          <Columns>
            <asp:BoundField DataField="Letter" HeaderText="Letter" />
            <asp:BoundField DataField="Status" HeaderText="Status" />
          </Columns>
        </asp:GridView>
      </div>

    </form>
</body>
</html>
