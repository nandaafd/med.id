
using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;
using System.Net;

namespace BATCH336A.DataAccess
{
    public class DABiodata
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336AContext db;
        public DABiodata(BATCH336AContext _db) { db = _db; }

        public VMResponse GetAll()
        {
            try
            {
                List<VMMBiodatum> data = (
                    from t in db.MBiodata
                    where t.IsDelete == false
                    select new VMMBiodatum
                    {
                        Id = t.Id,
                        Fullname = t.Fullname,
                        MobilePhone = t.MobilePhone,
                        Image = t.Image,
                        ImagePath = t.ImagePath,
                        CreateBy = t.CreateBy,
                        CreateOn = t.CreateOn,
                        ModifiedBy = t.ModifiedBy,
                        ModifiedOn = t.ModifiedOn,
                        DeletedBy = t.DeletedBy,
                        DeletedOn = t.DeletedOn,
                        IsDelete = t.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} Biodata data Successfully fetched!" : "Token has no Data!";
                response.statusCode = (data.Count > 0) ? HttpStatusCode.OK : HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.statusCode = HttpStatusCode.NotFound;
            }
            return response;
        }
        
        public VMResponse GetById(long id)
        {
            try
            {
                VMMBiodatum? data = (
                    from t in db.MBiodata
                    where t.IsDelete == false && id == t.Id
                    select new VMMBiodatum
                    {
                        Id = t.Id,
                        Fullname = t.Fullname,
                        MobilePhone = t.MobilePhone,
                        Image = t.Image,
                        ImagePath = t.ImagePath,
                        CreateBy = t.CreateBy,
                        CreateOn = t.CreateOn,
                        ModifiedBy = t.ModifiedBy,
                        ModifiedOn = t.ModifiedOn,
                        DeletedBy = t.DeletedBy,
                        DeletedOn = t.DeletedOn,
                        IsDelete = t.IsDelete
                    }
                ).FirstOrDefault();

                if (data != null)
                {
                    response.data = data;
                    response.message = "berhasil mendapatkan data token dengan id "+data.Id;
                    response.statusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.message = "gagal mendapatkan data";
                    response.statusCode = HttpStatusCode.NoContent;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.statusCode = HttpStatusCode.NotFound;
            }
            return response;
        }

    }
}
