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
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        public String SelectedWord { get; set; }

        public SearchPage()
        {
            InitializeComponent();

            DataContext = new WordsData();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            WordsData.SearchWord(SearchTextBox.Text, SearchTypeComboBox.SelectedValue as String);
            if (SearchTextBox.Text == "")
            {
                SearchResultsListBox.ItemsSource = null;
                SearchResultsListBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (WordsData.SearchResults.Count != 0)
                {
                    SearchResultsListBox.ItemsSource = WordsData.SearchResults;
                    SearchResultsListBox.Visibility = Visibility.Visible;
                }
                else
                {
                    SearchResultsListBox.ItemsSource = null;
                    SearchResultsListBox.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void SearchResultsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SearchResultsListBox.SelectedItem != null)
            {
                SelectedWord = SearchResultsListBox.SelectedItem.ToString();
                SearchResultsListBox.Visibility = Visibility.Collapsed;
                SearchTextBox.Text = "";
                SearchResultsListBox.ItemsSource = null;
                UpdateWordPanel();
            }
        }

        private void UpdateWordPanel()
        {
            if (SelectedWord != null)
            {
                WordPanel.Visibility = Visibility.Visible;
                Word word = WordsData.GetWord(SelectedWord);
                if (word != null)
                {
                    WordNameLabel.Content = word.WordName;
                    CategoryLabel.Content = word.Category;
                    DefinitionTextBlock.Text = word.Definition;
                    Image.Source = new BitmapImage(new Uri(word.ImagePath, UriKind.Absolute));
                }
            }
            else
            {
                WordPanel.Visibility = Visibility.Collapsed;
            }
        }
    }
}
