using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Threading;

namespace WpfBinding
{
    public partial class MainWindow : Window
    {

        Employee em = new Employee()
        {
            FName = "",
            SName = "",
            BDay = "",
            HGrad = "",
            Job = "",
            Salary = 0

        };
        public MainWindow()
        {
            InitializeComponent();
            DataContext = em;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if (em.FName != "" && em.SName != "" && em.BDay != "" && em.HGrad != "" && em.Job != "" && em.Salary > 0)
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
                    sw.WriteLine($"Člověk číslo: {i / 2 + 1}");
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
                em.Salary = 0;
            }
            else
            {
                ableToSave.Content = "Jedna nebo více z možností nejsou vyplněny";
            }
        }
    }
    class Person : INotifyPropertyChanged
    {
        private string fName;
        public string FName
        {
            get => fName;
            set
            {
                fName = value;
                OnPropertyChanged("FName");
                OnPropertyChanged("Status");
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
                OnPropertyChanged("Status");
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
                OnPropertyChanged("Status");
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
                OnPropertyChanged("Status");
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
                OnPropertyChanged("Status");
            }
        }
        private int salary;
        public int Salary
        {
            get => salary;
            set
            {
                salary = value;
                OnPropertyChanged("Salary");
                OnPropertyChanged("Status");
            }
        }

    }
}
