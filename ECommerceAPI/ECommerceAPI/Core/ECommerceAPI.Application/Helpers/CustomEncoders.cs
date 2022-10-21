using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace ECommerceAPI.Application.Helpers
{
    public static class CustomEncoders
    {
        /// <summary>
        /// ssssssss
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UrlEncode(this string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            // http linkindeki özel karakterlerin sorun yaratmaması için!!!!     
            return WebEncoders.Base64UrlEncode(bytes);
        }

        /// <summary>
        /// ssssssss
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UrlDecode(this string value)
        {
            byte[] bytes = WebEncoders.Base64UrlDecode(value);
            return Encoding.UTF8.GetString(bytes);

        }
    }
}
