using Chat_Application_ServerAPI.Data.Models;


namespace ToDoListPractice.Data.Services.JWT
{
    public interface IJWTTokenService
    {
        string GenerateToken(ApplicationUser user);
    }
}
