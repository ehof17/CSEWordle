using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WordleLogicServiceApplication
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GenerateWord(string gameWordsXML);

        [OperationContract]
        ValidResponse IsValidGuess(string wordsXML,  string guess);

        [OperationContract]
        List<WordLetter> WordGuessChecker(string userGuess, string actualWord);

        [OperationContract]
        List<WordLetter> ConvertToDisplay(List<WordLetter> guess);

        [OperationContract]
        string GetHint(string actualWord, List<int> revealedPositions);
        [OperationContract]
        string SaveWordToList(string existingWordXML, string wordToAdd, string username);

    }
    [DataContract]
    public class WordLetter
    {
        [DataMember]
        public char Letter { get; set; }

        [DataMember]
        public LetterStatus Status { get; set; }

        [DataMember]
        public int Position { get; set; }

        public enum LetterStatus
        {
            Unknown = 0,
            CorrectLetter = 1,
            CorrectLetterWrongSpot = 2,
            IncorrectLetter = 3
        }

        public WordLetter(char letter)
        {
            Letter = letter;
            Status = LetterStatus.Unknown;
            Position = 0;
        }
    }

    [DataContract]
    public class ValidResponse
    {
        [DataMember]
        public bool isValidWord { get; set; }
        [DataMember]
        public string Message { get; set; }
    }


    [DataContract]
    public class GameWord
    {
  

        [DataMember(Order =0)]
        public string Word
        {
            get;
            set;
        }

        [DataMember(Order =1)]
        public string UsernameAdded
        {
            get;
            set;
        }
        [DataMember(Order = 2)] public DateTime TimeAdded { get; set; }
 
    }
    [CollectionDataContract(Name = "GameWords", ItemName = "GameWord")]
    public class GameWordList : List<GameWord>
    {
        public GameWordList()
        {
        }
        public GameWordList(IEnumerable<GameWord> collection) : base(collection)
        {
        }
    }
}

