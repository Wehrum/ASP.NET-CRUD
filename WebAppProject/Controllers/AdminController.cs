using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.Models;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;

namespace GettingStarted.Controllers
{

    //[Authorize(Roles = "Administrator")]
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Create(ProjectRole role)
        {
           if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole();
                identityRole.Name = role.RoleName;


                var result = await roleManager.CreateAsync(identityRole);

                if(result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Admin");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(role);
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(ProjectRole role)
        //{
        //    var roleExist = await roleManager.RoleExistsAsync(role.RoleName);
        //    if (!roleExist)
        //    {
        //        var result = await roleManager.CreateAsync(new IdentityRole(role.RoleName)); 
        //    }
        //    else
        //    {
        //        foreach (IdentityError error in roleExi)
        //        ModelState.AddModelError("", errorMessage);
        //    }
        //    return RedirectToAction("ListRoles", "Admin");
        //}

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult multimodel()
        {
            //var users = userManager.Users;
            dynamic mymodel = new ExpandoObject();
            mymodel.ListRoles = ListRoles();
            //mymodel.ListUsers = ListUsers();
            return View(mymodel);
        }
        [HttpGet]
        public async Task<IActionResult> ListUsers()
        {
            //var roles = roleManager.FindByNameAsync(UserName)
            var model = new List<UserRoleViewModel>();
            foreach (var user in userManager.Users)
            {
                var roles = new UserRoleViewModel();
                roles.UserId = user.Id;
                roles.UserName = user.UserName;
                var userRolesName = await userManager.GetRolesAsync(user);
                if (userRolesName == null || userRolesName.Count < 1)
                {
                    roles.RoleName = "No roles :(";
                }
                else
                {
                    if (userRolesName.Count == 1)
                    {
                        roles.RoleName = userRolesName[0];
                    }
                    else if (userRolesName.Count == 2)
                    {
                        roles.RoleName = $"{userRolesName[0]} | {userRolesName[1]}";
                    }
                    else if (userRolesName.Count == 3)
                    {
                        roles.RoleName = $"{userRolesName[0]} | {userRolesName[1]} | {userRolesName[2]}";
                    }
                    //for (int i = 0; i < userRolesName.Count; i++)
                    //{
                    //}
                    //    userview.RoleName = resultstring;
                }

                model.Add(roles);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if(role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("Error");
            }

            var model = new EditRoleViewModel();
            model.Id = role.Id;
            model.RoleName = role.Name;

            var users = await userManager.GetUsersInRoleAsync(role.Name);

            //if(users.co)
            //{

            //}

            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);

            //var users = await userManager.GetUsersInRoleAsync(role.Name);
            //if (users.isin)
            

        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("Error");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);

                if(result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }


        }
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("Error");
            }

            var model = new List<UserRoleViewModel>();

            foreach(var user in userManager.Users)
            {
                var UserRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    RoleName = role.Name
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    UserRoleViewModel.IsSelected = true;
                }
                else
                {
                    UserRoleViewModel.IsSelected = false;
                }

                model.Add(UserRoleViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("Error");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;
                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if(result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }

    }
}