using BATCH336A.ViewModel;
using Newtonsoft.Json;
using System.Net;

namespace BATCH336A.Models
{
    public class BiodataModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;

        private VMResponse? apiResponse = new VMResponse();
        private HttpContent content;
        private string jsonData;

        public BiodataModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }
        public List<VMMBiodatum>? GetAll()
        {
            List<VMMBiodatum>? dataBiodata = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Biodata").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        dataBiodata = JsonConvert.DeserializeObject<List<VMMBiodatum>?>(
                            JsonConvert.SerializeObject(apiResponse.data)
                            );
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Biodata API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataBiodata;
        }

        public VMMBiodatum GetById(int id)
        {
            VMMBiodatum? dataBiodata = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Biodata/GetById/" + id).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        dataBiodata = JsonConvert.DeserializeObject<VMMBiodatum>(
                            JsonConvert.SerializeObject(apiResponse.data)
                            );
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Biodata API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataBiodata;
        }
    }
}
