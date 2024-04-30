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
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();

            DataContext = new WordsData();
        }

        private void AddWordButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditWordPage());
        }

        private void ModifyWordButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button != null)
            {
                Word word = button.DataContext as Word;

                if (word != null)
                {
                    NavigationService.Navigate(new EditWordPage(word));
                }
            }
        }

        private void DeleteWordButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button != null)
            {
                Word word = button.DataContext as Word;

                if (word != null)
                {
                    WordsData.RemoveWord(word);
                }
            }
        }
        
    }
}
