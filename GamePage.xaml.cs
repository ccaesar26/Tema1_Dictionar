using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tema1_Dictionar
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private int currentWordIndex = 0;
        public GamePage()
        {
            InitializeComponent();

            DataContext = new GameData();
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            if (ActionButton.Content.ToString() == "Începe")
            {
                TitleLabel.Visibility = Visibility.Hidden;
                WordPanel.Visibility = Visibility.Visible;
                ActionButton.Content = "Verifică";
                PrintNextWord();
            }
            else if (ActionButton.Content.ToString() == "Verifică")
            {
                GameData gameData = (GameData)DataContext;
                var word = gameData.RandomWords[currentWordIndex].Item1;
                
                ResultLabel.Visibility = Visibility.Visible;

                if (WordsData.RemoveDiacritics(WordTextBox.Text.ToLower()) == WordsData.RemoveDiacritics(word.WordName.ToLower()))
                {
                    gameData.Score++;
                    ResultLabel.Content = "Corect!";
                }
                else
                {
                    ResultLabel.Content = "Greșit!";
                }

                currentWordIndex++;

                if (currentWordIndex < gameData.RandomWords.Count)
                {
                    ActionButton.Content = "Următorul";
                }
                else
                {
                    WordPanel.Visibility = Visibility.Hidden;
                    TitleLabel.Visibility = Visibility.Visible;
                    TitleLabel.Content = "Scorul tău este: " + gameData.Score;
                    ActionButton.Content = "Finalizare";
                    currentWordIndex = 0;
                }
            }
            else if (ActionButton.Content.ToString() == "Următorul")
            {
                if (currentWordIndex < ((GameData)DataContext).RandomWords.Count)
                {
                    PrintNextWord();
                }
                else
                {
                    ActionButton.Content = "Finalizare";
                }
            }
            else if (ActionButton.Content.ToString() == "Finalizare")
            {
                NavigationService.GoBack();
                NavigationService.RemoveBackEntry();
            }
        }

        private void PrintNextWord()
        {
            GameData gameData = (GameData)DataContext;

            var word = gameData.RandomWords[currentWordIndex].Item1;
            var hint = gameData.RandomWords[currentWordIndex].Item2;

            if (hint == EHint.Image)
            {
                WordImage.Source = new BitmapImage(new Uri(word.ImagePath, UriKind.Absolute));
                WordDefinition.Text = "";
            }
            else
            {
                WordImage.Source = null;
                WordDefinition.Text = word.Definition;
            }

            ActionButton.Content = "Verifică";
            ResultLabel.Visibility = Visibility.Hidden;
            WordTextBox.Text = "";
        }
    }
}
