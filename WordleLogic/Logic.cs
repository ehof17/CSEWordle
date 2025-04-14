using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleLogic
{
    public static class Logic
    {

        public static string GenerateWord(string filePath)
        {
            if(!File.Exists(filePath))
            {
                throw new FileNotFoundException("The file could not be found.\n", filePath);
            }
            string[] words = File.ReadAllLines(filePath);

            if(words.Length == 0)
            {
                throw new InvalidOperationException("The file is empty.\n");
            }

            Random rand = new Random();
            return words[rand.Next(words.Length)].Trim();
        }

        public static List<WordLetter> WordGuessChecker(string userGuess, string actualWord) 
        {

            //create two lists:
            //1) actual letter to hold each letter and status of actual guess in list
            //2) guess letter to hold each letter and status of guess in list
            List<WordLetter> actualLetter = new List<WordLetter>();
            List<WordLetter> guessLetter = new List<WordLetter>();

            //adds each letter to the list holding the WordLetter objects
            foreach (char c in actualWord)
            {
                actualLetter.Add(new WordLetter(c));
            }
            foreach (char c in userGuess)
            {
                guessLetter.Add(new WordLetter(c));
            }

            //loops through each letter and compares the actual and guess to each other
            for (int i = 0; i < actualLetter.Count; i++)
            {
                actualLetter[i].Status = WordLetter.LetterStatus.CorrectLetter;
                if (actualLetter[i].Letter == guessLetter[i].Letter) //letter is in the correct spot
                {
                    guessLetter[i].Status = WordLetter.LetterStatus.CorrectLetter;
                }else if (actualWord.Contains(guessLetter[i].Letter)) //letter is in the word but not the correct spot
                {
                    guessLetter[i].Status = WordLetter.LetterStatus.CorrectLetterWrongSpot;
                }
                else //letter is not in word
                {
                    guessLetter[i].Status = WordLetter.LetterStatus.IncorrectLetter;
                }

            }
            return guessLetter;
        }
    }
    public class WordLetter
    {
        public enum LetterStatus
        {
            Unknown = 0,
            CorrectLetter = 1,
            CorrectLetterWrongSpot = 2,
            IncorrectLetter = 3
        }
        public char Letter { get; set; }
        public LetterStatus Status { get; set; }

        public WordLetter(char letter)
        {
            Letter = letter;
            Status = LetterStatus.Unknown;
        }
    }
}
