using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminLTE1.Entities;
using AdminLTE1.Helper;
using AdminLTE1.Models;
using AdminLTE1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace AdminLTE1.Controllers
{
    [AuthorizeActionFilter]
    public class PermissionController : Controller
    {
        private readonly AddDbContext _context;
        public PermissionController(AddDbContext context)
        {
            this._context = context;
        }


        public IActionResult Country()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var country in _context.Country)
            {
                items.Add(new SelectListItem { Text = country.CName, Value = country.CountryId.ToString() });
            }
            ViewBag.lst = items;
            return View(ViewBag.lst);

        }

        public JsonResult GetCityByStateId(int StateId)
        {
            var cty = (_context.City.Where(data => data.SId == StateId).Select(x => new { value = x.CityId, text = x.CityName })).ToList();
            ViewData["cty"] = cty;
            return Json(ViewData["cty"]);
        }

        public IActionResult Index()
        {
            var role = (from r in _context.Roles
                        join per in _context.MenuPermissions
                        on r.Id equals Convert.ToString(per.RoleId)
                        select new EditRoleViewModel
                        {
                            RoleName = r.Name,
                            Id = r.Id
                        }).GroupBy(n => new { n.Id, n.RoleName }).Select(g => g.FirstOrDefault()).ToList();

            return View(role);
        }

        [HttpGet]
        public IActionResult CreatePermission(Guid RoleId)
        {
            List<PermissionViewModel> lstPermission = new List<PermissionViewModel>();

            var result = (from Menus in _context.MenuItems
                          join Permissions in _context.MenuPermissions.Where(r => r.RoleId == RoleId)
                          on Menus.Id equals Permissions.MenuId into menuPerm
                          from perm in menuPerm.DefaultIfEmpty()
                          select new PermissionViewModel
                          {
                              Id = Menus.Id,
                              Path = Menus.Path,
                              Name = Menus.Name,
                              ParentId = Menus.ParentId,
                              MenuLevel = Menus.MenuLevel,
                              HasAccess = perm == null ? false : !(string.IsNullOrEmpty(Convert.ToString(perm.RoleId)))
                          }).ToList();

            var RolesList = (from roles in _context.Roles
                             select new SelectListItem
                             {
                                 Value = roles.Id,
                                 Text = roles.Name
                             }).ToList();
            ViewBag.RolesList = RolesList;
            return View(result);
        }


        [HttpPost]
        public JsonResult CreatePermission([FromBody]UpdatePermissionViewModel[] model)
        {

            if (model != null)
            {
                var existing = _context.MenuPermissions.Where(R => R.RoleId == model.FirstOrDefault().RoleId).ToList();
                if (existing != null && existing.Count() > 0)

                {
                    return Json("Permission already assigned!");

                }

                else
                {
                    foreach (var item in model)
                    {
                        MenuPermission mnp = new MenuPermission();
                        mnp.MenuId = item.MenuId;
                        mnp.PermissionId = Guid.NewGuid();
                        mnp.RoleId = item.RoleId;
                        _context.MenuPermissions.Add(mnp);

                       

                        _context.SaveChanges();
                    }


                    return Json("Saved Successfully!");
                }

                //var assigned = "";
                //var alreadyexist = "";

                //var permissionName = _context.MenuItems.Where(a => a.Id == item.MenuId).FirstOrDefault().Name;
                //assigned += permissionName + "\n";

                //foreach (var item in model)
                //{
                //    var existing = _context.MenuPermissions.Where(R => R.RoleId == item.RoleId).ToList();
                //    if (existing != null && existing.Where(x => x.MenuId == item.MenuId).Count() > 0)
                //    {
                //        //return Json("Permisiion Already Assigned");
                //        var permissionName = _context.MenuItems.Where(a => a.Id == item.MenuId).FirstOrDefault().Name;
                //        alreadyexist += permissionName+"\n";
                //    }
                //    else
                //    {

                //        MenuPermission mnp = new MenuPermission();
                //        mnp.MenuId = item.MenuId;
                //        mnp.PermissionId = Guid.NewGuid();
                //        mnp.RoleId = item.RoleId;
                //        _context.MenuPermissions.Add(mnp);

                //        var permissionName = _context.MenuItems.Where(a => a.Id == item.MenuId).FirstOrDefault().Name;
                //        assigned += permissionName + "\n";

                //        _context.SaveChanges();

                //        //return Json("Saved Successfully!");
                //    }
                //}

                //return Json("Assigned permissions \n" + assigned+"alreadyexist permission \n"+alreadyexist);

            }
            return Json("NoData");

        }

        [HttpGet]
        public IActionResult EditPermission(Guid Id)
        {
            ViewBag.RoleId = Id;
            var result = (from Menus in _context.MenuItems
                          join Permissions in _context.MenuPermissions.Where(r => r.RoleId == Id)
                          on Menus.Id equals Permissions.MenuId into menuPerm
                          from perm in menuPerm.DefaultIfEmpty()
                          select new PermissionViewModel
                          {
                              Id = Menus.Id,
                              Path = Menus.Path,
                              Name = Menus.Name,
                              ParentId = Menus.ParentId,
                              MenuLevel = Menus.MenuLevel,
                              HasAccess = perm == null ? false : !(string.IsNullOrEmpty(Convert.ToString(perm.RoleId)))
                          }).ToList();
           
            return View(result);
        }

        [HttpPost]
        public JsonResult EditPermission([FromBody]UpdatePermissionViewModel[] model)
        {

            if (model != null)
            {

                int count = 0;
                foreach (var item in model)
                {
                    if (count == 0)
                    {
                        var existing = _context.MenuPermissions.Where(R => R.RoleId == item.RoleId);

                        foreach (var roles in existing)
                        {
                            _context.MenuPermissions.Remove(roles);
                        }
                        count++;
                    }
                    MenuPermission mnp = new MenuPermission();
                    mnp.MenuId = item.MenuId;
                    mnp.PermissionId = Guid.NewGuid();
                    mnp.RoleId = item.RoleId;
                    _context.MenuPermissions.Add(mnp);
                }

                _context.SaveChanges();

                return Json("Saved Successfully!");
            }
            return Json("NoData");
        }
        

        public IActionResult GetMenus()
        {
            List<MenuViewModel> lstmodel = new List<MenuViewModel>();
            var models = _context.MenuItems;

            foreach (var item in models)
            {
                MenuViewModel mvm = new MenuViewModel();
                mvm.Id = item.Id;
                mvm.Name = item.Name;
                mvm.Path = item.Path;
                mvm.ParentId = item.ParentId;
                mvm.MenuLevel = item.MenuLevel;
                mvm.MenuGrpId = item.MenuGrpId;

                lstmodel.Add(mvm);
            }

            ViewBag.successMessage = TempData["successMessage"];
            return View(lstmodel);
        }


        [HttpGet]
        public IActionResult CreateMenu()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenu(MenuViewModel model)
        {
            if (model != null)
            {
                var menuModel = new MenuItem();
                menuModel.Name = model.Name;
                menuModel.Path = model.Path;
                menuModel.ParentId = model.ParentId;
                menuModel.MenuLevel = model.MenuLevel;
                menuModel.MenuGrpId = model.MenuGrpId;

                _context.MenuItems.Add(menuModel);
                _context.SaveChanges();

                return RedirectToAction("GetMenus");
            }
            return View();
        }

        [HttpGet]
        public IActionResult ViewPermission(Guid Id)
        {
            var result = (from Menus in _context.MenuItems
                          join Permissions in _context.MenuPermissions.Where(r => r.RoleId == Id)
                          on Menus.Id equals Permissions.MenuId into menuPerm
                          from perm in menuPerm.DefaultIfEmpty()
                          select new PermissionViewModel
                          {
                              Id = Menus.Id,
                              Path = Menus.Path,
                              Name = Menus.Name,
                              ParentId = Menus.ParentId,
                              MenuLevel = Menus.MenuLevel,
                              HasAccess = perm == null ? false : !(string.IsNullOrEmpty(Convert.ToString(perm.RoleId)))
                          }).ToList();
            return View(result);
        }


        [HttpGet]
        public IActionResult DeletePermissions(Guid Id)
        {
            var permission = _context.MenuPermissions.Where(m => m.RoleId == Id);
            foreach (var perm in permission)
            {
                _context.MenuPermissions.Remove(perm);
            }
            var perms = _context.MenuPermissions.FirstOrDefault(m => m.RoleId == Id);
            _context.MenuPermissions.Remove(perms);
            _context.SaveChanges();

            TempData["successMessage"] = "Permissions Deleted Successfully.";
            TempData.Keep();

            return RedirectToAction("Index");

        }


        [HttpGet]
        public IActionResult EditMenu(int Id)
        {
            MenuViewModel mvm = new MenuViewModel();
            var menu = _context.MenuItems.FirstOrDefault(m => m.Id == Id);

            if (menu != null)
            {
                mvm.Id = menu.Id;
                mvm.Name = menu.Name;
                mvm.Path = menu.Path;
                mvm.ParentId = menu.ParentId;
                mvm.MenuLevel = menu.MenuLevel;
                mvm.MenuGrpId = menu.MenuGrpId;
            }
            return View(mvm);
        }

        [HttpGet]
        public IActionResult DeleteMenu(int Id)
        {
            var permission = _context.MenuPermissions.Where(m => m.MenuId == Id);
            foreach (var perm in permission)
            {
                _context.MenuPermissions.Remove(perm);
            }
            var menu = _context.MenuItems.FirstOrDefault(m => m.Id == Id);
            _context.MenuItems.Remove(menu);
            _context.SaveChanges();

            TempData["successMessage"] = "Menu Deleted Successfully.";
            TempData.Keep();

            return RedirectToAction("GetMenus");

        }

        [HttpPost]
        public IActionResult EditMenu(MenuViewModel model)
        {
            if (model != null)
            {
                var MenuItem = _context.MenuItems.FirstOrDefault(m => m.Id == model.Id);
                if (MenuItem != null)
                {
                    MenuItem.Name = model.Name;
                    MenuItem.Path = model.Path;
                    MenuItem.ParentId = model.ParentId;
                    MenuItem.MenuLevel = model.MenuLevel;
                    MenuItem.MenuGrpId = model.MenuGrpId;

                    _context.Entry(MenuItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();

                    return RedirectToAction("GetMenus");
                }
                return View();
            }
            return View();
        }



        public IActionResult ListMenu()
        {
            List<MenuViewModel> lstmodel = new List<MenuViewModel>();
            var models = _context.MenuItems;

            foreach (var item in models)
            {
                MenuViewModel mvm = new MenuViewModel();
                mvm.Id = item.Id;
                mvm.Name = item.Name;
                mvm.Path = item.Path;
                mvm.ParentId = item.ParentId;
                mvm.MenuLevel = item.MenuLevel;
                mvm.MenuGrpId = item.MenuGrpId;

                lstmodel.Add(mvm);
            }

            ViewBag.successMessage = TempData["successMessage"];
            return View(lstmodel);
        }


        [HttpGet]
        public IActionResult GetPermissions(Guid RoleId)
        {
            List<PermissionViewModel> lstPermission = new List<PermissionViewModel>();

            var result = (from Menus in _context.MenuItems
                          join Permissions in _context.MenuPermissions.Where(r => r.RoleId == RoleId)
                          on Menus.Id equals Permissions.MenuId into menuPerm
                          from perm in menuPerm.DefaultIfEmpty()
                          select new PermissionViewModel
                          {
                              Id = Menus.Id,
                              Path = Menus.Path,
                              Name = Menus.Name,
                              ParentId = Menus.ParentId,
                              MenuLevel = Menus.MenuLevel,
                              HasAccess = perm == null ? false : !(string.IsNullOrEmpty(Convert.ToString(perm.RoleId)))
                          }).ToList();

            var RolesList = (from roles in _context.Roles
                             select new SelectListItem
                             {
                                 Value = roles.Id,
                                 Text = roles.Name
                             }).ToList();
            ViewBag.RolesList = RolesList;
            return View(result);

        }

        [HttpPost]
        public JsonResult UpdatePermission([FromBody]UpdatePermissionViewModel[] model)
        {

            if (model != null)
            {

                int count = 0;
                foreach (var item in model)
                {
                    if (count == 0)
                    {
                        var existing = _context.MenuPermissions.Where(R => R.RoleId == item.RoleId);

                        foreach (var roles in existing)
                        {
                            _context.MenuPermissions.Remove(roles);
                        }
                        count++;
                    }
                    MenuPermission mnp = new MenuPermission();
                    mnp.MenuId = item.MenuId;
                    mnp.PermissionId = Guid.NewGuid();
                    mnp.RoleId = item.RoleId;
                    _context.MenuPermissions.Add(mnp);
                }

                _context.SaveChanges();

                return Json("Saved Successfully!");
            }
            return Json("NoData");
        }


       
    }
}