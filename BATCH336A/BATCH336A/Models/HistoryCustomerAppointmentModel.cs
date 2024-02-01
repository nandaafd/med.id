using BATCH336A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BATCH336A.Models
{
    public class HistoryCustomerAppointmentModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;

        private VMResponse? apiResponse = new VMResponse();
        private HttpContent content;
        private string jsonData;

        public HistoryCustomerAppointmentModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }

        public List<VMMHistoryCustomer>? GetAll(long id)
        {
            List<VMMHistoryCustomer>? dataRole = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/HistoryCustomer/GetAll/GetAllById/"+id).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        dataRole = JsonConvert.DeserializeObject<List<VMMHistoryCustomer>?>(
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
                    throw new Exception("Role API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataRole;
        }
        public List<VMMHistoryCustomer>? GetByFilter(string filter)
        {
            List<VMMHistoryCustomer>? dataRole = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/HistoryCustomer/GetByFilter/"+filter).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        dataRole = JsonConvert.DeserializeObject<List<VMMHistoryCustomer>?>(
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
                    throw new Exception("Role API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataRole;
        }
        public List<VMTPrescription>? GetPrescriptionById(int id)
        {
            List<VMTPrescription>? data = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/HistoryCustomer/GetPrescriptionById/" + id).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMTPrescription>?>(
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
                    throw new Exception("Role API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return data;
        }
        public VMMHistoryCustomer GetById(int id)
        {
            VMMHistoryCustomer? data = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/HistoryCustomer/GetById/" + id).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<VMMHistoryCustomer>(
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
                    throw new Exception("Role API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return data;
        }
        public async Task<VMResponse> Update(VMTPrescription data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse>(
                     await httpClient.PutAsync(apiUrl + "/api/HistoryCustomer", content).Result.Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.Created || apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMTPrescription>(
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
                    throw new Exception("Category API cannot be reached!");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message = ex.Message;
                apiResponse.data = null;
            }
            return apiResponse;
        }

    }
}
