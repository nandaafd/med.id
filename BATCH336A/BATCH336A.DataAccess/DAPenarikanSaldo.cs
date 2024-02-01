using BATCH336A.DataModel;
using BATCH336A.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336A.DataAccess
{
    public class DAPenarikanSaldo
    {
        private VMResponse response = new VMResponse();
        private readonly BATCH336AContext db;
        private int cekPass = 3;
        public DAPenarikanSaldo(BATCH336AContext _db)
        {
            db = _db;
        }
        public VMResponse GetById(long? id)
        {
            try
            {
                if (id > 0)
                {
                    VMPenarikanSaldo? data = (
                        from cw in db.TCustomerWallets
                        join c in db.MCustomers
                        on cw.CustomerId equals c.Id
                        where cw.CustomerId == id
                        select new VMPenarikanSaldo
                        {
                            CustWalletId = c.Id,
                            CustId = cw.CustomerId,
                            Saldo = cw.Balance,
                            Pin = cw.Pin,
                            CreatedBy = cw.CreatedBy,
                            CreatedOn = cw.CreatedOn
                        }
                        ).FirstOrDefault();

                    if (data != null)
                    {
                        response.data = data;
                        response.message = $"Customer dengan id = {id} berhasil ditemukan ";
                        response.statusCode = System.Net.HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = $"Customer dengan id = {id} tidak ditemukan ";
                        response.statusCode = System.Net.HttpStatusCode.NoContent;
                    }
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
        public VMResponse GetDefNomId()
        {
            try
            {
                List<VMPenarikanSaldo?> defNom = (
                    from dn in db.MWalletDefaultNominals
                    select new VMPenarikanSaldo
                    {
                        DefaultNominal = dn.Nominal,
                        NomDefId = dn.Id
                    }
                ).ToList();

                if (defNom != null)
                {
                    response.data = defNom;
                    response.message = $"Default Nominal berhasil ditemukan ";
                    response.statusCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    response.message = $"Default Nominal tidak ditemukan ";
                    response.statusCode = System.Net.HttpStatusCode.NoContent;
                }
            }
            catch (Exception ex)
            {
                response.statusCode = HttpStatusCode.NotFound;
                response.message = ex.Message;
            }
            return response;
        }
        public VMResponse GetCustNomId(long? id)
        {
            try
            {
                if (id > 0)
                {
                    List<VMPenarikanSaldo?> custNom = (
                        from cn in db.TCustomerCustomNominals
                        where cn.CustomerId == id
                        orderby cn.Nominal
                        select new VMPenarikanSaldo
                        {
                            NominalCustom = cn.Nominal,
                            NomCustId = cn.Id
                        }
                    ).ToList();

                    if (custNom != null)
                    {
                        response.data = custNom;
                        response.message = $"Customer dengan id = {id} berhasil ditemukan ";
                        response.statusCode = System.Net.HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = $"Customer dengan id = {id} tidak ditemukan ";
                        response.statusCode = System.Net.HttpStatusCode.NoContent;
                    }
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

        public VMResponse CekPin(VMPenarikanSaldo data)
        {
            using (IDbContextTransaction dbTrans = db.Database.BeginTransaction())
            {
                try
                {
                    VMPenarikanSaldo? existingdata = (VMPenarikanSaldo?)GetById(data.CustId).data;
                    if (data.Pin != existingdata.Pin) {
                        cekPass--;
                        if (cekPass == 0)
                        {
                            response.message = "Akun Terblokir";
                            response.statusCode = HttpStatusCode.BadRequest;
                            cekPass = 3;
                            return response;
                        }
                        else {
                            response.message = $"Pin salah, {cekPass} kali kesempatan lagi";
                            response.statusCode = HttpStatusCode.BadRequest;
                            return response;
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    response.data = data;
                    response.message = ex.Message;
                }
            }
            return response;
        }
        public VMResponse Transaksi(VMPenarikanSaldo data)
        {
            using (IDbContextTransaction dbTrans = db.Database.BeginTransaction())
            {
                TCustomerWallet? cw;
                Random numbRandom = new Random();
                string otp = numbRandom.Next(100000, 1000000).ToString("D6");
                int newOtp = int.Parse(otp);
                try
                {
                    VMPenarikanSaldo? existingdata = (VMPenarikanSaldo?)GetById(data.CustId).data;
                    if (data.Transaksi > existingdata.Saldo)
                    {
                        response.message = "Saldo tidak mencukupi";
                        response.statusCode = HttpStatusCode.BadRequest;
                        return response;
                    }

                    
                    VMPenarikanSaldo? nominalDef = (
                        from nd in db.MWalletDefaultNominals
                        where nd.Nominal == data.Transaksi
                        select new VMPenarikanSaldo
                        {
                            NomDefId = nd.Id
                        }
                        ).FirstOrDefault();

                    MWalletDefaultNominal existingNominal = findNominalCus(data.Transaksi);

                    if (existingNominal == null)
                    {
                        //create custom nominal
                        TCustomerCustomNominal cnn = new TCustomerCustomNominal();
                        cnn.CustomerId = existingdata.CustId;
                        cnn.Nominal = data.Transaksi;
                        cnn.CreatedBy = 1;
                        cnn.CreatedOn = DateTime.Now;
                        cnn.IsDelete = false;

                        db.Add(cnn); // untuk insert data
                        db.SaveChanges(); //untuk jalanin query
                    }

                    //create customer withdraw atau transaksi
                    TCustomerWalletWithdraw cww = new TCustomerWalletWithdraw();
                    cww.CustomerId = existingdata.CustId;
                    cww.WalletDefaultNominalId = nominalDef?.NomDefId;
                    cww.BankName = "ShopeePay";
                    cww.Otp = newOtp;
                    cww.Amount = data.Transaksi;
                    cww.CreatedBy = 1;
                    cww.CreatedOn = DateTime.Now;
                    cww.IsDelete = false;

                    db.Add(cww); // untuk insert data
                    db.SaveChanges(); //untuk jalanin query

                    //update saldo
                    cw = db.TCustomerWallets.Where(x => x.CustomerId
                    == existingdata.CustId).FirstOrDefault();
                    cw.Balance = existingdata.Saldo - cww.Amount;
                    cw.ModifiedBy = 1;
                    cw.ModifiedOn = DateTime.Now;
                    cw.IsDelete = false;

                    db.Update(cw);
                    db.SaveChanges();

                    dbTrans.Commit(); // save data base changes

                    VMPenarikanSaldo dataResponse = new VMPenarikanSaldo()
                    {
                        CustId = cww.CustomerId,
                        Amount = cww.Amount,
                        Otp = newOtp,
                        Saldo = cw.Balance,
                        Transaksi = data.Transaksi

                    };

                    response.data = dataResponse;
                    response.message = "Transaksi has been add to table";
                    response.statusCode = HttpStatusCode.Created; //201
                }
                catch (Exception ex)
                {
                    dbTrans.Rollback(); // jika terjadi eroor saat add dan save changes
                    response.data = data;
                    response.message = ex.Message;
                }
            }
            return response;
        }

        public MWalletDefaultNominal? findNominalCus(long? transaksi)
        {
            MWalletDefaultNominal? responseData = new MWalletDefaultNominal();
            try
            {
                responseData = (
                    from dn in db.MWalletDefaultNominals
                    where dn.Nominal == transaksi
                    select new MWalletDefaultNominal
                    {
                        Id = dn.Id,
                    }
                ).FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }
    }
}
