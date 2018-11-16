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

namespace EIS.views
{
    /// <summary>
    /// Interaction logic for FormView.xaml
    /// </summary>
    public partial class FormView : UserControl
    {
        public FormView(Login user)
        {
            InitializeComponent();
            initializeForm(user);
        }

        private void initializeForm(Login user){
            EmpId.Text = user.emp_id;
            VendorGrid.Visibility = user.role.Equals("contractor") ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
        }

        private void submitForm(object sender, RoutedEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();
            empInfo.first_name = FirstName.Text;
            empInfo.middle_name = MiddleName.Text;
            empInfo.last_name = LastName.Text;
            empInfo.email_id = EmailId.Text;
            empInfo.emp_id = EmpId.Text;
            empInfo.dob = DOB.Text;
            empInfo.date_of_joining = DOJ.Text;
            empInfo.date_of_leaving = DOL.Text;
            empInfo.city = City.Text;
            empInfo.address = Address.Text;
            empInfo.department = Dept.Text;
            empInfo.salary =  Int32.Parse(Salary.Text);
            empInfo.vendor = Vendor.Text;

            Connection.setData(empInfo);
    }
        
    }
}
