using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WPFLibraryManagementSystem.Services;

namespace WPFLibraryManagementSystem.View
{
    public partial class Book : Window
    {
        private readonly BookService _bookService;
        private readonly AuthorService _authorService;

        public Domain.Entities.Book NewBook { get; set; }

        public Book()
        {
            InitializeComponent();
            _bookService = new BookService();
            _authorService = new AuthorService();
            NewBook = new Domain.Entities.Book();
            LoadBooks();
            LoadAuthors();

            BookList.SelectionChanged += BookList_SelectionChanged;
        }

        private async void LoadBooks()
        {
            try
            {
                List<Domain.Entities.Book> books = await _bookService.GetBooksAsync();
                BookList.ItemsSource = books;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading books: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LoadAuthors()
        {
            try
            {
                List<Domain.Entities.Author> authors = await _authorService.GetAuthorsAsync();
                AuthorComboBox.ItemsSource = authors;
                AuthorComboBox.DisplayMemberPath = "AuthorName";
                AuthorComboBox.SelectedValuePath = "AuthorID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading authors: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BookList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BookList.SelectedItem != null)
            {
                var selectedBook = (Domain.Entities.Book)BookList.SelectedItem;

                TitleTextBox.Text = selectedBook.Title;
                PublishedDatePicker.SelectedDate = selectedBook.PublishedDate;
                ISBNTextBox.Text = selectedBook.ISBN;
                AvailableCopiesTextBox.Text = selectedBook.AvailableCopies.ToString();
                TotalCopiesTextBox.Text = selectedBook.TotalCopies.ToString();
                AuthorComboBox.SelectedValue = selectedBook.AuthorID;

                btnCreateBook.Content = "Update";
            }
        }

        private async void CreateBookButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string title = TitleTextBox.Text;
                DateTime publishedDate = PublishedDatePicker.SelectedDate ?? DateTime.Now;
                string isbn = ISBNTextBox.Text;
                int availableCopies = Convert.ToInt32(AvailableCopiesTextBox.Text);
                int totalCopies = Convert.ToInt32(TotalCopiesTextBox.Text);
                int authorID = Convert.ToInt32(AuthorComboBox.SelectedValue);

                if (btnCreateBook.Content.ToString() == "Create")
                {
                    Domain.Entities.Book newBook = new Domain.Entities.Book
                    {
                        Title = title,
                        PublishedDate = publishedDate,
                        ISBN = isbn,
                        AvailableCopies = availableCopies,
                        TotalCopies = totalCopies,
                        AuthorID = authorID
                    };
                    await _bookService.CreateBookAsync(newBook);
                }
                else if (btnCreateBook.Content.ToString() == "Update")
                {
                    var selectedBook = (Domain.Entities.Book)BookList.SelectedItem;
                    selectedBook.Title = title;
                    selectedBook.PublishedDate = publishedDate;
                    selectedBook.ISBN = isbn;
                    selectedBook.AvailableCopies = availableCopies;
                    selectedBook.TotalCopies = totalCopies;
                    selectedBook.AuthorID = authorID;

                    await _bookService.UpdateBookAsync(selectedBook);
                }

                LoadBooks();
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
                Domain.Entities.Book selectedBook = BookList.SelectedItem as Domain.Entities.Book;

                if (selectedBook == null)
                {
                    MessageBox.Show("Please select a book to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                await _bookService.DeleteBookAsync(selectedBook.BookID);

                LoadBooks();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting the book: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ClearFields()
        {
            TitleTextBox.Clear();
            PublishedDatePicker.SelectedDate = null;
            ISBNTextBox.Clear();
            AvailableCopiesTextBox.Clear();
            TotalCopiesTextBox.Clear();
            AuthorComboBox.SelectedIndex = -1;

            btnCreateBook.Content = "Create";
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
