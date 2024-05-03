using Domain.Entities;
using Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace LibraryManagementSystem.Controllers
{
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IUnitOfWork context;

        public AdminsController(IUnitOfWork context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route(RouteConstants.CreateAdmin)]
        public async Task<IActionResult> CreateAdmin(Admin Admin)
        {
            try
            {
                Admin.DateCreated = DateTime.Now;

                context.AdminRepository.Add(Admin);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadAdminByKey", new { key = Admin.AdminID }, Admin);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        [HttpGet]
        [Route(RouteConstants.RaedAdmins)]
        public async Task<IActionResult> ReadAdmins()
        {
            try
            {
                var AdminsInDb = await context.AdminRepository.GetAdmins();
                AdminsInDb = AdminsInDb.OrderByDescending(x => x.DateCreated);
                return Ok(AdminsInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        [HttpGet]
        [Route(RouteConstants.ReadAdminByKey)]
        public async Task<IActionResult> ReadAdminByKey(int key)
        {
            try
            {
                if (key < 1)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var AdminsInDb = await context.AdminRepository.GetAdminByKey(key);

                if (AdminsInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(AdminsInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        [HttpPut]
        [Route(RouteConstants.UpdateAdmin)]
        public async Task<IActionResult> UpdateAdmin(int key, Admin Admin)
        {
            try
            {
                if (key != Admin.AdminID)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                Admin.DateModified = DateTime.Now;

                context.AdminRepository.Update(Admin);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        [HttpDelete]
        [Route(RouteConstants.DeleteAdmin)]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            try
            {
                var AdminToDelete = await context.AdminRepository.GetByIdAsync(id);

                if (AdminToDelete == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                context.AdminRepository.Delete(AdminToDelete);
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