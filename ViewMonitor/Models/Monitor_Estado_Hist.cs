using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViewMonitor.Models
{
    public class Monitor_Estado_Hist
    {
        public int Monitor_Estado_HistID { get; set; }
       
        public bool estado { get; set; }
        public DateTime Fecha { get; set; }

        public int MonitorID { get; set; }
        public Monitor Monitor { get; set; }
    }
}
