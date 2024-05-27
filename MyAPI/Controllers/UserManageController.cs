using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPI.Db;
using MyAPI.Models;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManageController : ControllerBase
    {
        private readonly ApplicationDbContext _userServices;
        private ResponseDto _responseDto;

        public UserManageController(ApplicationDbContext userServices)
        {
            _userServices = userServices;
            _responseDto = new ResponseDto();
        }
        [HttpGet]
        public async Task<ResponseDto> GetAll()
        {
            var user = await _userServices.ApplicationUsers.ToListAsync();
            if (user.Count > 0)
            {
                return new()
                {
                    Result = user,
                    IsSuccess = true,
                    Message = string.Empty
                };
            }
            else
            {
                return new()
                {
                    Result = "No Record",
                    Message = "No Record",
                    IsSuccess = false
                };
            }
        }
        [HttpGet("{userId:int}")]
        public async Task<ResponseDto> GetById(int userId)
        {
            var user = await _userServices.ApplicationUsers.FirstOrDefaultAsync(m => m.Id == userId);
            if (user?.Id != 0)
            {
                return new()
                {
                    Result = user,
                    IsSuccess = true,
                    Message = string.Empty
                };
            }
            else
            {
                return new()
                {
                    Result = "No user found with id = " + userId,
                    Message = "No user found with id = " + userId,
                    IsSuccess = false
                };
            }
        }
        [HttpDelete("{userId}")]
        public async Task<ResponseDto> DeleteById(int userId)
        {
            int user = 0;
            var userdetails = await _userServices.ApplicationUsers.FirstOrDefaultAsync(m => m.Id == userId);
            if (userdetails != null)
            {
                _userServices.ApplicationUsers.Remove(userdetails);
                user = await _userServices.SaveChangesAsync();
            }

            if (user == 1)
            {
                return new()
                {
                    Result = user,
                    IsSuccess = true,
                    Message = string.Empty
                };
            }
            else
            {
                return new()
                {
                    Result = "No user found with id = " + userId,
                    Message = "No user found with id = " + userId,
                    IsSuccess = false
                };
            }
        }
        [HttpPut("{userId}")]
        public async Task<ResponseDto> Update(int userId, ApplicationUser users)
        {
            if (userId != users.Id)
            {
                return new()
                {
                    Result = "user id is not matched.",
                    IsSuccess = false,
                    Message = string.Empty
                };
            }
            _userServices.ApplicationUsers.Update(users);
            var user = await _userServices.SaveChangesAsync();
            if (user == 1)
            {
                return new()
                {
                    Result = user,
                    IsSuccess = true,
                    Message = string.Empty
                };
            }
            else
            {
                return new()
                {
                    Result = "No user found with id = " + userId,
                    Message = "No user found with id = " + userId,
                    IsSuccess = false
                };
            }
        }
        [HttpPost()]
        public async Task<ResponseDto> Post(ApplicationUser users)
        {

            _userServices.ApplicationUsers.Add(users);
            var user = await _userServices.SaveChangesAsync();
            if (user == 1)
            {
                return new()
                {
                    Result = user,
                    IsSuccess = true,
                    Message = string.Empty
                };
            }
            else
            {
                return new()
                {
                    Result = "Not saved ,",
                    Message = "Not saved",
                    IsSuccess = false
                };
            }
        }
    }
}
