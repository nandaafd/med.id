using BATCH336A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BATCH336A.Models
{
    public class MedicalFacilityCategoryModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;
        private VMResponse? apiResponse;
        private HttpContent content;
        private string jsonData;
        public MedicalFacilityCategoryModel(IConfiguration _config)
        {
            //Menagmabil alamat api yang di simpan di appsetting.json
            apiUrl = _config["ApiUrl"];
        }
        public List<VMMMedicalFacilityCategory>? GetAll()
        {
            List<VMMMedicalFacilityCategory>? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(httpClient.GetStringAsync(apiUrl + "/api/MedicalFacility").Result);
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == System.Net.HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMMMedicalFacilityCategory>?>
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
        public List<VMMMedicalFacilityCategory>? Search(string? filter)
        {
            List<VMMMedicalFacilityCategory>? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(httpClient.GetStringAsync(apiUrl + "/api/MedicalFacility/Search/" + filter).Result);
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == System.Net.HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMMMedicalFacilityCategory>?>
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
        public async Task<VMResponse> CreateAsync(VMMMedicalFacilityCategory data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await
                    (await httpClient.PostAsync($"{apiUrl}/api/MedicalFacility", content))
                    .Content.ReadAsStringAsync());
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == System.Net.HttpStatusCode.Created || apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMMedicalFacilityCategory?>
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
        public VMMMedicalFacilityCategory? GetById(int id)
        {
            VMMMedicalFacilityCategory? dataById = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>
                    (httpClient.GetStringAsync(apiUrl + "/api/MedicalFacility/GetById/" + id).Result);
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == System.Net.HttpStatusCode.OK)
                    {
                        dataById = JsonConvert.DeserializeObject<VMMMedicalFacilityCategory>(JsonConvert.SerializeObject(apiResponse.data));
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
        internal async Task<VMResponse> UpdateAsync(VMMMedicalFacilityCategory data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await
                    (await httpClient.PutAsync($"{apiUrl}/api/MedicalFacility/Update", content))
                    .Content.ReadAsStringAsync());
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == System.Net.HttpStatusCode.Created || apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMMedicalFacilityCategory?>
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
                await httpClient.DeleteAsync($"{apiUrl}/api/MedicalFacility/Delete/{id}/{userId}").Result.Content.ReadAsStringAsync()
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
