using Cars.Models;
using System.Windows;

namespace Cars
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static CarShowroomEntities5 db = new CarShowroomEntities5();
    }
}
