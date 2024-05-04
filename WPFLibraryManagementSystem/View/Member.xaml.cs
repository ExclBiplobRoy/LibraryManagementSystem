using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WPFLibraryManagementSystem.Services;

namespace WPFLibraryManagementSystem.View
{
    public partial class Member : Window
    {
        private readonly MemberService _memberService;

        public Domain.Entities.Member NewMember { get; set; }

        public Member()
        {
            InitializeComponent();
            _memberService = new MemberService();
            NewMember = new Domain.Entities.Member();
            LoadMembers();

            MemberList.SelectionChanged += MemberList_SelectionChanged;
        }

        private void MemberList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MemberList.SelectedItem != null)
            {
                var selectedMember = (Domain.Entities.Member)MemberList.SelectedItem;

                FirstNameTextBox.Text = selectedMember.FirstName;
                LastNameTextBox.Text = selectedMember.LastName;
                PhoneNumberTextBox.Text = selectedMember.PhoneNumber;
                EmailTextBox.Text = selectedMember.Email;

                btnCreateMember.Content = "Update";
            }
        }

        private async void LoadMembers()
        {
            try
            {
                List<Domain.Entities.Member> members = await _memberService.GetMembersAsync();
                MemberList.ItemsSource = members;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading members: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void CreateMemberButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string firstName = FirstNameTextBox.Text;
                string lastName = LastNameTextBox.Text;
                string phoneNumber = PhoneNumberTextBox.Text;
                string email = EmailTextBox.Text;

                if (btnCreateMember.Content.ToString() == "Create")
                {
                    Domain.Entities.Member newMember = new Domain.Entities.Member
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        PhoneNumber = phoneNumber,
                        Email = email,
                        RegistrationDate = DateTime.Now // Set registration date to current date
                    };
                    await _memberService.CreateMemberAsync(newMember);
                }
                else if (btnCreateMember.Content.ToString() == "Update")
                {
                    var selectedMember = (Domain.Entities.Member)MemberList.SelectedItem;
                    selectedMember.FirstName = firstName;
                    selectedMember.LastName = lastName;
                    selectedMember.PhoneNumber = phoneNumber;
                    selectedMember.Email = email;

                    await _memberService.UpdateMemberAsync(selectedMember);
                }

                LoadMembers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while processing the operation: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnDeleteMember_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Domain.Entities.Member selectedMember = MemberList.SelectedItem as Domain.Entities.Member;

                if (selectedMember == null)
                {
                    MessageBox.Show("Please select a member to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                await _memberService.DeleteMemberAsync(selectedMember.MemberID);

                LoadMembers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting the member: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ClearFields()
        {
            FirstNameTextBox.Clear();
            LastNameTextBox.Clear();
            PhoneNumberTextBox.Clear();
            EmailTextBox.Clear();

            btnCreateMember.Content = "Create";
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
