using System;
using System.Collections.Generic;
using System.Windows;
using WPFLibraryManagementSystem.Services;

namespace WPFLibraryManagementSystem.View
{
    /// <summary>
    /// Interaction logic for AdminTest.xaml
    /// </summary>
    public partial class AdminTest : Window
    {
        private readonly AdminService _adminService;

        public AdminTest()
        {
            InitializeComponent();
            _adminService = new AdminService();
            LoadAdmins();
        }

        private async void LoadAdmins()
        {
            try
            {
                List<Domain.Entities.Admin> admins = await _adminService.GetAdminsAsync();
                foreach (var admin in admins)
                {
                    admin.FirstName += " " + admin.SurName;
                }
                AdminList.ItemsSource = admins;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading admins: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}