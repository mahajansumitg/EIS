using EIS.model;
using EIS.Pages;
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

namespace EIS
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void login(object sender, RoutedEventArgs e)
        {
            string loginQuery = "select * from Login where user_name = '" + userName.Text + "'";
            List<Login> loginList = Connection.getData<Login>(loginQuery);

            if (loginList.Count() > 0 && loginList.First().pswd == pswd.Password)
                this.Content = new mainPage(loginList.First(), this);
            else
                MessageBox.Show("Entered user_name or password is incorrect");
        }
    }
}
