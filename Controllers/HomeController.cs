using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebServiceProject.ServiceReference1;

namespace WebServiceProject.Controllers
{
    public class HomeController : Controller
    {
        private CountryInfoServiceSoapTypeClient request = new CountryInfoServiceSoapTypeClient("CountryInfoServiceSoap");
        public ActionResult Index()
        {           
            var countryName = request.ListOfCountryNamesByCode();
            List<SelectListItem> items = new List<SelectListItem>();
            for (int i=0;i< countryName.Length;i++)
            {
                items.Add(new SelectListItem { Text = "" + countryName[i].sName + "", Value = "" + countryName[i].sISOCode + "" });
            }
            
            ViewBag.country = items;

            return View();
        }
        [Route("getcapitalcity-{countycode?}")]
        public ActionResult GetCapitalCity(string countycode)
        {
            var capitalCity = request.CapitalCity(countycode);

            return Json(capitalCity);
        }
        [Route("getcountrycode-{county?}")]
        public ActionResult GetCountryCode(string county)
        {
            var countryIsoCode = request.CountryISOCode(county);

            return Json(countryIsoCode);
        }
        [Route("getcountrycurrency-{countycode?}")]
        public ActionResult GetCountryCurrency(string countycode)
        {
            var countrycurrency = request.CountryCurrency(countycode);

            return Json(countrycurrency);
        }
        [Route("createdb")]
        public ActionResult CreateDb()
        {
            if (Request["type"] == "add")
            {
                dbConnection db = new dbConnection();
                DataTable dt = null;
                string country_id,country_name = "", country_capital_city = "", country_code = "", country_currency = "";
                country_id = Guid.NewGuid().ToString();
                country_name = Request["country_name"].ToString();
                country_capital_city = Request["country_capital_city"].ToString();
                country_code = Request["country_code"].ToString();
                country_currency = Request["country_currency"].ToString();
                dt = db.InsertCountry(country_id,country_name, country_capital_city, country_code, country_currency, DateTime.Now);
                if (dt != null)
                {
                    Response.Write("true");
                    return View();
                }
                else
                {
                    Response.Write("false");
                    return View();
                }
            }
            return View();
        }
    }
}