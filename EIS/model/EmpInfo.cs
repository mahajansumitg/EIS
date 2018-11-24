using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace EIS.model
{
    public class EmpInfo : INotifyPropertyChanged, IDataErrorInfo
    {
        public string first_name;
        public string middle_name;
        public string last_name;
        public string email_id;
        public string emp_id;
        public string city;
        public string address;
        public string department;
        public string vendor;

        public DateTime dob;
        public DateTime doj;
        public DateTime dol;

        public string salary;

        public string FirstName
        {
            get { return first_name; }
            set { OnPropertyChanged(ref first_name, value); }
        }

        public string MiddleName
        {
            get { return middle_name; }
            set { OnPropertyChanged(ref middle_name, value); }
        }

        public string LastName
        {
            get { return last_name; }
            set { OnPropertyChanged(ref last_name, value); }
        }

        public string EmailId
        {
            get { return email_id; }
            set { OnPropertyChanged(ref email_id, value); }
        }

        public string EmpId
        {
            get { return emp_id; }
            set { OnPropertyChanged(ref emp_id, value); }
        }

        public string City
        {
            get { return city; }
            set { OnPropertyChanged(ref city, value); }
        }

        public string Address
        {
            get { return address; }
            set { OnPropertyChanged(ref address, value); }
        }

        public string Department
        {
            get { return department; }
            set { OnPropertyChanged(ref department, value); }
        }

        public string Vendor
        {
            get { return vendor; }
            set { OnPropertyChanged(ref vendor, value); }
        }

        public string DOB
        {
            get
            {
                return GetFormatedDate(dob);
            }
            set
            {
                OnPropertyChanged(ref dob, DateTime.Parse(value));
            }
        }

        public string DOJ
        {
            get
            {
                return GetFormatedDate(doj);
            }
            set
            {
                OnPropertyChanged(ref doj, DateTime.Parse(value));
            }
        }

        public string DOL
        {
            get
            {
                return GetFormatedDate(dol);
            }
            set
            {
                OnPropertyChanged(ref dol, DateTime.Parse(value));
            }
        }

        public int Salary
        {
            get { return Int32.Parse(salary); }
            set { OnPropertyChanged(ref salary, value.ToString()); }
        }

        //INotifyPropertyChanged
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

        //IDataErrorInfo
        public string Error { get { return null; } }

        public Dictionary<string, string> ErrorCollection { get; set; } = new Dictionary<string, string>();

        public string this[string name]
        {
            get
            {
                string result = null;
                Regex avoidSpecialChar = new Regex("^[a-zA-Z]*$");
                Regex emailPattern = new Regex("[/w -] +@([/w -] +//.) +[/w -] + ");
                switch (name)
                {
                    case "FirstName":
                        if (string.IsNullOrWhiteSpace(first_name)) result = name + " should not be empty";
                        else if (!avoidSpecialChar.IsMatch(first_name)) result = name + " should not contain numbers and special chars";
                        break;
                    case "MiddleName":
                        if (string.IsNullOrWhiteSpace(middle_name)) result = name + " should not be empty";
                        else if (!avoidSpecialChar.IsMatch(middle_name)) result = name + " should not contain  numbers and special chars";
                        break;
                    case "LastName":
                        if (string.IsNullOrWhiteSpace(last_name)) result = name + " should not be empty";
                        else if (!avoidSpecialChar.IsMatch(last_name)) result = name + " should not contain  numbers and special chars";
                        break;
                    case "EmailId":
                        if (string.IsNullOrWhiteSpace(city)) result = name + " should not be empty";
                        //else if (!emailPattern.IsMatch(last_name)) result = "Invalid Email Id";
                        break;
                    case "City":
                        if (string.IsNullOrWhiteSpace(city)) result = name + " should not be empty";
                        break;
                    case "Address":
                        if (string.IsNullOrWhiteSpace(address)) result = name + " should not be empty";
                        break;
                    case "Department":
                        if (string.IsNullOrWhiteSpace(department)) result = name + " should not be empty";
                        break;
                    case "Salary":
                        if (string.IsNullOrWhiteSpace(salary)) result = name + " should not be empty";
                        else if (Int32.Parse(salary) <= 0) result = name + " should be more than 0";
                        break;
                    case "DOB":
                        if (string.IsNullOrWhiteSpace(DOB)) result = name + " should not be empty";
                        break;
                    case "DOJ":
                        if (string.IsNullOrWhiteSpace(DOJ)) result = name + " should not be empty";
                        break;
                    case "Vendor":
                        if (string.IsNullOrWhiteSpace(Vendor)) result = name + " should not be empty";
                        break;

                }

                if (ErrorCollection.ContainsKey(name))
                    ErrorCollection[name] = result;
                else if (result != null)
                    ErrorCollection.Add(name, result);

                foreach (string value in ErrorCollection.Values)
                {
                    if (value != null)
                    {
                        SubmitEnabled = false;
                        break;
                    }
                    SubmitEnabled = true;
                }

                OnPropertyChanged("ErrorCollection");
                OnPropertyChanged("SubmitEnabled");
                return result;
            }
        }

        //Button Enable
        public bool SubmitEnabled { get; set; } = true;
    }
}
