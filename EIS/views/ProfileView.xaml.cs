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

namespace EIS.views
{
    /// <summary>
    /// Interaction logic for FormView.xaml
    /// </summary>
    public partial class ProfileView : UserControl
    {
        Boolean isUserPresent = false;
        MainPage parent;

        public ProfileView()
        {
            InitializeComponent();
        }

        public ProfileView(EmpInfo empInfo, MainPage parent)
        {
            this.parent = parent;
            InitializeComponent();
            initializeForm(empInfo);
        }

        public ProfileView(Login user)
        {
            InitializeComponent();
            initializeForm(user);
        }

        private void initializeForm(Login user)
        {
            EmpId.Text = user.emp_id;
            VendorGrid.Visibility = user.role.Equals("contractor") ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;

            string findQuery = "select * from EmpInfo where emp_id = '" + user.emp_id + "'";
            List<EmpInfo> EmpInfoList = Connection.getData<EmpInfo>(findQuery);

            if (EmpInfoList.Count == 0) return;

            initializeForm(EmpInfoList.First());
        }
        private void initializeForm(EmpInfo empInfo)
        {
            this.isUserPresent = true;

            FirstName.Text = empInfo.first_name;
            MiddleName.Text = empInfo.middle_name;
            LastName.Text = empInfo.last_name;
            EmailId.Text = empInfo.email_id;
            EmpId.Text = empInfo.emp_id;
            DOB.Text = empInfo.dob.ToString().Substring(0, 10);
            DOJ.Text = empInfo.doj.ToString().Substring(0, 10);
            DOL.Text = empInfo.dol.ToString().Substring(0, 10);
            City.Text = empInfo.city;
            Address.Text = empInfo.address;
            Dept.Text = empInfo.department;
            Salary.Text = empInfo.salary.ToString();
            Vendor.Text = empInfo.vendor;
        }

        private void updateProfile(object sender, RoutedEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();
            empInfo.first_name = FirstName.Text;
            empInfo.middle_name = MiddleName.Text;
            empInfo.last_name = LastName.Text;
            empInfo.email_id = EmailId.Text;
            empInfo.emp_id = EmpId.Text;
            empInfo.dob = DateTime.Parse(DOB.Text);
            empInfo.doj = DateTime.Parse(DOJ.Text);
            empInfo.dol = DateTime.Parse(DOL.Text);
            empInfo.city = City.Text;
            empInfo.address = Address.Text;
            empInfo.department = Dept.Text;
            empInfo.salary = Int32.Parse(Salary.Text);
            empInfo.vendor = Vendor.Text;

            if (isUserPresent)
                Connection.updateData(empInfo, "emp_id");
            else
                Connection.setData(empInfo);

            MessageBox.Show("Profile successfully uodated");

            if (parent != null) parent.DataContext = new DashBoardView(parent);
        }

    }
}
