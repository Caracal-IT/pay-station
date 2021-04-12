using System;
using Microsoft.AspNetCore.Http;

namespace Caracal.PayStation.Web.Helpers {
    public static class Cookies {
        private const string TokenKey = "X-UserToken";

        public static void SetUserToken(HttpResponse response, string token) {
            var option = new CookieOptions {MaxAge = TimeSpan.FromMinutes(20), HttpOnly = true};
            response.Cookies.Append(TokenKey, token, option);
        }

        public static string GetUserToken(HttpRequest request) => 
            request.Cookies[TokenKey];
    }
}