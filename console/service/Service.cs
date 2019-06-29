using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using helper;

namespace service
{
    public class GetContacts : IDisposable
    {

        public GetContacts()
        {
            Console.WriteLine("Constructor Service1");
        }

        public object GetData()
        {

            try
            {
                //Get arguments
                string[] arguments = Environment.GetCommandLineArgs();

                //Connect to Dynamics CRM
                var httpClient = ConnectToCRM(arguments);

                if (httpClient != null)
                {
                    //Call Api
                    CallerAsync(httpClient).Wait();

                    //Dispose
                    httpClient.Dispose();
                }

            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }

            Console.ReadKey();

            return null;

        }

        public void Dispose()
        {
            Console.WriteLine("Dispose Service1");
        }

        /// <summary>
        /// ConnectToCRM
        /// </summary>
        /// <param name="cmdargs"></param>
        /// <returns></returns>
        private static HttpClient ConnectToCRM(String[] cmdargs)
        {
            HttpClient httpClient;

            Configuration config = null;
            if (cmdargs.Length > 0)
                config = new FileConfiguration(cmdargs[0]);
            else
                config = new FileConfiguration(null);
            Authentication auth = new Authentication(config);
            httpClient = new HttpClient(auth.ClientHandler, true)
            {
                BaseAddress = new Uri(config.ServiceUrl + "api/data/v8.1/"),
                Timeout = new TimeSpan(0, 2, 0)
            };
            httpClient.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
            httpClient.DefaultRequestHeaders.Add("OData-Version", "4.0");
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        /// <summary>
        /// DisplayException
        /// </summary>
        /// <param name="ex"></param>
        private static void DisplayException(Exception ex)
        {
            Console.WriteLine("The application terminated with an error.");
            Console.WriteLine(ex.Message);
            while (ex.InnerException != null)
            {
                Console.WriteLine("\t* {0}", ex.InnerException.Message);
                ex = ex.InnerException;
            }
        }

        /// <summary>
        /// CallerAsync
        /// </summary>
        /// <param name="httpClient"></param>
        /// <returns></returns>
        private static async System.Threading.Tasks.Task CallerAsync(HttpClient httpClient)
        {
            //Calls the https REST-API ../contacts
            var result = await httpClient.GetAsync("contacts");

            //retrieve the response data
            var contents = await result.Content.ReadAsStringAsync();

            //converting the response data to Json
            var results = JsonConvert.DeserializeObject<dynamic>(contents);

            //Write each contact to console
            foreach (var item in results["value"])
            {
                Console.WriteLine(item);
            }
        }

    }

}
