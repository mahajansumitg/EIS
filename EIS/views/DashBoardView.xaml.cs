using System;
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
using EIS.Pages;

namespace EIS.views
{
    /// <summary>
    /// Interaction logic for DashBoardView.xaml
    /// </summary>
    public partial class DashBoardView : UserControl
    {
        private Dictionary<int, List<EmpInfo>> empInfoDict;
        private EmpInfo CurrentEmployee;
        private MainPage parent;

        private static int RecordsPerPage = 5;
        public int currentPage {get; set;}
        public int lastPage { get; set; }

        public DashBoardView()
        {
            InitializeComponent();
        }

        public DashBoardView(MainPage parent)
        {
            InitializeClassVariables(parent);
            InitializeComponent();
            InitializePaginaion();
        }

        private void InitializeClassVariables(MainPage parent)
        {
            this.parent = parent;
            this.currentPage = 1;
            this.lastPage = 1;
            empInfoDict = new Dictionary<int, List<EmpInfo>>();
        }

        private void InitializePaginaion()
        {
            string findQuery = "select * from EmpInfo;";
            List<EmpInfo> empInfoList = Connection.getData<EmpInfo>(findQuery);
            empInfoDict = getEmpInfoDictionary(empInfoList);
            pagination.DataContext = this;
            setPageInListView();
        }

        private Dictionary<int, List<EmpInfo>> getEmpInfoDictionary(List<EmpInfo> empInfoList)
        {
            Dictionary<int, List<EmpInfo>> empInfoDict = new Dictionary<int, List<EmpInfo>>();
            int page = 1;
            for (int i = 0; i < empInfoList.Count; i += RecordsPerPage)
            {
                empInfoDict.Add(page++, empInfoList.GetRange(i, Math.Min(empInfoList.Count - i, RecordsPerPage)));
            }
            lastPage = --page;
            return empInfoDict;
        }

        private void EmpChecked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            CurrentEmployee = radioButton.DataContext as EmpInfo;
        }

        private void updateCurrentEmployee(object sender, RoutedEventArgs e)
        {
            if (CurrentEmployee == null) return;
            parent.DataContext = new ProfileView(CurrentEmployee, parent);
        }

        private void deleteCurrentEmployee(object sender, RoutedEventArgs e)
        {
            if (CurrentEmployee == null) return;
            Connection.deleteData<EmpInfo>("emp_id", CurrentEmployee.emp_id);
            CurrentEmployee = null;
            InitializePaginaion();
        }

        private void prevPage(object sender, RoutedEventArgs e)
        {
            if (currentPage != 1) currentPage--;
            setPageInListView();
        }

        private void nextPage(object sender, RoutedEventArgs e)
        {
            if (currentPage != lastPage) currentPage++;
            setPageInListView();
        }

        private void pageChanged(object sender, TextChangedEventArgs e)
        {
            if (CurrentPage.Text.Equals("")) return;

            currentPage = Int32.Parse(CurrentPage.Text);
            if (currentPage < 1) currentPage = 1;
            if(currentPage > lastPage) currentPage = lastPage;
            setPageInListView();
        }

        private void setPageInListView()
        {
            if (currentPage == 0) return;

            List<EmpInfo> temp = new List<EmpInfo>();
            empInfoDict.TryGetValue(currentPage, out temp);
            lstEmpInfo.DataContext = temp;
            CurrentPage.Text = currentPage.ToString();
        }
    }
}
