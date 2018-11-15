using EIS.model;
using EIS.views;
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

namespace EIS.Pages
{
    /// <summary>
    /// Interaction logic for mainPage.xaml
    /// </summary>
    public partial class mainPage : Page
    {
        Login user;
        LoginWindow parent;

        public mainPage(Login user, LoginWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.user = user;
            userName.Content = user.user_name;
            userId.Content = user.emp_id;
            userRole.Content = user.role;

            if (user.role.Equals("admin"))
            {
                DataContext = new DashBoardView();
                dashButtton.Visibility = System.Windows.Visibility.Visible;
                formButton.Visibility = System.Windows.Visibility.Visible;
            }
            else
                DataContext = new ProfileView(user);
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            Connection.close();
        }

        private void switchToDashBoard(object sender, RoutedEventArgs e)
        {
            formButton.Visibility = System.Windows.Visibility.Visible;
            dashButtton.Visibility = System.Windows.Visibility.Hidden;
            DataContext = new DashBoardView();
        }

        private void switchToForm(object sender, RoutedEventArgs e)
        {
            dashButtton.Visibility = System.Windows.Visibility.Visible;
            formButton.Visibility = System.Windows.Visibility.Hidden;
            DataContext = new ProfileView(user);
        }
    }
}
