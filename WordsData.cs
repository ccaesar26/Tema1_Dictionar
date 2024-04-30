using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1_Dictionar
{
    internal class WordsData
    {
        public static ObservableCollection<Word> Words { get; set; }
        public static ObservableCollection<String> Categories { get; set; }
        public static ObservableCollection<String> SearchTypes { get; set; }
        public static ObservableCollection<String> SearchResults { get; set; }

        static WordsData()
        {
            Words = new ObservableCollection<Word>(JsonUtility.PullWords());
            SearchResults = new ObservableCollection<string>();
            UpdateCategories();
        }

        public static void AddWord(Word word)
        {
            Words.Add(word);
            UpdateCategories();
            JsonUtility.PushWords(Words.ToList());
        }

        public static void RemoveWord(Word word)
        {
            Words.Remove(word);
            UpdateCategories();
            JsonUtility.PushWords(Words.ToList());
        }

        public static void ModifyWord(Word oldWord, Word newWord)
        {
            int index = Words.IndexOf(oldWord);
            Words[index] = newWord;
            UpdateCategories();
            JsonUtility.PushWords(Words.ToList());
        }

        public static List<Word> GetWordsByCategory(string category)
        {
            if (category == "Toate")
            {
                return Words.ToList();
            }

            List<Word> wordsByCategory = new List<Word>();

            foreach (Word word in Words)
            {
                if (word.Category == category)
                {
                    wordsByCategory.Add(word);
                }
            }

            return wordsByCategory;
        }

        public static void SearchWord(string search, string category = "Toate")
        {
            List<Word> wordsBySearch = new List<Word>();

            List<Word> ToSearchWords = GetWordsByCategory(category);

            foreach (Word word in ToSearchWords)
            {
                string w = RemoveDiacritics(word.WordName.ToLower());
                string s = RemoveDiacritics(search.ToLower());
                if (w.Contains(s))
                {
                    wordsBySearch.Add(word);
                }
            }

            SearchResults = new ObservableCollection<string>(wordsBySearch.Select(x => x.WordName));
        }

        public static void AddCategory(string category)
        {
            Categories.Add(category);
        }

        private static void UpdateCategories()
        {
            Categories = new ObservableCollection<string>();

            foreach (Word word in Words)
            {
                if (!Categories.Contains(word.Category))
                {
                    Categories.Add(word.Category);
                }
            }

            UpdateSearchTypes();
        }

        private static void UpdateSearchTypes()
        {
            SearchTypes = new ObservableCollection<string>
            {
                "Toate"
            };
            foreach (var item in Categories)
            {
                SearchTypes.Add(item);
            }
        }

        public static string RemoveDiacritics(string text)
        {
            string normalized = text.Normalize(NormalizationForm.FormD);
            var builder = new StringBuilder();

            foreach (char c in normalized)
            {
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(c);
                if (category != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(c);
                }
            }

            return builder.ToString().Normalize(NormalizationForm.FormC);
        }

        internal static Word GetWord(string selectedWord)
        {
            foreach (Word word in Words)
            {
                if (word.WordName == selectedWord)
                {
                    return word;
                }
            }
            return null;
        }
    }
}
