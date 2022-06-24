//using IOSystem.Models;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using Microsoft.Data.SqlClient;
//using System.Linq;
//using System.Threading.Tasks;

//namespace IOSystem.Controllers
//{
//    public class ReportController : Controller
//    {
//        private readonly IWebHostEnvironment _webHostEnvironment;
//        private readonly IConfiguration configuration;
//        public ReportController(IWebHostEnvironment webHostEnvironment, IConfiguration config)
//        {
//            this._webHostEnvironment = webHostEnvironment;
//            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

//            configuration = config;
//        }

        


//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult Print()
//        {
//            var dt = new DataTable();
//            //dt = GetIRDetailList();
//            string mimetype = "";
//            int extension = 1;
//            var path = $"{this._webHostEnvironment.WebRootPath}\\RDLCReports\\IRDetailReport.rdlc";
//            Dictionary<string, string> paramaters = new Dictionary<string, string>();
//            paramaters.Add("rp1","RDLC Report");
//            LocalReport localReport = new LocalReport(path);
//            var result = localReport.Execute(RenderType.Pdf,extension,paramaters,mimetype);

//            return File(result.MainStream, "application/pdf");
//        }

//        public IActionResult IRDetailPrint()
//        {
//            var dt = new DataTable();
//            dt = GetIRDetailList();
//            string mimetype = "";
//            int extension = 1;
//            var path = $"{this._webHostEnvironment.WebRootPath}\\RDLCReports\\IRDetailReport.rdlc";

//            Dictionary<string, string> paramaters = new Dictionary<string, string>();
//            paramaters.Add("rp1", "RDLC Report");
            
//            LocalReport localReport = new LocalReport(path);
//            localReport.AddDataSource("dsIRDetailReport", dt);

//            var result = localReport.Execute(RenderType.Pdf, extension, paramaters, mimetype);

//            return File(result.MainStream, "application/pdf");
//        }

//        public DataTable GetIRDetailList()
//        {
//            //string constr = configuration.GetConnectionString("DatabaseConnection");
//            string constr = @"Data Source=DESKTOP-07AD3O7;Initial Catalog=IOSystem;integrated security=true";

//            var dt = new DataTable();
//            using (SqlConnection con = new SqlConnection(constr))
//            {
//                SqlCommand cmd = new SqlCommand("SP_GetIRDetailReport", con);
//                cmd.CommandType = CommandType.StoredProcedure;
//                con.Open();
//                SqlDataAdapter da = new SqlDataAdapter(cmd);
//                da.Fill(dt);
//                con.Close();
//            }
//            return dt;
//        }
//    }
//}
