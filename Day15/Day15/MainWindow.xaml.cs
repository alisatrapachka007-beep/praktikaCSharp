using System.Windows;
using Day15.ViewModels;

namespace Day15
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Медицинские записи\nВерсия 1.0\n\n" +
                "Ctrl+N - Новый пациент\n" +
                "Ctrl+R - Добавить запись\n" +
                "Ctrl+E - Редактировать\n" +
                "Del - Удалить",
                "О программе");
        }
    }
}