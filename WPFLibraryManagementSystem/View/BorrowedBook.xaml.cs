using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WPFLibraryManagementSystem.Services;

namespace WPFLibraryManagementSystem.View
{
    public partial class BorrowedBook : Window
    {
        private readonly BorrowedBookService _borrowedBookService;
        private readonly BookService _bookService;
        private readonly MemberService _memberService;

        public Domain.Entities.BorrowedBook NewBorrowedBook { get; set; }

        public BorrowedBook()
        {
            InitializeComponent();
            _borrowedBookService = new BorrowedBookService();
            _bookService = new BookService();
            _memberService = new MemberService();
            NewBorrowedBook = new Domain.Entities.BorrowedBook();
            LoadBorrowedBooks();
            LoadBooks();
            LoadMembers();

            BorrowedBookList.SelectionChanged += BorrowedBookList_SelectionChanged;
        }

        private async void LoadBorrowedBooks()
        {
            try
            {
                List<Domain.Entities.BorrowedBook> borrowedBooks = await _borrowedBookService.GetBorrowedBooksAsync();
                BorrowedBookList.ItemsSource = borrowedBooks;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading borrowed books: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LoadBooks()
        {
            try
            {
                List<Domain.Entities.Book> books = await _bookService.GetBooksAsync();
                BookComboBox.ItemsSource = books;
                BookComboBox.DisplayMemberPath = "Title";
                BookComboBox.SelectedValuePath = "BookID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading books: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LoadMembers()
        {
            try
            {
                List<Domain.Entities.Member> members = await _memberService.GetMembersAsync();
                foreach (var member in members)
                {
                    member.FullName = member.FirstName + " " + member.LastName;
                }
                MemberComboBox.ItemsSource = members;
                MemberComboBox.DisplayMemberPath = "FullName";
                MemberComboBox.SelectedValuePath = "MemberID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading members: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BorrowedBookList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BorrowedBookList.SelectedItem != null)
            {
                var selectedBorrowedBook = (Domain.Entities.BorrowedBook)BorrowedBookList.SelectedItem;

                MemberComboBox.SelectedValue = selectedBorrowedBook.MemberID;
                BorrowDatePicker.SelectedDate = selectedBorrowedBook.BorrowDate;
                ReturnDatePicker.SelectedDate = selectedBorrowedBook.ReturnDate;
                BookComboBox.SelectedValue = selectedBorrowedBook.BookID;
                StatusTextBox.Text = selectedBorrowedBook.Status;

                btnCreateBorrowedBook.Content = "Update";
            }
        }

        private async void CreateBorrowedBookButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int memberID = Convert.ToInt32(MemberComboBox.SelectedValue);
                DateTime borrowDate = BorrowDatePicker.SelectedDate ?? DateTime.Now;
                DateTime? returnDate = ReturnDatePicker.SelectedDate;
                int bookID = Convert.ToInt32(BookComboBox.SelectedValue);
                string status = StatusTextBox.Text;

                if (btnCreateBorrowedBook.Content.ToString() == "Create")
                {
                    Domain.Entities.BorrowedBook newBorrowedBook = new Domain.Entities.BorrowedBook
                    {
                        MemberID = memberID,
                        BorrowDate = borrowDate,
                        ReturnDate = returnDate,
                        BookID = bookID,
                        Status = status
                    };
                    await _borrowedBookService.CreateBorrowedBookAsync(newBorrowedBook);
                }
                else if (btnCreateBorrowedBook.Content.ToString() == "Update")
                {
                    var selectedBorrowedBook = (Domain.Entities.BorrowedBook)BorrowedBookList.SelectedItem;
                    selectedBorrowedBook.MemberID = memberID;
                    selectedBorrowedBook.BorrowDate = borrowDate;
                    selectedBorrowedBook.ReturnDate = returnDate;
                    selectedBorrowedBook.BookID = bookID;
                    selectedBorrowedBook.Status = status;

                    await _borrowedBookService.UpdateBorrowedBookAsync(selectedBorrowedBook);
                }

                LoadBorrowedBooks();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while processing the operation: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnDeleteBorrowedBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Domain.Entities.BorrowedBook selectedBorrowedBook = BorrowedBookList.SelectedItem as Domain.Entities.BorrowedBook;

                if (selectedBorrowedBook == null)
                {
                    MessageBox.Show("Please select a borrowed book to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                await _borrowedBookService.DeleteBorrowedBookAsync(selectedBorrowedBook.BorrowID);

                LoadBorrowedBooks();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting the borrowed book: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFields()
        {
            MemberComboBox.SelectedIndex = -1;
            BorrowDatePicker.SelectedDate = null;
            ReturnDatePicker.SelectedDate = null;
            BookComboBox.SelectedIndex = -1;
            StatusTextBox.Clear();

            btnCreateBorrowedBook.Content = "Create";
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
