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
    public class ManageDeviceViewModel
    {
       
        public Device Device { get; set; }

        public ICollection<DataLog> DataLogs { get; set; }

        public chartData refridgerantData { get; set; }
        public chartData powerData { get; set; }
        public chartData airData { get; set; }

        public List<string> labels { get; set; }
    }

}
