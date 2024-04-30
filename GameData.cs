using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1_Dictionar
{
    public enum EHint
    {
        Image,
        Description
    }

    internal class GameData
    {
        public static int wordsNumber = 5;

        public int Score { get; set; } = 0;

        public ObservableCollection<Tuple<Word, EHint>> RandomWords { get; set; }

        public GameData()
        {
            RandomWords = new ObservableCollection<Tuple<Word, EHint>>(GetFiveRandomWords());
        }

        private List<Tuple<Word, EHint>> GetFiveRandomWords()
        { 
            var w = new List<Tuple<Word, EHint>>();

            Random random = new Random();

            ISet<int> usedIndices = new HashSet<int>();

            for (int i = 0; i < wordsNumber; i++) 
            {
                int randomIndex;
                do
                {
                    randomIndex = random.Next(0, WordsData.Words.Count);
                }
                while (usedIndices.Contains(randomIndex));
                usedIndices.Add(randomIndex);

                EHint randomHint = (EHint) random.Next(0, 2);

                if (WordsData.Words[randomIndex].ImagePath.Contains("no_image"))
                {
                    randomHint = EHint.Description;
                }
                
                w.Add(new Tuple<Word, EHint>(WordsData.Words[randomIndex], randomHint));
            }

            return w;
        }
    }
}
