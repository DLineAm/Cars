using Cars.Annotations;
using Cars.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using static Cars.App;

namespace Cars.Views
{
    /// <summary>
    /// Логика взаимодействия для CustomerDetails.xaml
    /// </summary>
    public partial class CustomerDetails : Window, INotifyPropertyChanged
    {
        public CustomerDetails(Customers customer = null)
        {
            InitializeComponent();
            Cars = db.Cars.ToList();
            Staff = db.Staff.ToList();

            if (customer != null)
            {
                Customer = customer;
                Car = customer.Cars;
                CStaff = customer.Staff;
                Mark = customer.Completion_mark;
            }

            this.DataContext = this;
        }

        private void Apply(object sender, RoutedEventArgs e)
        {
            Customer.Cars = Car;
            Customer.Staff = CStaff;
            Customer.Completion_mark = Mark.ToString();

            this.DialogResult = true;
        }
        private Customers _customer = new Customers();

        public Customers Customer 
        { 
            get => _customer;
            set
            {
                _customer = value;
                //OnPropertyChanged();
            }
        }

        private Models.Cars _car = new Models.Cars();

        public Models.Cars Car 
        { 
            get => _car;
            set
            {
                _car = value;
                //OnPropertyChanged();
            }
        }

        private List<Models.Cars> _cars;

        public List<Models.Cars> Cars 
        { 
            get => _cars;
            set
            {
                _cars = value;
                OnPropertyChanged();
            }
        }

        private Staff _cstaff;

        public Staff CStaff
        { 
            get => _cstaff;
            set
            {
                _cstaff = value;
                OnPropertyChanged();
            }
        }

        private List<Staff> _staff;

        public List<Staff> Staff
        { 
            get => _staff;
            set
            {
                _staff = value;
                OnPropertyChanged();
            }
        }

        private string _flag;

        public string Mark
        { 
            get => _flag;
            set
            {
                _flag = value;
                OnPropertyChanged();
            }
        }

        private List<string> _flags = new List<string>{ "Завершено", "Не завершено"};

        public List<string> Marks
        { 
            get => _flags;
            set
            {
                _flags = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
