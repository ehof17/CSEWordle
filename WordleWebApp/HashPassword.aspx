<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HashPassword.aspx.cs" Inherits="WordleWebApp.HashPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Security DLL Class – Try It</title>
<style>
    body { font-family: Arial; margin: 20px; }
    h2 { margin-bottom: 5px; }
    .section { margin-bottom: 30px; }
    .description { font-size: 0.9em; color: #555; margin-bottom: 8px; }
    .args { font-size: 0.85em; color: #333; margin-left: 20px; margin-bottom: 12px; }
    input[type=text], input[type=password] { width: 300px; }
    .result { margin-top: 10px; font-weight: bold; }
  </style>
</head>
<body>
  <form id="form1" runat="server">
 <h1>
     Try It page for Security DLL Module
 </h1>

    <!-- Password Hasher -->
    <div class="section">
      <h2>1. Password Hasher</h2>
      <div class="description">
        Applies a secure one‑way hash to your plaintext password.
      </div>
      <div class="args">
        <strong>Arguments:</strong><br />
        <code>txtInput</code> (string): the plaintext password to hash.<br />
        <strong>Return Type:</strong> <code>string</code> — the hashed password.
      </div>
      <asp:TextBox ID="txtInput" runat="server"
                   TextMode="Password"
                   placeholder="Enter password…" />
      <asp:Button ID="btnHash" runat="server" Text="Hash"
                  OnClick="btnHash_Click" />
      <div class="result">
        Hashed output: <asp:Label ID="lblOutput" runat="server" />
      </div>
    </div>

    <!-- Verify Password -->
    <div class="section">
      <h2>2. Verify Password</h2>
      <div class="description">
        Checks whether the entered password, when hashed, matches the stored hash.
      </div>
      <div class="args">
        <strong>Arguments:</strong><br />
        <code>txtVerifyPassword</code> (string): the plaintext password to verify.<br />
        <code>txtStoredHash</code>     (string): the previously stored hash.<br />
        <strong>Return Type:</strong> <code>bool</code> — true if they match.
      </div>
      <asp:TextBox ID="txtVerifyPassword" runat="server"
                   TextMode="Password"
                   placeholder="Enter password to verify…" /><br /><br />
      <asp:TextBox ID="txtStoredHash" runat="server"
                   placeholder="Enter stored hash…" /><br /><br />
      <asp:Button ID="btnVerify" runat="server" Text="Verify"
                  OnClick="btnVerify_Click" />
      <div class="result">
        Verification result: <asp:Label ID="lblVerifyResult" runat="server" />
      </div>
    </div>

  </form>
</body>
</html>