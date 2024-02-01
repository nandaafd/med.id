
using BATCH336A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BATCH336A.Models
{
    public class PendaftaranModel
    {
        private readonly string? apiUrl;
        private readonly HttpClient? httpClient = new HttpClient();
        private VMResponse? apiResponse = new VMResponse();
        private HttpContent content;
        private string jsonData;

        public PendaftaranModel(IConfiguration _config) 
        {
            apiUrl = _config["ApiUrl"];
        }
        public async Task<VMResponse> CreateAsync(VMMPendaftaran data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse>(
                     await httpClient.PostAsync(apiUrl + "/api/Pendaftaran", content).Result.Content.ReadAsStringAsync()
                );
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.Created || apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMPendaftaran>(
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

        public async Task<VMResponse> GetByEmail(string email)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Pendaftaran/GetByEmail/" + email).Result);
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMUser?>(JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        apiResponse.statusCode = HttpStatusCode.NotFound;
                        throw new Exception("Email belum terdaftar, silahkan mendaftar");
                    }
                }
                else
                {
                    apiResponse.statusCode = HttpStatusCode.NotFound;
                    throw new Exception("Email belum terdaftar, silahkan mendaftar");
                }
            }
            catch (Exception ex)
            {
                apiResponse.statusCode = HttpStatusCode.NotFound;
                apiResponse.message = $" {ex.Message}";
            }
            return apiResponse;
        }
        public async Task<VMResponse> SendEmail(string email, int otp)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(
                     await httpClient.PostAsync(apiUrl + $"/api/Pendaftaran/SendEmail/{email}/{otp}", content).Result.Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMUser?>(JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        apiResponse.statusCode = HttpStatusCode.InternalServerError;
                        throw new Exception("Gagal mengirim email, periksa sambungan anda");
                    }
                }
                else
                {
                    apiResponse.statusCode = HttpStatusCode.InternalServerError;
                    throw new Exception("Gagal mengirim email, periksa sambungan anda");
                }
            }
            catch (Exception ex)
            {
                apiResponse.statusCode = HttpStatusCode.InternalServerError;
                apiResponse.message = $" {ex.Message}";
            }
            return apiResponse;
        }
    }
}
