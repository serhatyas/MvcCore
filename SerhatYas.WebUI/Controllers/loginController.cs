using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SerhatYas.Business.Abstract;
using SerhatYas.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerhatYas.WebUI.Controllers
{
    public class loginController : Controller
    {
        private readonly IUserService _userService;
        public loginController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            string userGuid = Request.Cookies["userId"];
            if (userGuid != null)
                return Redirect("/");

            return View();
        }
        [HttpPost]
        public IActionResult index(ServiceVM model)
        {
            var isExist = _userService.Get(x => x.Mail == model.User.Mail && x.Password == model.User.Password && !x.IsDeleted && !x.IsPassive).Data;
            if(isExist!=null)
            {
                isExist.LastLoginDate = DateTime.Now;
                _userService.Update(isExist);
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append("userId", isExist.Id.ToString(), cookieOptions);
                return Redirect("/");
            }
            return View(model);
        }

        public IActionResult logout()
        {
            Response.Cookies.Delete("userId");
            return Redirect("/login");
        }
    }
}
