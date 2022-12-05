using ISRPO_Cherednichenko402_PR14.ApplicationData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ISRPO_Cherednichenko402_PR14.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAdd.xaml
    /// </summary>
    public partial class PageAdd : Page
    {

        private Pechi _currentPech = new Pechi();

        public PageAdd(Pechi selectedPech)
        {
            InitializeComponent();
            if (selectedPech != null)
                _currentPech = selectedPech;


            DataContext = _currentPech;

            ComboColour.ItemsSource = DBEntities.GetContext().Colour.ToList();

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.AppFrame.MainFrame.GoBack();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(_currentPech.Name))
                errors.AppendLine("Укажите наименование");
            if (_currentPech.Colour == null)
                errors.AppendLine("Выберите цвет");
            if (_currentPech.Cena <= 0)
                errors.AppendLine("Цена не может быть ниже или равняться 0");
            if (_currentPech.Sklad < 0)
                errors.AppendLine("Количество не может быть ниже 0");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //добавление
            if (_currentPech.idPech == 0)
                DBEntities.GetContext().Pechi.Add(_currentPech);

            //сохранение
            try
            {
                DBEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
