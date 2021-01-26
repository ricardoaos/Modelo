using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FiscalPrime.Backend.Http.RacUtils
{
    public static class TokenUtils
    {
        public static async Task<string> GetResourceOwnerToken(string racUrl, string clientId, string clientSecret, string userName, string password)
        {
            var client = new HttpClient();
            var response = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = $"{racUrl}/connect/token",

                ClientId = clientId,
                ClientSecret = clientSecret,
                Scope = "authorization_api",

                UserName = userName,
                Password = password
            });

            if (response.IsError) throw new Exception(response.Error);

            return response.AccessToken;
        }
    }
}
