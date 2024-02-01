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
    public class DACariDokter
    {
        private VMResponse response = new VMResponse();
        private readonly BATCH336AContext db;

        public DACariDokter(BATCH336AContext _db)
        {
            db = _db;
        }
        public VMResponse GetAll() => Get("", "", "", "");

        public List<VMCariDokter> HitungPengalaman(List<VMCariDokter> list)
        {
            foreach(VMCariDokter doc in list)
            {
                List<int> tahunMasuk = new List<int>();
                List<int> tahunKeluar = new List<int>();

                foreach (VMLokasiCariDokter loc in doc.Locations)
                {
                    tahunMasuk.Add((int)loc.startYear);

                    if(loc.endYear != null)
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
                    else if(masuk == tahunKeluar[keluar] && tahunMasuk.Count -1 > keluar)
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
                    else if(masuk == tahunKeluar[keluar])
                    {
                        loop = false;
                    }
                }

                doc.pengalaman = hitung;
            }

            return list;
        }

        public VMResponse Get(string? location, string spec, string? name, string? treat)
        {
            try
            {
                List<VMCariDokter> data = (
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
                        && (b.Fullname.Contains(name ?? "")
                            && s.Name.Contains(spec))
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
                                && l.Name.Contains(location ?? "")
                            select new VMLokasiCariDokter
                            {
                                kategoriFaskesId = mf.MedicalFacilityCategoryId,
                                lamaBekerja = DateTime.Now > dof.StartDate ? (new DateTime(1, 1, 1) + (DateTime.Now - (DateTime)dof.StartDate!)).Year - 1 : 0,
                                startDate = dof.StartDate,
                                endDate = dof.EndDate,
                                startYear = dof.StartDate != null ? int.Parse(dof.StartDate.Value.ToString("yyyy")) : null,
                                endYear = dof.EndDate != null ? int.Parse(dof.EndDate.Value.ToString("yyyy")) : int.Parse(DateTime.Now.ToString("yyyy")),

                                namaRumahSakit = mf.Name,
                                namaLokasi = $"{l.Name}, {ll.Name}",
                                namaTreatment = (
                                    from dot in db.TDoctorOfficeTreatments
                                    join dt in db.TDoctorTreatments
                                        on dot.DoctorTreatmentId equals dt.Id
                                    where dot.DoctorOfficeId == dof.Id
                                        && dt.Name.Contains(treat ?? "")
                                        && dot.IsDelete == false
                                        && dt.IsDelete == false
                                    select new VMMTindakan
                                    {
                                        treatment = dt.Name
                                    }).ToList(),
                                jamOperasional = (
                                    from dos in db.TDoctorOfficeSchedules
                                    join mfs in db.MMedicalFacilitySchedules
                                        on dos.MedicalFacilityScheduleId equals mfs.Id
                                    where mfs.MedicalFacilityId == mf.Id && dos.DoctorId == d.Id
                                        && mfs.IsDelete == false && dos.IsDelete == false
                                    select new VMMMedicalFacilitySchedule
                                    {
                                        Day = mfs.Day,
                                        TimeScheduleStart = mfs.TimeScheduleStart,
                                        TimeScheduleEnd = mfs.TimeScheduleEnd,
                                        slot = dos.Slot

                                    }).ToList()

                            }).ToList()

                    }).ToList();

                List<VMCariDokter> data2 = HitungPengalaman(data);

                response.data = data2;
                response.message = (data2.Count > 0)
                    ? $"{data2.Count} Doctor's data successfully fetched"
                    : "Cari Dokter has no data";
                response.statusCode = (data2.Count > 0)
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

        public VMResponse GetJamOperasional()
        {
            try
            {

            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.statusCode = HttpStatusCode.NotFound;
            }
            return response;
        }

        public VMResponse Getloc()
        {
            try
            {
                List<VMLokasiCariDokter> data = (
                            from l in db.MLocations
                            where l.IsDelete == false
                            select new VMLokasiCariDokter
                            {
                                namaLokasi = l.Name
                            }).ToList();
                response.data = data;
                response.message = (data.Count > 0)
                    ? $"{data.Count} Location data successfully fetched"
                    : "Location has no data";
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

        public VMResponse GetTreatment()
        {
            try
            {
                List<VMMTindakan> data = (List<VMMTindakan>)(
                            from dt in db.TDoctorTreatments
                            where dt.IsDelete == false
                            select new VMMTindakan
                            {
                                treatment = dt.Name
                            }).Distinct().ToList();
                response.data = data;
                response.message = (data.Count > 0)
                    ? $"{data.Count} Treatment data successfully fetched"
                    : "Treatment has no data";
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
    }
    
}
