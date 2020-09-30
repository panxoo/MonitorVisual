using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewMonitor.Models.SistemaMonitoreo
{
    public class MonitorVisualInput
    {

        public List<MonitoresEstado> MonitoresVisual { get; set; }
        public List<Agrupacion> Agrupaciones { get; set; }

        public class MonitoresEstado
        {
            public int MonitorID { get; set; }
            public string MonitorNom { get; set; }
            public string JobNom { get; set; }
            public bool MonitorEstado { get; set; } 
            public int Agrupacion { get; set; }
            public bool Alarma { get; set; }
        }
    }


}
