using EIS.model;
using EIS.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.CompilerServices;

using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using static EIS.model.EmpInfo;

namespace EIS.views
{
    /// <summary>
    /// Interaction logic for FormView.xaml
    /// </summary>
    public partial class ProfileView : UserControl
    {
        Boolean isUserPresent;
        MainPage parent;

        public EmpInfo empInfo = new EmpInfo();
       
        public ProfileView()
        {
            InitializeComponent();
        }

        public ProfileView(EmpInfo empInfo, MainPage parent)
        {
            this.parent = parent;
            this.empInfo = empInfo;
            this.isUserPresent = true;

            InitializeComponent();
            FormGrid.DataContext = this.empInfo;
        }

        public ProfileView(Login user)
        {
            empInfo.emp_id = user.emp_id;
            InitializeComponent();

            VendorGrid.Visibility = user.role.Equals("contractor") ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;

            string findQuery = "select * from EmpInfo where emp_id = '" + user.emp_id + "'";
            List<EmpInfo> EmpInfoList = Connection.getData<EmpInfo>(findQuery);

            isUserPresent = (EmpInfoList.Count != 0);


            if(isUserPresent) empInfo = EmpInfoList.First();

            FormGrid.DataContext = empInfo;
        }

        private void updateProfile(object sender, RoutedEventArgs e)
        {
            if (isUserPresent)
                Connection.updateData(empInfo, "emp_id");
            else
                Connection.setData(empInfo);

            MessageBox.Show("Profile successfully updated");

            if (parent != null) parent.DataContext = new DashBoardView(parent);
        }
    }

}
