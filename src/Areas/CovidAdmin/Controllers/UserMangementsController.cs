using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LIVESTOCK.Areas.CovidAdmin.Models;
using LIVESTOCK.Areas.CovidAdmin.Models.ViewModel;
using LIVESTOCK.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static LIVESTOCK.Pages.Account.LoginModel;

namespace LIVESTOCK.Areas.CovidAdmin.Controllers
{
    [Area("CovidAdmin")]
    public class UserMangementsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> RoleManager;

        public UserMangementsController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            RoleManager = roleManager;
        }
        public ActionResult ViewUsers(short id)
        {
            ViewBag.LabID = id;
            return View();
        }
        public async Task<IActionResult> Index(short id)
        {
            List<UserInfo> userList = new List<UserInfo>();
            var users = await _userManager.Users.Select(p => new ApplicationUser { UserName = p.UserName, PhoneNumber = p.PhoneNumber,Email=p.Email, LabAccess = p.LabAccess }).ToListAsync();
            InputModel inputModel = new InputModel();
            foreach (var v in users)
            {
                //inputModel = new InputModel();                
                //inputModel.Email = v.Name;                
                
                    if (v.LabAccess == id)
                    {
                        UserInfo Obj = new UserInfo();
                        Obj.Name = v.UserName;                    
                        Obj.Password = v.PhoneNumber;                    
                        Obj.Email = v.Email;                    
                        Obj.LabName = _context.Lab.Find(v.LabAccess).Name; 
                        userList.Add(Obj);
                    }                                                  
            }
            return PartialView(userList);
        }

        [Authorize(Roles = "CovidAdmin")]
        public IActionResult Create(short id)
        {
            ViewBag.LabID = id;
            return View();
        }

        // POST: CovidAdmin/Labs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "CovidAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterLabUser Input, short LabID)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.FullName, Email = Input.Email, PhoneNumber = Input.Password, Role = Input.SectionRole, RegionalAccess = Input.RegionalAccess, LabAccess = Input.LabAccess };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {                   
                    await _userManager.AddToRoleAsync(user, user.Role);
                    return RedirectToAction(nameof(ViewUsers), new { LabID});
                }               
            }
            return View();
        }

    }
}