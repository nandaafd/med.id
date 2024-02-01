using BATCH336A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace BATCH336A.Models
{
    public class BuatJanjiModel
    {
        private readonly string? apiUrl;
        private readonly HttpClient? httpClient = new HttpClient();
        private VMResponse? apiResponse = new VMResponse();
        private HttpContent content;
        private string jsonData;
        private object? dataa;

        public BuatJanjiModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }

        public VMCariDokter? GetDocById(int id)
        {
            VMCariDokter? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/BuatJanji/GetDoc?id=" + id).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        //data = (List<VMTblMCategory>?)apiResponse.data;
                        data = JsonConvert.DeserializeObject<VMCariDokter?>(
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

        public VMMCustomer2? GetCustById(int id)
        {
            VMMCustomer2? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/BuatJanji/GetCust?id=" + id).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        //data = (List<VMTblMCategory>?)apiResponse.data;
                        data = JsonConvert.DeserializeObject<VMMCustomer2?>(
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
                    throw new Exception("Buat Janji API cannot be reached");
                }
            }
            catch (Exception ex )
            {
                apiResponse.message += $" {ex.Message}";
                apiResponse.data = null;
            }
            return data;
        }

        public List<VMMMedicalFacilitySchedule>? GetJadwal(long offId)
        {
            List<VMMMedicalFacilitySchedule>? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync($"{apiUrl}/api/BuatJanji/GetJadwal?offId={offId}").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        //data = (List<VMTblMCategory>?)apiResponse.data;
                        data = JsonConvert.DeserializeObject<List<VMMMedicalFacilitySchedule>?>(
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
                    throw new Exception("Buat Janji API cannot be reached");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
                apiResponse.data = null;
            }
            return data;
        }

        public List<VMMTindakan>? GetTreatment(long offId)
        {
            List<VMMTindakan>? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync($"{apiUrl}/api/BuatJanji/GetTreatment?offId={offId}").Result);

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
                    throw new Exception("Buat Janji API cannot be reached");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
                apiResponse.data = null;
            }
            return data;
        }

        public async Task<VMResponse> CreateAsync(VMTAppointment data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                        await httpClient.PostAsync($"{apiUrl}/api/Appointment", content).Result.Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.Created || apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMTAppointment?>(
                            JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Appointment API cannot be reached");
                }
            }
            catch
            {
                apiResponse.data = null;
            }
            return apiResponse;
        }

        public List<VMTAppointment>? GetApp(long schedId, string appDate2)
        {
            List<VMTAppointment>? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync($"{apiUrl}/api/Appointment/GetApp?schedId={schedId}&appDate={appDate2}").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMTAppointment>?>(
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

        public VMTDoctorOfficeSchedule? GetSlot(long id)
        {
            VMTDoctorOfficeSchedule? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync($"{apiUrl}/api/Appointment/GetSlot?id={id}").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<VMTDoctorOfficeSchedule?>(
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
                    throw new Exception("Buat Janji API cannot be reached");
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
