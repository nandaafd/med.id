
using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;
using System.Net;

namespace BATCH336A.DataAccess
{
    public class DAMenu
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336AContext db;
        public DAMenu(BATCH336AContext _db) { db = _db; }

        public VMResponse GetAll()
        {
            try
            {
                List<VMMMenu> data = (
                    from c in db.MMenus
                    where c.IsDelete == false
                    select new VMMMenu
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Url = c.Url,
                        ParentId = c.ParentId,
                        BigIcon = c.BigIcon,
                        SmallIcon = c.SmallIcon,
                        CreateBy = c.CreateBy,
                        CreateOn = (DateTime)c.CreateOn!,
                        ModifiedBy = c.ModifiedBy,
                        ModifiedOn = c.ModifiedOn,
                        DeletedBy = c.DeletedBy,
                        DeletedOn = c.DeletedOn,
                        IsDelete = c.IsDelete,
                        Roles = (
                            from mr in db.MMenuRoles
                            where mr.IsDelete == false && mr.MenuId == c.Id
                            select new VMMMenuRole
                            {
                                Id = mr.Id,
                                MenuId = mr.MenuId,
                                RoleId = mr.RoleId
                            }
                        ).ToList()
            }
                    
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} Menu data Successfully fetched!" : "Menu has no Data!";
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
                VMMMenu? data = (
                    from c in db.MMenus
                    where c.IsDelete == false && c.Id == id
                    select new VMMMenu
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Url = c.Url,
                        ParentId = c.ParentId,
                        BigIcon = c.BigIcon,
                        SmallIcon = c.SmallIcon,
                        CreateBy = c.CreateBy,
                        CreateOn = (DateTime)c.CreateOn!,
                        ModifiedBy = c.ModifiedBy,
                        ModifiedOn = c.ModifiedOn,
                        DeletedBy = c.DeletedBy,
                        DeletedOn = c.DeletedOn,
                        IsDelete = c.IsDelete
                    }
                ).FirstOrDefault();

                if (data != null)
                {
                    response.data = data;
                    response.message = "berhasil mendapatkan data dengan id "+data.Id;
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

        public VMResponse Create(VMMMenu data)
        {
            using (IDbContextTransaction dbTrans = db.Database.BeginTransaction())
            {
                try
                {
                    MMenu menu = new MMenu();

                    menu.Name = data.Name;
                    menu.Url = data.Url;
                    menu.ParentId = menu.Id;
                    menu.BigIcon = data.BigIcon;
                    menu.SmallIcon = data.SmallIcon;

                    menu.CreateBy = data.CreateBy;
                    menu.CreateOn = DateTime.Now;

                    menu.IsDelete = false;

                    db.Add(menu);
                    db.SaveChanges();

                    dbTrans.Commit();

                    response.data = menu;
                    response.statusCode = HttpStatusCode.Created;
                    response.message = "New Menu has been Successfully Created!";

                }
                catch (Exception e)
                {
                    //Undo changes from Transaction process
                    dbTrans.Rollback();

                    response.data = data;
                    response.message = "New Menu has been Failed Created! : " + e.Message;
                }
            }
            return response;
        }
        public VMResponse Update(VMMMenu data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMMMenu? existingData = (VMMMenu?)GetById(data.Id).data;

                    if (existingData.Id != null)
                    {
                        MMenu menu = new MMenu()
                        {
                            Id = existingData.Id,
                            Name = data.Name,
                            Url = data.Url,
                            ParentId = existingData.ParentId,
                            SmallIcon = data.SmallIcon,
                            BigIcon = data.BigIcon,
                            IsDelete = false,
                            CreateBy = existingData.CreateBy,
                            CreateOn = existingData.CreateOn,
                            ModifiedBy = data.ModifiedBy,
                            ModifiedOn = DateTime.Now,
                        };
                        db.Update(menu);
                        db.SaveChanges();
                        dbTran.Commit();

                        response.data = menu;
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
