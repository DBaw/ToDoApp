using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Dto;

namespace ToDoApp.Utilities.Repository
{
    public interface IUserRepository
    {
        Task AddUserAsync(UserDto user);
        Task<List<UserDto>> ListUsersAsync();
        Task<bool> UserExistsAsync(string username);
        Task DeleteUserAsync(int userId);
    }
}
