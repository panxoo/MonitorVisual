using System;

namespace ViewMonitor.Models
{
    public class Monitor_Estado_Ultimo
    {
        public int Monitor_Estado_UltimoID {get;set;}
        public string PeriodoError {get; set;}
        public DateTime FechaEstado {get;set;} 
        public int MonitorID { get; set; }
        public Monitor Monitor { get; set; }
    }
}