using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;
using System.Net;


namespace BATCH336A.DataAccess
{
    public class DAProfile
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336AContext db;
        public DAProfile(BATCH336AContext _db) { db = _db; }

      
        public VMResponse GetById(int id)
        {
            try
            {
                if (id > 0)
                {
                    VMProfile? data =
                    (
                    from b in db.MBiodata
                    join u in db.MUsers on b.Id equals u.BiodataId
                    join c in db.MCustomers on u.BiodataId equals c.Id
                    where b.IsDelete == false && u.IsDelete == false && c.IsDelete == false && u.Id == id
                    select new VMProfile
                    {
                        Id = u.Id,
                        Fullname = b.Fullname,
                        Dob = c.Dob,
                        MobilePhone = b.MobilePhone,
                        BiodataId = u.BiodataId,
                        RoleId = u.RoleId,
                        Email = u.Email,
                        Password = u.Password,
                        LoginAttempt = u.LoginAttempt,
                        IsLocked = u.IsLocked,
                        LastLogin = u.LastLogin,
                        CreateBy = u.CreateBy,
                        CreateOn = (DateTime)u.CreateOn!,
                        ModifiedBy = u.ModifiedBy,
                        ModifiedOn = u.ModifiedOn,
                        DeletedBy = u.DeletedBy,
                        DeletedOn = u.DeletedOn,
                        IsDelete = u.IsDelete
                    }
                    ).FirstOrDefault();

                    //response.data = (data != null) ? data : null;
                    //response.message = (data != null) ? $"Category data Successfully fetched!" : $"id {id} Category has no Data!";
                    //response.statusCode = (data != null) ? HttpStatusCode.OK : HttpStatusCode.NoContent;

                    if (data != null)
                    {
                        response.data = data;
                        response.message = $"User data Successfully fetched!";
                        response.statusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = $"id {id} User has no Data!";
                        response.statusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = $"Please Input User ID First!";
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
                List<VMProfile> data = (
                    from b in db.MBiodata
                    join u in db.MUsers on b.Id equals u.BiodataId
                    join c in db.MCustomers on u.BiodataId equals c.Id
                    where b.IsDelete == false && u.IsDelete == false && c.IsDelete == false
                    select new VMProfile
                    {
                        Id = u.Id,
                        BiodataId = u.BiodataId,
                        Fullname = b.Fullname,
                        Dob = c.Dob,
                        MobilePhone = b.MobilePhone,
                        Email = u.Email,
                        Password = u.Password,
                        LoginAttempt = u.LoginAttempt,
                        IsLocked = u.IsLocked,
                        LastLogin = u.LastLogin,
                        CreateBy = u.CreateBy,
                        CreateOn = (DateTime)u.CreateOn!,
                        ModifiedBy = u.ModifiedBy,
                        ModifiedOn = u.ModifiedOn,
                        DeletedBy = u.DeletedBy,
                        DeletedOn = u.DeletedOn,
                        IsDelete = u.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} User data Successfully fetched!" : "User has no Data!";
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
                if (email != null)
                {
                    VMProfile? data =
                    (
                    from b in db.MBiodata
                    join u in db.MUsers on b.Id equals u.BiodataId
                    join c in db.MCustomers on u.BiodataId equals c.Id
                    where b.IsDelete == false && u.IsDelete == false && c.IsDelete == false && u.Email == email
                    select new VMProfile
                    {
                        Id = u.Id,
                        BiodataId = u.BiodataId,
                        Fullname = b.Fullname,
                        Dob = c.Dob,
                        MobilePhone = b.MobilePhone,
                        RoleId = u.RoleId,
                        Email = u.Email,
                        Password = u.Password,
                        LoginAttempt = u.LoginAttempt,
                        IsLocked = u.IsLocked,
                        LastLogin = u.LastLogin,
                        CreateBy = u.CreateBy,
                        CreateOn = (DateTime)u.CreateOn!,
                        ModifiedBy = u.ModifiedBy,
                        ModifiedOn = u.ModifiedOn,
                        DeletedBy = u.DeletedBy,
                        DeletedOn = u.DeletedOn,
                        IsDelete = u.IsDelete
                    }
                    ).FirstOrDefault();


                    if (data != null)
                    {
                        response.data = data;
                        response.message = $"User data Successfully fetched!";
                        response.statusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = $"email {email} User has no Data!";
                        response.statusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = $"Please Input User Email First!";
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
        public VMResponse Update(VMProfile data)
        {
            using (IDbContextTransaction dbTrans = db.Database.BeginTransaction())
            {
                try
                {

                    VMProfile? existingData = (VMProfile?)GetById((int)data.Id).data;
                   // List<VMProfile?> existingData = (List<VMProfile?>)GetById((int)data.Id).data;
                    if (existingData != null)
                    {
                        MUser user = new MUser();
                        MCustomer customer = new MCustomer();
                        MBiodatum biodata = new MBiodatum();

                        user.Id = existingData.Id;
                        user.BiodataId = existingData.BiodataId;
                        biodata.Fullname = existingData.Fullname;
                       
                        biodata.MobilePhone = biodata.MobilePhone;
                        customer.Dob = existingData.Dob;
                        user.RoleId = user.RoleId;
                        user.Email = existingData.Email;
                        user.Password = existingData.Password;
                        user.RoleId = existingData.RoleId;
                        user.CreateBy = existingData.CreateBy;
                        user.CreateOn = existingData.CreateOn;
                        user.ModifiedBy = existingData.Id;
                        user.ModifiedOn = DateTime.Now;

                        user.DeletedBy = data.DeletedBy != null ? data.DeletedBy : existingData.DeletedBy;
                        user.DeletedOn = data.DeletedOn != null ? user.ModifiedOn : existingData.DeletedOn;
                        user.IsLocked = data.IsLocked != null ? data.IsLocked : existingData.IsLocked;
                        user.IsDelete = data.IsDelete != null ? data.IsDelete : existingData.IsDelete;

                        user.LoginAttempt = data.LoginAttempt;
                        user.LastLogin = data.LastLogin ?? null;

                        biodata.DeletedBy = data.DeletedBy != null ? data.DeletedBy : existingData.DeletedBy;
                        biodata.DeletedOn = data.DeletedOn != null ? biodata.ModifiedOn : existingData.DeletedOn;
                        
                        biodata.IsDelete = data.IsDelete != null ? data.IsDelete : existingData.IsDelete;

                        customer.DeletedBy = data.DeletedBy != null ? data.DeletedBy : existingData.DeletedBy;
                        customer.DeletedOn = data.DeletedOn != null ? customer.ModifiedOn : existingData.DeletedOn;
                        
                        customer.IsDelete = data.IsDelete != null ? data.IsDelete : existingData.IsDelete;

                        


                        db.Update(user);
                        db.SaveChanges();
                        db.Update(biodata);
                        db.SaveChanges();
                        db.Update(customer);


                        db.SaveChanges();

                        //Save changes from Transaction to Database
                        dbTrans.Commit();

                        //Update API Response
                        response.data = data;
                        response.statusCode = HttpStatusCode.OK;
                        response.message = $"Update with Name {data.Id} has been Succesfully Updated!";

                    }
                    else
                    {
                        response.data = data;
                        response.message = $"Requested Data Can't be Updated!";
                        response.statusCode = HttpStatusCode.NotFound;
                    }
                }
                catch (Exception ex)
                {
                    dbTrans.Rollback();

                    response.data = data;
                    response.message = "Category has been Failed Updated! : " + ex.Message;
                }
            }

            return response;
        }
    }
}
