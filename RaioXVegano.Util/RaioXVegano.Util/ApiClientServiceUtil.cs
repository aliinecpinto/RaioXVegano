using Newtonsoft.Json;
using NLog;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace RaioXVegano.Util
{
    public static class ApiClientServiceUtil<Request, Response>
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public static Response Get(Uri requestUrl, Request request)
        {
            _log.Info($"Método Get... ");

            HttpClient httpClient = InstanciarHttpClient();

            var result = httpClient.GetAsync(requestUrl.AbsoluteUri).Result;

            Response response = MontaRetornoDoServico(result);

            _log.Info($"Método Get... OK");

            return response;
        }

        public static Response Post(Uri requestUrl, Request request)
        {
            _log.Info($"Método Post... ");

            HttpClient httpClient = InstanciarHttpClient();

            var result = httpClient.PostAsync(requestUrl.AbsoluteUri, MontaHttpContent(request)).Result;

            Response response = MontaRetornoDoServico(result);

            _log.Info($"Método Post... OK");

            return response;
        }

        public static Response Put(Uri requestUrl, Request request)
        {
            _log.Info($"Método Put... ");

            HttpClient httpClient = InstanciarHttpClient();

            var result = httpClient.PutAsync(requestUrl.AbsoluteUri, MontaHttpContent(request)).Result;

            Response response = MontaRetornoDoServico(result);

            _log.Info($"Método Put... OK");

            return response;
        }

        private static HttpClient InstanciarHttpClient()
        {
            _log.Info($"Método InstanciarHttpClient... ");

            HttpClient httpClient = new HttpClient();

            HttpClientHandler handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            httpClient = new HttpClient(handler)
            {
                MaxResponseContentBufferSize = int.MaxValue
            };

            addHeaders(httpClient);

            _log.Info($"Método InstanciarHttpClient... OK");

            return httpClient;
        }

        private static HttpContent MontaHttpContent(Request content)
        {
            var json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private static JsonSerializerSettings MicrosoftDateFormatSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }

        private static void addHeaders(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
        }

        private static Response MontaRetornoDoServico(HttpResponseMessage response)
        {
            _log.Info($"Método MontaRetornoDoServico... ");

            var data = string.Empty;
            if (response.IsSuccessStatusCode)
            {
                data = response.Content.ReadAsStringAsync().Result;
            }

            _log.Info($"Método MontaRetornoDoServico... OK");

            return JsonConvert.DeserializeObject<Response>(data);
        }
    }
}
