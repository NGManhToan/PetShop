﻿using PetShop.Manager.Token;
using PetShop.Manager.Token.Interface;
using System.Globalization;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;


namespace PetShop.Models.UtilsProject
{
    public class Utils
    {
        public static string KeyToken = "17JjUurO1RvK9mzN3LU0lqVexW6gibCo";


        public static int GetUserIdFromToken(HttpRequest request)
        {
            var headers = request.Headers;

            if (headers.ContainsKey("Authorization") || headers.ContainsKey("AuthorizationSwagger"))
            {
                string token = headers["Authorization"];

                if (String.IsNullOrEmpty(token))
                {
                    token = headers["AuthorizationSwagger"];
                }

                token = (token ?? "").Replace("Bearer ", "");

                if (Signature.CheckTokenValid(token))
                {
                    IAuthService authService = new JWTService(KeyToken);
                    List<Claim> claims = authService.GetTokenClaims(token).ToList();
                    return Convert.ToInt32(claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.NameIdentifier)).Value);
                }
            }

            return 0;
        }

        public static ulong GetRoleFromToken(HttpRequest request)
        {
            var headers = request.Headers;


            if (headers.ContainsKey("Authorization") || headers.ContainsKey("AuthorizationSwagger"))
            {
                string token = headers["Authorization"];


                if (token == null)
                {
                    token = headers["AuthorizationSwagger"];
                }

                if (!String.IsNullOrEmpty(token) && token.Contains("Bearer "))
                {
                    token = token.Replace("Bearer ", "");
                    if (Signature.CheckTokenValid(token))
                    {
                        IAuthService authService = new JWTService(KeyToken);
                        List<Claim> claims = authService.GetTokenClaims(token).ToList();
                        return Convert.ToUInt64(claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Role)).Value);

                        //var roles = claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Role)).Value.Split(",");

                        //// Get the first role in the array.
                        //int role = int.Parse(roles[0]);

                        //return role;
                        ////return Convert.ToInt32(claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Role)).Value);
                    }
                }

            }


            return 0;
        }

        public static DateTime DateNow()
        {
            return DateTime.Now;
        }

        public static string FormatDateToString(DateTime dateTime)
        {
            return dateTime.ToString("dd-MM-yyyy HH:mm");
        }

        public static string GenerateID()
        {
            return Guid.NewGuid().ToString();
        }

        public static string GenerateCode(ulong Id)
        {
            return (1000000 + Id).ToString();
        }

        public static string LinkMedia(string input)
        {
            return BundleConnection.LinkMedia + input;
        }

        public static string RemoveAcentuation(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.Normalize(NormalizationForm.FormD);
            var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();

            return new string(chars).Normalize(NormalizationForm.FormC).Replace(" ", "").Replace("\n", "").ToLower();
        }

        public static string RemoveAcentuationFinder(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.Normalize(NormalizationForm.FormD);
            var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();

            return new string(chars).Normalize(NormalizationForm.FormC).Replace("đ", "d").Replace("Đ", "D").ToLower();
        }

        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z 0-9-/-]", "");
        }






    }
}
