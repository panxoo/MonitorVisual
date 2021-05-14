using System;

namespace ViewMonitor.Models
{
    public class Monitor_Estado
    {
        public int Monitor_EstadoID { get; set; }
        public bool Estado { get; set; }
        public DateTime Fecha { get; set; }
        public int MonitorID { get; set; }
        public Monitor Monitor { get; set; }
    }
}