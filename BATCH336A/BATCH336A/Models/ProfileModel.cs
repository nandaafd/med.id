using BATCH336A.ViewModel;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;

namespace BATCH336A.Models
{
    public class ProfileModel
    {
        private readonly string apiUrl;
        private readonly HttpClient httpClient = new HttpClient();
        private VMResponse? apiResponse;
        private HttpContent content;
        private string jsonData;

        public ProfileModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }
        public VMMUser? Get(int id)
        {
            VMMUser? data = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                    httpClient.GetStringAsync($"{apiUrl}/api/Profile/Get/{id}").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<VMMUser?>(
                            JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new ArgumentNullException("Profile API cannot be reached");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return data;
        }
    }
}
