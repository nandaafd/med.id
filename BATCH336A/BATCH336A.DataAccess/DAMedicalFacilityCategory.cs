using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic;
using System.Net;
using System.Xml.Linq;

namespace BATCH336A.DataAccess
{
    public class DAMedicalFacilityCategory
    {
        private VMResponse response = new VMResponse();
        private readonly BATCH336AContext db;
        public DAMedicalFacilityCategory(BATCH336AContext _db)
        {
            db = _db;
        }
        public VMResponse GetAll() => GetAllByFilter("");
        public VMResponse GetAllByFilter(string? filter)
        {
            try
            {
                List<VMMMedicalFacilityCategory> data = (
                    from mmfc in db.MMedicalFacilityCategories
                    where mmfc.IsDelete == false &&
                    mmfc.Name.Contains(filter ?? "")
                    select new VMMMedicalFacilityCategory
                    {
                        Id = mmfc.Id,
                        Name = mmfc.Name,
                        CreatedBy = mmfc.CreatedBy,
                        CreatedOn = mmfc.CreatedOn,
                        ModifiedBy = mmfc.ModifiedBy,
                        ModifiedOn = mmfc.ModifiedOn,
                        DeletedBy = mmfc.DeletedBy,
                        DeletedOn = mmfc.DeletedOn,
                        IsDelete = mmfc.IsDelete,
                    }
                ).ToList();
                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} success fatched!" : "Facility has no data!";
                response.statusCode = (data.Count > 0) ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NoContent;
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
                if (id > 0)
                {
                    VMMMedicalFacilityCategory? data = (
                        from mmfc in db.MMedicalFacilityCategories
                        where mmfc.Id == id
                        select new VMMMedicalFacilityCategory
                        {
                            Id = mmfc.Id,
                            Name = mmfc.Name,
                            CreatedBy = mmfc.CreatedBy,
                            CreatedOn = mmfc.CreatedOn,
                            ModifiedBy = mmfc.ModifiedBy,
                            ModifiedOn = mmfc.ModifiedOn,
                            DeletedBy = mmfc.DeletedBy,
                            DeletedOn = mmfc.DeletedOn,
                            IsDelete = mmfc.IsDelete,
                        }
                        ).FirstOrDefault();
                    if (data != null)
                    {
                        response.data = data;
                        response.message = $"Data dengan id = {id} berhasil ditemukan ";
                        response.statusCode = System.Net.HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = $"Data dengan id = {id} tidak ditemukan ";
                        response.statusCode = System.Net.HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = $"Input ID dengan benar";
                    response.statusCode = System.Net.HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                response.statusCode = HttpStatusCode.NotFound;
                response.message = ex.Message;
            }
            return response;
        }
        public VMResponse CreateData(VMMMedicalFacilityCategory data)
        {

            if (data.Name == null)
            {
                response.message = "Input is not correct!";
                return response;
            }
            MMedicalFacilityCategory? existingData = findDataByName(data.Name);
            if (existingData != null)
            {
                
                response.message = "Facility category exist";
                response.statusCode = HttpStatusCode.BadRequest;
                return response;
                
            }
            //starting db transaction process for create, using agar tidak nutup atau selesai
            using (IDbContextTransaction dbTrans = db.Database.BeginTransaction())
            {
                try
                {
                    //create data for new category
                    MMedicalFacilityCategory mmfc = new MMedicalFacilityCategory();
                    mmfc.Name = data.Name.Trim();
                    mmfc.CreatedBy = data.CreatedBy;
                    mmfc.CreatedOn = DateTime.Now;
                    mmfc.IsDelete = false;

                    for (int i = 0; i < mmfc.Name.Length; i++) {
                        if (mmfc.Name[i] == ' ' && mmfc.Name[i + 1] == ' ') {
                            response.message = "Input is not correct!";
                            response.statusCode = HttpStatusCode.BadRequest;
                            return response;
                        }
                    }

                    db.Add(mmfc); // untuk insert data
                    db.SaveChanges(); //untuk jalanin query
                    dbTrans.Commit(); // save data base changes

                    VMMMedicalFacilityCategory dataResponse = new VMMMedicalFacilityCategory()
                    {
                        Id = mmfc.Id,
                        Name = mmfc.Name,
                        CreatedBy = mmfc.CreatedBy,
                        CreatedOn = mmfc.CreatedOn,
                        IsDelete = false,
                    };
                    response.data = dataResponse;
                    response.message = "Facility category has been add to table";
                    response.statusCode = HttpStatusCode.Created; //201
                }
                catch (Exception ex)
                {
                    dbTrans.Rollback(); // jika terjadi eroor saat add dan save changes
                    response.data = data;
                    response.message = ex.Message;
                }
            }
            return response;
        }
        public VMResponse Update(VMMMedicalFacilityCategory data)
        {
            //starting db transaction process for update, using agar tidak nutup atau selesai
            using (IDbContextTransaction dbTrans = db.Database.BeginTransaction())
            {
                try
                {

                    // get id from getbyid
                    VMMMedicalFacilityCategory? existingData = (VMMMedicalFacilityCategory?)GetById(data.Id ?? 0).data;
                    if (existingData == null)
                    {
                        response.data = null;
                        response.message = $"Error Occured when update id = {data.Id}";
                        response.statusCode = HttpStatusCode.NotFound;
                        return response;
                    }

                    MMedicalFacilityCategory? existingDataByName = findDataByName(data.Name);
                    if (existingDataByName != null && existingDataByName.Id != data.Id)
                    {
                        response.data = null;
                        response.message = "Facility category exist";
                        response.statusCode = HttpStatusCode.BadRequest;
                        return response;
                    }

                    //update data for new category
                    MMedicalFacilityCategory mmfc = new MMedicalFacilityCategory()
                    {
                        Id = existingData.Id ?? 0,//(int)data.id 
                        CreatedBy = existingData.CreatedBy,
                        CreatedOn = existingData.CreatedOn ?? DateTime.Now,

                        Name = data.Name.Trim(),
                        ModifiedBy = data.ModifiedBy,
                        ModifiedOn = DateTime.Now,

                        IsDelete = false,
                    };

                    for (int i = 0; i < mmfc.Name.Length; i++)
                    {
                        if (mmfc.Name[i] == ' ' && mmfc.Name[i + 1] == ' ')
                        {
                            response.message = "Input is not correct!";
                            response.statusCode = HttpStatusCode.BadRequest;
                            return response;
                        }
                    }

                    db.Update(mmfc); // untuk insert data
                    db.SaveChanges(); //untuk jalanin query
                    dbTrans.Commit(); // save data base changes

                    response.data = mmfc;
                    response.message = $"Category name = {data.Name} has been update from table";
                    response.statusCode = HttpStatusCode.OK;
                }
                catch (Exception ex)
                {
                    dbTrans.Rollback(); // jika terjadi eroor saat add dan save changes
                    response.data = data;
                    response.message = ex.Message;
                }
            }
            return response;
        }
        public VMResponse Delete(long id, long userId)
        {
            if (id != 0 && userId != 0)
            {
                using (IDbContextTransaction dbTrans = db.Database.BeginTransaction())
                {
                    try
                    {
                        VMMMedicalFacilityCategory? existingData = (VMMMedicalFacilityCategory?)GetById(id).data;
                        //proses delete category if have data
                        if (existingData != null)
                        {
                            MMedicalFacilityCategory mmfc = new MMedicalFacilityCategory()
                            {
                                Id = existingData.Id ?? 0,//(int)data.id 
                                CreatedBy = existingData.CreatedBy,
                                CreatedOn = existingData.CreatedOn ?? DateTime.Now,
                                Name = existingData.Name,
                                ModifiedBy = existingData.ModifiedBy,
                                ModifiedOn = existingData.ModifiedOn,

                                DeletedBy = userId,
                                DeletedOn = DateTime.Now,
                                IsDelete = true,
                            };
                            db.Update(mmfc); // untuk insert data
                            db.SaveChanges(); //untuk jalanin query
                            dbTrans.Commit(); // save data base changes

                            response.data = mmfc;
                            response.message = $"Facility name = {existingData.Name} has been delete from table";
                            response.statusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            response.message = "ID is wrong, can't delete";
                            response.statusCode = HttpStatusCode.NotFound;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback(); // jika terjadi eroor saat add dan save changes
                        response.message = ex.Message;
                    }
                }
            }
            else
            {
                response.message = "Please input id and user id first";
                response.statusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }
        public MMedicalFacilityCategory? findDataByName(string name)
        {
            MMedicalFacilityCategory? responseData = new MMedicalFacilityCategory();
            try
            {
                responseData = (
                    from mmfc in db.MMedicalFacilityCategories
                    where mmfc.IsDelete == false &&
                    mmfc.Name.ToLower() == (name.ToLower()).Trim()
                    select new MMedicalFacilityCategory
                    {
                        Id = mmfc.Id,
                        Name = mmfc.Name,
                        CreatedBy = mmfc.CreatedBy,
                        CreatedOn = mmfc.CreatedOn,
                        ModifiedBy = mmfc.ModifiedBy,
                        ModifiedOn = mmfc.ModifiedOn,
                        DeletedBy = mmfc.DeletedBy,
                        DeletedOn = mmfc.DeletedOn,
                        IsDelete = mmfc.IsDelete,
                    }
                ).FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }

    }
}