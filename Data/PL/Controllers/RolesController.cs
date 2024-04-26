using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using PL.Models;
using System.Data;

namespace PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<ApplicationRole> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<ApplicationRole> roleManager, 
            ILogger<ApplicationRole> logger,UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Goo()
        {
            var rotes= await _roleManager.Roles.ToListAsync();

            return View(rotes);
        }
        public IActionResult Create()
        {
            return View(new ApplicationRole());
        }
        [HttpPost]
        public async Task<IActionResult> Create(ApplicationRole role)
        {
            if (ModelState.IsValid)
            {
               
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Goo");

                foreach (var i in result.Errors)
                {
                    _logger.LogError(i.Description);
                    ModelState.AddModelError("", i.Description);
                }

            }
            return View(role);
        }
        public async Task<IActionResult> Details(string id, string viewname = "Details")
        {

            if (id is null)
                return NotFound();

            var user = await _roleManager.FindByIdAsync(id);

            if (user is null)
                return NotFound();

            return View(viewname, user);
        }
        public async Task<IActionResult> Update(string id)
        {


            return await Details(id, "Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id, ApplicationRole Arole)
        {
            if (id != Arole.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                var Roles = await _roleManager.FindByIdAsync(id);
                Roles.Name = Arole.Name;
                Roles.NormalizedName = Arole.Name.ToUpper();
                var result = await _roleManager.UpdateAsync(Roles);

                if (result.Succeeded)
                    return RedirectToAction("Goo");
                foreach (var i in result.Errors)
                {
                    _logger.LogError(i.Description);
                    ModelState.AddModelError("", i.Description);
                }
            }

            return View(Arole);
        }
        public async Task<IActionResult> Delete(string id)
        {


            var Roles = await _roleManager.FindByIdAsync(id);

            var result = await _roleManager.DeleteAsync(Roles);
            if (Roles is null)
                return NotFound();
            if (result.Succeeded)
                return RedirectToAction("Goo");
            foreach (var i in result.Errors)
            {
                _logger.LogError(i.Description);
                ModelState.AddModelError("", i.Description);
            }


            return RedirectToAction("Goo");
        }
        public async Task<IActionResult> AddOrRemoveUsers(string roleid)
        {


            var Roles = await _roleManager.FindByIdAsync(roleid);

            if (Roles is null)
                return NotFound();
            ViewBag.roleid = roleid;
            var usersInRole = new List<UserInRoleViewModel>();
            var users = await _userManager.Users.ToListAsync();
            foreach (var i in users)
            {
                var userInRole = new UserInRoleViewModel { UserId = i.Id, UserName = i.UserName };


                if (await _userManager.IsInRoleAsync(i, Roles.Name))
                    userInRole.IsSelected = true;
                else
                    userInRole.IsSelected = false;

                usersInRole.Add(userInRole);

            }
            return View(usersInRole);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleid, List<UserInRoleViewModel> users)
        {
            var Roles = await _roleManager.FindByIdAsync(roleid);

            if (Roles is null)
                return NotFound();
            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                    if (appUser != null)
                    {
                        if (user.IsSelected && !await _userManager.IsInRoleAsync(appUser, Roles.Name))
                            await _userManager.AddToRoleAsync(appUser, Roles.Name);
                      else if(!user.IsSelected && await _userManager.IsInRoleAsync(appUser, Roles.Name))
                                await _userManager.RemoveFromRoleAsync(appUser, Roles.Name);
                    }
                }
                 return RedirectToAction("Update",new {id =roleid});
            }
            return View(users);
        }
    }
}
