using Cars.Annotations;
using Cars.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using static Cars.App;

namespace Cars.Views
{
    /// <summary>
    /// Логика взаимодействия для ManufactureDetails.xaml
    /// </summary>
    public partial class ManufactureDetails : Window, INotifyPropertyChanged
    {
        public ManufactureDetails(Manufactures manufacture = null)
        {
            InitializeComponent();
            //Manufactures = db.Manufactures.ToList();
            Staff = db.Staff.ToList();

            if (manufacture != null)
            {
                Manufacture = manufacture;
                CStaff = manufacture.Staff;
                //CStaff = customer.Staff;
                //Mark = customer.Completion_mark;
            }

            this.DataContext = this;
        }

        private void Apply(object sender, RoutedEventArgs e)
        {
            Manufacture.Staff = CStaff;

            this.DialogResult = true;
        }

        private Manufactures _manufacture = new Manufactures();

        public Manufactures Manufacture 
        { 
            get => _manufacture;
            set => _manufacture = value;
        }

        private Staff _cstaff;

        public Staff CStaff
        { 
            get => _cstaff;
            set => SetField(ref _cstaff, value);
        }

        private List<Staff> _staff;

        public List<Staff> Staff
        { 
            get => _staff;
            set => SetField(ref _staff, value);
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
