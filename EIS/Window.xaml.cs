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
    public partial class Window : System.Windows.Window
    {
        public Window()
        {
            InitializeComponent();
            this.Content = new LoginPage(this);
        }
    }
}
