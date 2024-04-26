using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PL.Models;
using System.Collections.Generic;

namespace PL.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UsersController> _logger;

        public UsersController(UserManager<ApplicationUser> userManager, ILogger<UsersController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Go( string SearchValue = "")
        {
            List<ApplicationUser> users;
            if (string.IsNullOrEmpty(SearchValue))
            {
                users =await _userManager.Users.ToListAsync(); ;
               
            }


            else
            {
                users = await _userManager.Users.Where(user=>user.Email.Trim().ToLower().Contains(SearchValue.Trim().ToLower())).ToListAsync();
            }

            return View(users);
        }

        public async Task<IActionResult> Details(string id ,string viewname= "Details")
        {
           
            if (id is null) 
                return NotFound();

            var user= await _userManager.FindByIdAsync(id);

            if (user is null)
                return NotFound();

          return View(viewname,user);
        }
        public async Task<IActionResult> Update(string id)
        {


            return await Details(id, "Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id,ApplicationUser Auser)
        {
            if(id!=Auser.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                var users = await _userManager.FindByIdAsync(id);
                users.UserName = Auser.UserName;
                users.NormalizedUserName = Auser.UserName.ToUpper();
                var result = await _userManager.UpdateAsync(users);

                if (result.Succeeded)
                    return RedirectToAction("Go");
                foreach (var i in result.Errors)
                {
                    _logger.LogError(i.Description);
                    ModelState.AddModelError("", i.Description);
                }
            }

                return View(Auser);
        }
        public async Task<IActionResult> Delete(string id)
        {
       
           
                var users = await _userManager.FindByIdAsync(id);
             
                var result = await _userManager.DeleteAsync(users);
                if (users is null)
                    return NotFound();
                if (result.Succeeded)
                    return RedirectToAction("Go");
                foreach (var i in result.Errors)
                {
                    _logger.LogError(i.Description);
                    ModelState.AddModelError("", i.Description);
                }
            

            return RedirectToAction("Go");
        }
    }
}
