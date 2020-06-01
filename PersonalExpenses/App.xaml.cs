using PersonalExpenses.classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace PersonalExpenses
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    { 
        public static ObservableCollection<Expense> _expenses;
        public static ObservableCollection<TotalAmount> _DatesTotalAmount;
        public static ObservableCollection<string> _uniqueDates;
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        public static List<string> _categories = new List<string> { "Food", "Clothes","Car" ,"party"};
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjUyNDIyQDMxMzgyZTMxMmUzMEdaK1dDMmhpNW96VUYxMzVMWm9oTzJ4UjVrOWZEUWFMSzUzMTNNaFRSTkE9");
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //get the data from storage 
         _expenses= MyStorage.ReadXML<ObservableCollection<Expense>>("Expenses.xml");
            
             if(_expenses==null)
            {
                _expenses = new ObservableCollection<Expense>(); 
               
               // _expenses = Generateexpenses(20);
            }
            ObservableCollection<string> uniqueDates = new ObservableCollection<string>();
            foreach (Expense ex in _expenses)
            {
                if(uniqueDates.IndexOf(ex.Datetime) < 0)
                {
                    uniqueDates.Add(ex.Datetime);
                }
            }

            ObservableCollection<TotalAmount> amountList = new ObservableCollection<TotalAmount>(); ;

            for (int i = 0; i < uniqueDates.Count(); i++)
            {
                
                double toAm = 0.0;
                for(int j = 0; j < _expenses.Count(); j++)
                {
                   
                    if (uniqueDates[i] == _expenses[j].Datetime.ToString())
                    {
                        toAm += _expenses[j].Amount;
                    }
                }
                amountList.Add(new TotalAmount { CurrentDate = uniqueDates[i], Amount = toAm });
            }
            _uniqueDates = uniqueDates;
            _DatesTotalAmount = amountList;
        }

     

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            MyStorage.WriteXml<ObservableCollection<Expense>>(_expenses, "Expenses.xml");
        }
    }

}
