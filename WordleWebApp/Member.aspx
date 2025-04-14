<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Inherits="WordleWebApp.Member" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Welcome to the member page</h1>

            <p>Here users will be able to play puzzles.</p>
            <p>
                <asp:Button ID="gameGeneratorBtn" runat="server" Text="Click here to play game." OnClick="gameGeneratorBtn_Click" />
            </p>
            <p>
                <asp:Label ID="messageLbl" runat="server" Text=""></asp:Label>
            </p>
            <p>
                <asp:TextBox ID="guess1TB" runat="server"></asp:TextBox>
            </p>
            <p>
                <asp:Label ID="guess1Lbl" runat="server" Text=""></asp:Label>
            </p>
            <p>
                <asp:TextBox ID="guess2TB" runat="server"></asp:TextBox>
            </p>
            <p>
                <asp:Label ID="guess2Lbl" runat="server" Text=""></asp:Label>
            </p>
            <p>
                <asp:TextBox ID="guess3TB" runat="server"></asp:TextBox>
            </p>
            <p>
                <asp:Label ID="guess3Lbl" runat="server" Text=""></asp:Label>
            </p>
            <p>
                <asp:TextBox ID="guess4TB" runat="server"></asp:TextBox>
            </p>
            <p>
                <asp:Label ID="guess4Lbl" runat="server" Text=""></asp:Label>
            </p>
            <p>
                <asp:TextBox ID="guess5TB" runat="server"></asp:TextBox>
            </p>
            <p>
                <asp:Label ID="guess5Lbl" runat="server" Text=""></asp:Label>
            </p>
            <p>
                <asp:TextBox ID="guess6TB" runat="server"></asp:TextBox>
            </p>
            <p>
                <asp:Label ID="guess6Lbl" runat="server" Text=""></asp:Label>
            </p>
            <p>
                <asp:Button ID="guessButton" runat="server" Text="Guess" Height="40px" OnClick="guessButton_Click" Width="110px" />
            </p>
            <p>
                <asp:Label ID="winStatusLbl" runat="server" Text=""></asp:Label>
            </p>
        </div>
    </form>
</body>
</html>
