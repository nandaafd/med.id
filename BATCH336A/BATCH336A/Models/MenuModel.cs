using BATCH336A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BATCH336A.Models
{
    public class MenuModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;
        private readonly string imageFolder;

        private VMResponse? apiResponse;
        private IWebHostEnvironment webHostEnv;
        private HttpContent content;

        public MenuModel(IConfiguration _config, IWebHostEnvironment _webHostEnv)
        {
            apiUrl = _config["ApiUrl"];
            webHostEnv = _webHostEnv;
            imageFolder = _config["ImagesFolder"];
        }

        public string? UploadFile(IFormFile? imageFile)
        {
            string uniqueFileName = "";
            string uploadFolder = "";

            if (imageFile != null)
            {
                uploadFolder = $"{webHostEnv.WebRootPath}\\images\\assets\\";
                uniqueFileName = $"{Guid.NewGuid()}-{imageFile.FileName}";

                //upload process
                using (FileStream fileStream = new FileStream($"{uploadFolder}{uniqueFileName}", FileMode.CreateNew))
                {
                    imageFile.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        public bool DeleteOldImage(string oldImageFileName)
        {
            try
            {
                oldImageFileName = $"{webHostEnv.WebRootPath}\\images\\assets\\{oldImageFileName}";

                if (File.Exists(oldImageFileName))
                {
                    File.Delete(oldImageFileName);
                }
                else
                {
                    throw new ArgumentNullException("File doesn't Exist!");
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<VMMMenu>? GetAll()
        {
            List<VMMMenu>? dataMenu = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Menu").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        dataMenu = JsonConvert.DeserializeObject<List<VMMMenu>?>(
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
                    throw new Exception("Menu API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataMenu;
        }
        public VMMMenu? GetById(int id)
        {
            VMMMenu? dataMenu = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Menu/GetById/"+id).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        dataMenu = JsonConvert.DeserializeObject<VMMMenu>(
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
                    throw new Exception("Menu API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataMenu;
        }

        public VMResponse? Create(VMMMenu data)
        {
            try
            {
                if (data.ImageFile != null)
                {
                    data.BigIcon = UploadFile(data.ImageFile);
                    data.ImageFile = null;
                }
                else
                {
                    data.BigIcon = null;
                }

                string jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                    httpClient.PostAsync($"{apiUrl}/api/Menu", content).Result.Content.ReadAsStringAsync().Result
                );

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode != HttpStatusCode.OK || apiResponse.statusCode != HttpStatusCode.Created)
                    {
                        throw new Exception(apiResponse.message);

                    }
                }
                else
                {
                    apiResponse.statusCode = HttpStatusCode.NotFound;
                    throw new ArgumentNullException("Menu Api can't be reached!");
                }
            }
            catch (Exception e)
            {
                apiResponse.statusCode = (apiResponse.statusCode == HttpStatusCode.InternalServerError) ? HttpStatusCode.InternalServerError : apiResponse.statusCode;
                apiResponse.message += $" {e.Message}";
            }

            return apiResponse;
        }

        public VMResponse? Update(VMMMenu data)
        {
            try
            {
                if (data.ImageFile != null)
                {
                    data.BigIcon = UploadFile(data.ImageFile);
                    data.ImageFile = null;
                }
                else
                {
                    data.BigIcon = null;
                }

                string jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                    httpClient.PutAsync($"{apiUrl}/api/Menu", content).Result.Content.ReadAsStringAsync().Result
                );

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode != HttpStatusCode.OK || apiResponse.statusCode != HttpStatusCode.Created)
                    {
                        throw new Exception(apiResponse.message);

                    }
                }
                else
                {
                    apiResponse.statusCode = HttpStatusCode.NotFound;
                    throw new ArgumentNullException("Menu Api can't be reached!");
                }
            }
            catch (Exception e)
            {
                apiResponse.statusCode = (apiResponse.statusCode == HttpStatusCode.InternalServerError) ? HttpStatusCode.InternalServerError : apiResponse.statusCode;
                apiResponse.message += $" {e.Message}";
            }

            return apiResponse;
        }

        public async Task<VMResponse> DeleteAsync(long id, long userId)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(
                    await httpClient.DeleteAsync($"{apiUrl}/api/Menu/Delete/{id}/{userId}").Result.Content.ReadAsStringAsync()
                );
                if (apiResponse == null)
                {
                    throw new Exception("api cannot be reached");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
                apiResponse.data = null;
            }
            return apiResponse;
        }

    }
}
