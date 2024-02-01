using BATCH336A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BATCH336A.Models
{
    public class CourierModel
    {
        private readonly string apiUrl;
        private readonly HttpClient httpClient = new HttpClient();
        private VMResponse? apiResponse;
        private HttpContent content;
        private string jsonData;
        
        public CourierModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }
        public List<VMMCourier>? GetAll()
        {
            List<VMMCourier>? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Courier").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        //data = (List<VMTblMCategory>?)apiResponse.data;
                        data = JsonConvert.DeserializeObject<List<VMMCourier>?>(
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
                    throw new Exception("Courier API cannot be reached");
                }
            }
            catch (Exception ex)
            {

            }

            return data;
        }

        public List<VMMCourier>? GetBy(string filter)
        {
            List<VMMCourier>? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Courier/GetBy/" + filter).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        //data = (List<VMTblMCategory>?)apiResponse.data;
                        data = JsonConvert.DeserializeObject<List<VMMCourier>?>(
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
                    throw new Exception("Courier API cannot be reached");
                }
            }
            catch (Exception ex)
            {

            }

            return data;
        }
        public VMMCourier? GetById(long id)
        {
            VMMCourier? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Courier/Get/" + id).Result);
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<VMMCourier?>(JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Courier API could not be reached");
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }
        public async Task<VMResponse> CreateAsync(VMMCourier data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                        await httpClient.PostAsync($"{apiUrl}/api/Courier", content).Result.Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.Created || apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMCourier?>(
                            JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Courier API cannot be reached");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
                apiResponse.data = null;
            }
            return apiResponse;
        }
        public async Task<VMResponse> UpdateAsync(VMMCourier data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                    await (
                        await httpClient.PutAsync($"{apiUrl}/api/Courier", content)
                   ).Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.Created || apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMCourier?>(
                            JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Courier API cannot be reached");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
                apiResponse.data = null;
            }
            return apiResponse;
        }
        public async Task<VMResponse> DeleteAsync(int id, int DeletedBy)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                        await httpClient.DeleteAsync($"{apiUrl}/api/Courier?id={id}&DeletedBy={DeletedBy}").Result.Content.ReadAsStringAsync()
                    );

                if (apiResponse == null)
                {
                    throw new Exception("Courier API cannot be reached !");
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
