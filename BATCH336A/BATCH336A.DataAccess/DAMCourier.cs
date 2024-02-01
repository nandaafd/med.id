using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using System.Threading.Tasks;
using BATCH336A.DataModel;
using BATCH336A.ViewModel;

namespace BATCH336A.DataAccess
{
    public class DAMCourier
    {
        private VMResponse response = new VMResponse();
        private readonly BATCH336AContext db;
        public DAMCourier(BATCH336AContext _db)
        {
            db = _db;
        }
        public VMResponse GetByFilter() => GetByFilter("");

        public VMResponse GetByFilter(string filter)
        {
            try
            {
                List<VMMCourier> data = (
                        from c in db.MCouriers
                        where c.IsDelete == false
                            && c.Name.Contains(filter ?? "")

                        select new VMMCourier
                        {
                            Id = c.Id,
                            Name = c.Name,
                            IsDelete = c.IsDelete,
                            CreatedBy = c.CreatedBy,
                            CreatedOn = c.CreatedOn,

                            ModifiedBy = c.ModifiedBy,
                            ModifiedOn = c.ModifiedOn,
                            DeletedBy = c.DeletedBy,
                            DeletedOn = c.DeletedOn

                        }
                    ).ToList();

                response.data = data;
                response.message = (data.Count > 0)
                    ? $"{data.Count} Courier data successfully fetched"
                    : "Courier has no data";
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
                    VMMCourier? data = (
                            from c in db.MCouriers
                            where c.IsDelete == false
                                && c.Id == id
                            select new VMMCourier
                            {
                                Id = c.Id,
                                Name = c.Name,
                                IsDelete = c.IsDelete,

                                CreatedBy= c.CreatedBy,
                                CreatedOn = c.CreatedOn,
                                ModifiedBy = c.ModifiedBy,
                                ModifiedOn = c.ModifiedOn
                            }
                        ).FirstOrDefault();

                    if (data != null)
                    {
                        response.data = data;
                        response.message = $"Courier data successfully fetched";
                        response.statusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = "Courier data could not be found";
                        response.statusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = "Please input a valid Courier Id";
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

        public VMResponse Create(VMMCourier data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    if (db.MCouriers.Any(c => c.Name == data.Name)) {
                       
                        response.message = $"Courier with Name={data.Name} already exists in the database";
                        response.data = data;
                        response.statusCode = HttpStatusCode.Conflict; // HTTP 409 Conflict 
                    }
                    else
                    {
                        MCourier courier = new MCourier();
                        courier.Name = data.Name;

                        courier.IsDelete = false;
                        courier.CreatedBy = data.CreatedBy;
                        courier.CreatedOn = DateTime.Now;
                        db.Add(courier);
                        db.SaveChanges();

                        dbTran.Commit();
                        response.data = courier;
                        response.message = "New Courier Data has been Succesfully Created";
                        response.statusCode = HttpStatusCode.Created;
                    }
                        

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

        public VMResponse Update(VMMCourier data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMMCourier? existingData = (VMMCourier?)GetById(data.Id).data;

                    if (existingData != null)
                    {
                        MCourier courier = new MCourier()
                        {
                            Id = existingData.Id,
                            Name = data.Name,

                            IsDelete = false,

                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = existingData.CreatedOn,

                            ModifiedBy = data.ModifiedBy,
                            ModifiedOn = DateTime.Now
                        };

                        //Process to insert data into DB Table
                        db.Update(courier);
                        db.SaveChanges();

                        //Commit changes to database
                        dbTran.Commit();

                        //Update API Response
                        response.data = courier;
                        response.message = $"Courier with Name={existingData.Name} has been successfully updated";
                        response.statusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = "Requested Courier data cannot be updated";
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
        public VMResponse Delete(int id, int DeletedBy)
        {
            if (id != 0 && DeletedBy != 0)
            {
                using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    try
                    {
                        VMMCourier? existingData = (VMMCourier?)GetById(id).data;
                        if (existingData != null)
                        {
                            MCourier courier = new MCourier()
                            {
                                Id = existingData.Id,
                                Name = existingData.Name,
                                CreatedBy = existingData.CreatedBy,
                                CreatedOn = existingData.CreatedOn,

                                //mark data as deleted
                                IsDelete = true,

                                DeletedBy = DeletedBy,
                                DeletedOn = DateTime.Now
                            };

                            //Process to insert data into DB Table
                            db.Update(courier);
                            db.SaveChanges();

                            //Commit changes to database
                            dbTran.Commit();

                            //Update API Response
                            response.data = courier;
                            response.message = $"Courier with Name={existingData.Name} has been successfully deleted";
                            response.statusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            response.message = $"Courier with id {id} cannot be found";
                            response.statusCode = HttpStatusCode.NotFound;
                        }
                    }
                    catch (Exception ex)
                    {
                        //undo changes 
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
