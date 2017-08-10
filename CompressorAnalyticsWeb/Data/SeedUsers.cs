using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CompressorAnalyticsWeb.Models;



namespace CompressorAnalyticsWeb.Data
{
    public class SeedUsers
    {
        public async static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<Data.ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<Models.ApplicationUser>>();

            /// Seed Users
            if(context.Users.Count() <= 0)
            {
                var user = new Models.ApplicationUser();
                user.Email = "dan.young12@gmail.com";
                user.EmailConfirmed = true;
                user.UserName = "dan.young12@gmail.com";
                var x = await userManager.CreateAsync(user, "Dyoung01!");
                await context.SaveChangesAsync();
            }


            /// Seed Device
            if(context.Devices.Count() <= 0)
            {
                ApplicationUser user = await userManager.FindByEmailAsync("dan.young12@gmail.com");
                Device device = new Device()
                {
                    deviceId = "danspi",
                    dataTransferInterval = 2000,
                    userId = user.Id,
                    refrigerantType = "R22",
                    deviceKey = "DU16pNDxrTsRmhC7/sIgDQO3j6n+HvZamxWqkToPogg="
            };

                context.Devices.Add(device);
                await context.SaveChangesAsync();
            }
 



        }
    }
}
