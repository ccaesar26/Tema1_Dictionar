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
    /// Interaction logic for AdminLogInPage.xaml
    /// </summary>
    public partial class AdminLogInPage : Page
    {
        public AdminLogInPage()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            if (Admin.CheckAdmin(username.Text, password.Password))
            {
                NavigationService.Navigate(new AdminPage());
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
        }
    }
}
