using System;

namespace ViewMonitor.Models
{
    public class Monitor_Estado_Hist
    {
        public int Monitor_Estado_HistID { get; set; }

        public bool estado { get; set; }
        public DateTime Fecha { get; set; }

        public int MonitorID { get; set; }
        public Monitor Monitor { get; set; }
        public bool FalsoPositivo { get; set; }
        public string Nota { get; set; }
    }
}
