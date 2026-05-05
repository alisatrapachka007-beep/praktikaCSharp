using System.Windows;
using MedicalRecordsApp.ViewModels;

namespace MedicalRecordsApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Программа для учета медицинских записей\n\nВерсия 3.0\n\n" +
                "Горячие клавиши:\n" +
                "Ctrl+R - Записать пациента\n" +
                "Ctrl+E - Редактировать запись\n" +
                "Del - Удалить запись",
                "О программе",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                    }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}