using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Insomnia.Portal.BI.Interfaces;
using Insomnia.Portal.General.Expansions;

namespace Insomnia.Portal.BI.Services
{
    public class SendData : ISender
    {
        public SendData()
        {

        }

        public async Task<T> Get<T>(string url, string token = null)
        {
            if (String.IsNullOrEmpty(url))
                return default(T);

            using (HttpClient httpClient = new HttpClient())
            {
                if (!String.IsNullOrEmpty(token))
                    httpClient.DefaultRequestHeaders.Add("Token", token);
                var response = await httpClient.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();

                return content.ToObject<T>();
            }
        }
    }
}
