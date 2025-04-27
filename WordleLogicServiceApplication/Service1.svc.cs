using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;

namespace WordleLogicServiceApplication
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public static Dictionary<string, bool> wordValidityCache = new Dictionary<string, bool>();
        private static readonly DataContractSerializer gamewordser = new DataContractSerializer(typeof(GameWordList));
        private static GameWordList LoadWords(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml)) return new GameWordList();
            using (var r = XmlReader.Create(new StringReader(xml)))
            {
                return (GameWordList)gamewordser.ReadObject(r);
            }
        }
        public string GenerateWord(string gameWordsXML)
        {
            GameWordList gameWords = LoadWords(gameWordsXML);
            List<string> words = gameWords.Select(x => x.Word).Distinct().ToList();

            Random rand = new Random();
            return words[rand.Next(words.Count)].Trim();
        }
        //Written by Alex Alvarado and edited by Eli Hoffman
        public List<WordLetter> WordGuessChecker(string userGuess, string actualWord)
        {

            //create two lists:
            //1) actual letter to hold each letter and status of actual guess in list
            //2) guess letter to hold each letter and status of guess in list
            List<WordLetter> actualLetter = new List<WordLetter>();
            List<WordLetter> guessLetter = new List<WordLetter>();

            int length = userGuess.Length;
            
            //adds each letter to the list holding the WordLetter objects
            foreach (char c in actualWord)
            {
                actualLetter.Add(new WordLetter(char.ToUpper(c)));
            }
            foreach (char c in userGuess)
            {
                guessLetter.Add(new WordLetter(char.ToUpper(c)));
            }

            // Get the counts of each letter 
            Dictionary<char, int> letterCounts = new Dictionary<char, int>();
            for (int i = 0; i < actualWord.Length; i++)
            {
                char c = char.ToUpper(actualWord[i]);
                if (!letterCounts.ContainsKey(c))
                {
                    letterCounts[c] = 0;
                }
                letterCounts[c]++;
            }


            // first check the correct letters
            for (int i = 0; i < length; i++)
            {
                if (char.ToUpper(userGuess[i]) == char.ToUpper(actualWord[i]))
                {
                    guessLetter[i].Status = WordLetter.LetterStatus.CorrectLetter;

                    letterCounts[char.ToUpper(userGuess[i])]--;
                }
            }

            for (int i = 0; i < length; i++)
            {
                // Skip letters already marked as correct
                if (guessLetter[i].Status == WordLetter.LetterStatus.CorrectLetter)
                {
                    continue;
                }

                char letter = char.ToUpper(userGuess[i]);
                // If the letter exists and there's still count left
                if (letterCounts.ContainsKey(letter) && letterCounts[letter] > 0)
                {
                    guessLetter[i].Status = WordLetter.LetterStatus.CorrectLetterWrongSpot;
                    letterCounts[letter]--;
                }
                // This incorrect doesn't mean that the letter is not in the word at all necessarily
                // just that there is not another instance of the letter available
                // or it could be that the letter wasn't in the word at all
                else
                {
                    guessLetter[i].Status = WordLetter.LetterStatus.IncorrectLetter;
                    guessLetter[i].Position = i;
                }
            }

            return guessLetter;

        }

        //Written by Alex Alvarado

        public ValidResponse IsValidGuess(string wordsXML, string guess)
        {
            GameWordList words = LoadWords(wordsXML);
            ValidResponse res = new ValidResponse();

            // First check if the guess is in the provided list of GameWords
            if (words.Any(x => string.Equals(x.Word, guess, StringComparison.OrdinalIgnoreCase)))
            {
                res.isValidWord = true;
                res.Message = "Word already present";
                return res;
            }


            // Hits a dictionary API to tell if an inputted word is in their dictionary
            // First, check if we've already cached the result
            if (wordValidityCache.ContainsKey(guess))
            {
                bool cachedValidity = wordValidityCache[guess];
                res.isValidWord = cachedValidity;
                res.Message = "Validity returned from dictionary";
                return res;
            }

            try
            {
                // Build the API URL 
                string url = "https://api.dictionaryapi.dev/api/v2/entries/en/" + guess;
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(url);
                    // If the response contains "No Definitions Found", then this is an invalid word
                    if (json.Contains("No Definitions Found"))
                    {
                        wordValidityCache[guess] = false;
                        res.isValidWord = false;
                        res.Message = "Word not found in dictionary";
                        return res;
                    }
                    // Otherwise, there is a definition and the word is a real word
                    else
                    {
                        wordValidityCache[guess] = true;
                        res.isValidWord = true;
                        res.Message = "Word found in dictionary";
                        return res;

                    }
                }
            }
            catch (Exception)
            {
                // For other errors, treat as invalid.
                //wordValidityCache[guess] = false;
                res.isValidWord = false;
                res.Message = "Error occured attempting to query dictionary";
                return res;
            }
        }

        private static string SaveWords(GameWordList list)
        {
            using (var sw = new StringWriter())
            {
                using (var w = XmlWriter.Create(sw))
                {
                    gamewordser.WriteObject(w, list);
                    w.Flush();
                }

                return sw.ToString();
            }
        }
        public string SaveWordToList(string existingWordXML, string wordToAdd, string username)
        {
            ValidResponse resp = IsValidGuess(existingWordXML, wordToAdd);
            if (!resp.isValidWord)
            {
                return "Invalid word!";

            }
            // Means that the word was valid since it was provided in the wordslist
            if (resp.Message == "Word already present")
            {
                return "Word already exists!";
            }

            GameWordList existingGameWords = LoadWords(existingWordXML);

            existingGameWords.Add(new GameWord
            {
                UsernameAdded = username,
                Word = wordToAdd,
                TimeAdded = DateTime.UtcNow
            });
            return SaveWords(existingGameWords);

        }


    }
}
