using Entities;

namespace Services.Services
{
    public interface IJwtService
    {
        string Generate(User user);
    }
}