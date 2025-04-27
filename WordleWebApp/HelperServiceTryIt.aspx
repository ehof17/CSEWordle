<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HelperServiceTryIt.aspx.cs" Inherits="WordleWebApp.HelperServiceTryIt" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Helper-Service — Try It</title>
    <style>
        body { font-family: sans-serif; margin: 2rem; }
        label, input, button, textarea { display: block; margin: .5rem 0; }
        textarea { width: 100%; height: 6rem; }
        .small { font-size: .85rem; color: #666; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>HelperService Try It Page <code>GetHint</code></h2>

        <label>Actual word
            <asp:TextBox ID="txtActual" runat="server" Width="250" />
        </label>

        <label>Already-revealed positions (zero-based, comma-separated)
            <asp:TextBox ID="txtPositions" runat="server" Width="250" />
            <span class="small">(e.g. 0,3,4)</span>
        </label>

        <asp:Button ID="btnCall" runat="server" Text="Call Service" OnClick="btnCall_Click" />

        <h3>Result</h3>
        <asp:TextBox ID="txtResult" runat="server" TextMode="MultiLine" ReadOnly="true" />

    </form>
</body>
</html>
