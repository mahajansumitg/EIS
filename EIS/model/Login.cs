using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EIS.model
{
    public class Login : INotifyPropertyChanged
    {
        public string user_name;
        public string pswd;
        public string role;
        public string emp_id;
        public string UserName
        {
            get { return user_name; }
            set { OnPropertyChanged(ref user_name, value); }
        }

        public string PSWD
        {
            get { return pswd; }
            set { OnPropertyChanged(ref pswd, value); }
        }
        public string Role
        {
            get { return role; }
            set { OnPropertyChanged(ref role, value); }
        }
        public string EmpId
        {
            get { return emp_id; }
            set { OnPropertyChanged(ref emp_id, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static string GetFormatedDate(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged<T>(ref T backingField, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value)) return;

            backingField = value;
            OnPropertyChanged(propertyName);
        }
    }
}
