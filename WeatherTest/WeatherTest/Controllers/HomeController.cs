using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherTest.Models;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace WeatherTest.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public List<City> Citys
        {
            get
            {
                List<City> added_citys = new List<City>();
                City city = new City();
                city.City_id = 1;
                city.name = "CD Obregon";
                city.lat = (decimal)27.48;
                city.lon = (decimal)-109.94;
                added_citys.Add(city);

                city = new City();
                city.City_id = 2;
                city.name = "Navojoa";
                city.lat = (decimal)27.07;
                city.lon = (decimal)-109.44;
                added_citys.Add(city);

                city = new City();
                city.City_id = 3;
                city.name = "Hermosillo";
                city.lat = (decimal)29.09;
                city.lon = (decimal)-110.96;
                added_citys.Add(city);

                city = new City();
                city.City_id = 4;
                city.name = "Nogales";
                city.lat = (decimal)31.30;
                city.lon = (decimal)-110.94;
                added_citys.Add(city);

                return added_citys.ToList();
            }
        }

        public List<Unit> Units
        {
            get
            {
                List<Unit> added_units = new List<Unit>();
                Unit unit = new Unit();
                unit.Unit_id = 1;
                unit.name = "Celsius";
                unit.api_description = string.Empty;
                added_units.Add(unit);

                unit = new Unit();
                unit.Unit_id = 2;
                unit.name = "Fahrenheit";
                unit.api_description = "I";
                added_units.Add(unit);

                return added_units.ToList();
            }
        }

        public JsonResult Get_Api_Result(string city_id, string unit_id, string selected_date)
        {
            DateTime date = DateTime.Parse(selected_date);
            DateTime today = DateTime.Today;
            int days = (date - today.Date).Days + 1;
            //double days = 5;
            string json_content = "";
            string unit_description = "";
            City city = city_id != "undefined" ? Citys.FirstOrDefault(c => c.City_id == int.Parse(city_id)) : Citys.FirstOrDefault();
            Unit unit = unit_id != "undefined" ? Units.FirstOrDefault(u => u.Unit_id == int.Parse(unit_id)) : Units.FirstOrDefault();
            unit_description = unit.api_description == string.Empty ? "" : "&units=" + unit.api_description;
            WebRequest request = WebRequest.Create("https://api.weatherbit.io/v2.0/forecast/daily?lat=" + city.lat + "&lon=" + city.lon + unit_description + "&days=" + days.ToString() + "&key=a1ea39d3341d431cb1cb0bb718a062af");
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            json_content = reader.ReadLine();
            //string json = JsonConvert.SerializeObject(json_content);
            object obj = new JavaScriptSerializer().DeserializeObject(json_content);
            //request.CreateResponse(HttpStatusCode.Accepted, obj);

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get_Units()
        {
            List<Unit> unit_list = Units.ToList();
            return Json(unit_list.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get_Citys()
        {
            List<City> city_list = Citys.ToList();
            //string json = Json(city_list.ToList(), JsonRequestBehavior.AllowGet).ToString();
            //JsonResult json = Json(city_list.ToList(), JsonRequestBehavior.AllowGet).;
            return Json(city_list.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}