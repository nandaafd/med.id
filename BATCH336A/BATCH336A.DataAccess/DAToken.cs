
using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;
using System.Net;

namespace BATCH336A.DataAccess
{
    public class DAToken
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336AContext db;
        public DAToken(BATCH336AContext _db) { db = _db; }

        public VMResponse GetAll()
        {
            try
            {
                List<VMTToken> data = (
                    from t in db.TTokens
                    where t.IsDelete == false
                    select new VMTToken
                    {
                        Id = t.Id,
                        Email = t.Email,
                        UserId = t.UserId,
                        Token = t.Token,
                        ExpiredOn = t.ExpiredOn,
                        IsExpired = t.IsExpired,
                        UsedFor = t.UsedFor,
                        CreatedBy = t.CreatedBy,
                        CreatedOn = t.CreatedOn,
                        ModifiedBy = t.ModifiedBy,
                        ModifiedOn = t.ModifiedOn,
                        DeletedBy = t.DeletedBy,
                        DeletedOn = t.DeletedOn,
                        IsDelete = t.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} Token data Successfully fetched!" : "Token has no Data!";
                response.statusCode = (data.Count > 0) ? HttpStatusCode.OK : HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.statusCode = HttpStatusCode.NotFound;
            }
            return response;
        }
        public VMResponse GetByEmail(string email)
        {
            try
            {
                List<VMTToken> data = (
                    from t in db.TTokens
                    where t.IsDelete == false && t.Email == email
                    orderby t.CreatedOn descending
                    select new VMTToken
                    {
                        Id = t.Id,
                        Email = t.Email,
                        UserId = t.UserId,
                        Token = t.Token,
                        ExpiredOn = t.ExpiredOn,
                        IsExpired = t.IsExpired,
                        UsedFor = t.UsedFor,
                        CreatedBy = t.CreatedBy,
                        CreatedOn = t.CreatedOn,
                        ModifiedBy = t.ModifiedBy,
                        ModifiedOn = t.ModifiedOn,
                        DeletedBy = t.DeletedBy,
                        DeletedOn = t.DeletedOn,
                        IsDelete = t.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} Token data Successfully fetched!" : "Token has no Data!";
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
                VMTToken? data = (
                    from t in db.TTokens
                    where t.IsDelete == false && t.Id == id
                    select new VMTToken
                    {
                        Id = t.Id,
                        Email = t.Email,
                        UserId = t.UserId,
                        Token = t.Token,
                        ExpiredOn = t.ExpiredOn,
                        IsExpired = t.IsExpired,
                        UsedFor = t.UsedFor,
                        CreatedBy = t.CreatedBy,
                        CreatedOn = t.CreatedOn,
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

        public VMResponse Create(VMTToken data)
        {
            using (IDbContextTransaction dbTrans = db.Database.BeginTransaction())
            {
                try
                {
                    TToken token = new TToken()
                    {
                        Email = data.Email,
                        UserId = data.UserId,
                        Token = data.Token,
                        ExpiredOn = data.ExpiredOn,
                        IsExpired = data.IsExpired,
                        UsedFor = data.UsedFor,
                        CreatedBy = data.CreatedBy,
                        CreatedOn = DateTime.Now,
                        IsDelete = false
                    };

                    db.Add(token);
                    db.SaveChanges();

                    dbTrans.Commit();

                    response.data = token;
                    response.statusCode = HttpStatusCode.Created;
                    response.message = "New Token has been Successfully Created!";

                }
                catch (Exception e)
                {
                    //Undo changes from Transaction process
                    dbTrans.Rollback();

                    response.data = data;
                    response.message = "New Token has been Failed Created! : " + e.Message;
                }
            }
            return response;
        }
        public VMResponse Update(VMTToken data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMTToken? existingData = (VMTToken?)GetById(data.Id).data;

                    if (existingData.Id != null)
                    {
                        TToken token = new TToken()
                        {
                            Id = existingData.Id,
                            Email = existingData.Email,
                            UserId = existingData.UserId,
                            Token = existingData.Token,
                            ExpiredOn = existingData.ExpiredOn,
                            IsExpired = true,
                            UsedFor = existingData.UsedFor,
                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = existingData.CreatedOn,
                            ModifiedBy = data.ModifiedBy,
                            ModifiedOn = DateTime.Now,
                            IsDelete = false
                        };
                        db.Update(token);
                        db.SaveChanges();
                        dbTran.Commit();

                        response.data = token;
                        response.message = $"data menu with id={data.Id} success update";
                        response.statusCode = System.Net.HttpStatusCode.OK;
                    }
                    else
                    {
                        response.data = data;
                        response.message = "requested product data cannot be found";
                        response.statusCode = System.Net.HttpStatusCode.NotFound;
                    }
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    response.data = data;
                    response.statusCode = HttpStatusCode.InternalServerError;
                    response.message = ex.Message;
                }
            }
            return response;
        }

        public VMResponse Delete(long id, long userId)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMMMenu? existingData = (VMMMenu?)GetById(id).data;

                    if (existingData.Id != null)
                    {
                        MMenu menu = new MMenu()
                        {
                            Id = existingData.Id,
                            Name = existingData.Name,
                            Url = existingData.Url,
                            ParentId = existingData.ParentId,
                            SmallIcon = existingData.SmallIcon,
                            BigIcon = existingData.BigIcon,
                            IsDelete = true,
                            CreateBy = existingData.CreateBy,
                            CreateOn = existingData.CreateOn,
                            ModifiedBy = existingData.ModifiedBy,
                            ModifiedOn = existingData.ModifiedOn,
                            DeletedBy = userId,
                            DeletedOn = DateTime.Now,
                        };
                        db.Update(menu);
                        db.SaveChanges();
                        dbTran.Commit();

                        response.data = menu;
                        response.message = $"data menu with id={id} success deleted";
                        response.statusCode = System.Net.HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = "requested product data cannot be found";
                        response.statusCode = System.Net.HttpStatusCode.NotFound;
                    }
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    response.message = ex.Message;
                    response.statusCode=System.Net.HttpStatusCode.InternalServerError;
                }
            }
            return response;
        }
    }
}
