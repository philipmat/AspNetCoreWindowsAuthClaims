using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace AspNetCoreWindowsAuthClaims.Auth
{
    internal class MyClaimsLoader : IClaimsTransformation
    {
        private readonly MagicPowersInfoProvider _powersProvider;

        public MyClaimsLoader(MagicPowersInfoProvider powersProvider)
        {
            _powersProvider = powersProvider;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var identity = (ClaimsIdentity)principal.Identity;

            // create a new ClaimsIdentity copying the existing one
            var claimsIdentity = new ClaimsIdentity(
                identity.Claims,
                identity.AuthenticationType,
                identity.NameClaimType,
                identity.RoleClaimType);

            // check if user can haz ting
            if (await _powersProvider.CanHasPowerAsync(identity.Name).ConfigureAwait(false))
            {
                claimsIdentity.AddClaim(
                    new Claim(Constants.HasMagicPowersClaim, "Cheezburger"));
            }

            // create a new ClaimsPrincipal in observation
            // of the documentation note
            return new ClaimsPrincipal(claimsIdentity);

        }
    }
}