using BATCH336A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BATCH336A.Models
{
    public class PenarikanSaldoModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;
        private VMResponse? apiResponse;
        private HttpContent content;
        private string jsonData;

        public PenarikanSaldoModel(IConfiguration _config)
        {
            //Menagmabil alamat api yang di simpan di appsetting.json
            apiUrl = _config["ApiUrl"];
        }

        public VMPenarikanSaldo? GetById(long id)
        {
            VMPenarikanSaldo? dataById = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>
                    (httpClient.GetStringAsync(apiUrl + "/api/PenarikanSaldo/GetById/" + id).Result);
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == System.Net.HttpStatusCode.OK)
                    {
                        dataById = JsonConvert.DeserializeObject<VMPenarikanSaldo>(JsonConvert.SerializeObject(apiResponse.data));
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
        public List<VMPenarikanSaldo>? GetDefNom()
        {
            List<VMPenarikanSaldo>? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(httpClient.GetStringAsync(apiUrl + "/api/PenarikanSaldo").Result);
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == System.Net.HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMPenarikanSaldo>?>
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
        public List<VMPenarikanSaldo>? GetCustNom(long id)
        {
            List<VMPenarikanSaldo>? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(httpClient.GetStringAsync(apiUrl + "/api/PenarikanSaldo/GetCustNom/" + id).Result);
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == System.Net.HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMPenarikanSaldo>?>
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
        public async Task<VMResponse> UpdateAsync(VMPenarikanSaldo data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await
                    (await httpClient.PutAsync($"{apiUrl}/api/PenarikanSaldo", content))
                    .Content.ReadAsStringAsync());
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == System.Net.HttpStatusCode.Created || apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMPenarikanSaldo?>
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
        public async Task<VMResponse> CekPin(VMPenarikanSaldo data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await
                    (await httpClient.PutAsync($"{apiUrl}/api/PenarikanSaldo/CekPin", content))
                    .Content.ReadAsStringAsync());
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == System.Net.HttpStatusCode.Created || apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMPenarikanSaldo?>
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
    }
}
