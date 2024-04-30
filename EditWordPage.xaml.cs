using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for EditWordPage.xaml
    /// </summary>
    public partial class EditWordPage : Page
    {
        private Word initialWord;

        public EditWordPage()
        {
            InitializeComponent();
            TitleLabel.Content = "Adăugare cuvânt";
            CategoryComboBox.ItemsSource = WordsData.Categories;
            initialWord = null;
        }

        public EditWordPage(Word word)
        {
            InitializeComponent();
            TitleLabel.Content = "Editare cuvânt";
            WordTextBox.Text = word.WordName;
            CategoryComboBox.ItemsSource = WordsData.Categories;
            CategoryComboBox.SelectedItem = word.Category;
            DefinitionTextBox.Text = word.Definition;
            Image.Source = new BitmapImage(new Uri(word.ImagePath, UriKind.Absolute));
            initialWord = word;
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewCategoryWindow createNewCategoryWindow = new CreateNewCategoryWindow();
            createNewCategoryWindow.ShowDialog();
            CategoryComboBox.SelectedItem = createNewCategoryWindow.CategoryName;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                string sourcePath = openFileDialog.FileName;
                string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                string destinationFolder = System.IO.Path.Combine(projectDirectory, "Words");
                string filename = System.IO.Path.GetFileName(sourcePath);
                string destinationPath = System.IO.Path.Combine(destinationFolder, filename);

                try
                {
                    File.Copy(sourcePath, destinationPath, true);
                    Image.Source = new BitmapImage(new Uri(destinationPath, UriKind.Absolute));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }   
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string wordName = WordTextBox.Text;
            string category = CategoryComboBox.Text;
            string definition = DefinitionTextBox.Text;
            string imagePath = Image.Source.ToString();

            Word word = new Word(wordName, category, definition, imagePath);

            if (wordName == "" || category == "" || definition == "")
            {
                MessageBox.Show("Toate câmpurile trebuie completate!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (initialWord != null)
            {
                WordsData.RemoveWord(initialWord);
            }

            WordsData.AddWord(word);

            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
        }
    }
}
