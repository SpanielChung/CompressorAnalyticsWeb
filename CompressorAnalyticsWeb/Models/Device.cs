using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressorAnalyticsWeb
{
    /// <summary>
    /// Systems settings are sent to the cloud when they are changed.
    /// They are then retrieved when the sytem is restarted
    /// </summary>
    public class Device
    {
        public string deviceId { get; set; }

        public string customerPrimaryEmail { get; set; }
        public string customerSecondaryEmail { get; set; }

        public string refrigerantType {get;set;}
        public string compressorManufacturer { get; set; }

        public string compressorModel { get; set; }

        public ICollection<DataLog> DataLogs { get; set; }

    }

}
