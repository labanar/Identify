using Identify.Application.Password;
using System.Threading.Tasks;

namespace Identify.Application
{
    public interface IHashingService
    {
        Task<Hash> CreateHash(string plainText);
        Task<string> ComputeHash(string plainText, string salt);
    }
}
