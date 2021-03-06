﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartProfil.Data;
using SmartProfil.Models;

namespace SmartProfil.Controllers
{
    public class TestRoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext db;

        public TestRoleController(RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext db)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.db = db;
        }
        public async Task<IActionResult> AddUserToAdmin()
        {
            if (!await this.roleManager.RoleExistsAsync("Admin"))
            {
                await this.roleManager.CreateAsync(new ApplicationRole
                {
                    Name = "Admin"
                });
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.userManager.AddToRoleAsync(user,"Admin");

            return this.Redirect("/");
        }
    }
}
