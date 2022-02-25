using Cars.Annotations;
using Cars.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Cars.Models;
using static Cars.App;

namespace Cars
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Cars = db.Cars.ToList();
            Customers = db.Customers.ToList();
            Manufactures = db.Manufactures.ToList();
            SAgreements = db.Supplementary_agreement.ToList();

            FilterListMan = Manufactures;
            FilterListMan.Insert(0, new Manufactures() {Name_Manufactures = "Все"});

            SelectedFilterMan = FilterListMan[0];
        }

        private List<Models.Cars> _cars;

        public List<Models.Cars> Cars 
        { 
            get => _cars;
            set => SetField(ref _cars, value);
        }

        private Models.Cars _car;

        public Models.Cars Car 
        { 
            get => _car;
            set => SetField(ref _car, value);
        }

        private List<Customers> _cusomers;

        public List<Customers> Customers 
        { 
            get => _cusomers;
            set => SetField(ref _cusomers, value);
        }

        private Customers _customer;

        public Customers Customer 
        { 
            get => _customer;
            set => SetField(ref _customer, value);
        }

        private List<Manufactures> _manufactures;

        public List<Manufactures> Manufactures 
        { 
            get => _manufactures;
            set => SetField(ref _manufactures, value);
        }

        private Manufactures _manufacture;

        public Manufactures Manufacture 
        { 
            get => _manufacture;
            set => SetField(ref _manufacture, value);
        }

        private List<Manufactures> _filterListMan;

        public List<Manufactures> FilterListMan 
        { 
            get => _filterListMan;
            set => SetField(ref _filterListMan, value);
        }

        private Manufactures _selectedFilterMan;

        public Manufactures SelectedFilterMan 
        { 
            get => _selectedFilterMan;
            set 
            {
                SetField(ref _selectedFilterMan, value);
                CarSort();
            }
        }

        private void CarSort()
        {
            if (FilterListMan.FindIndex(p => p.Name_Manufactures == SelectedFilterMan.Name_Manufactures) == 0)
            {
                Cars = db.Cars.ToList();
                CarSearch = CarSearch;
                return;
            }

            Cars = db.Cars.Where(p => p.Manufactures.id_Manufactures == SelectedFilterMan.id_Manufactures).ToList();
        }

        private List<Supplementary_agreement> _sAgreements;

        public List<Supplementary_agreement> SAgreements 
        { 
            get => _sAgreements;
            set => SetField(ref _sAgreements, value);
        }

        private Supplementary_agreement _sAgreement;

        public Supplementary_agreement SAgreement
        { 
            get => _sAgreement;
            set => SetField(ref _sAgreement, value);
        }

        private ICommand _deleteCarCommand;

        public ICommand DeleteCarCommand => _deleteCarCommand ?? (_deleteCarCommand = new RelayCommand(param => DeleteCar()));

        private ICommand _cahngeCarCommand;

        public ICommand ChangeCarCommand => _cahngeCarCommand ?? (_cahngeCarCommand = new RelayCommand(param => ChangeCar()));

        private ICommand _addCarCommand;

        public ICommand AddCarCommand => _addCarCommand ?? (_addCarCommand = new RelayCommand(param => AddCar()));

        
        private ICommand _deleteCustomerCommand;

        public ICommand DeleteCustomerCommand => _deleteCustomerCommand ?? (_deleteCustomerCommand = new RelayCommand(param => DeleteCustomer()));


        private ICommand _changeCustomerCommand;

        public ICommand ChangeCustomerCommand => _changeCustomerCommand ?? (_changeCustomerCommand = new RelayCommand(param => ChangeCustomer()));

        private ICommand _addCustomerCommand;

        public ICommand AddCustomerCommand => _addCustomerCommand ?? (_addCustomerCommand = new RelayCommand(param => AddCustomer()));

        private ICommand _deleteManufactureCommand;

        public ICommand DeleteManufactureCommand => _deleteManufactureCommand ?? (_deleteManufactureCommand = new RelayCommand(param => DeleteManufacture()));


        private ICommand _changeManufactureCommand;

        public ICommand ChangeManufactureCommand => _changeManufactureCommand ?? (_changeManufactureCommand = new RelayCommand(param => ChangeManufacture()));


        private ICommand _addManufactureCommand;

        public ICommand AddManufactureCommand => _addManufactureCommand ?? (_addManufactureCommand = new RelayCommand(param => AddManufacture()));

        private ICommand _deleteSupAgCommand;

        public ICommand DeleteSupAgCommand => _deleteSupAgCommand ?? (_deleteSupAgCommand = new RelayCommand(param => DeleteSupAgreement()));

        private ICommand _changeSupAgCommand;

        public ICommand ChangeSupAgCommand => _changeSupAgCommand ?? (_changeSupAgCommand = new RelayCommand(param => ChangeSupAgreement()));


        private ICommand _addSupAgCommand;

        public ICommand AddSupAgCommand => _addSupAgCommand ?? (_addSupAgCommand = new RelayCommand(param => AddSupAgreement()));

        private void AddSupAgreement()
        {
            var window = new SupAgreementDetails();
            if(!(bool) window.ShowDialog()) return;
            try
            {
                db.Supplementary_agreement.Add(window.SupAgreeement);
                db.SaveChanges();
                SAgreements = db.Supplementary_agreement.ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        private void DeleteSupAgreement()
        {
            db.Supplementary_agreement.Remove(SAgreement);
            db.SaveChanges();
            SAgreements = db.Supplementary_agreement.ToList();
        }

        
        private void ChangeSupAgreement()
        {
            if (SAgreement == null)
            {
                MessageBox.Show("Сначала выберите запись!", "Ошибка", MessageBoxButton.OK);
                return;
            }
            var window = new SupAgreementDetails(SAgreement);
            if((bool)window.ShowDialog() != true) return;
            try
            {
                db.SaveChanges();
                SAgreements = db.Supplementary_agreement.ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }



        private void AddManufacture()
        {
            var window = new ManufactureDetails();
            if(!(bool) window.ShowDialog()) return;
            try
            {
                db.Manufactures.Add(window.Manufacture);
                db.SaveChanges();
                Manufactures = db.Manufactures.ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void DeleteManufacture()
        {
            if (Manufacture == null)
                return;

            db.Manufactures.Remove(Manufacture);
            db.SaveChanges();
            Manufactures = db.Manufactures.ToList();
        }

        private void ChangeManufacture()
        {
            if (Manufacture == null)
            {
                MessageBox.Show("Сначала выберите запись!", "Запись не выбрана", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var window = new ManufactureDetails(Manufacture);
            if (!(bool) window.ShowDialog()) return;
            try
            {
                db.SaveChanges();

            }
            catch (DbEntityValidationException  e)
            {
                foreach (var validationError in e.EntityValidationErrors)
                {
                    foreach (var err in validationError.ValidationErrors)
                    {
                        var obj = "Object: "+validationError.Entry.Entity;
                        MessageBox.Show(obj + "\n" + err.ErrorMessage);
                    }
                }
            }
            Manufactures = db.Manufactures.ToList();
        }


        private void AddCar()
        {
            var window = new CarDetails();
            if(!(bool) window.ShowDialog()) return;
            try
            {
                db.Cars.Add(window.Car);
                db.SaveChanges();
                Cars = db.Cars.ToList();
                CarSort();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void ChangeCar()
        {
            if (Car == null)
            {
                MessageBox.Show("Сначала выберите запись!", "Запись не выбрана", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var window = new CarDetails(Car);
            if (!(bool) window.ShowDialog()) return;
            try
            {
                db.SaveChanges();

            }
            catch (DbEntityValidationException  e)
            {
                foreach (var validationError in e.EntityValidationErrors)
                {
                    foreach (var err in validationError.ValidationErrors)
                    {
                        var obj = "Object: "+validationError.Entry.Entity;
                        MessageBox.Show(obj + "\n" + err.ErrorMessage);
                    }
                }
            }
            Cars = db.Cars.ToList();
            CarSort();
        }

        private void DeleteCar()
        {
            if (Car == null)
                return;

            db.Cars.Remove(Car);
            db.SaveChanges();
            Cars = db.Cars.ToList();
            CarSort();
        }

        private void DeleteCustomer()
        {
            if (Customer == null)
                return;

            db.Customers.Remove(Customer);
            db.SaveChanges();
            Customers = db.Customers.ToList();
        }

        private void ChangeCustomer()
        {
            if (Customer == null)
            {
                MessageBox.Show("Сначала выберите запись!", "Запись не выбрана", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var window = new CustomerDetails(Customer);
            if (!(bool) window.ShowDialog()) return;
            try
            {
                db.SaveChanges();

            }
            catch (DbEntityValidationException  e)
            {
                foreach (var validationError in e.EntityValidationErrors)
                {
                    foreach (var err in validationError.ValidationErrors)
                    {
                        var obj = "Object: "+validationError.Entry.Entity;
                        MessageBox.Show(obj + "\n" + err.ErrorMessage);
                    }
                }
            }
            Customers = db.Customers.ToList();
        }
        
        private void AddCustomer()
        {
            var window = new CustomerDetails();
            if(!(bool) window.ShowDialog()) return;
            try
            {
                db.Customers.Add(window.Customer);
                db.SaveChanges();
                Customers = db.Customers.ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private string _carSearch;

        public string CarSearch
        {
            get => _carSearch;
            set
            {
                _carSearch = value;
                OnPropertyChanged();
                if (string.IsNullOrWhiteSpace(_carSearch) || _carSearch.Length == 0)
                {
                    Cars = db.Cars.ToList();
                    return;
                }

                Cars = db.Cars.ToList().Where(p => Math.LevenshteinDistance(p.Brand, CarSearch) < 4 ||
                                          Math.LevenshteinDistance(p.Number_body, CarSearch) < 4).ToList();
            }
        }

        private string _sAgreementSearch;

        public string SAgreementSearch
        {
            get => _sAgreementSearch;
            set
            {
                _sAgreementSearch = value;
                OnPropertyChanged();
                if (string.IsNullOrWhiteSpace(_sAgreementSearch) || _sAgreementSearch.Length == 0)
                {
                    SAgreements = db.Supplementary_agreement.ToList();
                    return;
                }

                SAgreements = db.Supplementary_agreement.ToList().Where(p => Math.LevenshteinDistance(p.Name_Equipment, _sAgreementSearch) < 4 ||
                                                                             Math.LevenshteinDistance(p.Price, _sAgreementSearch) < 4).ToList();
            }
        }

        private string _manufacturesSearch;

        public string ManufacturesSearch
        {
            get => _manufacturesSearch;
            set
            {
                _manufacturesSearch = value;
                OnPropertyChanged();
                if (string.IsNullOrWhiteSpace(_manufacturesSearch) || _manufacturesSearch.Length == 0)
                {
                    Manufactures = db.Manufactures.ToList();
                    return;
                }

                Manufactures = db.Manufactures.ToList().Where(p => Math.LevenshteinDistance(p.Addres, _manufacturesSearch) < 4 ||
                                                                   Math.LevenshteinDistance(p.Name_Manufactures, _manufacturesSearch) < 4).ToList();
            }
        }

        private string _customersSearch;

        public string CustomersSearch
        {
            get => _customersSearch;
            set
            {
                _customersSearch = value;
                OnPropertyChanged();
                if (string.IsNullOrWhiteSpace(_customersSearch) || _customersSearch.Length == 0)
                {
                    Customers = db.Customers.ToList();
                    return;
                }

                Customers = db.Customers.ToList().Where(p => Math.LevenshteinDistance(p.Addres, _customersSearch) < 4 ||
                                                             Math.LevenshteinDistance(p.Pasport_data, _customersSearch) < 4 ||
                                                             Math.LevenshteinDistance(p.FIO, _customersSearch) < 4).ToList();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] [CanBeNull] string propName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propName);
            return true;
        }
    }
}
