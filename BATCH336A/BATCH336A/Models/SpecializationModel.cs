using BATCH336A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BATCH336A.Models
{
    public class SpecializationModel
    {
        private readonly string? apiUrl;
        private readonly HttpClient? httpClient = new HttpClient();
        private VMResponse? apiResponse = new VMResponse();
        private HttpContent content;
        private string jsonData;

        public SpecializationModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }

        public List<VMMSpecialization>? GetAll()
        {
            List<VMMSpecialization>? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Specialization").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        //data = (List<VMTblMCategory>?)apiResponse.data;
                        data = JsonConvert.DeserializeObject<List<VMMSpecialization>?>(
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
                    throw new Exception("Specialization API cannot be reached");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
                apiResponse.data = null;
            }

            return data;
        }

        public List<VMMSpecialization>? GetBy(string filter)
        {
            List<VMMSpecialization>? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Specialization/GetByName/" + filter).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        //data = (List<VMTblMCategory>?)apiResponse.data;
                        data = JsonConvert.DeserializeObject<List<VMMSpecialization>?>(
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
                    throw new Exception("Category API cannot be reached");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
                apiResponse.data = null;
            }

            return data;
        }

        public VMMSpecialization? GetById(long id)
        {
            VMMSpecialization? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Specialization/GetById/" + id).Result);
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<VMMSpecialization?>(JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Specialization API could not be reached");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
                apiResponse.data = null;
            }
            return data;
        }

        public async Task<VMResponse> CreateAsync(VMMSpecialization data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                        await httpClient.PostAsync($"{apiUrl}/api/Specialization", content).Result.Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.Created || apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMSpecialization?>(
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

        public async Task<VMResponse> UpdateAsync(VMMSpecialization data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                    await (
                        await httpClient.PutAsync($"{apiUrl}/api/Specialization", content)
                   ).Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.Created || apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMSpecialization?>(
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
                        await httpClient.DeleteAsync($"{apiUrl}/api/Specialization?id={id}&userId={userId}").Result.Content.ReadAsStringAsync()
                    );

                if (apiResponse == null)
                {
                    throw new Exception("Specialization API cannot be reached !");
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
