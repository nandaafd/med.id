
using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using System.Net;
using System.Threading.Tasks.Dataflow;

namespace BATCH336A.DataAccess
{
    public class DAHistoryCustomer
    {
        private VMResponse response = new VMResponse();
        private readonly BATCH336AContext db;
        public DAHistoryCustomer(BATCH336AContext _db) { db = _db; }
        
        public VMResponse GetAll(long? id)
        {
            try
            {
                List<VMMHistoryCustomer> data = (
                    from a in db.TAppointments
                    join c in db.MCustomers on a.CustomerId equals c.Id
                    join b in db.MBiodata on c.BiodataId equals b.Id
                    join cm in db.MCustomerMembers on c.Id equals cm.CustomerId
                    join cr in db.MCustomerRelations on cm.CustomerRelationId equals cr.Id
                    join doo in db.TDoctorOffices on a.DoctorOfficeId equals doo.Id
                    join mf in db.MMedicalFacilities on doo.MedicalFacilityId equals mf.Id
                    join mfc in db.MMedicalFacilityCategories on mf.MedicalFacilityCategoryId equals mfc.Id
                    join ad in db.TAppointmentDones on a.Id equals ad.AppointmentId
                    join dot in db.TDoctorOfficeTreatments on a.DoctorOfficeTreatmentId equals dot.Id
                    join dt in db.TDoctorTreatments on dot.DoctorTreatmentId equals dt.Id
                    join dk in db.MDoctors on dt.DoctorId equals dk.Id
                    join cds in db.TCurrentDoctorSpecializations on dk.Id equals cds.DoctorId
                    join s in db.MSpecializations on cds.SpecializationId equals s.Id
                    join bd in db.MBiodata on dk.BiodataId equals bd.Id
                    

                    where a.IsDelete == false && ad.IsDelete == false && a.AppointmentDate < DateTime.Now &&
                    c.BiodataId == id || cm.ParentBiodataId == id
                    select new VMMHistoryCustomer
                    {
                        Id = a.Id,
                        AppointmentCustomerId = a.CustomerId,
                        AppointmentDate = a.AppointmentDate,
                        AppointmentCreateBy = a.CreatedBy,
                        AppointmentCreateOn = a.CreatedOn,
                        ParentBiodata = cm.ParentBiodataId,
                        BiodataId = c.BiodataId,

                        AppointmentDoctorOfficeId = a.DoctorOfficeId,
                        MedicalFacilityName = mf.Name,

                        AppointmentDoctorOfficeScheduleId = a.DoctorOfficeScheduleId,

                        AppointmentDoctorOfficeTreatmentId = a.DoctorOfficeTreatmentId,
                        TreatmentName = dt.Name,
                        DoctorName = bd.Fullname,
                        DoctorSpecialization = s.Name,


                        AppointmentCustomerName = b.Fullname,
                        CustomerDob = c.Dob,
                        CustomerGender = c.Gender,

                        AppointmentDoneId = ad.Id,
                        AppointmentId = ad.Id,
                        Diagnosis = ad.Diagnosis,

                        Prescriptions = (
                            from p in db.TPrescriptions
                            join m in db.MMedicalItems on p.MedicalItemId equals m.Id
                            join mu in db.MUsers on p.ModifiedBy equals mu.Id into userJoin
                            from mu in userJoin.DefaultIfEmpty()
                            join mb in db.MBiodata on mu.BiodataId equals mb.Id into biodataJoin
                            from mb in biodataJoin.DefaultIfEmpty()
                            where p.IsDelete == false && p.AppointmentId == a.Id
                            select new VMTPrescription
                            {
                                Id= p.Id,
                                AppointmentId = p.AppointmentId,
                                MeidicalItemName = m.Name,
                                MedicalItemId = p.MedicalItemId,
                                Dosage = p.Dosage,
                                Directions = p.Directions,
                                Time = p.Time,
                                Notes = p.Notes,
                                CreatedBy = p.CreatedBy,
                                CreatedOn = p.CreatedOn,
                                ModifiedBy = p.ModifiedBy,
                                ModifiedByName = mb.Fullname,
                                IsDelete = p.IsDelete,
                                PrintAttempt = p.PrintAttempt
                            }
                        ).ToList()
                    }
                 ).ToList();

                if (data != null)
                {
                    response.data = data;
                    response.statusCode = HttpStatusCode.OK;
                    response.message = "berhasil mengambil data appointment";
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.statusCode = System.Net.HttpStatusCode.NotFound;
            }
            return response;
        }
        public VMResponse GetByFilter(string filter)
        {
            try
            {
                List<VMMHistoryCustomer> data = (
                    from a in db.TAppointments
                    join c in db.MCustomers on a.CustomerId equals c.Id
                    join b in db.MBiodata on c.BiodataId equals b.Id
                    join cm in db.MCustomerMembers on c.Id equals cm.CustomerId
                    join cr in db.MCustomerRelations on cm.CustomerRelationId equals cr.Id
                    join doo in db.TDoctorOffices on a.DoctorOfficeId equals doo.Id
                    join mf in db.MMedicalFacilities on doo.MedicalFacilityId equals mf.Id
                    join mfc in db.MMedicalFacilityCategories on mf.MedicalFacilityCategoryId equals mfc.Id
                    join ad in db.TAppointmentDones on a.Id equals ad.AppointmentId
                    join dot in db.TDoctorOfficeTreatments on a.DoctorOfficeTreatmentId equals dot.Id
                    join dt in db.TDoctorTreatments on dot.DoctorTreatmentId equals dt.Id
                    join dk in db.MDoctors on dt.DoctorId equals dk.Id
                    join cds in db.TCurrentDoctorSpecializations on dk.Id equals cds.DoctorId
                    join s in db.MSpecializations on cds.SpecializationId equals s.Id
                    join bd in db.MBiodata on dk.BiodataId equals bd.Id

                    where a.IsDelete == false && ad.IsDelete == false && a.AppointmentDate < DateTime.Now
                    && b.Fullname.Contains(filter ?? "") || bd.Fullname.Contains(filter??"")
                    select new VMMHistoryCustomer
                    {
                        Id = a.Id,
                        AppointmentCustomerId = a.CustomerId,
                        AppointmentDate = a.AppointmentDate,
                        AppointmentCreateBy = a.CreatedBy,
                        AppointmentCreateOn = a.CreatedOn,
                        ParentBiodata = cm.ParentBiodataId,
                        BiodataId = c.BiodataId,


                        AppointmentDoctorOfficeId = a.DoctorOfficeId,
                        MedicalFacilityName = mf.Name,

                        AppointmentDoctorOfficeScheduleId = a.DoctorOfficeScheduleId,

                        AppointmentDoctorOfficeTreatmentId = a.DoctorOfficeTreatmentId,
                        TreatmentName = dt.Name,
                        DoctorName = bd.Fullname,
                        DoctorSpecialization = s.Name,


                        AppointmentCustomerName = b.Fullname,
                        CustomerDob = c.Dob,
                        CustomerGender = c.Gender,

                        AppointmentDoneId = ad.Id,
                        AppointmentId = ad.Id,
                        Diagnosis = ad.Diagnosis,

                        Prescriptions = (
                            from p in db.TPrescriptions
                            join m in db.MMedicalItems on p.MedicalItemId equals m.Id
                            join mu in db.MUsers on p.ModifiedBy equals mu.Id into userJoin
                            from mu in userJoin.DefaultIfEmpty()
                            join mb in db.MBiodata on mu.BiodataId equals mb.Id into biodataJoin
                            from mb in biodataJoin.DefaultIfEmpty()
                            where p.IsDelete == false && p.AppointmentId == a.Id
                            select new VMTPrescription
                            {
                                Id = p.Id,
                                AppointmentId = p.AppointmentId,
                                MeidicalItemName = m.Name,
                                MedicalItemId = p.MedicalItemId,
                                Dosage = p.Dosage,
                                Directions = p.Directions,
                                Time = p.Time,
                                Notes = p.Notes,
                                CreatedBy = p.CreatedBy,
                                CreatedOn = p.CreatedOn,
                                ModifiedBy = p.ModifiedBy,
                                ModifiedByName = mb.Fullname,
                                IsDelete = p.IsDelete,
                                PrintAttempt = p.PrintAttempt
                            }
                        ).ToList()
                    }
                 ).ToList();

                if (data != null)
                {
                    response.data = data;
                    response.statusCode = HttpStatusCode.OK;
                    response.message = "berhasil mengambil data appointment";
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.statusCode = System.Net.HttpStatusCode.NotFound;
            }
            return response;
        }
        public VMResponse GetById(long id)
        {
            try
            {
                VMMHistoryCustomer? data = (
                    from a in db.TAppointments
                    join c in db.MCustomers on a.CustomerId equals c.Id
                    join b in db.MBiodata on c.BiodataId equals b.Id
                    join cm in db.MCustomerMembers on c.Id equals cm.CustomerId
                    join cr in db.MCustomerRelations on cm.CustomerRelationId equals cr.Id
                    join doo in db.TDoctorOffices on a.DoctorOfficeId equals doo.Id
                    join mf in db.MMedicalFacilities on doo.MedicalFacilityId equals mf.Id
                    join mfc in db.MMedicalFacilityCategories on mf.MedicalFacilityCategoryId equals mfc.Id
                    join ad in db.TAppointmentDones on a.Id equals ad.AppointmentId
                    join dot in db.TDoctorOfficeTreatments on a.DoctorOfficeTreatmentId equals dot.Id
                    join dt in db.TDoctorTreatments on dot.DoctorTreatmentId equals dt.Id
                    join dk in db.MDoctors on dt.DoctorId equals dk.Id
                    join cds in db.TCurrentDoctorSpecializations on dk.Id equals cds.DoctorId
                    join s in db.MSpecializations on cds.SpecializationId equals s.Id
                    join bd in db.MBiodata on dk.BiodataId equals bd.Id

                    where a.IsDelete == false && ad.IsDelete == false && a.AppointmentDate < DateTime.Now && a.Id == id
                    select new VMMHistoryCustomer
                    {
                        Id = a.Id,
                        AppointmentCustomerId = a.CustomerId,
                        AppointmentDate = a.AppointmentDate,
                        AppointmentCreateBy = a.CreatedBy,
                        AppointmentCreateOn = a.CreatedOn,
                        ParentBiodata = cm.ParentBiodataId,
                        BiodataId = c.BiodataId,

                        AppointmentDoctorOfficeId = a.DoctorOfficeId,
                        MedicalFacilityName = mf.Name,

                        AppointmentDoctorOfficeScheduleId = a.DoctorOfficeScheduleId,

                        AppointmentDoctorOfficeTreatmentId = a.DoctorOfficeTreatmentId,
                        TreatmentName = dt.Name,
                        DoctorName = bd.Fullname,
                        DoctorSpecialization = s.Name,

                        AppointmentCustomerName = b.Fullname,
                        CustomerDob = c.Dob,
                        CustomerGender = c.Gender,

                        AppointmentDoneId = ad.Id,
                        AppointmentId = ad.Id,
                        Diagnosis = ad.Diagnosis,

                        Prescriptions = (
                            from p in db.TPrescriptions
                            join m in db.MMedicalItems on p.MedicalItemId equals m.Id
                            where p.IsDelete == false && p.AppointmentId == a.Id
                            select new VMTPrescription
                            {
                                Id = p.Id,
                                AppointmentId = p.AppointmentId,
                                MeidicalItemName = m.Name,
                                MedicalItemId = p.MedicalItemId,
                                Dosage = p.Dosage,
                                Directions = p.Directions,
                                Time = p.Time,
                                Notes = p.Notes,
                                CreatedBy = p.CreatedBy,
                                CreatedOn = p.CreatedOn,
                                PrintAttempt = p.PrintAttempt
                            }
                        ).ToList()
                    }
                 ).FirstOrDefault();

                if (data != null)
                {
                    response.data = data;
                    response.statusCode = HttpStatusCode.OK;
                    response.message = "berhasil mengambil data appointment by id";
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.statusCode = System.Net.HttpStatusCode.NotFound;
            }
            return response;
        }
        public VMResponse Update(VMTPrescription? data)
        {
            using(IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    if (data.AppointmentId != null)
                    {
                        VMTPrescription? existingData = (VMTPrescription?)GetPrescriptionById(data.Id).data;
                        if (existingData != null)
                        {
                            TPrescription prescription = new TPrescription();
                            prescription.Id = existingData.Id;
                            prescription.AppointmentId = existingData.AppointmentId;
                            prescription.MedicalItemId = existingData.MedicalItemId;
                            prescription.Dosage = existingData.Dosage;
                            prescription.Directions = existingData.Directions;
                            prescription.Time = existingData.Time;
                            prescription.Notes = existingData.Notes;
                            prescription.CreatedBy = existingData.CreatedBy;
                            prescription.CreatedOn = existingData.CreatedOn;
                            prescription.ModifiedBy = data.ModifiedBy;
                            //prescription.ModifiedBy = data.ModifiedBy;
                            prescription.ModifiedOn = DateTime.Now;
                            prescription.PrintAttempt = data.PrintAttempt;
                            prescription.IsDelete = false;

                            db.Update(prescription);
                            db.SaveChanges();
                        }
                    }
                    dbTran.Commit();
                    response.message = "success update prescription";
                    response.statusCode = HttpStatusCode.OK;
                }
                catch(Exception ex)
                {
                    dbTran.Rollback();
                    response.message = ex.Message;
                }
            }
            return response;
        }
        public VMResponse GetPrescriptionById(long? id)
        {
            try
            {
                VMTPrescription? data = (
                     from p in db.TPrescriptions
                     join m in db.MMedicalItems on p.MedicalItemId equals m.Id
                     where p.Id == id && p.IsDelete == false
                    select new VMTPrescription
                    {
                        Id = p.Id,
                        AppointmentId = p.AppointmentId,
                        MeidicalItemName = m.Name,
                        MedicalItemId = p.MedicalItemId,
                        Dosage = p.Dosage,
                        Directions = p.Directions,
                        Time = p.Time,
                        Notes = p.Notes,
                        CreatedBy = p.CreatedBy,
                        CreatedOn = p.CreatedOn
                    }
                ).FirstOrDefault();
                if (data != null)
                {
                    response.data = data;
                    response.statusCode = HttpStatusCode.OK;
                    response.message = "berhasil mengambil data prescription berdasarkan id";
                }
                else
                {
                    throw new Exception();
                }
            }
            catch(Exception ex)
            {
                response.message = ex.Message;
                response.statusCode = System.Net.HttpStatusCode.NotFound;
            }
            return response;
        }
        //public VMMUser? GetUserById(long id)
        //{
        //    VMMUser? data = new VMMUser();
        //    try
        //    {
        //        data = (
        //            from u in db.MUsers
        //            join b in db.MBiodata on u.BiodataId equals b.Id
        //            where u.BiodataId == id && u.IsDelete == false
        //            select new VMMUser
        //            {
        //                BiodataName = b.Fullname,
        //            }
        //            ).FirstOrDefault();
        //    }
        //    catch(System.Exception ex)
        //    {

        //    }
        //    return data;
        //}
    }
}
