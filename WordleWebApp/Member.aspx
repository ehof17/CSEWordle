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
                <asp:Label ID="testLbl" runat="server" Text=""></asp:Label>
            </p>
        </div>
    </form>
</body>
</html>
