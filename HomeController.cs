using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using EMAIL.DBModel;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace EMAIL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()                                    
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SendEmail()
        {
            return View();
        }

      [HttpPost]  
        public ActionResult SendEmail(string receiver, string subject, string message)
        {
            try
            {
                var resultrank = new EMAIL.DBModel.tbl_mail().getAll();
                    //tbl_BoardProceeding().sp_AllBoardPerson(Model.MinuteSheetCode);

              

                    if (ModelState.IsValid)
                    {

                    foreach (var recs in resultrank)
                    {

                        var senderEmail = new MailAddress("exaction1997@gmail.com", "Zain");
                        var receiverEmail = new MailAddress(recs.UserEmail, "Receiver");
                        var password = "z41005792";
                        var sub = subject;
                        var host = new UriBuilder(System.Web.HttpContext.Current.Request.Url.Scheme, System.Web.HttpContext.Current.Request.Url.Host, System.Web.HttpContext.Current.Request.Url.Port);
                        message =  "<strong> Comment By :  <span>    " + "-"  + " </span> on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </strong>  <p><strong>" + "ABC" + "</strong>. |-| "  + "</p> <img src='" + host + "/images/image-3.png' ><br/><br/> <strong> Forward to  :  <span style='color:"  + "'>   "  + " </span> on " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + " </strong><hr> ";

                        var body = message;
                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(senderEmail.Address, password)
                        };
                        using (var mess = new MailMessage(senderEmail, receiverEmail)
                        {
                            Subject = subject,
                            Body = body
                        })
                        {
                            smtp.Send(mess);
                        }
                    }
                        return View();
                    }

                


            }
            catch (Exception ex)
            {
                ViewBag.Error = "Some Error";
            }
            return View();
        }


        public ActionResult CreateTable()
        {
            ViewBag.Message = "Your Datatable.";

            return View();
        }

        public ActionResult exportReport()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "CrystalReport1.rpt"));
            rd.SetDataSource(new EMAIL.DBModel.tbl_mail().getAll());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Employee_list.pdf");
            }
            catch
            {
                throw;
            }
        }
    }
}