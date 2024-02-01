using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336A.DataAccess
{
    public class DAAppointment
    {
        private VMResponse response = new VMResponse();
        private readonly BATCH336AContext db;

        public DAAppointment(BATCH336AContext _db)
        {
            db = _db;
        }

        //public List<VMTAppointment>? Get(long schedId, DateTime appDate)
        //{
        //    List<VMTAppointment>? data = new List<VMTAppointment>();

        //    try
        //    {
        //        data = (
        //            from a in db.TAppointments
        //            where a.DoctorOfficeScheduleId == schedId
        //                && a.AppointmentDate == appDate
        //                && a.IsDelete == false
        //            select new VMTAppointment
        //            {
        //                Id = a.Id,
        //                AppointmentDate = a.AppointmentDate,
        //                DoctorOfficeId = a.DoctorOfficeId,
        //                DoctorOfficeScheduleId = a.DoctorOfficeScheduleId,
        //                DoctorOfficeTreatmentId = a.DoctorOfficeTreatmentId

        //            }).ToList();

        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return data;
        //}

        public VMResponse? Get(long schedId, string appDate)
        {
            List<VMTAppointment>? data = new List<VMTAppointment>();

            try
            {
                data = (
                    from a in db.TAppointments
                    where a.DoctorOfficeScheduleId == schedId
                        && a.AppointmentDate == DateTime.ParseExact(appDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                        && a.IsDelete == false
                    select new VMTAppointment
                    {
                        Id = a.Id,
                        CustomerId = a.CustomerId,
                        AppointmentDate = a.AppointmentDate,
                        DoctorOfficeId = a.DoctorOfficeId,
                        DoctorOfficeScheduleId = a.DoctorOfficeScheduleId,
                        DoctorOfficeTreatmentId = a.DoctorOfficeTreatmentId

                    }).ToList();

                response.data = data;
                response.message = (data.Count != 0)
                    ? $"Data is successfully fetched"
                    : "Appointment has no data";
                response.statusCode = (data.Count != 0)
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

        public VMResponse? GetSlot(long id)
        {
            VMTDoctorOfficeSchedule? data = null;
            try
            {
                data = (
                    from dos in db.TDoctorOfficeSchedules
                    where dos.Id == id
                    select new VMTDoctorOfficeSchedule
                    {
                        Id = dos.Id,
                        DoctorId = dos.DoctorId,
                        MedicalFacilityScheduleId = dos.MedicalFacilityScheduleId,
                        Slot = dos.Slot

                    }).FirstOrDefault();

                response.data = data;
                response.message = (data != null)
                    ? $"Data is successfully fetched"
                    : "Appointment has no data";
                response.statusCode = (data != null)
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

        public VMResponse CreateAppointment(VMTAppointment data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    TAppointment appointment = new TAppointment()
                    {
                        CustomerId = data.CustomerId,
                        DoctorOfficeId = data.DoctorOfficeId,
                        DoctorOfficeScheduleId = data.DoctorOfficeScheduleId,
                        DoctorOfficeTreatmentId = data.DoctorOfficeTreatmentId,
                        AppointmentDate = data.AppointmentDate,

                        IsDelete = false,
                        CreatedBy = 2,
                        CreatedOn = DateTime.Now
                    };

                    //Process to insert data into DB Table
                    db.Add(appointment);
                    db.SaveChanges();

                    //Commit changes to database
                    dbTran.Commit();

                    //Update API Response
                    response.data = appointment;
                    response.message = "New Appointment data has been successfully created";
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
    }
}
