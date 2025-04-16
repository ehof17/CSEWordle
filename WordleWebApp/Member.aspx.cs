using System;
using System.Collections.Generic;
using System.Web.UI;
using WordleLogic;

namespace WordleWebApp
{
    public partial class Member : System.Web.UI.Page
    {
<<<<<<< HEAD
        private const int MaxGuesses = 6;
        private const int WordLength = 5;

=======
        public static int GuessCounter = 0;
        public static string word;
        public static List<WordLetter> GuessedWordAccuracy;
>>>>>>> master
        protected void Page_Load(object sender, EventArgs e)
        {
            // When the page is first loaded (not on postback), start a new game.
            if (!IsPostBack)
            {
                StartNewGame();
            }
        }
       
        private void StartNewGame()
        {
            string filePath = Server.MapPath("~/App_Data/words.txt");
            string generatedWord = Logic.GenerateWord(filePath).ToLower();

            Session["ActualWord"] = generatedWord;
            Session["CurrentGuessIndex"] = 0;
            Session["PastGuesses"] = new List<string>();

            ResetKeyboad();
            keyboardLiteral.Text = BuildKeyboardHTML();

            testLbl.Text = $"(DEBUG) Word: {generatedWord}";
            resultLbl.Text = "";
            guessesPanel.Controls.Clear();
        }

        //Edited by Alex Alvarado 4/14
        protected void gameGeneratorBtn_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            StartNewGame();
        }

        protected void submitGuessBtn_Click(object sender, EventArgs e)
        {
            int currentGuessIndex = (int)(Session["CurrentGuessIndex"] ?? 0);

            if (currentGuessIndex >= MaxGuesses)
            {
                resultLbl.Text = $"No more guesses allowed. The word was {(string)Session["ActualWord"]}.";
                return;
            }
            
            string userGuess = guessTextBox.Text.Trim().ToLower();

            if (userGuess.Length != WordLength)
            {
                resultLbl.Text = "Your guess must have exactly 5 letters.";
                return;
            }
            string actualWord = (string)Session["ActualWord"];

            List<WordLetter> guessResult = Logic.WordGuessChecker(userGuess, actualWord);

            // Update the keyboard with the new info
            ProcessForKeyboard(guessResult);
            keyboardLiteral.Text = BuildKeyboardHTML();

            string feedbackHtml = BuildGuessHtml(guessResult);
            List<string> pastGuesses = (List<string>)Session["PastGuesses"];
            pastGuesses.Add(feedbackHtml);
            Session["PastGuesses"] = pastGuesses;

            UpdateGuessesPanel(pastGuesses);

            currentGuessIndex++;
            Session["CurrentGuessIndex"] = currentGuessIndex;

            if (userGuess == actualWord)
            {
                resultLbl.Text = "Congratulations! You guessed the word correctly.";
                submitGuessBtn.Enabled = false;
            }
            else if (currentGuessIndex == MaxGuesses)
            {
                resultLbl.Text = $"Game Over! The correct word was: {actualWord}.";
                submitGuessBtn.Enabled = false;
            }
            else
            {
                resultLbl.Text = $"Guess {currentGuessIndex} / {MaxGuesses}";
            }

            guessTextBox.Text = "";
        }
        private string BuildGuessHtml(List<WordLetter> guessResult)
        {
            string html = "<div class='guess-row'>";
            // process all the letters
            foreach (WordLetter wl in guessResult)
            {
                string cssClass = GetStatusCSSClass(wl.Status);
                html += $"<span class='letter-box {cssClass}'>{char.ToUpper(wl.Letter)}</span>";
            }
            html += "</div>";
            return html;
        }
        
        private void UpdateGuessesPanel(List<string> pastGuesses)
        {
            guessesPanel.Controls.Clear();

            // For each guess, add a Label to the panel.
            foreach (string guessFeedback in pastGuesses)
            {
                var lbl = new System.Web.UI.WebControls.Label();
                lbl.Text = guessFeedback;
                
                guessesPanel.Controls.Add(lbl);
            }
        }

