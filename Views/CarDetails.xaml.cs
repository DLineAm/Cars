using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Cars.Annotations;
using Cars.Models;

namespace Cars.Views
{
    /// <summary>
    /// Логика взаимодействия для CarDetails.xaml
    /// </summary>
    public partial class CarDetails : Window, INotifyPropertyChanged
    {
        public CarDetails(Models.Cars car = null)
        {
            InitializeComponent();

            Manufactures = App.db.Manufactures.ToList();
            Type_bodies = App.db.Type_body.ToList();
            Agreements = App.db.Supplementary_agreement.ToList();
            Staff = App.db.Staff.ToList();

            if (car != null)
            {
                Car = car;

                Manufacture = Car.Manufactures;
                Type_body = Car.Type_body;
                Agreement1 = Car.Supplementary_agreement;
                Agreement2 = Car.Supplementary_agreement1;
                Agreement3 = Car.Supplementary_agreement2;
                CStaff = Car.Staff;
            }

            this.DataContext = this;
            //this._isAdd = _isAdd;
            //Car.Manufactures.Name_Manufactures
        }

        private bool _isAdd;

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

        private Manufactures _manufacture;

        public Manufactures Manufacture 
        { 
            get => _manufacture;
            set
            {
                _manufacture = value;
                OnPropertyChanged();
            }
        }

        private List<Manufactures> _manufactures;

        public List<Manufactures> Manufactures 
        { 
            get => _manufactures;
            set
            {
                _manufactures = value;
                OnPropertyChanged();
            }
        }

        private Type_body _typeBody;

        public Type_body Type_body 
        { 
            get => _typeBody;
            set
            {
                _typeBody = value;
                OnPropertyChanged();
            }
        }

        private List<Type_body> _typeBodies;

        public List<Type_body> Type_bodies 
        { 
            get => _typeBodies;
            set
            {
                _typeBodies = value;
                OnPropertyChanged();
            }
        }

        private Supplementary_agreement _agreement1;

        public Supplementary_agreement Agreement1
        { 
            get => _agreement1;
            set
            {
                _agreement1 = value;
                OnPropertyChanged();
            }
        }

        private Supplementary_agreement _agreement2;

        public Supplementary_agreement Agreement2
        { 
            get => _agreement2;
            set
            {
                _agreement2 = value;
                OnPropertyChanged();
            }
        }

        private Supplementary_agreement _agreement3;

        public Supplementary_agreement Agreement3
        { 
            get => _agreement3;
            set
            {
                _agreement3 = value;
                OnPropertyChanged();
            }
        }

        private List<Supplementary_agreement> _agreements;

        public List<Supplementary_agreement> Agreements
        { 
            get => _agreements;
            set
            {
                _agreements = value;
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


        private void Apply(object sender, RoutedEventArgs e)
        {
            Car.Manufactures = Manufacture;
            Car.Staff = CStaff;
            Car.Supplementary_agreement = Agreement1;
            Car.Supplementary_agreement1 = Agreement2;
            Car.Supplementary_agreement2 = Agreement3;
            Car.Type_body = Type_body;

            this.DialogResult = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
