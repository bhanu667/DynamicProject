using System;
using System.Collections.Generic;
using System.Linq;
using AdminLTE1.Entities;
using AdminLTE1.Models;
using AdminLTE1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace AdminLTE1.Controllers
{
    public class TestController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly AddDbContext _context;

        public TestController(AddDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<TreeViewNode> nodes = new List<TreeViewNode>();

            //Loop and add the Parent Nodes.
            foreach (StateData type in _context.StateData)
            {
                nodes.Add(new TreeViewNode { id = type.Id.ToString(), parent = "#", text = type.Title });
            }

            //Loop and add the Child Nodes.
            foreach (CityData subType in _context.CityData)
            {
                nodes.Add(new TreeViewNode { id = subType.StateId.ToString() + "-" + subType.Id.ToString(), parent = subType.StateId.ToString(), text = subType.Name });
            }

            //Serialize to JSON string.
            ViewBag.Json = JsonConvert.SerializeObject(nodes);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string selectedItems)
        {
            List<TreeViewNode> items = JsonConvert.DeserializeObject<List<TreeViewNode>>(selectedItems);
            return RedirectToAction("Index");
        }


        public IActionResult Permission(Guid RoleId)
        {
            List<TreeViewNode> nodes = new List<TreeViewNode>();
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

            // Loop and add the Parent Nodes.
            foreach (var type in result.Where(p => p.ParentId == 0).ToList())
            {
                nodes.Add(new TreeViewNode { id = type.Id.ToString(), parent = "#", text = type.Name, selected = type.HasAccess });
            }

            //Loop and add the Child Nodes.
            foreach (var subType in result.Where(p => p.ParentId > 0).ToList())
            {
                nodes.Add(new TreeViewNode { id = subType.ParentId.ToString() + "-" + subType.Id.ToString(), parent = subType.ParentId.ToString(), text = subType.Name, selected = subType.HasAccess });
            }

            //Serialize to JSON string.

            ViewBag.Json = JsonConvert.SerializeObject(nodes);
            return View();
        }
        
        public IActionResult TreeViewFinal1(Guid RoleId)
        {

            List<PermissionViewModel> lstPermission = new List<PermissionViewModel>();
            List<Final> obj = new List<Final>();

            var RolesList = (from roles in _context.Roles
                             select new SelectListItem
                             {
                                 Value = roles.Id,
                                 Text = roles.Name
                             }).ToList();
            ViewBag.RolesList = RolesList;

            var Permission = (from per in _context.MenuPermissions
                              where (per.RoleId == RoleId)
                              select per.MenuId);

            var per1 = string.Join(",", Permission);
            ViewBag.Per = per1;

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

            // Loop and add the Parent Nodes.
            foreach (var type in result.Where(p => p.ParentId == 0))
            {
                List<Child> ch = new List<Child>();
                foreach (var subChild in result.Where(x => x.ParentId == type.Id))
                {
                    ch.Add(new Child { id = subChild.Id, title = "'" + subChild.Name + "'", Checked = subChild.HasAccess }); 
                }
                obj.Add(new Final { id = type.Id, title = "'" + type.Name + "'", subs = ch, Checked = type.HasAccess });
            }
            //Serialize to JSON string.
            var items = JsonConvert.SerializeObject(obj);
            ViewBag.Json = items.Replace('"', ' ');
            return View();
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