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
    public class DASpecialization
    {
        private VMResponse response = new VMResponse();
        private readonly BATCH336AContext db;

        public DASpecialization(BATCH336AContext _db)
        {
            db = _db;
        }

        public VMResponse GetByFilter() => GetByFilter("");

        public VMResponse GetByFilter(string name)
        {
            try
            {
                List<VMMSpecialization> data = (
                    from s in db.MSpecializations
                    where s.IsDelete == false
                        && s.Name.Contains(name ?? "")
                    select new VMMSpecialization
                    {
                        Id = s.Id,
                        Name = s.Name,
                        CreatedBy = s.CreatedBy,
                        CreatedOn = s.CreatedOn,
                        ModifiedBy = s.ModifiedBy,
                        ModifiedOn = s.ModifiedOn,
                        DeletedBy  = s.DeletedBy,
                        DeletedOn = s.DeletedOn,
                        IsDelete = s.IsDelete

                    }).ToList();
                response.data = data;
                response.message = (data.Count > 0)
                    ? $"{data.Count} Specialization data successfully fetched"
                    : "Specialization has no data";
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
                    VMMSpecialization? data = (
                        from  s in db.MSpecializations
                        where s.Id == id
                            && s.IsDelete == false
                        select new VMMSpecialization
                        {
                            Id = s.Id,
                            Name = s.Name,
                            CreatedBy = s.CreatedBy,
                            CreatedOn = s.CreatedOn,
                            ModifiedBy = s.ModifiedBy,
                            ModifiedOn = s.ModifiedOn,
                            DeletedBy = s.DeletedBy,
                            DeletedOn = s.DeletedOn,
                            IsDelete = s.IsDelete

                        }).FirstOrDefault();
                    
                    if(data != null)
                    {
                        response.data = data;
                        response.message = $"Specialization data with ID={id} is successfully fetched";
                        response.statusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = "Specialization data could not be found";
                        response.statusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = "Please input a valid Specialization Id";
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

        public VMMSpecialization? GetByName(string name)
        {
            VMMSpecialization? data = new VMMSpecialization();

            try
            {
                data = (
                    from s in db.MSpecializations
                    where s.Name == name
                        && s.IsDelete == false
                    select new VMMSpecialization
                    {
                        Id = s.Id,
                        Name = s.Name,
                        CreatedBy = s.CreatedBy,
                        CreatedOn = s.CreatedOn,
                        ModifiedBy = s.ModifiedBy,
                        ModifiedOn = s.ModifiedOn,
                        DeletedBy = s.DeletedBy,
                        DeletedOn = s.DeletedOn,
                        IsDelete = s.IsDelete

                    }).FirstOrDefault();
            }
            catch(Exception ex)
            {
                
            }

            return data;
        }

        public VMResponse CreateSpecialization(VMMSpecialization data)
        {
            string character = "!@#$%^&*()+=_{}[]|?><,.";

            foreach(char c in data.Name)
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
            VMMSpecialization? existingData = GetByName(data.Name);
            if (existingData != null)
            {
                response.message = "Nama spesialisasi sudah dibuat";
                response.statusCode = HttpStatusCode.BadRequest;
                return response;
            }
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    MSpecialization specialization = new MSpecialization();
                    specialization.Name = data.Name;
                    specialization.CreatedBy = data.CreatedBy;
                    specialization.IsDelete = false;
                    specialization.CreatedOn = DateTime.Now;

                    //Process to insert data into DB Table
                    db.Add(specialization);
                    db.SaveChanges();

                    //Commit changes to database
                    dbTran.Commit();

                    //Update API Response
                    response.data = specialization;
                    response.message = "New Specialization data has been successfully created";
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

        public VMResponse UpdateSpecialization(VMMSpecialization data)
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
            VMMSpecialization? existData = GetByName(data.Name);
            if (existData != null)
            {
                response.message = "Specialization name already exist";
                response.statusCode = HttpStatusCode.BadRequest;
                return response;
            }
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMMSpecialization? existingData = (VMMSpecialization?)GetById(data.Id ?? 0).data;

                    if(existingData != null)
                    {
                        MSpecialization specialization = new MSpecialization();
                        specialization.Id = existingData.Id ?? 0;
                        specialization.CreatedBy = existingData.CreatedBy;
                        specialization.CreatedOn = existingData.CreatedOn ?? DateTime.Now;
                        specialization.IsDelete = existingData.IsDelete;

                        specialization.Name = data.Name;
                        specialization.ModifiedBy = data.ModifiedBy;
                        specialization.ModifiedOn = DateTime.Now;

                        db.Update(specialization);
                        db.SaveChanges();

                        //Commit changes to database
                        dbTran.Commit();

                        //Update API Response
                        response.data = specialization;
                        response.message = $"Specialization with ID={existingData.Id} has been successfully updated";
                        response.statusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = "Requested Specialization data cannot be updated";
                        response.data = data;
                        response.statusCode = HttpStatusCode.NotFound;
                    }
                }
                catch(Exception ex)
                {
                    //undo changes 
                    dbTran.Rollback();

                    response.message = ex.Message;
                    response.data = data;
                }
            }
                return response;
        }

        public VMResponse DeleteSpecialization(long id, long userId)
        {
            if (userId != 0 && id != 0)
            {
                using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    try
                    {
                        VMMSpecialization? existingData = (VMMSpecialization?)GetById(id).data;

                        if (existingData != null)
                        {
                            MSpecialization specialization = new MSpecialization()
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

                            db.Update(specialization);
                            db.SaveChanges();

                            //Commit changes to database
                            dbTran.Commit();

                            //Update API Response
                            response.data = specialization;
                            response.message = $"Specialization with ID={existingData.Id} has been successfully deleted";
                            response.statusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            response.message = $"Specialization with id {id} cannot be found";
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
