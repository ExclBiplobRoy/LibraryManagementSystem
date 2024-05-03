using System.Windows;
using WPFLibraryManagementSystem.View;

namespace WPFLibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AdminTest adminTest = new AdminTest();
            adminTest.Show();
            this.Close();
        }
    }
}