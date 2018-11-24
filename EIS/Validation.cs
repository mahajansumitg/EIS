using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EIS
{
    //public class Validation : ValidationRule
    //{
    //    public Type ValidationType { get; set; }
    //    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    //    {
    //        string strValue = Convert.ToString(value);

    //        if (string.IsNullOrEmpty(strValue))
    //            return new ValidationResult(false, $"Value cannot be coverted to string.");
    //        bool canConvert = false;
    //        switch (ValidationType.Name)
    //        {

    //            case "Boolean":
    //                bool boolVal = false;
    //                canConvert = bool.TryParse(strValue, out boolVal);
    //                return canConvert ? new ValidationResult(true, null) : new ValidationResult(false, $"Input should be type of boolean");
    //            case "Int32":
    //                int intVal = 0;
    //                canConvert = int.TryParse(strValue, out intVal);
    //                return canConvert ? new ValidationResult(true, null) : new ValidationResult(false, $"Input should be type of Int32");
    //            case "Double":
    //                double doubleVal = 0;
    //                canConvert = double.TryParse(strValue, out doubleVal);
    //                return canConvert ? new ValidationResult(true, null) : new ValidationResult(false, $"Input should be type of Double");
    //            case "Int64":
    //                long longVal = 0;
    //                canConvert = long.TryParse(strValue, out longVal);
    //                return canConvert ? new ValidationResult(true, null) : new ValidationResult(false, $"Input should be type of Int64");
    //            default:
    //                throw new InvalidCastException($"{ValidationType.Name} is not supported");
    //        }
    //    }


    //    public Boolean validation()
    //    {
    //        if (string.IsNullOrEmpty(first_name))
    //        {

    //        }
    //    }


    //public class Validation : IDataErrorInfo
    //{


    //}

    public class Validation : ObservableObject, IDataErrorInfo
    {
        public string Error { get { return null; } }
        private string _username;

        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        public string this[string name]
        {
            get
            {
                string result = null;

                switch (name)
                {
                    case "Username":
                        if (string.IsNullOrWhiteSpace(Username))
                            result = "Username cannot be empty";
                        else if (Username.Length < 5)
                            result = "Username must be a minimum of 5 characters.";
                        break;
                }

                if (ErrorCollection.ContainsKey(name))
                    ErrorCollection[name] = result;
                else if (result != null)
                    ErrorCollection.Add(name, result);

                OnPropertyChanged("ErrorCollection");
                return result;
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                OnPropertyChanged(ref _username, value);
            }
        }

    }

}

