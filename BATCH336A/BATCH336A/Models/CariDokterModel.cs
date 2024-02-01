using BATCH336A.ViewModel;
using Newtonsoft.Json;
using System.Net;

namespace BATCH336A.Models
{
    public class CariDokterModel
    {
        private readonly string? apiUrl;
        private readonly HttpClient? httpClient = new HttpClient();
        private VMResponse? apiResponse = new VMResponse();
        private HttpContent content;
        private string jsonData;

        public CariDokterModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }

        public List<VMCariDokter>? GetAll()
        {
            List<VMCariDokter>? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/CariDokter").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        //data = (List<VMTblMCategory>?)apiResponse.data;
                        data = JsonConvert.DeserializeObject<List<VMCariDokter>?>(
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
                    throw new Exception("Cari Dokter API cannot be reached");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
                apiResponse.data = null;
            }

            return data;
        }

        public List<VMCariDokter>? GetBy(string location, string spec, string name, string treat)
        {
            List<VMCariDokter>? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync($"{apiUrl}/api/CariDokter/Get?location={location}&spec={spec}&name={name}&treat={treat}").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        //data = (List<VMTblMCategory>?)apiResponse.data;
                        data = JsonConvert.DeserializeObject<List<VMCariDokter>?>(
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
                    throw new Exception("Cari Dokter API cannot be reached");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
                apiResponse.data = null;
            }

            return data;
        }

        public List<VMLokasiCariDokter>? GetLoc()
        {
            List<VMLokasiCariDokter>? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/CariDokter/GetLoc").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        //data = (List<VMTblMCategory>?)apiResponse.data;
                        data = JsonConvert.DeserializeObject<List<VMLokasiCariDokter>?>(
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
                    throw new Exception("Cari Dokter API cannot be reached");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
                apiResponse.data = null;
            }

            return data;
        }

        public List<VMMTindakan>? GetTreatment()
        {
            List<VMMTindakan>? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/CariDokter/GetTreatment").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        //data = (List<VMTblMCategory>?)apiResponse.data;
                        data = JsonConvert.DeserializeObject<List<VMMTindakan>?>(
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
                    throw new Exception("Cari Dokter API cannot be reached");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
                apiResponse.data = null;
            }

            return data;
        }
    }
}
