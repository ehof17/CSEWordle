<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Inherits="WordleWebApp.Member" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Member Page</title>
    <style type="text/css">
        .letter-box {
            width: 30px;
            height: 30px;
            display: inline-block;
            text-align: center;
            line-height: 30px;
            margin: 2px;
            font-weight: bold;
            font-size: 18px;
            border-radius: 3px;
        }
        .correct-letter {
            background-color: green;
            color: white;
        }
        .correct-letter-wrong-spot {
            background-color: yellow;
            color: black;
        }
        .incorrect-letter {
            background-color: gray;
            color: white;
        }
        .unknown-letter {
            background-color: lightgray;
            color: black;
        }
        .guess-row {
            margin-bottom: 5px;
        }

        .keyboard { margin-top: 20px; }
        .keyboard-row { text-align: center; }
    
    
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Welcome to the member page</h1>

            <p>Here users will be able to play puzzles.</p>
            <p>
                <asp:Button ID="gameGeneratorBtn" runat="server" Text="Click here to play a game." OnClick="gameGeneratorBtn_Click" />
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
            <!-- maxlength 5 for wordle but may want this to be dynamic !-->
            <asp:TextBox ID="guessTextBox" runat="server" MaxLength="5"></asp:TextBox>

            <asp:Button ID="submitGuessBtn" runat="server" Text="Submit Guess" OnClick="submitGuessBtn_Click" />
            <br /><br />

           
            <asp:Label ID="resultLbl" runat="server" Text=""></asp:Label>

            <asp:Panel ID="guessesPanel" runat="server"></asp:Panel>
            
            <asp:Literal ID="keyboardLiteral" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
