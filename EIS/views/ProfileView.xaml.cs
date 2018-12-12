using EIS.model;
using EIS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EIS.views
{
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
            FormGrid.DataContext = empInfo;

            VendorGrid.Visibility = user.role.Equals("contractor") ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            empInfo.IsContractor = user.role.Equals("contractor");

            string findQuery = "select * from EmpInfo where emp_id = '" + user.emp_id + "'";
            List<EmpInfo> EmpInfoList = Connection.getData<EmpInfo>(findQuery);

            isUserPresent = (EmpInfoList.Count != 0);


            if (isUserPresent)
            {
                empInfo = EmpInfoList.First();
                FormGrid.DataContext = empInfo;
            }

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
