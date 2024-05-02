using Domain.Entities;
using Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace LibraryManagementSystem.Controllers
{
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IUnitOfWork context;

        public MembersController(IUnitOfWork context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route(RouteConstants.CreateMember)]
        public async Task<IActionResult> CreateMember(Member Member)
        {
            try
            {
                Member.DateCreated = DateTime.Now;

                context.MemberRepository.Add(Member);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadMemberByKey", new { key = Member.MemberID }, Member);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        [HttpGet]
        [Route(RouteConstants.RaedMembers)]
        public async Task<IActionResult> ReadMembers()
        {
            try
            {
                var MembersInDb = await context.MemberRepository.GetMembers();
                MembersInDb = MembersInDb.OrderByDescending(x => x.DateCreated);
                return Ok(MembersInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        [HttpGet]
        [Route(RouteConstants.ReadMemberByKey)]
        public async Task<IActionResult> ReadMemberByKey(int key)
        {
            try
            {
                if (key < 1)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var MembersInDb = await context.MemberRepository.GetMemberByKey(key);

                if (MembersInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(MembersInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        [HttpPut]
        [Route(RouteConstants.UpdateMember)]
        public async Task<IActionResult> UpdateMember(int key, Member Member)
        {
            try
            {
                if (key != Member.MemberID)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                Member.DateModified = DateTime.Now;

                context.MemberRepository.Update(Member);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        [HttpDelete]
        [Route(RouteConstants.DeleteMember)]
        public async Task<IActionResult> DeleteMember(int id)
        {
            try
            {
                var MemberToDelete = await context.MemberRepository.GetByIdAsync(id);

                if (MemberToDelete == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                context.MemberRepository.Delete(MemberToDelete);
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