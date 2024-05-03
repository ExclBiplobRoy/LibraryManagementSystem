using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WPFLibraryManagementSystem.Services;

namespace WPFLibraryManagementSystem.View
{
    public partial class AdminTest : Window
    {
        private readonly AdminService _adminService;

        public Domain.Entities.Admin NewAdmin { get; set; }

        public string FullName;

        public AdminTest()
        {
            InitializeComponent();
            _adminService = new AdminService();
            NewAdmin = new Domain.Entities.Admin();
            LoadAdmins();

            AdminList.SelectionChanged += AdminList_SelectionChanged;
        }

        private void AdminList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AdminList.SelectedItem != null)
            {
                var selectedAdmin = (Domain.Entities.Admin)AdminList.SelectedItem;

                FirstNameTextBox.Text = selectedAdmin.FirstName;
                LastNameTextBox.Text = selectedAdmin.SurName;
                EmailTextBox.Text = selectedAdmin.Email;
                PasswordBox.Password = selectedAdmin.Password;
                LevelTextBox.Text = selectedAdmin.Level.ToString();

                btnCreateAdmin.Content = "Update";
            }
        }

        private async void LoadAdmins()
        {
            try
            {
                List<Domain.Entities.Admin> admins = await _adminService.GetAdminsAsync();
                AdminList.ItemsSource = admins;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading admins: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void CreateAdminButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string firstName = FirstNameTextBox.Text;
                string lastName = LastNameTextBox.Text;
                string email = EmailTextBox.Text;
                string password = PasswordBox.Password;
                int level = Convert.ToInt32(LevelTextBox.Text);

                if (btnCreateAdmin.Content.ToString() == "Create")
                {
                    Domain.Entities.Admin newAdmin = new Domain.Entities.Admin
                    {
                        FirstName = firstName,
                        SurName = lastName,
                        Email = email,
                        Password = password,
                        Level = level
                    };
                    await _adminService.CreateAdminAsync(newAdmin);
                }
                else if (btnCreateAdmin.Content.ToString() == "Update")
                {
                    var selectedAdmin = (Domain.Entities.Admin)AdminList.SelectedItem;
                    selectedAdmin.FirstName = firstName;
                    selectedAdmin.SurName = lastName;
                    selectedAdmin.Email = email;
                    selectedAdmin.Password = password;
                    selectedAdmin.Level = level;

                    await _adminService.UpdateAdminAsync(selectedAdmin);
                }

                LoadAdmins();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while processing the operation: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Domain.Entities.Admin selectedAdmin = AdminList.SelectedItem as Domain.Entities.Admin;

                if (selectedAdmin == null)
                {
                    MessageBox.Show("Please select an admin to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                await _adminService.DeleteAdminAsync(selectedAdmin.AdminID);

                LoadAdmins();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting the admin: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ClearFields()
        {
            FirstNameTextBox.Clear();
            LastNameTextBox.Clear();
            EmailTextBox.Clear();
            PasswordBox.Clear();
            LevelTextBox.Clear();

            btnCreateAdmin.Content = "Create";
        }
    }
}