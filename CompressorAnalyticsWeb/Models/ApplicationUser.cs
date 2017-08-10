using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompressorAnalyticsWeb.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Secondary Email")]
        public string secondaryEmail { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Secondary Phone #")]
        public string secondaryPhoneNumber { get; set; }

        public ICollection<Device> Devices { get; set; }


    }

   
}
