using Domain.Entities;
using Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Utilities;

namespace LibraryManagementSystem.Controllers
{
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork context;

        public BooksController(IUnitOfWork context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route(RouteConstants.CreateBook)]
        public async Task<IActionResult> CreateBook(Book Book)
        {
            try
            {
                Book.DateCreated = DateTime.Now;

                context.BookRepository.Add(Book);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadBookByKey", new { key = Book.BookID }, Book);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        [HttpGet]
        [Route(RouteConstants.ReadBooks)]
        public async Task<IActionResult> ReadBooks()
        {
            try
            {
                var BooksInDb = await context.BookRepository.GetBooks();
                foreach (var Book in BooksInDb) 
                {
                    Book.Author = await context.AuthorRepository.GetAuthorByKey(Book.AuthorID);
                }

                BooksInDb = BooksInDb.OrderByDescending(x => x.DateCreated);
                return Ok(BooksInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        [HttpGet]
        [Route(RouteConstants.ReadBookByKey)]
        public async Task<IActionResult> ReadBookByKey(int key)
        {
            try
            {
                if (key < 1)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var BooksInDb = await context.BookRepository.GetBookByKey(key);

                if (BooksInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(BooksInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        [HttpPut]
        [Route(RouteConstants.UpdateBook)]
        public async Task<IActionResult> UpdateBook(int key, Book Book)
        {
            try
            {
                if (key != Book.BookID)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                Book.DateModified = DateTime.Now;

                context.BookRepository.Update(Book);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        [HttpDelete]
        [Route(RouteConstants.DeleteBook)]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var BookToDelete = await context.BookRepository.GetByIdAsync(id);

                if (BookToDelete == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                context.BookRepository.Delete(BookToDelete);
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