        private string GetStatusCSSClass(WordleLogic.WordLetter.LetterStatus status)
        {
            // Status will determine css class, mainly affecting background color
            // Correct -> Green, partially correct -> yellow, unknown -> gray, incorrect -> darkgray
            string cssClass = "";
            switch (status)
            {
                case WordleLogic.WordLetter.LetterStatus.CorrectLetter:
                    cssClass = "correct-letter";
                    break;
                case WordleLogic.WordLetter.LetterStatus.CorrectLetterWrongSpot:
                    cssClass = "correct-letter-wrong-spot";
                    break;
                case WordleLogic.WordLetter.LetterStatus.IncorrectLetter:
                    cssClass = "incorrect-letter";
                    break;
                default:
                    cssClass = "unknown-letter";
                    break;
            }
            return cssClass;
        }

        // helper function to rank the priority of letter
        // 
        private int GetLetterPriority(WordleLogic.WordLetter.LetterStatus status)
        {
            switch (status)
            {
                case WordleLogic.WordLetter.LetterStatus.CorrectLetter: return 3;
                case WordleLogic.WordLetter.LetterStatus.CorrectLetterWrongSpot: return 2; 
                case WordleLogic.WordLetter.LetterStatus.IncorrectLetter: return 1; 
                default: return 0;
            }
        }


        private string BuildKeyboardHTML()
        {
            // Layout of a keyboard
            string[] rows = new string[]
            {
                "QWERTYUIOP",
                "ASDFGHJKL",
                "ZXCVBNM"
            };

            // Get the dictionary of all the letters and their status
            Dictionary<char, WordleLogic.WordLetter.LetterStatus> keyboardState = Session["KeyboardState"] as Dictionary<char, WordleLogic.WordLetter.LetterStatus>;
            if (keyboardState == null)
            {
                keyboardState = new Dictionary<char, WordleLogic.WordLetter.LetterStatus>();
                Session["KeyboardState"] = keyboardState;
            }

            string html = "<div class='keyboard'>";

            // Build each row
            foreach (string row in rows)
            {
                html += "<div class='keyboard-row' style='margin-bottom: 5px;'>";
                foreach (char c in row)
                {
                    // Get the status for each letter
                    // if it hasn't been guessed yet then it remains Unknown.
                    WordleLogic.WordLetter.LetterStatus status;
                    if (!keyboardState.TryGetValue(char.ToUpper(c), out status))
                    {
                        status = WordleLogic.WordLetter.LetterStatus.Unknown;
                    }

                    string cssClass = GetStatusCSSClass(status);
                    html += $"<span class='letter-box {cssClass}'>{char.ToUpper(c)}</span>";
                }
                html += "</div>";
            }
            html += "</div>";
            return html;
        }
        private void ResetKeyboad()
        {
            Session["KeyboardState"] = new Dictionary<char, WordleLogic.WordLetter.LetterStatus>();
        }
        private void ProcessForKeyboard(List<WordLetter> guessResult)
        {

            Dictionary<char, WordleLogic.WordLetter.LetterStatus> keyboardState = Session["KeyboardState"] as Dictionary<char, WordleLogic.WordLetter.LetterStatus>;
            if (keyboardState == null)
            {
                keyboardState = new Dictionary<char, WordleLogic.WordLetter.LetterStatus>();
                Session["KeyboardState"] = keyboardState;
            }


            // Process all letters in the guess result
            foreach (var wl in guessResult)
            {
               
                char letter = char.ToUpper(wl.Letter);
                int newPriority = GetLetterPriority(wl.Status);

                // Assume current letter has not been guessed, priority unknown
                int currentPriority = 0;
                // But if it has been guessed then get the priority we have saved
                if (keyboardState.ContainsKey(letter))
                {
                    currentPriority = GetLetterPriority(keyboardState[letter]);
                }

                // Replace with the higher priority
                if (newPriority > currentPriority)
                {
                    keyboardState[letter] = wl.Status;
                }
            }

            Session["KeyboardState"] = keyboardState;
=======
            word = Logic.GenerateWord(Server.MapPath("~/App_Data/words.txt"));
            messageLbl.Text = "A word has been generated. You have six guesses. For each attempt, a letter means corect letter and position, a ? means correct letter but " +
                "incorrect position, and _ means incorrect letter.";
            GuessCounter = 0;
            guess1Lbl.Text = "";
            guess2Lbl.Text = "";
            guess3Lbl.Text = "";
            guess4Lbl.Text = "";
            guess5Lbl.Text = "";
            guess6Lbl.Text = "";
            guess1TB.Text = "";
            guess2TB.Text = "";
            guess3TB.Text = "";
            guess4TB.Text = "";
            guess5TB.Text = "";
            guess6TB.Text = "";
            winStatusLbl.Text = "";
        }

