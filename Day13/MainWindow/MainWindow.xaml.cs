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
                "Программа для учета медицинских записей\n\nВерсия 2.0\n\nГорячие клавиши:\nCtrl+R - Записать пациента\nCtrl+E - Редактировать запись\nDel - Удалить запись",
                "О программе",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}