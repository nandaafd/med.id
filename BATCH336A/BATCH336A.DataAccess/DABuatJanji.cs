using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BATCH336A.DataAccess
{
    public class DABuatJanji
    {
        private VMResponse response = new VMResponse();
        private readonly BATCH336AContext db;
        private readonly DAAppointment appointment;

        public DABuatJanji(BATCH336AContext _db)
        {
            db = _db;
        }

        public VMResponse GetCust(int id)
        {
            try
            {
                VMMCustomer2? data = (
                    from cus in db.MCustomers
                    join b in db.MBiodata
                        on cus.BiodataId equals b.Id
                    where cus.IsDelete == false && b.Id == id
                    select new VMMCustomer2
                    {
                        Id = cus.Id,
                        BiodataId = b.Id,
                        Name = b.Fullname,
                        Dob = cus.Dob,
                        Gender = cus.Gender,
                        Height = cus.Height,
                        Weight = cus.Weight,
                        Members = (
                            from cm in db.MCustomerMembers
                            join cr in db.MCustomerRelations
                                on cm.CustomerRelationId equals cr.Id
                            join cus in db.MCustomers
                                on cm.CustomerId equals cus.Id
                            join b in db.MBiodata
                                on cus.BiodataId equals b.Id
                            where cm.ParentBiodataId == id
                            select new VMMCustomerMember
                            {
                                Id = cm.Id,
                                ParentBiodataId = cm.ParentBiodataId,
                                CustomerId = cm.CustomerId,
                                CustomerName = b.Fullname,
                                CustomerRelationId = cr.Id,
                                CustomerRelationName = cr.Name
                            }).ToList()

                    }).FirstOrDefault();

                response.data = data;
                response.message = (data != null)
                    ? $"Customer's data with biodata id = {id} is successfully fetched"
                    : "Cari Dokter has no data";
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

        public VMCariDokter HitungPengalaman(VMCariDokter doc)
        {
            List<int> tahunMasuk = new List<int>();
            List<int> tahunKeluar = new List<int>();

            foreach (VMLokasiCariDokter loc in doc.Locations)
            {
                tahunMasuk.Add((int)loc.startYear);

                if (loc.endYear != null)
                {
                    tahunKeluar.Add((int)loc.endYear);
                }

            }

            if (tahunMasuk.Count > tahunKeluar.Count)
            {
                tahunKeluar.Add(int.Parse(DateTime.Now.ToString("yyyy")));
            }

            int hitung = 0;
            bool loop = true;

            int masuk = tahunMasuk.Min();
            int keluar = 0;
            tahunMasuk.Sort();
            tahunKeluar.Sort();

            while (loop)
            {
                if (masuk < tahunKeluar[keluar])
                {
                    hitung++;
                    masuk++;
                }
                else if (masuk == tahunKeluar[keluar] && tahunMasuk.Count - 1 > keluar)
                {
                    if (tahunKeluar[keluar] < tahunMasuk[keluar + 1])
                    {
                        masuk = tahunMasuk[keluar + 1];
                        keluar++;
                    }
                    else
                    {
                        keluar++;
                    }

                }
                else if (masuk == tahunKeluar[keluar])
                {
                    loop = false;
                }
            }

            doc.pengalaman = hitung;

            return doc;
        }

        public VMResponse GetDoc(int id)
        {
            try
            {
                VMCariDokter? data = (
                    from b in db.MBiodata
                    join d in db.MDoctors
                        on b.Id equals d.BiodataId
                    join cs in db.TCurrentDoctorSpecializations
                        on d.Id equals cs.DoctorId
                    join s in db.MSpecializations
                        on cs.SpecializationId equals s.Id
                    //join dt in db.
                    where b.IsDelete == false
                        && d.IsDelete == false
                        && cs.IsDelete == false
                        && s.IsDelete == false
                        && d.Id == id
                    select new VMCariDokter
                    {
                        id = d.Id,
                        doctorName = b.Fullname,
                        specialization = s.Name,
                        ImagePath = b.ImagePath,
                        pengalaman = null,

                        Locations = (
                            from dof in db.TDoctorOffices
                            join mf in db.MMedicalFacilities
                                on dof.MedicalFacilityId equals mf.Id
                            join l in db.MLocations
                                on mf.LocationId equals l.Id
                            join ll in db.MLocationLevels
                                on l.LocationLevelId equals ll.Id
                            where dof.DoctorId == d.Id
                            select new VMLokasiCariDokter
                            {
                                medicalFacilityId = mf.Id,
                                doctorOfficeId = dof.Id,
                                kategoriFaskesId = mf.MedicalFacilityCategoryId,
                                lamaBekerja = DateTime.Now > dof.StartDate ? (new DateTime(1, 1, 1) + (DateTime.Now - (DateTime)dof.StartDate!)).Year - 1 : 0,
                                startDate = dof.StartDate,
                                endDate = dof.EndDate,
                                startYear = dof.StartDate != null ? int.Parse(dof.StartDate.Value.ToString("yyyy")) : null,
                                endYear = dof.EndDate != null ? int.Parse(dof.EndDate.Value.ToString("yyyy")) : int.Parse(DateTime.Now.ToString("yyyy")),
                                namaRumahSakit = mf.Name,
                                namaLokasi = $"{l.Name}, {ll.Name}",
                                //lamaBekerja = (DateTime.Now - (DateTime)dof.StartDate!).ToString("d"),
                                namaTreatment = (
                                    from dot in db.TDoctorOfficeTreatments
                                    join dt in db.TDoctorTreatments
                                        on dot.DoctorTreatmentId equals dt.Id
                                    where dot.DoctorOfficeId == dof.Id
                                    select new VMMTindakan
                                    {
                                        medicalFacilityId = mf.Id,
                                        doctorOfficeId = dof.Id,
                                        doctorOfficeTreatmentId = dot.Id,
                                        treatment = dt.Name
                                    }).ToList(),
                                jamOperasional = (
                                    from dos in db.TDoctorOfficeSchedules
                                    join mfs in db.MMedicalFacilitySchedules
                                        on dos.MedicalFacilityScheduleId equals mfs.Id
                                    where mfs.MedicalFacilityId == mf.Id && dos.DoctorId == d.Id
                                    select new VMMMedicalFacilitySchedule
                                    {
                                        medicalFacilityId = mf.Id,
                                        doctorOfficeId = dof.Id,
                                        doctorOfficeScheduleId = dos.Id,
                                        Day = mfs.Day,
                                        TimeScheduleStart = mfs.TimeScheduleStart,
                                        TimeScheduleEnd = mfs.TimeScheduleEnd,
                                        slot = dos.Slot

                                    }).ToList()

                            }).ToList()

                    }).FirstOrDefault();

                VMCariDokter data2 = HitungPengalaman(data);

                response.data = data2;
                response.message = (data2 != null)
                    ? $"Doctor's data with doctor id = {id} is successfully fetched"
                    : "Cari Dokter has no data";
                response.statusCode = (data2 != null)
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

        public VMResponse GetJadwal(long offId)
        {
            try
            {
                List<VMMMedicalFacilitySchedule>? jadwal = (
                    from dos in db.TDoctorOfficeSchedules
                    join mfs in db.MMedicalFacilitySchedules
                        on dos.MedicalFacilityScheduleId equals mfs.Id
                    join mf in db.MMedicalFacilities
                        on mfs.MedicalFacilityId equals mf.Id
                    join dof in db.TDoctorOffices
                        on dos.DoctorId equals dof.DoctorId
                    where
                        mf.Id == dof.MedicalFacilityId && dof.Id == offId && dos.Slot > 0
                    select new VMMMedicalFacilitySchedule
                    {
                        doctorOfficeId = dof.Id,
                        medicalFacilityId = dof.MedicalFacilityId,
                        doctorOfficeScheduleId = dos.Id,
                        Day = mfs.Day,
                        TimeScheduleStart = mfs.TimeScheduleStart,
                        TimeScheduleEnd = mfs.TimeScheduleEnd,
                        slot = dos.Slot

                    }).ToList();

                response.data = jadwal;
                response.message = (jadwal != null)
                    ? $"Data is successfully fetched"
                    : "Jadwal Dokter has no data";
                response.statusCode = (jadwal != null)
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

        public VMResponse GetTreatment(long offId)
        {
            try
            {
                List<VMMTindakan> data = (
                    from dot in db.TDoctorOfficeTreatments
                    join dt in db.TDoctorTreatments
                        on dot.DoctorTreatmentId equals dt.Id
                    join dof in db.TDoctorOffices
                        on dot.DoctorOfficeId equals dof.Id
                    where dof.Id == offId
                    select new VMMTindakan
                    {
                        doctorOfficeTreatmentId = dot.Id,
                        treatment = dt.Name
                    }).ToList();

                response.data = data;
                response.message = (data != null)
                    ? $"Data is successfully fetched"
                    : "Treatment Dokter has no data";
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
    }
}
