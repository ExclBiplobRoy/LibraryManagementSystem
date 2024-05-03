using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WPFLibraryManagementSystem.Services;

namespace WPFLibraryManagementSystem.View
{
    public partial class Author : Window
    {
        private readonly AuthorService _authorService;

        public Domain.Entities.Author NewAuthor { get; set; }

        public Author()
        {
            InitializeComponent();
            _authorService = new AuthorService();
            NewAuthor = new Domain.Entities.Author();
            LoadAuthors();

            AuthorList.SelectionChanged += AuthorList_SelectionChanged;
        }

        private void AuthorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AuthorList.SelectedItem != null)
            {
                var selectedAuthor = (Domain.Entities.Author)AuthorList.SelectedItem;

                AuthorNameTextBox.Text = selectedAuthor.AuthorName;
                AuthorBioTextBox.Text = selectedAuthor.AuthorBio;

                btnCreateAuthor.Content = "Update";
            }
        }

        private async void LoadAuthors()
        {
            try
            {
                List<Domain.Entities.Author> authors = await _authorService.GetAuthorsAsync();
                AuthorList.ItemsSource = authors;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading authors: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void CreateAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string authorName = AuthorNameTextBox.Text;
                string authorBio = AuthorBioTextBox.Text;

                if (btnCreateAuthor.Content.ToString() == "Create")
                {
                    Domain.Entities.Author newAuthor = new Domain.Entities.Author
                    {
                        AuthorName = authorName,
                        AuthorBio = authorBio
                    };
                    await _authorService.CreateAuthorAsync(newAuthor);
                }
                else if (btnCreateAuthor.Content.ToString() == "Update")
                {
                    var selectedAuthor = (Domain.Entities.Author)AuthorList.SelectedItem;
                    selectedAuthor.AuthorName = authorName;
                    selectedAuthor.AuthorBio = authorBio;

                    await _authorService.UpdateAuthorAsync(selectedAuthor);
                }

                LoadAuthors();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while processing the operation: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnDeleteAuthor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Domain.Entities.Author selectedAuthor = AuthorList.SelectedItem as Domain.Entities.Author;

                if (selectedAuthor == null)
                {
                    MessageBox.Show("Please select an author to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                await _authorService.DeleteAuthorAsync(selectedAuthor.AuthorID);

                LoadAuthors();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting the author: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ClearFields()
        {
            AuthorNameTextBox.Clear();
            AuthorBioTextBox.Clear();

            btnCreateAuthor.Content = "Create";
        }
    }
}
