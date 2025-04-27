<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Staff.aspx.cs" Inherits="WordleWebApp.Staff" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
    body {
        margin: 0;
        font-family: Arial, sans-serif;
        background: #f7f7f7;
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100vh;
    }
    .container {
        background: #fff;
        padding: 20px 30px;
        border-radius: 8px;
        box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        width: 100%;
        max-width: 500px;
    }
    h1 {
        text-align: center;
        margin-bottom: 20px;
        font-size: 24px;
    }
    .actions {
        text-align: center;
        margin-bottom: 15px;
    }
    .actions .btn {
        margin: 0 5px;
        padding: 8px 16px;
        background: #007bff;
        color: #fff;
        border: none;
        border-radius: 4px;
        cursor: pointer;
    }
    .result {
        text-align: center;
        margin-bottom: 15px;
        font-weight: bold;
        height: 24px;
    }
</style>
</head>
<body>
    <form id="form1" runat="server" class="container">
        <h1>Welcome to the staff page</h1>
        <div class="actions">
            
            <p>Here users can append words to the list.</p>
            <p>
                <asp:TextBox ID="enterWordHereTB" runat="server"></asp:TextBox>
                <asp:Button ID="addWordBtn" runat="server" Text="Add word" OnClick="addWordBtn_Click" CssClass="btn"/>
            </p>
            <p>
                <asp:Label ID="WordAddedLbl" runat="server" Text=""></asp:Label>
            </p>
            <p>
                &nbsp;</p>
            <p>Upgrade a user to admin here.</p>
            <p>
                <asp:Label ID="enterUsernameLbl" runat="server" Text="Enter user's to add username:"></asp:Label>
                <asp:TextBox ID="enterUsernameTB" runat="server"></asp:TextBox>
            </p>
            <p>
                <asp:Button ID="addUserBtn" runat="server" Text="Add User" CssClass="btn" OnClick="addUserBtn_Click"/>
            </p>
            <p>
                <asp:Label ID="successLbl" runat="server" Text=""></asp:Label>
            </p>

            <p>
                <asp:Button ID="backButton" runat="server" Text="Back" OnClick="backButton_Click" CssClass="btn"/>
                <asp:Button ID="logoutBtn" runat="server" Text="Logout" PostBackUrl="~/Logout.aspx" CssClass="btn"/>
            </p>
        </div>
    </form>
</body>
</html>