        //Edited by Alex Alvarado 4/14
        protected void guessButton_Click(object sender, EventArgs e)
        {
            
            bool wonGame = updateGuessLogic();
            if(wonGame)
            {
                winStatusLbl.Text = $"You won in {GuessCounter} guesses.";
            }
            if(GuessCounter >= 6)
            {
                winStatusLbl.Text = "Out of guesses.";
            }
        }

        //Edited by Alex Alvarado 4/14
        public bool updateGuessLogic()
        {
            string guess;
            string displayWord;

            try
            {
                switch (GuessCounter) //this switch case will give us the correct text box to read the guess from 
                {
                    case 0:
                        guess = guess1TB.Text;
                        break;
                    case 1:
                        guess = guess2TB.Text;
                        break;
                    case 2:
                        guess = guess3TB.Text;
                        break;
                    case 3:
                        guess = guess4TB.Text;
                        break;
                    case 4:
                        guess = guess5TB.Text;
                        break;
                    case 5:
                        guess = guess6TB.Text;
                        break;
                    default:
                        guess = "";
                        Console.WriteLine("Something went wrong. Exceeded number of guesses which is not allowed!\n");
                        break;
                }
                if (!Logic.IsValidGuess(Server.MapPath("~/App_Data/words.txt"), guess)) //if the guess is invalid
                {
                    switch (GuessCounter) //this switch case will give us the correct label to edit invalid word to
                    {
                        case 0:
                            guess1Lbl.Text = "Invalid word. Please try again.";
                            break;
                        case 1:
                            guess2Lbl.Text = "Invalid word. Please try again.";
                            break;
                        case 2:
                            guess3Lbl.Text = "Invalid word. Please try again.";
                            break;
                        case 3:
                            guess4Lbl.Text = "Invalid word. Please try again.";
                            break;
                        case 4:
                            guess5Lbl.Text = "Invalid word. Please try again.";
                            break;
                        case 5:
                            guess6Lbl.Text = "Invalid word. Please try again.";
                            break;
                        default:
                            Console.WriteLine("Something went wrong. Exceeded number of guesses which is not allowed!");
                            break;
                    }
                    return false;
                }
                // if word is valid test guess against the actual word and increment guessCounter
                GuessedWordAccuracy = Logic.convertToDisplay(Logic.WordGuessChecker(guess, word));
                displayWord = string.Concat(GuessedWordAccuracy.Select(wl => wl.Letter));
                switch (GuessCounter) //this switch case will give us the correct label to edit the display word to
                {
                    case 0:
                        guess1Lbl.Text = displayWord;
                        break;
                    case 1:
                        guess2Lbl.Text = displayWord;
                        break;
                    case 2:
                        guess3Lbl.Text = displayWord;
                        break;
                    case 3:
                        guess4Lbl.Text = displayWord;
                        break;
                    case 4:
                        guess5Lbl.Text = displayWord;
                        break;
                    case 5:
                        guess6Lbl.Text = displayWord;
                        break;
                    default:
                        Console.WriteLine("Something went wrong. Exceeded number of guesses which is not allowed!");
                        break;
                }
                GuessCounter++;

                if (displayWord == guess)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error: {error}");
                return false;
            }
>>>>>>> master
        }
    }
}