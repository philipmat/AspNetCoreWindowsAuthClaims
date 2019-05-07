using System.Threading.Tasks;

namespace AspNetCoreWindowsAuthClaims.Auth
{
    public class MagicPowersInfoProvider
    {
        public async Task<bool> CanHasPowerAsync(string userId)
        {
            return await Task.FromResult(true);
        }
    }
}
