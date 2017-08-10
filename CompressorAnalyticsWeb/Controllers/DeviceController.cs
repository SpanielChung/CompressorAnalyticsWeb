using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CompressorAnalyticsWeb.Data;
using CompressorAnalyticsWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace CompressorAnalyticsWeb.Controllers
{
    //[Authorize]
    public class DeviceController : Controller
    {
        private ApplicationDbContext db;
        public DeviceController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<IActionResult> Index()
        {

            return View(await db.Devices.ToListAsync());
        }

        public async Task<IActionResult> Manage(string id)
        {
            ManageDeviceViewModel model = new ManageDeviceViewModel();

            var device = db.Devices.FindAsync(id);
            var DataLogs = db.DataLogs.Where(x => x.deviceId == id).ToListAsync();

            model.Device = await device;
            model.DataLogs = await DataLogs;

            return View(model);

        }
    }
}
