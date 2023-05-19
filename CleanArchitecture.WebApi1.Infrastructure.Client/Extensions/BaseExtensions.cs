using System.ComponentModel;

namespace CleanArchitecture.WebApi1.Infrastructure.Client.Extensions
{

    public static class BaseExtensions
    {
      static string BaseUrl { get; set; } = "http://192.168.1.30:81/";
         //static string BaseUrl { get; set; } = "https://api.callwash.app/";
        // static string BaseUrl { get; set; } = "https://localhost:4221/";
        public static string ToFullUrl(this string str)
        {
            return BaseUrl + str;
        }

        public static string ToDescriptionString(this Enum val)
        {
            var attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0
                ? attributes[0].Description
                : val.ToString();
        }
    }
}
