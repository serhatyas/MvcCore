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
    public class projectsController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        private readonly IUserProjectMapService _userProjectMapService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public projectsController(IProjectService projectService, IUserService userService, IUserProjectMapService userProjectMapService, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _userProjectMapService = userProjectMapService;
            _userService = userService;
            _projectService = projectService;
        }
        public IActionResult Index()
        {
            string userGuid = Request.Cookies["userId"];
            if (userGuid == null)
                return Redirect("/login");
            ServiceVM model = new ServiceVM();
            model.ProjectList = _projectService.GetList(x => !x.IsDeleted).Data;
            model.UserProjectMapList = _userProjectMapService.GetList(x => model.ProjectList.Select(y => y.Id.ToString()).Contains(x.ProjectId.ToString()) && !x.IsDeleted && !x.IsPassive).Data;
            model.UserList = _userService.GetList().Data;
            return View(model);
        }
        public IActionResult create()
        {
            string userGuid = Request.Cookies["userId"];
            if (userGuid == null)
                return Redirect("/login");
            ServiceVM model = new ServiceVM();
            model.UserList = _userService.GetList(x => !x.IsDeleted && !x.IsPassive).Data;
            return View(model);
        }
        [HttpPost]
        public IActionResult create(ServiceVM model, IFormFile file, IFormCollection fc)
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
                model.Project.Icon = "/uploads/" + fileName;
            }
            var result = _projectService.Add(model.Project).Data;
            _ = result.Id > 0 ? TempData["success"] = "Kayıt başarıyla eklendi" : TempData["warning"] = "Kayıt eklenirken bir hata meydana geldi.";
            if (result.Id > 0)
            {
                var members = fc["cmbMembers"].ToString().Split(',');
                for (int i = 0; i < members.Length; i++)
                {
                    _userProjectMapService.Add(new Entities.Concrete.UserProjectMap()
                    {
                        ProjectId=result.Id,
                        UserId=int.Parse(members[i])
                    });
                }
            }
            return Redirect("/projects");
        }
        public IActionResult update(int id)
        {
            string userGuid = Request.Cookies["userId"];
            if (userGuid == null)
                return Redirect("/login");
            ServiceVM model = new ServiceVM();
            model.Project = _projectService.Get(x => x.Id == id && !x.IsDeleted).Data;
            model.UserList = _userService.GetList(x => !x.IsDeleted).Data;
            model.UserProjectMapList = _userProjectMapService.GetList(x => x.ProjectId == id).Data;
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
                model.Project.Icon = "/uploads/" + fileName;
            }
            result = _projectService.Update(model.Project).Success;
            _ = result ? TempData["success"] = "Kayıt başarıyla düzenlendi" : TempData["warning"] = "Kayıt düzenlenirken bir hata meydana geldi.";
            return Redirect("/projects");
        }
        public IActionResult passive(int id)
        {
            string userGuid = Request.Cookies["userId"];
            if (userGuid == null)
                return Redirect("/login");
            bool result = false;
            var isExist = _projectService.Get(x => x.Id == id).Data;
            if (isExist != null)
            {
                isExist.IsPassive = !isExist.IsPassive;
                result = _projectService.Update(isExist).Success;
            }

            _ = result ? TempData["success"] = "Kayıt başarıyla düzenlendi" : TempData["warning"] = "Kayıt düzenlenirken bir hata meydana geldi.";
            return Redirect("/projects");
        }
        public IActionResult delete(int id)
        {
            string userGuid = Request.Cookies["userId"];
            if (userGuid == null)
                return Redirect("/login");
            bool result = false;
            var isExist = _projectService.Get(x => x.Id == id).Data;
            if (isExist != null)
            {
                isExist.IsDeleted = true;
                result = _projectService.Update(isExist).Success;
            }

            _ = result ? TempData["success"] = "Kayıt başarıyla silindi" : TempData["warning"] = "Kayıt silinirken bir hata meydana geldi.";
            return Redirect("/projects");
        }
    }
}
