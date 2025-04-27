<%@ Page Language="C#" AutoEventWireup="true"
         CodeBehind="WordleLogicTryIt.aspx.cs"
         Inherits="WordleLogicTryIt"
         ValidateRequest="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WordleLogic Service – Try It</title>
    <style>
      body { font-family: Arial; margin: 20px; }
      h2   { margin-bottom: 5px; }
      .section { margin-bottom: 30px; }
      .description { font-size: .9em; color: #555; margin-bottom: 8px; }
      .meta        { font-size: .85em; color: #333; margin-left: 20px; margin-bottom: 12px; }
      input[type=text] { width: 300px; }
      .result { margin-top: 10px; font-weight: bold; }
    </style>
</head>
<body>
<form id="form1" runat="server">

<h1>Try-It page for <em>Wordle Logic Service</em></h1>

<!-- 0. Shared XML input ---------------------------------------------------- -->
<div class="section">
  <h2>Word List XML</h2>
  <div class="description">
      Paste the XML here. Leave blank to fall back to the default
      <code>App_Data/words.xml</code>.
  </div>

  <asp:TextBox ID="txtXmlInput" runat="server"
               TextMode="MultiLine" Rows="8" Width="100%" />
</div>

<!-- 1. GenerateWord -------------------------------------------------------- -->
<div class="section">
  <h2>1. GenerateWord</h2>
  <div class="description">
      Picks a random word from the XML supplied above.
  </div>
    <div class="meta">
    <strong>Arguments:</strong><br />
    <code>wordsXml</code> (<em>string</em>) – the XML shown in the “Word List XML” box.<br />
    <strong>Return Type:</strong> <code>string</code> – one randomly-selected word.
</div>

  <asp:Button ID="btnGenerateWord" runat="server"
              Text="Generate" OnClick="btnGenerateWord_Click" />
  <br /><span class="result">Result:</span>
 <asp:Label ID="lblGenerateResult" runat="server" />
</div>

<!-- 2. IsValidGuess -------------------------------------------------------- -->
<div class="section">
  <h2>2. IsValidGuess</h2>
  <div class="description">
      Checks whether the guess occurs in the XML (or a dictionary fallback).
  </div>
  <div class="meta">
    <strong>Arguments:</strong><br />
    <code>wordsXml</code> (<em>string</em>) – XML in the “Word List XML” box.<br />
    <code>guess</code>    (<em>string</em>) – the word to validate.<br />
    <strong>Return Type:</strong> <code>ValidResponse</code> – object with<br />
    &nbsp;&nbsp;<code>isValidWord</code> (bool)<br />
    &nbsp;&nbsp;<code>Message</code>     (string)
  </div>

  Guess word:
  <asp:TextBox ID="txtGuess" runat="server" />
  <asp:Button ID="btnIsValid" runat="server"
              Text="Check" OnClick="btnIsValid_Click" />
  <br /><span class="result">Result:</span>
  <asp:Label ID="lblIsValid" runat="server" />
<br />
<span class="result">isValidWord:</span>
<asp:Label ID="lblValidBool" runat="server" />
<br />
<span class="result">Message:</span>
<asp:Label ID="lblValidMessage" runat="server" />

</div>

<!-- 3. WordGuessChecker ---------------------------------------------------- -->
<div class="section">
  <h2>3. WordGuessChecker</h2>
    <div class="description">
    Compares the guess to the actual word and returns a
    per-letter breakdown.
</div>
   <div class="meta">
    <strong>Arguments:</strong><br />
    <code>userGuess</code> (<em>string</em>) – the word the player entered.<br />
    <code>actualWord</code> (<em>string</em>) – the target word to compare against.<br />
    <strong>Return Type:</strong>&nbsp;<code>WordLetter[]</code> – an array where each item contains:<br />
    &nbsp;&nbsp;<code>Letter</code> (char)<br />
    &nbsp;&nbsp;<code>Status</code> (enum)<br />
    &nbsp;&nbsp;&nbsp;&nbsp;<code>CorrectLetter</code> – right letter, right spot<br />
    &nbsp;&nbsp;&nbsp;&nbsp;<code>CorrectLetterWrongSpot</code> – right letter, wrong spot<br />
    &nbsp;&nbsp;&nbsp;&nbsp;<code>IncorrectLetter</code> – letter is not in the word, or all instances of the letter have been used up already
</div>
  Your&nbsp;Guess:  <asp:TextBox ID="txtUserGuess" runat="server" /><br />
  Actual&nbsp;Word: <asp:TextBox ID="txtActualWord" runat="server" />
  <asp:Button ID="btnCheckGuess" runat="server"
              Text="Run Checker" OnClick="btnCheckGuess_Click" />
  <br />
  <asp:GridView ID="gvWGC" runat="server" AutoGenerateColumns="false">
    <Columns>
        <asp:BoundField DataField="Key"   HeaderText="Property" />
        <asp:BoundField DataField="Value" HeaderText="Value"    />
    </Columns>
</asp:GridView>

</div>

<!-- 4. SaveWordToList ------------------------------------------------------ -->
<div class="section">
  <h2>4. SaveWordToList</h2>
<div class="description">
    Will return an xml string of all the words supplied previously along with the new word. If the existing XML is empty, it will return a new xml string. If the word is invalid or the word already exists, an message will be retur
</div>
    <div class="meta">
    <strong>Arguments:</strong><br />
    <code>existingWordsXml</code> (<em>string</em>) – the existing XML, or the words already stored.<br />
    <code>wordToAdd</code> (<em>string</em>) – the word you want to add.<br />
    <code>username</code> (<em>string</em>) – who’s adding it.<br />
    <strong>Return Type:</strong> <code>string</code> – either the updated XML<br />
    &nbsp;&nbsp;or an error message like “Invalid word!” or “Word already exists!”.
</div>
    <p>Note: Saving XML will prefill this text with the result for convience</p>
  Existing&nbsp;XML:<br />
  <asp:TextBox ID="txtExistingXml" runat="server"
               TextMode="MultiLine" Rows="6" Width="100%" /><br />

  Word&nbsp;to&nbsp;Add:
  <asp:TextBox ID="txtWordToAdd" runat="server" />&nbsp;&nbsp;
  Username:
  <asp:TextBox ID="txtUsername" runat="server" />

  <asp:Button ID="btnSaveWord" runat="server"
              Text="Save Word" OnClick="btnSaveWord_Click" />

  <br /><span class="result">Result / Updated XML:</span><br />
  <asp:TextBox ID="txtSaveResult" runat="server"
               TextMode="MultiLine" Rows="6" ReadOnly="true" Width="100%" />
</div>

</form>
</body>
</html>
