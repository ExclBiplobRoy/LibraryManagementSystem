using Domain.Entities;
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
            View.Admin admin = new ();
            admin.ShowDialog();
        }

        private void AuthorMenuItem_Click(object sender, RoutedEventArgs e)
        {
            View.Author author = new ();
            author.ShowDialog();
        }
    }
}
