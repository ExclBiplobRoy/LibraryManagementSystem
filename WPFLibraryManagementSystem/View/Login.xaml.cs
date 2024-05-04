using System;
using System.Windows;
using WPFLibraryManagementSystem.Services;

namespace WPFLibraryManagementSystem.View
{
    public partial class Login : Window
    {
        private readonly AdminService _adminService;

        public Login()
        {
            InitializeComponent();
            _adminService = new AdminService();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = EmailTextBox.Text;
                string password = PasswordBox.Password;

                Domain.Entities.Admin loginAdmin = await _adminService.AuthenticateAsync(email, password);

                if (loginAdmin.AdminID > 0)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid email or password. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while logging in: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
