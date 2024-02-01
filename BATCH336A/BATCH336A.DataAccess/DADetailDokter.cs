using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336A.DataAccess
{
    public class DADetailDokter
    {
        private VMResponse response = new VMResponse();
        private readonly BATCH336AContext db;
        public DADetailDokter(BATCH336AContext _db) {
            db = _db;
        }
        public VMResponse GetProfileById(int id)
        {
            try
            {
                if (id > 0)
                {
                    VMDetailDokter? data = (
                        from pd in db.VwProfileDoctor2s
                        where pd.DoctorId == id
                        orderby pd.Pengalaman descending
                        select new VMDetailDokter
                        {
                            DoctorName = pd.Fullname,
                            DoctorSpecialization = pd.Name,
                            Pengalaman = pd.Pengalaman,
                            Image = pd.ImagePath
                        }
                        ).FirstOrDefault();
                    response.data = data;
                    response.message = $" success fatched!";
                    response.statusCode = System.Net.HttpStatusCode.OK;
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
        public VMResponse GetEducationById(int id) {
            try {
                if (id > 0)
                {
                    List<VMDetailDokter>? data = (
                        from de in db.MDoctorEducations
                        join d in db.MDoctors
                        on de.DoctorId equals d.Id
                        where d.Id == id
                        select new VMDetailDokter
                        {
                            InstitutionName = de.InstitutionName,
                            Major = de.Major,
                            EndYear = de.EndYear,
                        }
                        ).ToList();
                    response.data = data;
                    response.message = (data.Count > 0) ? $"{data.Count} success fatched!" : "Category has no data!";
                    response.statusCode = (data.Count > 0) ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NoContent;
                }
                else
                {
                    response.message = $"Input ID dengan benar";
                    response.statusCode = System.Net.HttpStatusCode.BadRequest;
                }
            }
            catch(Exception ex) {
                response.statusCode = HttpStatusCode.NotFound;
                response.message = ex.Message;
            }
            return response;
        }
        public VMResponse GetMedisById(int id)
        {
            try
            {
                if (id > 0)
                {
                    List<VMDetailDokter>? data = (
                        from d in db.MDoctors
                        join dt in db.TDoctorTreatments
                        on d.Id equals dt.DoctorId
                        where d.Id == id
                        select new VMDetailDokter
                        {
                            TreatmentName = dt.Name
                        }
                        ).ToList();
                    response.data = data;
                    response.message = (data.Count > 0) ? $"{data.Count} success fatched!" : "Category has no data!";
                    response.statusCode = (data.Count > 0) ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NoContent;
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

        public VMResponse GetRiwayatById(int id)
        {
            try
            {
                if (id > 0)
                {
                    List<VMDetailDokter>? data = (
                        from dof in db.TDoctorOffices
                        join d in db.MDoctors
                        on dof.DoctorId equals d.Id
                        join mf in db.MMedicalFacilities
                        on dof.MedicalFacilityId equals mf.Id
                        join l in db.MLocations
                        on mf.LocationId equals l.Id
                        join ll in db.MLocationLevels
                        on l.LocationLevelId equals ll.Id
                        where d.Id == id && mf.Id != 5
                        select new VMDetailDokter
                        {
                         FacilityName=$"{mf.Name}, {ll.Name}",
                         StartYear = dof.StartDate,
                         EndIsYear = dof.EndDate,
                         
                         Specialization = dof.Specialization
                        }
                        ).ToList();
                    response.data = data;
                    response.message = (data.Count > 0) ? $"{data.Count} success fatched!" : "Category has no data!";
                    response.statusCode = (data.Count > 0) ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NoContent;
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

        public VMResponse GetJadwalByMfId(int id)
        {
            try
            {
                if (id > 0)
                {
                    List<VMDetailDokter>? data = (
                        from dof in db.TDoctorOffices
                        join d in db.MDoctors
                        on dof.DoctorId equals d.Id
                        join ds in db.TCurrentDoctorSpecializations
                        on d.Id equals ds.DoctorId
                        join mf in db.MMedicalFacilities
                        on dof.MedicalFacilityId equals mf.Id
                        join l in db.MLocations
                        on mf.LocationId equals l.Id
                        join ll in db.MLocationLevels
                        on l.LocationLevelId equals ll.Id
                        join dot in db.TDoctorOfficeTreatments
                        on dof.Id equals dot.DoctorOfficeId
                        join dotp in db.TDoctorOfficeTreatmentPrices
                        on dot.Id equals dotp.DoctorOfficeTreatmentId
                        join mfs in db.MMedicalFacilitySchedules
                        on mf.Id equals mfs.MedicalFacilityId
                        where dof.DoctorId == id && mf.Id != 5 && dof.IsDelete == false
                        select new VMDetailDokter
                        {
                            NameMF = mf.Name,
                            StartYear = dof.StartDate,
                            EndIsYear = dof.DeletedOn,
                            LocationName = $"{mf.FullAdress} {l.Name}, {ll.Name}",
                            Specialization = dof.Specialization,
                            Price = dotp.Price,
                            StartJam = mfs.TimeScheduleStart,
                            EndJam = mfs.TimeScheduleEnd,
                            Schedule = $"{mfs.TimeScheduleStart} - {mfs.TimeScheduleEnd}",
                            Day = mfs.Day,
                            IdMf = mf.Id
                        }
                        ).ToList();
                    response.data = data;
                    response.message = (data.Count > 0) ? $"{data.Count} success fatched!" : "Category has no data!";
                    response.statusCode = (data.Count > 0) ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NoContent;
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
        public VMResponse GetLokasiByDoctorId(int id)
        {
            try
            {
                if (id > 0)
                {
                    //List<VMDetailDokter>? data = (
                    var data = (
                        from mf in db.VwMedicalFacilities
                        where mf.DoctorId == id
                        select new VMDetailDokter
                        {
                            NameMF = mf.Name,
                            LocationName = mf.Alamatlengkap,
                            Specialization = mf.Specialization,
                            Price = mf.Price,
                            OnlinePrice = mf.Biasa,
                            IdMf = mf.MedicalFacilityId
                        }
                    ).ToList();
                    response.data = data;
                    response.message = (data.Count > 0) ? $"{data.Count} success fatched!" : "Category has no data!";
                    response.statusCode = (data.Count > 0) ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NoContent;
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
    }
}
