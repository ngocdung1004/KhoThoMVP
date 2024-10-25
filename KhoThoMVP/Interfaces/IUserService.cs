using KhoThoMVP.DTOs;

namespace KhoThoMVP.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> CreateUserAsync(UserDto userDto);
        Task<UserDto> UpdateUserAsync(int id, UserDto userDto);
        Task DeleteUserAsync(int id);
        Task<UserDto> GetUserByEmailAsync(string email);
    }
}
