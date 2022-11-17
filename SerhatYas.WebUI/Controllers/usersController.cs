using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SerhatYas.Business.Abstract;
using SerhatYas.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SerhatYas.WebUI.Controllers
{
    public class usersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public usersController(  IUserService userService,   IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _userService = userService;
        }
        public IActionResult Index()
        {
            string userGuid = Request.Cookies["userId"];
            if (userGuid == null)
                return Redirect("/login");
            ServiceVM model = new ServiceVM();
            model.UserList = _userService.GetList(x=>!x.IsDeleted).Data;
            return View(model);
        }
        public IActionResult create()
        {
            string userGuid = Request.Cookies["userId"];
            if (userGuid == null)
                return Redirect("/login");
            ServiceVM model = new ServiceVM();
            return View();
        }
        [HttpPost]
        public IActionResult create(ServiceVM model, IFormFile file)
        {
            string userGuid = Request.Cookies["userId"];
            if (userGuid == null)
                return Redirect("/login");

            if (file != null)
            {
                var rootPath = _webHostEnvironment.WebRootPath;
                var fileName = DateTime.Now.ToString().Replace(":", string.Empty).Replace(":", string.Empty).Replace("-", string.Empty).Replace(".", string.Empty).Replace(" ", string.Empty) + Path.GetExtension(file.FileName);
                var savedPath = rootPath + @"\uploads\" + fileName;
                using (FileStream fs = new FileStream(savedPath, FileMode.Create))
                {
                    file.CopyTo(fs);
                }
                model.User.Photo = "/uploads/" + fileName;
            }
            bool result = _userService.Add(model.User).Success;
            _ = result? TempData["success"] = "Kayıt başarıyla eklendi" : TempData["warning"] = "Kayıt eklenirken bir hata meydana geldi.";
         
            return Redirect("/users");
        }
        public IActionResult update(int id)
        {
            string userGuid = Request.Cookies["userId"];
            if (userGuid == null)
                return Redirect("/login");
            ServiceVM model = new ServiceVM();
            model.User = _userService.Get(x =>x.Id==id).Data;
            return View(model);
        }
        [HttpPost]
        public IActionResult update(ServiceVM model, IFormFile file)
        {
            string userGuid = Request.Cookies["userId"];
            if (userGuid == null)
                return Redirect("/login");
            bool result = false;
            if (file != null)
            {
                var rootPath = _webHostEnvironment.WebRootPath;
                var fileName = DateTime.Now.ToString().Replace(":", string.Empty).Replace(":", string.Empty).Replace("-", string.Empty).Replace(".", string.Empty).Replace(" ", string.Empty) + Path.GetExtension(file.FileName);
                var savedPath = rootPath + @"\uploads\" + fileName;
                using (FileStream fs = new FileStream(savedPath, FileMode.Create))
                {
                    file.CopyTo(fs);
                }
                model.User.Photo = "/uploads/" + fileName;
            }
            result = _userService.Update(model.User).Success;
            _ = result ? TempData["success"] = "Kayıt başarıyla düzenlendi" : TempData["warning"] = "Kayıt düzenlenirken bir hata meydana geldi.";
            return Redirect("/users");
        }
        public IActionResult passive(int id)
        {
            string userGuid = Request.Cookies["userId"];
            if (userGuid == null)
                return Redirect("/login");
            bool result = false;
            var isExist = _userService.Get(x => x.Id == id).Data;
            if (isExist != null)
            {
                isExist.IsPassive = !isExist.IsPassive;
                result = _userService.Update(isExist).Success;
            }

            _ = result ? TempData["success"] = "Kayıt başarıyla düzenlendi" : TempData["warning"] = "Kayıt düzenlenirken bir hata meydana geldi.";
            return Redirect("/users");
        }
        public IActionResult delete(int id)
        {
            string userGuid = Request.Cookies["userId"];
            if (userGuid == null)
                return Redirect("/login");
            bool result = false;
            var isExist = _userService.Get(x => x.Id == id).Data;
            if (isExist != null)
            {
                isExist.IsDeleted = true;
                result = _userService.Update(isExist).Success;
            }

            _ = result ? TempData["success"] = "Kayıt başarıyla silindi" : TempData["warning"] = "Kayıt silinirken bir hata meydana geldi.";
            return Redirect("/users");
        }
    }
}
