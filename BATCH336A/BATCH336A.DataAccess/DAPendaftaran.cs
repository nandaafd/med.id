using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;


namespace BATCH336A.DataAccess
{
    public class DAPendaftaran
    {
        private VMResponse response = new VMResponse();
        private readonly BATCH336AContext db;

        public DAPendaftaran(BATCH336AContext _db)
        {
            db = _db;
        }

        public VMResponse CreatePendaftaran(VMMPendaftaran data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    if (data != null)
                    {
                        MBiodatum biodata = new MBiodatum();
                        MUser user = new MUser();

                        biodata.Fullname = data.Biodatum.Fullname;
                        biodata.MobilePhone = data.Biodatum.MobilePhone;

                        biodata.CreateBy = data.Biodatum.CreateBy;
                        biodata.CreateOn = DateTime.Now;
                        biodata.IsDelete = false;
                        db.Add(biodata);
                        db.SaveChanges();

                        biodata.CreateBy = biodata.Id;
                        db.SaveChanges();

                        user.BiodataId = biodata.Id;
                        user.Email = data.UserData.Email;
                        user.Password = data.UserData.Password;
                        user.RoleId = (data.UserData.RoleId == 0) ? null : data.UserData.RoleId;

                        user.CreateBy = biodata.Id;
                        user.CreateOn = biodata.CreateOn;
                        user.IsDelete = false;

                        db.Add(user);
                        db.SaveChanges();

                        if (data.UserData.RoleId == 1) //admin
                        {
                            MAdmin admin = new MAdmin()
                            {
                                BiodataId = biodata.Id,
                                Code = $"ADM-{biodata.Id}",
                                CreatedBy = user.Id,
                                CreatedOn = DateTime.Now,
                                IsDelete = false
                            };
                            db.Add(admin);
                            db.SaveChanges();
                        }
                        else if (data.UserData.RoleId == 2) //pasien
                        {
                            MCustomer customer = new MCustomer()
                            {
                                BiodataId = biodata.Id,
                                CreateBy = user.Id,
                                CreateOn = DateTime.Now,
                                IsDelete = false
                            };
                            db.Add(customer);
                            db.SaveChanges();
                        }
                        else if (data.UserData.RoleId == 3) //faskes
                        {
                            MMedicalFacility facility = new MMedicalFacility()
                            {
                                Name = biodata.Fullname,
                                Email = data.UserData.Email,
                                Phone = biodata.MobilePhone,
                                CreatedBy = user.Id,
                                CreatedOn = DateTime.Now,
                            };
                            db.Add(facility);
                            db.SaveChanges();
                        }
                        else if (data.UserData.RoleId == 4) //dokter
                        {
                            MDoctor doctor = new MDoctor()
                            {
                                BiodataId = biodata.Id,
                                CreateBy = user.Id,
                                CreateOn = DateTime.Now,
                                IsDelete = false
                            };
                            db.Add(doctor);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        throw new Exception("Gagal mendaftarkan akun");
                    }
                    dbTran.Commit();
                    response.statusCode = System.Net.HttpStatusCode.Created;
                    response.data = data;
                    response.message = "anda berhasil mendaftarkan, silahkan masuk!";

                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    response.message = ex.Message;
                    response.statusCode = System.Net.HttpStatusCode.BadRequest;
                }
            }

            return response;
        }
        public VMResponse GetByEmail(string? email)
        {
            try
            {
                VMMUser? data = (
                    from u in db.MUsers
                    where u.Email == email
                    && u.IsDelete == false
                    select new VMMUser
                    {
                        Id = u.Id,
                        BiodataId = u.BiodataId,
                        Email = u.Email,
                        Password = u.Password,
                        RoleId = u.RoleId
                    }
                ).FirstOrDefault();
                if (data != null)
                {
                    response.data = data;
                    response.message = "Email sudah terdaftar, login atau mendaftar dengan email lain untuk masuk.";
                    response.statusCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    response.message = "Email belum terdaftar, silahkan mendaftar.";
                    response.statusCode = System.Net.HttpStatusCode.NoContent;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.statusCode = System.Net.HttpStatusCode.NotFound;
            }
            return response;
        }
    }
}
