﻿using System;
using EIS.model;
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
    /// Interaction logic for DashBoardView.xaml
    /// </summary>
    public partial class DashBoardView : UserControl
    {
        public DashBoardView()
        {
            InitializeComponent();
            InitializePaginaion();
        }

        private void InitializePaginaion()
        {
            string findQuery = "select * from EmpInfo;";
            List<EmpInfo> EmpInfoList = Connection.getData<EmpInfo>(findQuery);
            lstEmpInfo.DataContext = EmpInfoList;
        }
    }
}
