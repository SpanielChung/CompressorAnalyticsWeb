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
            DateTime start = DateTime.UtcNow.AddDays(-1);
            var dataLogs = db.DataLogs.Where(x => x.deviceId == id && x.timeStamp >= start).OrderBy(x => x.timeStamp).Take(10).ToListAsync();

            model.Device = await device;
            model.DataLogs = await dataLogs;

            return View(model);

        }

        [HttpGet]
        public async Task<JsonResult> GetTodaysPower(string deviceId)
        {
            DateTime start = DateTime.UtcNow.AddHours(-12);
            int year = start.Year;
            int month = start.Month;
            //  Average all results over the minute
            List<DateTime> timeFrame = new List<DateTime>();

            var query = (from i in db.DataLogs
                                 where i.timeStamp >= start && i.deviceId == deviceId
                                 group i by new {i.timeStamp.Day, i.timeStamp.Hour, i.timeStamp.Minute } into grp
                                 select new
                                 {
                                     day = grp.Key.Day,
                                     hour = grp.Key.Hour,
                                     minute = grp.Key.Minute,
                                     compressorCurrent = grp.Average(x => x.compressorCurrent),
                                     fanCurrent = grp.Average(x=>x.fanCurrent)
                                 }).ToListAsync();

            for (int i = 1; i <= 720;i++)
            {
                timeFrame.Add(start.AddMinutes(i));
            }

            var results = await query;

            var data = (from t in timeFrame
                        join i in results on new { x = t.Day, y = t.Hour, z = t.Minute } equals new { x = i.day, y = i.hour, z = i.minute } into joined
                        from j in joined.DefaultIfEmpty()
                        select new
                        {
                            timeStamp = new DateTime(year, month, t.Day, t.Hour, t.Minute, 0),
                            compressorCurrent = j == null? 0 : j.compressorCurrent,
                            fanCurrent = j == null? 0 : j.fanCurrent
                        }).ToList();

            chartData powerChart = new chartData();
            dataset set = new dataset();
            powerChart.labels = data.Select(x => x.timeStamp.ToString()).ToList();
            set = new dataset() { label = "Compressor Current", data = data.Select(x=>x.compressorCurrent).ToList()};
            powerChart.datasets.Add(set);
            set = new dataset() { label = "Fan current", data = data.Select(x => x.fanCurrent).ToList() };
            powerChart.datasets.Add(set);
            return Json(powerChart);
        }

        [HttpGet]
        public async Task<JsonResult> GetTodaysRefrigerant(string deviceId)
        {
            DateTime start = DateTime.UtcNow.AddHours(-12);
            int year = start.Year;
            int month = start.Month;
            //  Average all results over the minute
            List<DateTime> timeFrame = new List<DateTime>();

            var query = (from i in db.DataLogs
                         where i.timeStamp >= start && i.deviceId == deviceId
                         group i by new { i.timeStamp.Day, i.timeStamp.Hour, i.timeStamp.Minute } into grp
                         select new
                         {
                             day = grp.Key.Day,
                             hour = grp.Key.Hour,
                             minute = grp.Key.Minute,
                             suctionTemp = grp.Average(x => x.suctionTemp),
                             compressionTemp = grp.Average(x => x.compressionTemp),
                             condensorTemp = grp.Average(x => x.condensorTemp),
                             expansionTemp = grp.Average(x => x.expansionTemp)
                         }).ToListAsync();

            for (int i = 1; i <= 720; i++)
            {
                timeFrame.Add(start.AddMinutes(i));
            }

            var results = await query;

            var data = (from t in timeFrame
                        join i in results on new { x = t.Day, y = t.Hour, z = t.Minute } equals new { x = i.day, y = i.hour, z = i.minute } into joined
                        from j in joined.DefaultIfEmpty()
                        select new
                        {
                            timeStamp = new DateTime(year, month, t.Day, t.Hour, t.Minute, 0),
                            suctionTemp = j == null? 0 : j.suctionTemp,
                            compressionTemp = j == null ? 0 : j.compressionTemp,
                            condensorTemp = j == null ? 0 : j.condensorTemp,
                            expansionTemp = j == null ? 0 : j.expansionTemp
                        }).ToList();

            chartData powerChart = new chartData();
            dataset set = new dataset();
            powerChart.labels = data.Select(x => x.timeStamp.ToString()).ToList();
            set = new dataset() { label = "Suction Temp", data = data.Select(x => x.suctionTemp).ToList() };
            powerChart.datasets.Add(set);
            set = new dataset() { label = "Compression Temp", data = data.Select(x => x.compressionTemp).ToList() };
            powerChart.datasets.Add(set);
            set = new dataset() { label = "Condensor Temp", data = data.Select(x => x.condensorTemp).ToList() };
            powerChart.datasets.Add(set);
            set = new dataset() { label = "Expansion Temp", data = data.Select(x => x.expansionTemp).ToList() };
            powerChart.datasets.Add(set);
            return Json(powerChart);
        }

        [HttpGet]
        public async Task<JsonResult> GetTodaysAirTemp(string deviceId)
        {
            DateTime start = DateTime.UtcNow.AddHours(-12);
            int year = start.Year;
            int month = start.Month;
            //  Average all results over the minute
            List<DateTime> timeFrame = new List<DateTime>();

            var query = (from i in db.DataLogs
                         where i.timeStamp >= start && i.deviceId == deviceId
                         group i by new { i.timeStamp.Day, i.timeStamp.Hour, i.timeStamp.Minute } into grp
                         select new
                         {
                             day = grp.Key.Day,
                             hour = grp.Key.Hour,
                             minute = grp.Key.Minute,
                             rT = grp.Average(x => x.returnAirTemp),
                             dT = grp.Average(x => x.dischargeAirTemp)
                         }).ToListAsync();

            for (int i = 1; i <= 720; i++)
            {
                timeFrame.Add(start.AddMinutes(i));
            }

            var results = await query;

            var data = (from t in timeFrame
                        join i in results on new { x = t.Day, y = t.Hour, z = t.Minute } equals new { x = i.day, y = i.hour, z = i.minute } into joined
                        from j in joined.DefaultIfEmpty()
                        select new
                        {
                            timeStamp = new DateTime(year, month, t.Day, t.Hour, t.Minute, 0),
                            rT = j == null ? 0 : j.rT,
                            dT = j == null ? 0 : j.dT,
                        }).ToList();

            chartData powerChart = new chartData();
            dataset set = new dataset();
            powerChart.labels = data.Select(x => x.timeStamp.ToString()).ToList();
            set = new dataset() { label = "Return Air Temp", data = data.Select(x => x.rT).ToList() };
            powerChart.datasets.Add(set);
            set = new dataset() { label = "Discharge Air Temp", data = data.Select(x => x.dT).ToList() };
            powerChart.datasets.Add(set);
            return Json(powerChart);
        }

        [HttpGet]
        public async Task<JsonResult> GetTodaysAirHumidity(string deviceId)
        {
            DateTime start = DateTime.UtcNow.AddHours(-12);
            int year = start.Year;
            int month = start.Month;
            //  Average all results over the minute
            List<DateTime> timeFrame = new List<DateTime>();

            var query = (from i in db.DataLogs
                         where i.timeStamp >= start && i.deviceId == deviceId
                         group i by new { i.timeStamp.Day, i.timeStamp.Hour, i.timeStamp.Minute } into grp
                         select new
                         {
                             day = grp.Key.Day,
                             hour = grp.Key.Hour,
                             minute = grp.Key.Minute,
                             rH = grp.Average(x => x.returnAirHumidity),
                             dH = grp.Average(x => x.dischargeAirHumidity)
                         }).ToListAsync();

            for (int i = 1; i <= 720; i++)
            {
                timeFrame.Add(start.AddMinutes(i));
            }

            var results = await query;

            var data = (from t in timeFrame
                        join i in results on new { x = t.Day, y = t.Hour, z = t.Minute } equals new { x = i.day, y = i.hour, z = i.minute } into joined
                        from j in joined.DefaultIfEmpty()
                        select new
                        {
                            timeStamp = new DateTime(year, month, t.Day, t.Hour, t.Minute, 0),
                            rH = j == null ? 0 : j.rH,
                            dH = j == null ? 0 : j.dH,
                        }).ToList();

            chartData powerChart = new chartData();
            dataset set = new dataset();
            powerChart.labels = data.Select(x => x.timeStamp.ToString()).ToList();
            set = new dataset() { label = "Return Air Humidity", data = data.Select(x => x.rH).ToList() };
            powerChart.datasets.Add(set);
            set = new dataset() { label = "Discharge Air Humidity", data = data.Select(x => x.dH).ToList() };
            powerChart.datasets.Add(set);
            return Json(powerChart);
        }
    }
}
