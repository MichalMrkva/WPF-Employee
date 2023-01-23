using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Threading;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace WpfBinding
{
    public partial class MainWindow : Window
    {
        

        Employee em = new Employee()
        {
            FName = "",
            SName = "",
            BDay = "01.01.2000",
            HGrad = "Základní škola",
            Job = "",
            Salary = ""

        };
        public MainWindow()
        {
            InitializeComponent();
            DataContext = em;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if (em.FName != "" && em.SName != "" && em.BDay != "" && em.HGrad != "" && em.Job != "" && Double.Parse(em.Salary) > 1000)
            {

                string s = "";
                int i = 1;
                using (StreamReader sr = new StreamReader("Employees.txt"))
                {
                    while (sr.ReadLine() != null) { i++; }
                }
                using (StreamReader sr = new StreamReader("Employees.txt"))
                {
                    s = sr.ReadToEnd();
                }
                using (StreamWriter sw = new StreamWriter("Employees.txt"))
                {
                    sw.Write(s);
                    sw.Write($"Člověk číslo: {i / 2 + 1} ");
                    sw.Write("Jméno: " + em.FName + " ");
                    sw.Write("Příjmení: " + em.SName + " ");
                    sw.Write("Narozeniny: " + em.BDay + " ");
                    sw.Write("Vzdělání: " + em.HGrad + " ");
                    sw.Write("Pracovní pozice: " + em.Job + " ");
                    sw.WriteLine("Plat: " + em.Salary.ToString() + " Kč");
                    ableToSave.Content = $"Uložen člověk {i / 2 + 1}";
                }
                em.FName = "";
                em.SName = "";
                em.BDay = "";
                em.HGrad = "";
                em.Job = "";
                em.Salary = "";
            }
            else
            {
                ableToSave.Content = "Jedna nebo více z možností nejsou vyplněny";
            }
        }
    }
    class Person : MainWindow, INotifyPropertyChanged
    {
        Regex str = new Regex(@"^[a-z]{3,20}$");
        Regex bday = new Regex(@"(?<day>0[1-9]|1[0-9]|2[0-9]|3[01]).(?<month> 0[1 - 9] | 1[012]).(?<year>[0 - 9]{4})");
        Regex salary = new Regex(@"^\d{5,6}$");
        private string fName;
        public string FName
        {
            get => fName;
            set
            {
                if (str.IsMatch(value))
                {
                    fName = value;
                    //tbFName.Background = ;
                    //labErrorFirstName.Background = "";
                }
                else
                {

                }
                OnPropertyChanged("FName");
                
            }
        }
        private string sName;
        public string SName
        {
            get => sName;
            set
            {
                sName = value;
                OnPropertyChanged("SName");
                
            }
        }
        private string bDay;
        public string BDay
        {
            get => bDay;
            set
            {
                bDay = value;
                OnPropertyChanged("BDay");
                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
    class Employee : Person
    {
        private string hGrad;
        public string HGrad
        {
            get => hGrad;
            set
            {
                hGrad = value;
                OnPropertyChanged("HGrad");
                
            }
        }
        private string job;
        public string Job
        {
            get => job;
            set
            {
                job = value;
                OnPropertyChanged("Job");
                
            }
        }
        private string salary;
        public string Salary
        {
            get => salary;
            set
            {
                salary = value;
                OnPropertyChanged("Salary");
                
            }
        }

    }
}
