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
    }
}
