using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Tema1_Dictionar
{
    internal class JsonUtility
    {
        private static Word[] words;

        public static Word[] Words
        {
            get { return words; }
            set { words = value; }
        }

        public static void SerializeWords()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(words, options);
            System.IO.File.WriteAllText("../../words.json", jsonString);
        }

        public static void DeserializeWords()
        {
            string jsonString = System.IO.File.ReadAllText("../../words.json");
            words = JsonSerializer.Deserialize<Word[]>(jsonString);
        }

        public static List<Word> PullWords()
        {
            List<Word> wordsList = new List<Word>();

            DeserializeWords();

            foreach (Word word in words)
            {
                wordsList.Add(word);
            }
            return wordsList;
        }

        public static void PushWords(List<Word> wordsList)
        {
            words = wordsList.ToArray();
            SerializeWords();
        }
    }
}
