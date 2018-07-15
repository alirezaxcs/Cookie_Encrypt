using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Cookie_Encrypt.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            // Set Data in Cookie 
            var cookieText = Encoding.UTF8.GetBytes("Text for Cookie");
            var encryptedValue = Convert.ToBase64String(MachineKey.Protect(cookieText, "ProtectCookie"));

            //--- Create cookie object and pass name of the cookie and value to be stored.
            HttpCookie cookieObject = new HttpCookie("NameOfCookie", encryptedValue);

            //---- Set expiry time of cookie.
            cookieObject.Expires.AddDays(5);

            //---- Add cookie to cookie collection.
            Response.Cookies.Add(cookieObject);

       
            return View();
        }
        public ActionResult GetCookieActionResult()
        {
            var Result_Of_Cookie_Data = GetCookie();
            return View((object)Result_Of_Cookie_Data);
        }
        public string GetCookie()
        {
            var bytes = Convert.FromBase64String(Request.Cookies["NameOfCookie"].Value);
            var output = MachineKey.Unprotect(bytes, "ProtectCookie");
            string result = Encoding.UTF8.GetString(output);
            return result;
        }
    }
}