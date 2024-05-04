namespace Utilities
{
    public class RouteConstants
    {
        public const string BaseRoute = "lms-api";

        #region Author
        public const string CreateAuthor = "author";

        public const string ReadAuthors = "authors";

        public const string ReadAuthorByKey = "author/key/{key}";

        public const string UpdateAuthor = "author/{key}";

        public const string DeleteAuthor = "author/{key}";
        #endregion

        #region Book
        public const string CreateBook = "book";

        public const string ReadBooks = "books";

        public const string ReadBookByKey = "book/key/{key}";

        public const string UpdateBook = "book/{key}";

        public const string DeleteBook = "book/{key}";
        #endregion

        #region Member
        public const string CreateMember = "member";

        public const string ReadMembers = "members";

        public const string ReadMemberByKey = "member/key/{key}";

        public const string UpdateMember = "member/{key}";

        public const string DeleteMember = "member/{key}";
        #endregion

        #region Borrowed
        public const string CreateBorrowedBook = "borrowed-book";

        public const string RaedBorrowedBooks = "borrowed-books";

        public const string ReadBorrowedBookByKey = "borrowed-book/key/{key}";

        public const string UpdateBorrowedBook = "borrowed-book/{key}";

        public const string DeleteBorrowedBook = "borrowed-book/{key}";
        #endregion

        #region Admin
        public const string CreateAdmin = "admin";

        public const string RaedAdmins = "admin";

        public const string ReadAdminByKey = "admin/key/{key}";

        public const string UpdateAdmin = "admin/{key}";

        public const string DeleteAdmin = "admin/{key}";
        #endregion
    }
}