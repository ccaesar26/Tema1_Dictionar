using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1_Dictionar
{
    public class Word
    {
        private string wordName;
        private string category;
        private string definition;
        private string imagePath;

        public Word(string wordName, string category, string definition, string imagePath)
        {
            this.wordName = wordName;
            this.category = category;
            this.definition = definition;
            this.imagePath = imagePath;
        }

        public string WordName
        {
            get { return wordName; }
            set { wordName = value; }
        }

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public string Definition
        {
            get { return definition; }
            set { definition = value; }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }
    }
}
