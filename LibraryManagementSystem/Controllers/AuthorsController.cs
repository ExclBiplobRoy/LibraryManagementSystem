using Domain.Entities;
using Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace LibraryManagementSystem.Controllers
{
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork context;

        public AuthorsController(IUnitOfWork context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route(RouteConstants.CreateAuthor)]
        public async Task<IActionResult> CreateAuthor(Author Author)
        {
            try
            {
                Author.DateCreated = DateTime.Now;

                context.AuthorRepository.Add(Author);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadAuthorByKey", new { key = Author.AuthorID }, Author);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        [HttpGet]
        [Route(RouteConstants.ReadAuthors)]
        public async Task<IActionResult> ReadAuthors()
        {
            try
            {
                var AuthorsInDb = await context.AuthorRepository.GetAuthors();
                AuthorsInDb = AuthorsInDb.OrderByDescending(x => x.DateCreated);
                return Ok(AuthorsInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        [HttpGet]
        [Route(RouteConstants.ReadAuthorByKey)]
        public async Task<IActionResult> ReadAuthorByKey(int key)
        {
            try
            {
                if (key < 1)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var AuthorsInDb = await context.AuthorRepository.GetAuthorByKey(key);

                if (AuthorsInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(AuthorsInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        [HttpPut]
        [Route(RouteConstants.UpdateAuthor)]
        public async Task<IActionResult> UpdateAuthor(int key, Author Author)
        {
            try
            {
                if (key != Author.AuthorID)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                Author.DateModified = DateTime.Now;

                context.AuthorRepository.Update(Author);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        [HttpDelete]
        [Route(RouteConstants.DeleteAuthor)]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var authorToDelete = await context.AuthorRepository.GetByIdAsync(id);

                if (authorToDelete == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                context.AuthorRepository.Delete(authorToDelete);
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