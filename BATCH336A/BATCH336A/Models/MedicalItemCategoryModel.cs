using BATCH336A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BATCH336A.Models
{
    public class MedicalItemCategoryModel
    {
        private readonly string? apiUrl;
        private readonly HttpClient? httpClient = new HttpClient();
        private VMResponse? apiResponse = new VMResponse();
        private HttpContent content;
        private string jsonData;

        public MedicalItemCategoryModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }

        public List<VMMMedicalItemCategory>? GetAll()
        {
            List<VMMMedicalItemCategory>? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/MedicalItemCategory").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        //data = (List<VMTblMCategory>?)apiResponse.data;
                        data = JsonConvert.DeserializeObject<List<VMMMedicalItemCategory>?>(
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
                    throw new Exception("Medical Item Category API cannot be reached");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message = ex.Message;
                apiResponse.data = null;
            }

            return data;
        }

        public List<VMMMedicalItemCategory>? GetBy(string filter)
        {
            List<VMMMedicalItemCategory>? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/MedicalItemCategory/GetByName/" + filter).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        //data = (List<VMTblMCategory>?)apiResponse.data;
                        data = JsonConvert.DeserializeObject<List<VMMMedicalItemCategory>?>(
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
                    throw new Exception("Medical Item Category API cannot be reached");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
                apiResponse.data = null;
            }

            return data;
        }

        public VMMMedicalItemCategory? GetById(long id)
        {
            VMMMedicalItemCategory? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/MedicalItemCategory/GetById/" + id).Result);
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<VMMMedicalItemCategory?>(JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Medical Item Category API could not be reached");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
                apiResponse.data = null;
            }
            return data;
        }

        public async Task<VMResponse> CreateAsync(VMMMedicalItemCategory data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                        await httpClient.PostAsync($"{apiUrl}/api/MedicalItemCategory", content).Result.Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.Created || apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMMedicalItemCategory?>(
                            JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Specialization API cannot be reached");
                }
            }
            catch
            {
                
                apiResponse.data = null;
            }
            return apiResponse;
        }

        public async Task<VMResponse> UpdateAsync(VMMMedicalItemCategory data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                    await (
                        await httpClient.PutAsync($"{apiUrl}/api/MedicalItemCategory", content)
                   ).Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.Created || apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMMedicalItemCategory?>(
                            JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Medical Item Category API cannot be reached");
                }
            }
            catch
            {
                //apiResponse.message += $" {ex.Message}";
                apiResponse.data = null;
            }
            return apiResponse;
        }

        public async Task<VMResponse> DeleteAsync(long id, long userId)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                        await httpClient.DeleteAsync($"{apiUrl}/api/MedicalItemCategory?id={id}&userId={userId}").Result.Content.ReadAsStringAsync()
                    );

                if (apiResponse == null)
                {
                    throw new Exception("Medical Item Category API cannot be reached !");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
            }
            return apiResponse;
        }
    }
}
