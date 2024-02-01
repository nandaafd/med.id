using BATCH336A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BATCH336A.Models
{
    public class EducationLevelModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;
        private VMResponse? apiResponse;
        private HttpContent content;
        private string jsonData;
        public EducationLevelModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }
        public List<VMEducationLevel>? GetAll()
        {
            List<VMEducationLevel>? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(httpClient.GetStringAsync(apiUrl + "/api/EducationLevel").Result);
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == System.Net.HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMEducationLevel>?>
                            (JsonConvert.SerializeObject(apiResponse.data)
                            );
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception(apiResponse.message);
                }
            }
            catch (Exception ex) { }
            return data;
        }
        public List<VMEducationLevel>? Search(string? filter)
        {
            List<VMEducationLevel>? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(httpClient.GetStringAsync(apiUrl + "/api/EducationLevel/Search/" + filter).Result);
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == System.Net.HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMEducationLevel>?>
                            (JsonConvert.SerializeObject(apiResponse.data)
                            );
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception(apiResponse.message);
                }
            }
            catch (Exception ex) { }
            return data;
        }
        public async Task<VMResponse> CreateAsync(VMEducationLevel data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await
                    (await httpClient.PostAsync($"{apiUrl}/api/EducationLevel", content))
                    .Content.ReadAsStringAsync());
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == System.Net.HttpStatusCode.Created || apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMEducationLevel?>
                            (JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Gabisa api nya");
                }
            }
            catch (Exception ex)
            {
                apiResponse.data = null;
            }
            return apiResponse;
        }
        public VMEducationLevel? GetById(int id)
        {
            VMEducationLevel? dataById = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>
                    (httpClient.GetStringAsync(apiUrl + "/api/EducationLevel/GetById/" + id).Result);
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == System.Net.HttpStatusCode.OK)
                    {
                        dataById = JsonConvert.DeserializeObject<VMEducationLevel>(JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception(apiResponse.message);
                }
            }
            catch (Exception ex)
            {

            }
            return dataById;
        }
        internal async Task<VMResponse> UpdateAsync(VMEducationLevel data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await
                    (await httpClient.PutAsync($"{apiUrl}/api/EducationLevel/Update", content))
                    .Content.ReadAsStringAsync());
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == System.Net.HttpStatusCode.Created || apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMEducationLevel?>
                            (JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Gabisa api nya");
                }
            }
            catch (Exception ex)
            {
                apiResponse.data = null;
            }
            return apiResponse;
        }
        public async Task<VMResponse> DeleteAsync(int id, int userId)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(
                await httpClient.DeleteAsync($"{apiUrl}/api/EducationLevel/Delete/{id}/{userId}").Result.Content.ReadAsStringAsync()
                ); ;
                if (apiResponse == null)
                {
                    throw new Exception("Category id not be found!");
                }
            }
            catch (Exception ex)
            {
                apiResponse.data = null;
            }
            return apiResponse;
        }
    }
}
