using BATCH336A.ViewModel;
using Newtonsoft.Json;
using System.Net;

namespace BATCH336A.Models
{
    public class RoleModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;

        private VMResponse? apiResponse = new VMResponse();
        private HttpContent content;
        private string jsonData;

        public RoleModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }
        public List<VMMRole>? GetAll()
        {
            List<VMMRole>? dataRole = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Role").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        dataRole = JsonConvert.DeserializeObject<List<VMMRole>?>(
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
                    throw new Exception("Role API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataRole;
        }

        public VMMRole GetById(int id)
        {
            VMMRole? dataRole = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Role/GetById/" + id).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        dataRole = JsonConvert.DeserializeObject<VMMRole>(
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
                    throw new Exception("Role API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataRole;
        }
    }
}
