using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Insomnia.Portal.General.Expansions
{
    public static class StringExpansions
    {
        public static bool CanParseToInt(this string number)
        {
            try
            {
                int.Parse(number);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool CanParseToFloat(this string number)
        {
            try
            {
                float.Parse(number);
            }
            catch
            {
                return false;
            }

            return true;
        }

        //Смысла проверять как-то лучше никакого. 
        public static bool IsEmail(this string email) => email.CorrectRegex(@"/.+@.+\..+/i");

        public static bool IsPhone(this string phone) => phone.CorrectRegex(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$");

        public static bool CorrectRegex(this string text, string regex) => Regex.IsMatch(text, regex);

        public static bool AllElementsOfNumber(this string[] elements) =>
            elements.All(x => x.CanParseToFloat());

        public static string[] ToArray(this string elements) => elements.Split(',');

        public static T ToObject<T>(this string json)
        {
            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            options.Converters.Add(new JsonStringEnumConverter());
            return JsonSerializer.Deserialize<T>(json, options);
        }

        public async static Task<T> ToObject<T>(this Task<string> json) => JsonSerializer.Deserialize<T>(await json);

        public static bool IsUrl(this string url) => Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        public static string FixUrl(this string url)
        {
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                var port = url.Split(':', '/')[1];
                if (!String.IsNullOrEmpty(port))
                {
                    if (port == "8443" || port == "443")
                        url = "https://" + url;
                    else
                        url = "http://" + url;
                }
            }
            return url.EndsWith('/') ? url : url + '/';
        }
    }
}
