using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336A.DataAccess
{
    public class DAMedicalItemCategory
    {
        private VMResponse response = new VMResponse();
        private readonly BATCH336AContext db;

        public DAMedicalItemCategory(BATCH336AContext _db)
        {
            db = _db;
        }

        public VMResponse GetByFilter() => GetByFilter("");

        public VMResponse GetByFilter(string name)
        {
            try
            {
                List<VMMMedicalItemCategory> data = (
                    from mic in db.MMedicalItemCategories
                    where mic.IsDelete == false
                        && mic.Name.Contains(name ?? "")
                    select new VMMMedicalItemCategory
                    {
                        Id = mic.Id,
                        Name = mic.Name,
                        CreatedBy = mic.CreatedBy,
                        CreatedOn = mic.CreatedOn,
                        ModifiedBy = mic.ModifiedBy,
                        ModifiedOn = mic.ModifiedOn,
                        DeletedBy = mic.DeletedBy,
                        DeletedOn = mic.DeletedOn,
                        IsDelete = mic.IsDelete

                    }).ToList();
                response.data = data;
                response.message = (data.Count > 0)
                    ? $"{data.Count} Medical Item Category data successfully fetched"
                    : "Medical Item Category has no data";
                response.statusCode = (data.Count > 0)
                    ? HttpStatusCode.OK
                    : HttpStatusCode.NoContent;

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
                    VMMMedicalItemCategory? data = (
                        from mic in db.MMedicalItemCategories
                        where mic.Id == id
                            && mic.IsDelete == false
                        select new VMMMedicalItemCategory
                        {
                            Id = mic.Id,
                            Name = mic.Name,
                            CreatedBy = mic.CreatedBy,
                            CreatedOn = mic.CreatedOn,
                            ModifiedBy = mic.ModifiedBy,
                            ModifiedOn = mic.ModifiedOn,
                            DeletedBy = mic.DeletedBy,
                            DeletedOn = mic.DeletedOn,
                            IsDelete = mic.IsDelete

                        }).FirstOrDefault();

                    if (data != null)
                    {
                        response.data = data;
                        response.message = $"Medical item category data with ID={id} is successfully fetched";
                        response.statusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = "Medical item category data could not be found";
                        response.statusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = "Please input a valid Medical item category Id";
                    response.statusCode = HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.statusCode = HttpStatusCode.NotFound;
            }
            return response;
        }

        public VMMMedicalItemCategory? GetByName(string name)
        {
            VMMMedicalItemCategory? data = new VMMMedicalItemCategory();

            try
            {
                data = (
                    from mic in db.MMedicalItemCategories
                    where mic.Name == name
                        && mic.IsDelete == false
                    select new VMMMedicalItemCategory
                    {
                        Id = mic.Id,
                        Name = mic.Name,
                        CreatedBy = mic.CreatedBy,
                        CreatedOn = mic.CreatedOn,
                        ModifiedBy = mic.ModifiedBy,
                        ModifiedOn = mic.ModifiedOn,
                        DeletedBy = mic.DeletedBy,
                        DeletedOn = mic.DeletedOn,
                        IsDelete = mic.IsDelete

                    }).FirstOrDefault();
            }
            catch (Exception ex)
            {

            }

            return data;
        }

        public VMResponse CreateMedicalItemCategory(VMMMedicalItemCategory data)
        {
            string character = "!@#$%^&*()+=_{}[]|?><,.";

            foreach (char c in data.Name)
            {
                foreach (char c2 in character)
                {
                    if (c == c2)
                    {
                        response.message = "Name must only contains alphabet";
                        response.statusCode = HttpStatusCode.BadRequest;
                        return response;
                    }
                }
            }
            VMMMedicalItemCategory? existingData = GetByName(data.Name);
            if (existingData != null)
            {
                response.message = "Medical Item Category name already exist";
                response.statusCode = HttpStatusCode.BadRequest;
                return response;
            }
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    MMedicalItemCategory medicalItemCat = new MMedicalItemCategory();
                    medicalItemCat.Name = data.Name;
                    medicalItemCat.CreatedBy = data.CreatedBy;
                    medicalItemCat.IsDelete = false;
                    medicalItemCat.CreatedOn = DateTime.Now;

                    //Process to insert data into DB Table
                    db.Add(medicalItemCat);
                    db.SaveChanges();

                    //Commit changes to database
                    dbTran.Commit();

                    //Update API Response
                    response.data = medicalItemCat;
                    response.message = "New Medical Item Category data has been successfully created";
                    response.statusCode = HttpStatusCode.Created;
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();

                    response.message = ex.Message;
                    response.data = data;
                }
            }
            return response;
        }

        public VMResponse UpdateMedicalItemCategory(VMMMedicalItemCategory data)
        {
            string character = "!@#$%^&*()+=_{}[]|?><,.";
            foreach (char c in data.Name)
            {
                foreach (char c2 in character)
                {
                    if (c == c2)
                    {
                        response.message = "Nama tidak boleh mengandung karakter selain alfabet";
                        response.statusCode = HttpStatusCode.BadRequest;
                        return response;
                    }
                }
            }
            VMMMedicalItemCategory? existData = GetByName(data.Name);
            if (existData != null)
            {
                response.message = "Nama kategori sudah dibuat";
                response.statusCode = HttpStatusCode.BadRequest;
                return response;
            }
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMMMedicalItemCategory? existingData = (VMMMedicalItemCategory?)GetById(data.Id ?? 0).data;

                    if (existingData != null)
                    {
                        MMedicalItemCategory medicalItemCat = new MMedicalItemCategory();
                        medicalItemCat.Id = existingData.Id ?? 0;
                        medicalItemCat.CreatedBy = existingData.CreatedBy;
                        medicalItemCat.CreatedOn = existingData.CreatedOn;
                        medicalItemCat.IsDelete = existingData.IsDelete;

                        medicalItemCat.Name = data.Name;
                        medicalItemCat.ModifiedBy = data.ModifiedBy;
                        medicalItemCat.ModifiedOn = DateTime.Now;

                        db.Update(medicalItemCat);
                        db.SaveChanges();

                        //Commit changes to database
                        dbTran.Commit();

                        //Update API Response
                        response.data = medicalItemCat;
                        response.message = $"Medical Item Category with ID={existingData.Id} has been successfully updated";
                        response.statusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = "Requested Medical Item Category data cannot be updated";
                        response.data = data;
                        response.statusCode = HttpStatusCode.NotFound;
                    }
                }
                catch (Exception ex)
                {
                    //undo changes 
                    dbTran.Rollback();

                    response.message = ex.Message;
                    response.data = data;
                }
            }
            return response;
        }

        public VMResponse DeleteMedicalItemCategory(long id, long userId)
        {
            if (userId != 0 && id != 0)
            {
                using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    try
                    {
                        VMMMedicalItemCategory? existingData = (VMMMedicalItemCategory?)GetById(id).data;

                        if (existingData != null)
                        {
                            MMedicalItemCategory medicalItemCat = new MMedicalItemCategory()
                            {
                                Id = (long)existingData.Id!,
                                Name = existingData.Name,
                                ModifiedBy = existingData.ModifiedBy,
                                ModifiedOn = existingData.ModifiedOn,
                                CreatedBy = existingData.CreatedBy,
                                CreatedOn = (DateTime)existingData.CreatedOn!,

                                IsDelete = true,

                                DeletedBy = userId,
                                DeletedOn = DateTime.Now
                            };

                            db.Update(medicalItemCat);
                            db.SaveChanges();

                            //Commit changes to database
                            dbTran.Commit();

                            //Update API Response
                            response.data = medicalItemCat;
                            response.message = $"Medical Item Category with ID={existingData.Id} has been successfully deleted";
                            response.statusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            response.message = $"Medical Item Category with id {id} cannot be found";
                            response.statusCode = HttpStatusCode.NotFound;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbTran.Rollback();

                        response.message = ex.Message;
                    }

                }
            }
            else
            {
                response.message = "Please input a valid Id and User Id";
                response.statusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }
    }
}
