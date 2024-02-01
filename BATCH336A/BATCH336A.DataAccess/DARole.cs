using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336A.DataAccess
{
    public class DARole
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336AContext db;
        public DARole(BATCH336AContext _db) { db = _db; }

        public VMResponse GetById(int id)
        {
            try
            {
                if (id > 0)
                {
                    VMMRole? data =
                    (
                    from r in db.MRoles
                    where r.IsDelete == false && r.Id == id
                    select new VMMRole
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Code = r.Code,
                        CreateBy = r.CreateBy,
                        CreateOn = (DateTime)r.CreateOn!,
                        ModifiedBy = r.ModifiedBy,
                        ModifiedOn = r.ModifiedOn,
                        DeletedBy = r.DeletedBy,
                        DeletedOn = r.DeletedOn,
                        IsDelete = r.IsDelete
                    }
                    ).FirstOrDefault();

                    //response.data = (data != null) ? data : null;
                    //response.message = (data != null) ? $"Category data Successfully fetched!" : $"id {id} Category has no Data!";
                    //response.statusCode = (data != null) ? HttpStatusCode.OK : HttpStatusCode.NoContent;

                    if (data != null)
                    {
                        response.data = data;
                        response.message = $"Role data Successfully fetched!";
                        response.statusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = $"id {id} Role has no Data!";
                        response.statusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = $"Please Input Role ID First!";
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

        public VMResponse GetAll()
        {
            try
            {
                List<VMMRole> data = (
                    from r in db.MRoles
                    where r.IsDelete == false
                    select new VMMRole
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Code = r.Code,
                        CreateBy = r.CreateBy,
                        CreateOn = (DateTime)r.CreateOn!,
                        ModifiedBy = r.ModifiedBy,
                        ModifiedOn = r.ModifiedOn,
                        DeletedBy = r.DeletedBy,
                        DeletedOn = r.DeletedOn,
                        IsDelete = r.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} Role data Successfully fetched!" : "Role has no Data!";
                response.statusCode = (data.Count > 0) ? HttpStatusCode.OK : HttpStatusCode.NoContent;
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
