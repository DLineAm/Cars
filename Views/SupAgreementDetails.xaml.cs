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
    /// Логика взаимодействия для SupAgreementDetails.xaml
    /// </summary>
    public partial class SupAgreementDetails : Window
    {
        public SupAgreementDetails(Supplementary_agreement sup = null)
        {
            InitializeComponent();

            if (sup != null)
            {
                SupAgreeement = sup;
            }

            this.DataContext = this;
        }

        private void Apply(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public Supplementary_agreement SupAgreeement { get; set; } = new Supplementary_agreement();


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
