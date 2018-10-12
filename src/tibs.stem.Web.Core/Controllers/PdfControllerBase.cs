using Abp.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Authorization;
using tibs.stem.EntityFrameworkCore;

namespace tibs.stem.Web.Controllers
{
    public abstract class PdfControllerBase : stemControllerBase
    {
        private readonly IAppFolders _appFolders;
        stemDbContext _stemDbContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public static IConfigurationRoot Configuration { get; set; }
        public PdfControllerBase(IAppFolders appFolders, stemDbContext stemDbContext, IHostingEnvironment hostingEnvironment)
        {
           _hostingEnvironment = hostingEnvironment;
            _appFolders = appFolders;
            _stemDbContext = stemDbContext;
        }

        public string ExportQuotationGenerate(int QuotationId)
        {
            string body = string.Empty;
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory());
            var filefinder = builder.GetFileProvider().GetFileInfo("Quotation.html");
            string filepath = filefinder.PhysicalPath;
            using (StreamReader reader = new StreamReader(filepath))
            {
                body = reader.ReadToEnd();
            }
            List<TestClass> Test = new List<TestClass>();
            try
            {

            var db = _stemDbContext.Database.GetDbConnection().ConnectionString;
            using (SqlConnection conn = new SqlConnection(db))
            {
                SqlCommand sqlComm = new SqlCommand("sp_QuotationProductsListTemplate", conn);
                sqlComm.Parameters.AddWithValue("@QuotationId", QuotationId);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();
                try
                {

                    using (var reader = sqlComm.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var ProductContent = (string)reader["data"];
                                if (ProductContent == "")
                                {
                                    body = body.Replace("{{productdisplay}}", "none");
                                }
                                body = body.Replace("{Product_Content}", ProductContent);

                        }

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                conn.Close();
            }

                using (SqlConnection conn = new SqlConnection(db))
                {
                    SqlCommand sqlComm = new SqlCommand("sp_QuotationServiceListTemplate", conn);
                    sqlComm.Parameters.AddWithValue("@QuotationId", QuotationId);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    try
                    {

                        using (var reader = sqlComm.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var ServiceContent = (string)reader["data"];
                                if(ServiceContent == "")
                                {
                                    body = body.Replace("{{servicedisplay}}", "none");
                                }
                                body = body.Replace("{Service_Content}", ServiceContent);

                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    conn.Close();
                }
                var list = Test.ToList();
        }
            catch (Exception ex)
            {
                throw ex;
            }



    var quotation = (from r in _stemDbContext.Quotations where r.Id == QuotationId select r).FirstOrDefault();
            var company = "";
            var freight = "";
            var payment = "";
            var packing = "";
            var warranty = "";
            var validity = "";
            var delivery = "";
            var contact = "";
            var title = "";
            var salesman = "";
            var email = "";
            var phonenumber = "";
            var currencycode = "";

            if (quotation.CompanyId != null)
            {
                company = (from r in _stemDbContext.Companys where r.Id == quotation.CompanyId select r.Name).FirstOrDefault();
            }
            if (quotation.FreightId != null)
            {
                freight = (from r in _stemDbContext.Freights where r.Id == quotation.FreightId select r.FreightName).FirstOrDefault();
            }
            if (quotation.PaymentId != null)
            {
                payment = (from r in _stemDbContext.QPayments where r.Id == quotation.PaymentId select r.PaymentName).FirstOrDefault();
            }
            if (quotation.PackingId != null)
            {
                packing = (from r in _stemDbContext.Packings where r.Id == quotation.PackingId select r.PackingName).FirstOrDefault();
            }
            if (quotation.WarrantyId != null)
            {
                warranty = (from r in _stemDbContext.Warrantys where r.Id == quotation.WarrantyId select r.WarrantyName).FirstOrDefault();
            }
            if (quotation.ValidityId != null)
            {
                validity = (from r in _stemDbContext.Validitys where r.Id == quotation.ValidityId select r.ValidityName).FirstOrDefault();
            }
            if (quotation.DeliveryId != null)
            {
                delivery = (from r in _stemDbContext.Deliverys where r.Id == quotation.DeliveryId select r.DeliveryName).FirstOrDefault();
            }
            if (quotation.ContactId != null)
            {
                contact = (from r in _stemDbContext.Contacts where r.Id == quotation.ContactId select r.Name).FirstOrDefault();

            }

            if (quotation.SalesmanId != null)
            {

                var sales = (from r in _stemDbContext.Users where r.Id == quotation.SalesmanId select r).ToArray();
                foreach (var dat in sales)
                {
                    salesman = dat.Name;
                    email = dat.EmailAddress;
                    phonenumber = dat.PhoneNumber;
                }
                //salesman = (from r in _scDbContext.Users where r.Id == quotation.SalesmanId select r.Name).FirstOrDefault();
                //email = (from r in _scDbContext.Users where r.Id == quotation.SalesmanId select r.EmailAddress).FirstOrDefault();
                //phonenumber = (from r in _scDbContext.Users where r.Id == quotation.SalesmanId select r.PhoneNumber).FirstOrDefault();
            }
            if (quotation.QuotationTitleId != null)
            {
                title = (from r in _stemDbContext.TitleOfQuotations where r.Id == quotation.QuotationTitleId select r.Name).FirstOrDefault();
            }
            if (quotation.CurrencyId != null)
            {
                currencycode = (from r in _stemDbContext.Currencies where r.Id == quotation.CurrencyId select r.Code).FirstOrDefault();
            }

            decimal total = ((quotation.Total * quotation.ExchangeRate) - quotation.OverallDiscount);

            var ServiceTotal = (from r in _stemDbContext.QuotationServices where r.QuotationId == quotation.Id && r.IsDeleted == false select r.Price * quotation.ExchangeRate).Sum();
            var ProductTotal = (from r in _stemDbContext.QuotationProducts where r.QuotationId == quotation.Id && r.IsDeleted == false select r.EstimatedPriceUSD * quotation.ExchangeRate).Sum();

            body = body.Replace("{SerTotal}", ServiceTotal.ToString());
            body = body.Replace("{ProdTotal}", ProductTotal.ToString());
            body = body.Replace("{NetTotal}", (quotation.Total * quotation.ExchangeRate).ToString());
            body = body.Replace("{DisTotal}", quotation.OverallDiscount.ToString());
            if(quotation.Vat == true)
            {
                body = body.Replace("{VatTotal}", quotation.VatAmount.ToString());
                body = body.Replace("{Total}", (total + quotation.VatAmount).ToString());
            }
            else
            {
                body = body.Replace("{VatTotal}", "0");
                body = body.Replace("{Total}", total.ToString());
            }



            body = body.Replace("{QutCompany}", company);
            //body = body.Replace("{QutCompany}", quotation.Companys.Name);
            body = body.Replace("{QutDate}", quotation.CreationTime.ToString());
            body = body.Replace("{QutProposalNo}", quotation.ProposalNumber);
            body = body.Replace("{QutProjectRef}", quotation.ProjectRef);
            body = body.Replace("{QutFreight}", freight);
            body = body.Replace("{QutPayment}", payment);
            body = body.Replace("{QutPacking}", packing);
            body = body.Replace("{QutWarranty}", warranty);
            body = body.Replace("{QutValidity}", validity);
            // body = body.Replace("{QutDelivery}", delivery);
            //string delivery = quotation.DeliveryId != null ? quotation.Deliverys.DeliveryName : "";
            body = body.Replace("{QutDelivery}", delivery);
            body = body.Replace("{QutContact}", contact);
            body = body.Replace("{QutCon_Mail}", email);
            body = body.Replace("{QutCon_Phone}", phonenumber);
            body = body.Replace("{QutSalesman}", salesman);
            body = body.Replace("{Title}", title);
            body = body.Replace("{QutConversionCode}", currencycode);
            body = body.Replace("{Conversion Code}", currencycode);
            //return body;

            var rootpath = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");

            Configuration = rootpath.Build();
            var TenantLogoImage = (from r in _stemDbContext.Tenants where r.Id == quotation.TenantId select r.LogoId).FirstOrDefault();
            if (TenantLogoImage != null)
            {
                var root = Configuration["App:ServerRootAddress"];
                body = body.Replace("{Image_Url}", root + "/Common/Logo/LogoImage_" + quotation.TenantId + ".jpg");
            }
            else
            {
                body = body.Replace("{Image_Url}", "");
            }


            string QuotationPath = _hostingEnvironment.WebRootPath + "\\Common\\QuotationPDF\\";
            //string QuotationPath = @"C:\Salescastle\QuotationPDF\";

            if (!Directory.Exists(QuotationPath))
            {
                Directory.CreateDirectory(QuotationPath);
            }
            string fileName = "QuotationPDF_" + QuotationId + ".pdf";

            if (System.IO.File.Exists(QuotationPath + fileName))
            {
                try
                {
                    System.IO.File.Delete(QuotationPath + fileName);
                }
                catch (System.IO.IOException e)
                {
                }
            }
            var generator = new NReco.PdfGenerator.HtmlToPdfConverter();
            try
            {
                generator.Orientation = NReco.PdfGenerator.PageOrientation.Portrait;
                generator.Zoom = 0.9f;
                generator.CustomWkHtmlArgs = " --load-media-error-handling ignore ";
                generator.Size = NReco.PdfGenerator.PageSize.Letter;
                generator.GeneratePdf(body, null, QuotationPath + fileName);
            }
            catch (Exception ex)
            {

            }
            return body;
        }

        public FileResult ExportQuotationDownload(int QuotationId)
        {
            string QuotationPath = _hostingEnvironment.WebRootPath + "\\Common\\QuotationPDF\\";
            string fileName = "QuotationPDF_" + QuotationId + ".pdf";
            byte[] fileBytes = System.IO.File.ReadAllBytes(QuotationPath + fileName);
            return File(fileBytes, "application/x-msdownload", fileName);
        }
    }
    public class TestClass
    {
        public string data { get; set; }
    }
}
