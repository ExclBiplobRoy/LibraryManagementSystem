using System.Windows;

namespace WPFLibraryManagementSystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AdminMenuItem_Click(object sender, RoutedEventArgs e)
        {
            View.Admin admin = new();
            admin.ShowDialog();
            this.Close();
        }

        private void AuthorMenuItem_Click(object sender, RoutedEventArgs e)
        {
            View.Author author = new();
            author.ShowDialog();
            this.Close();
        }

        private void BookMenuItem_Click(object sender, RoutedEventArgs e)
        {
            View.Book book = new();
            book.ShowDialog();
            this.Close();
        }
    }
}