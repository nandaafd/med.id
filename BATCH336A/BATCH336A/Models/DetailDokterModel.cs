using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BATCH336A.Models
{
    public class DetailDokterModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;
        private VMResponse? apiResponse;
        private HttpContent content;
        private string jsonData;

        public DetailDokterModel(IConfiguration _config) {
            apiUrl = _config["ApiUrl"];
        }
        public List<VMDetailDokter>? GetEducation(long id)
        {
            List<VMDetailDokter>? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(httpClient.GetStringAsync(apiUrl + "/api/DetailDokter/Education/" + id).Result);
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == System.Net.HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMDetailDokter>?>
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
        public List<VMDetailDokter>? GetMedis(long id)
        {
            List<VMDetailDokter>? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(httpClient.GetStringAsync(apiUrl + "/api/DetailDokter/Medis/" + id).Result);
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == System.Net.HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMDetailDokter>?>
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
        public List<VMDetailDokter>? GetRiwayat(long id)
        {
            List<VMDetailDokter>? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(httpClient.GetStringAsync(apiUrl + "/api/DetailDokter/Riwayat/" + id).Result);
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == System.Net.HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMDetailDokter>?>
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
        public List<VMDetailDokter>? GetJadwal(long id)
        {
            List<VMDetailDokter>? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(httpClient.GetStringAsync(apiUrl + "/api/DetailDokter/Jadwal/" + id).Result);
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == System.Net.HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMDetailDokter>?>
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
        public List<VMDetailDokter>? GetLokasi(long id)
        {
            List<VMDetailDokter>? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(httpClient.GetStringAsync(apiUrl + "/api/DetailDokter/Lokasi/" + id).Result);
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == System.Net.HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMDetailDokter>?>
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
        public VMDetailDokter? GetProfile(long id)
        {
            VMDetailDokter? profileById = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>
                    (httpClient.GetStringAsync(apiUrl + "/api/DetailDokter/Profile/" + id).Result);
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == System.Net.HttpStatusCode.OK)
                    {
                        profileById = JsonConvert.DeserializeObject<VMDetailDokter>(JsonConvert.SerializeObject(apiResponse.data));
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
            return profileById;
        }
    }
}
