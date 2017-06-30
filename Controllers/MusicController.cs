using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project2016.DAO;
using Project2016.Models;

namespace Project2016.Controllers
{
    public class MusicController : Controller
    {
        MusicApiController objMusicApi = new MusicApiController();

        // GET: login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {            
            bool isExists = objMusicApi.isUserExists(Username,Password);

            if (isExists) {
                System.Web.HttpContext.Current.Session["Username"] = Username;
                ViewData["Username"] = System.Web.HttpContext.Current.Session["Username"].ToString();
                return View("LocalSearch");
            }

            @ViewBag.error = "Username or Password is wrong.";
            return View();            
        }

        [HttpGet]
        public ActionResult Register()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Register(string Username, string Password)
        {            
            bool isExists = objMusicApi.isUserExists(Username, Password);

            if (!isExists)
            {
                bool isSuccess = objMusicApi.registerUser(Username, Password);

                if (isSuccess)
                {
                    System.Web.HttpContext.Current.Session["Username"] = Username;
                    ViewData["Username"] = System.Web.HttpContext.Current.Session["Username"].ToString();
                    return View("LocalSearch");
                }
                else                
                    @ViewBag.error = "Internal error occurred..";
            }
            else
                @ViewBag.error = "Username already exists.";

            return View();
        }

        public ActionResult GoogleSearch()
        {
            if (System.Web.HttpContext.Current.Session["Username"].ToString() == "")
                return View("Login");

            ViewData["Username"] = System.Web.HttpContext.Current.Session["Username"].ToString();
            return View();
        }

        [HttpGet]
        public ActionResult LocalSearch()
        {
            if (System.Web.HttpContext.Current.Session["Username"].ToString() == "")
                return View("Login");

            ViewData["Username"] = System.Web.HttpContext.Current.Session["Username"].ToString();
            return View();
        }

        [HttpPost]
        public JsonResult LocalSearch(string searchString, string optionString)
        {
            if (System.Web.HttpContext.Current.Session["Username"].ToString() == "")
                RedirectToAction("Login");

            ViewData["Username"] = System.Web.HttpContext.Current.Session["Username"].ToString();
            List<string> results = new List<string>();

            if (searchString != null) { 
                results = objMusicApi.searchString(searchString, optionString);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult MyHistory()
        {
            if (System.Web.HttpContext.Current.Session["Username"].ToString() == "")
                return View("Login");

            string username = System.Web.HttpContext.Current.Session["Username"].ToString();
            ViewData["Username"] = username;

            int userid = objMusicApi.getUserIdByUsername(username);
            List<MusicViewModel> lstMusicVwMdl = objMusicApi.searchHistory(userid);

            return View(lstMusicVwMdl);
        }

        [HttpPost]
        public JsonResult MyHistory(string username, string track, string album, string artist)
        {
            bool isSuccessful = false;
            int user_id = objMusicApi.getUserIdByUsername(username);
            isSuccessful = objMusicApi.saveHistory(user_id, track, album, artist);
            return Json( isSuccessful , JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logoff()
        {
            System.Web.HttpContext.Current.Session["Username"] = "";
            return View("Login");
        }
        
    }
}