<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WordleLogicTryit.aspx.cs" Inherits="WordleWebApp.WordleLogicTryit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WordleLogic Service – Try It</title>
    <style>
      body { font-family: Arial; margin: 20px; }
      h2 { margin-bottom: 5px; }
      .section { margin-bottom: 30px; }
      .section input[type=text] { width: 300px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
      <div class="section">
        <h2>1. GenerateWord</h2>
        File path: 
        <asp:TextBox ID="txtGenerateFilePath" runat="server" />
        <asp:Button ID="btnGenerateWord" runat="server" Text="Generate" OnClick="btnGenerateWord_Click" />
        <br /><strong>Result:</strong> <asp:Label ID="lblGenerateResult" runat="server" />
      </div>

      <div class="section">
        <h2>2. IsValidGuess</h2>
        File path: <asp:TextBox ID="txtValidFilePath" runat="server" /><br />
        Guess:     <asp:TextBox ID="txtGuess" runat="server" />
        <asp:Button ID="btnIsValid" runat="server" Text="Check" OnClick="btnIsValid_Click" />
        <br /><strong>Result:</strong> <asp:Label ID="lblIsValid" runat="server" />
      </div>

      <div class="section">
        <h2>3. WordGuessChecker</h2>
        Your Guess:  <asp:TextBox ID="txtUserGuess" runat="server" /><br />
        Actual Word: <asp:TextBox ID="txtActualWord" runat="server" />
        <asp:Button ID="btnCheckGuess" runat="server" Text="Run Checker" OnClick="btnCheckGuess_Click" />
        <br /><asp:GridView ID="gvGuessResults" runat="server" AutoGenerateColumns="false">
          <Columns>
            <asp:BoundField DataField="Letter" HeaderText="Letter" />
            <asp:BoundField DataField="Status" HeaderText="Status" />
          </Columns>
        </asp:GridView>
      </div>

      <div class="section">
        <h2>4. ConvertToDisplay</h2>
        JSON List of WordLetter: <br />
        <asp:TextBox ID="txtConvertJson" runat="server" TextMode="MultiLine" Width="400px" Height="100px" />
        <br />
        <asp:Button ID="btnConvert" runat="server" Text="Convert" OnClick="btnConvert_Click" />
        <br /><asp:GridView ID="gvDisplay" runat="server" AutoGenerateColumns="false">
          <Columns>
            <asp:BoundField DataField="Letter" HeaderText="Display Char" />
          </Columns>
        </asp:GridView>
      </div>
    </form>
</body>
</html>
