﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompressorAnalyticsWeb.Models
{
    public class DataLog
    {
        [Key]
        public int id { get; set; }
        public decimal compressorCurrent    {get; set;}        //pC
        public decimal fanCurrent           {get; set;}       //pF
        public decimal returnAirTemp        {get; set;}       //tR
        public decimal dischargeAirTemp     {get; set;}       //tD
        public decimal returnAirHumidity    {get; set;}       //rhR
        public decimal dischargeAirHumidity {get; set;}       //rhD 
        public decimal suctionTemp          {get; set;}       //  temp at inlet to compressor (coolant)
        public decimal compressionTemp      {get; set;}       //  temp at outlet of compressor (coolant)
        public decimal condensorTemp        {get; set;}       //  temp at outlet of condensor (coolant)
        public decimal expansionTemp        {get; set;}       //  temp at outlet of expansion line (coolant)

        public DateTime timeStamp { get; set; }

        public string deviceId { get; set; }
        [ForeignKey("deviceId")]
        public Device Device { get; set; }
        public DataLog()
        {

        }


    }
}
