using EIS.model;
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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        Window window;

        public LoginPage(Window window)
        {
            this.window = window;
            InitializeComponent();
        }

        private void login(object sender, RoutedEventArgs e)
        {
            string loginQuery = "select * from Login where user_name = '" + userName.Text + "'";
            List<Login> loginList = Connection.getData<Login>(loginQuery);

            if (loginList.Count() > 0 && loginList.First().pswd == pswd.Password)
                window.Content = new MainPage(loginList.First(), window);
            else
                MessageBox.Show("Entered user_name or password is incorrect");
        }
    }
}
