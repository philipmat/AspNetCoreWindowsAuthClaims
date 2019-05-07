# AspNetCoreWindowsAuthClaims
Claims loading in ASP.NET Core + Windows Auth

Based on: https://philipm.at/2018/aspnetcore_claims_with_windowsauthentication.html

## How to Run

1. Run the app;
2. Navigate to https://localhost:44323/Home/Yes ;
3. The response should read "*{YourDomain\YourUserName}*: Has cheezeburger!".

### Validate the negative

Can always access https://localhost:44323/Home/No for a claim that is never set.

Also:

####  If running from Visual Studio (IISExpress)

The claims transformation is performed on each page load.

1. Put a breakpoint in `MyClaimsLoader` line 31 (`if (await ...)`);
2. Reload https://localhost:44323/Home/Yes ;
3. When the breakpoint hits, move the next line to the return (avoids setting the claim);
4. The page should respond with status 403 (Forbidden).

#### If running in IIS

The claims are loaded only once per browser "session" (not ASP.NET Core session).

1. Change `MagicPowersInfoProvider` to return `false`;
2. Redeploy; close all browser windows or use a different browser;
3. Access  https://localhost:44323/Home/Yes ;
4. The page should now respond with status 403 (Forbidden).
