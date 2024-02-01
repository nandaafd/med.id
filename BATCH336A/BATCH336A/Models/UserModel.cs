using BATCH336A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BATCH336A.Models
{
    public class UserModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;

        private VMResponse? apiResponse = new VMResponse();
        private HttpContent content;
        private string jsonData;

        public UserModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }
        public List<VMMUser>? GetAll()
        {
            List<VMMUser>? dataUser = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/User").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        dataUser = JsonConvert.DeserializeObject<List<VMMUser>?>(
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
                    throw new Exception("User API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataUser;
        }

        public VMMUser GetById(int id)
        {
            VMMUser? dataUser = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/User/GetById/" + id).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        dataUser = JsonConvert.DeserializeObject<VMMUser>(
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
                    throw new Exception("User API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataUser;
        }

        public VMResponse? GetByEmail(string email)
        {

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/User/GetByEmail/" + email).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMUser>(
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
                    throw new Exception("User API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
                apiResponse.message += $"{ex.Message}";
            }

            return apiResponse;
        }
        public VMResponse? Update(VMMUser data)
        {
            try
            {
                //Serial dari data ke json
                //Deserial di jadikan ke object
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                        httpClient.PutAsync($"{apiUrl}/api/User", content).Result.Content.ReadAsStringAsync().Result
                );

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK || apiResponse.statusCode == HttpStatusCode.Created)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMUser?>(
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
                    throw new Exception("Data API Cannot be reached");
                }
            }
            catch (Exception e)
            {
                apiResponse.message += $" {e.Message}";
            }

            return apiResponse;
        }
    }
}
