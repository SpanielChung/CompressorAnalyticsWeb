using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompressorAnalyticsWeb.Models
{
    /// <summary>
    /// Systems settings are sent to the cloud when they are changed.
    /// They are then retrieved when the sytem is restarted
    /// </summary>
    public class Device
    {
        [Key]
        [Display(Name = "Device ID")]
        public string deviceId { get; set; }
        public string deviceKey { get; set; }

        //  Location
        [Display(Name = "Latitude (decimal)")]
        public decimal latitude { get; set; }
        [Display(Name = "Longitude (decimal)")]
        public decimal longitude { get; set; }
        [Display(Name = "Altitude")]
        public decimal altitude { get; set; }
        [Display(Name = "Address Line 1")]
        public string addressLine1 { get; set; }
        [Display(Name = "Address Line 2")]
        public string addressLine2 { get; set; }
        [Display(Name = "City")]
        public string city { get; set; }
        [Display(Name = "State")]
        public string state { get; set; }
        [Display(Name = "Zip")]
        public string zip { get; set; }

        

        //  System Config
        [Display(Name = "Refridgerant Type")]
        public string refrigerantType {get;set;}
        [Display(Name = "Compressor Manufacturer")]
        public string compressorManufacturer { get; set; }
        [Display(Name = "Compressor Model")]
        public string compressorModel { get; set; }

        //  System Settings
        [Display(Name ="Data Transfer Interval")]
        public decimal dataTransferInterval { get; set; }


        public string userId { get; set; }
        [ForeignKey("userId")]
        public ApplicationUser User { get; set; }

        public ICollection<DataLog> DataLogs { get; set; }

    }

}
