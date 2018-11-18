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
    public partial class MainPage : Page
    {
        Login user;
        Window window;

        public MainPage(Login user, Window window)
        {
            InitializeComponent();
            this.window = window;
            this.user = user;
            DockGrid.DataContext = this.user;

            if (user.role.Equals("admin"))
            {
                DataContext = new DashBoardView(this);
                formButton.Visibility = System.Windows.Visibility.Visible;
            }
            else
                DataContext = new ProfileView(user);
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            window.Content =  new LoginPage(window);
        }

        private void switchToDashBoard(object sender, RoutedEventArgs e)
        {
            formButton.Visibility = System.Windows.Visibility.Visible;
            dashButtton.Visibility = System.Windows.Visibility.Hidden;
            DataContext = new DashBoardView(this);
        }

        private void switchToForm(object sender, RoutedEventArgs e)
        {
            dashButtton.Visibility = System.Windows.Visibility.Visible;
            formButton.Visibility = System.Windows.Visibility.Hidden;
            DataContext = new ProfileView(user);
        }
    }
}
