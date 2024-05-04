using Domain.Entities;
using Infrastructure.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Utilities;

namespace LibraryManagementSystem.Controllers
{
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class BorrowedBooksController : ControllerBase
    {
        private readonly IUnitOfWork context;

        public BorrowedBooksController(IUnitOfWork context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route(RouteConstants.CreateBorrowedBook)]
        public async Task<IActionResult> CreateBorrowedBook(BorrowedBook BorrowedBook)
        {
            try
            {
                BorrowedBook.DateCreated = DateTime.Now;

                context.BorrowedBookRepository.Add(BorrowedBook);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadBorrowedBookByKey", new { key = BorrowedBook.BorrowID }, BorrowedBook);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        [HttpGet]
        [Route(RouteConstants.ReadBorrowedBooks)]
        public async Task<IActionResult> ReadBorrowedBooks()
        {
            try
            {
                var BorrowedBooksInDb = await context.BorrowedBookRepository.GetBorrowedBooks();

                foreach (var item in BorrowedBooksInDb)
                {
                    item.Book = await context.BookRepository.GetBookByKey(item.BookID);
                    item.Member = await context.MemberRepository.GetMemberByKey(item.MemberID);
                    item.Member.FullName = item.Member.FirstName + " " + item.Member.LastName;
                }

                BorrowedBooksInDb = BorrowedBooksInDb.OrderByDescending(x => x.DateCreated);
                return Ok(BorrowedBooksInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        [HttpGet]
        [Route(RouteConstants.ReadBorrowedBookByKey)]
        public async Task<IActionResult> ReadBorrowedBookByKey(int key)
        {
            try
            {
                if (key < 1)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var BorrowedBooksInDb = await context.BorrowedBookRepository.GetBorrowedBookByKey(key);

                if (BorrowedBooksInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(BorrowedBooksInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        [HttpPut]
        [Route(RouteConstants.UpdateBorrowedBook)]
        public async Task<IActionResult> UpdateBorrowedBook(int key, BorrowedBook BorrowedBook)
        {
            try
            {
                if (key != BorrowedBook.BorrowID)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                BorrowedBook.DateModified = DateTime.Now;

                context.BorrowedBookRepository.Update(BorrowedBook);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        [HttpDelete]
        [Route(RouteConstants.DeleteBorrowedBook)]
        public async Task<IActionResult> DeleteBorrowedBook(int id)
        {
            try
            {
                var BorrowedBookToDelete = await context.BorrowedBookRepository.GetByIdAsync(id);

                if (BorrowedBookToDelete == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                context.BorrowedBookRepository.Delete(BorrowedBookToDelete);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

    }
}
