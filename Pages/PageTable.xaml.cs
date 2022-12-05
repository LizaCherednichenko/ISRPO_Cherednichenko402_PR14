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
    /// Логика взаимодействия для PageTable.xaml
    /// </summary>
    public partial class PageTable : Page
    {
        public PageTable()
        {
            InitializeComponent();
            DGTable.ItemsSource = DBEntities.GetContext().Pechi.ToList();
        }


        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageAdd(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DBEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGTable.ItemsSource = DBEntities.GetContext().Pechi.ToList();
            }
        }

        private void BtnRedact_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageAdd((sender as Button).DataContext as Pechi));
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var itemsForDeleted = DGTable.SelectedItems.Cast<Pechi>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующее {itemsForDeleted.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DBEntities.GetContext().Pechi.RemoveRange(itemsForDeleted);
                    DBEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                    DGTable.ItemsSource = DBEntities.GetContext().Pechi.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
