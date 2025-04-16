using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using WordleLogic;

namespace WordleWebApp
{
    public partial class Member : System.Web.UI.Page
    {
        public static int GuessCounter = 0;
        public static string word;
        public static List<WordLetter> GuessedWordAccuracy;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Edited by Alex Alvarado 4/14
        protected void gameGeneratorBtn_Click(object sender, EventArgs e)
        {
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
        }
    }
}