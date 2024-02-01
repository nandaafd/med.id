using BATCH336A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BATCH336A.Models
{
    public class TokenModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;

        private VMResponse? apiResponse = new VMResponse();
        private HttpContent content;
        private string jsonData;

        public TokenModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }
        public List<VMTToken>? GetAll()
        {
            List<VMTToken>? data = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Token").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMTToken>?>(
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

            return data;
        }

        public List<VMTToken>? GetByEmail(string email)
        {
            List<VMTToken>? data = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Token/GetByEmail/"+email).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMTToken>?>(
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

            return data;
        }

        public VMTToken GetById(int id)
        {
            VMTToken? data = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Token/GetById/" + id).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<VMTToken>(
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
                    throw new Exception("Token API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return data;
        }
        public async Task<VMResponse> CreateAsync(VMTToken data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse>(
                     await httpClient.PostAsync(apiUrl + "/api/Token", content).Result.Content.ReadAsStringAsync()
                );
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.Created || apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMTToken>(
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
                    throw new Exception("Gagal mendaftar, periksa sambungan anda!");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
                apiResponse.data = null;
            }
            return apiResponse;
        }
        public async Task<VMResponse> Update(VMTToken data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse>(
                     await httpClient.PutAsync(apiUrl + "/api/Token", content).Result.Content.ReadAsStringAsync()
                );
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.Created || apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMTToken>(
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
                    throw new Exception("Gagal mendaftar, periksa sambungan anda!");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
                apiResponse.data = null;
            }
            return apiResponse;
        }
    }
}
