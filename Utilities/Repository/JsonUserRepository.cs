using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoApp.Dto;

namespace ToDoApp.Utilities.Repository
{
    public class JsonUserRepository : IUserRepository
    {
        private readonly string _filePath;

        public JsonUserRepository(string filePath)
        {
            _filePath = filePath;
        }

        public async Task AddUserAsync(UserDto user)
        {
            var users = await ListUsersAsync();
            users.Add(user);
            await SaveUsersAsync(users);
        }

        public async Task<List<UserDto>> ListUsersAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new List<UserDto>();
            }

            var jsonData = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<UserDto>>(jsonData) ?? new List<UserDto>();
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            var users = await ListUsersAsync();
            return users.Any(u => u.Name == username);
        }

        public async Task DeleteUserAsync(int userId)
        {
            var users = await ListUsersAsync();
            var userToDelete = users.FirstOrDefault(u => u.Id == userId);
            if (userToDelete != null)
            {
                users.Remove(userToDelete);
                await SaveUsersAsync(users);
            }
        }

        private async Task SaveUsersAsync(List<UserDto> users)
        {
            var jsonData = JsonSerializer.Serialize(users);
            await File.WriteAllTextAsync(_filePath, jsonData);
        }
    }
}
