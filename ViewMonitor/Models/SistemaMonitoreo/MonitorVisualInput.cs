using System;
using System.Collections.Generic;

namespace ViewMonitor.Models.SistemaMonitoreo
{
    public class MonitorVisualInput
    {
        public MonitorVisualInput()
        {
            MonitorEstadoDt = new MonitorEstadoDetalle();
        }

        public List<MonitoresEstado> MonitoresVisual { get; set; }
        public List<Agrupacion> Agrupaciones { get; set; }

        public MonitorEstadoDetalle MonitorEstadoDt { get; set; }

        public class MonitoresEstado
        {
            public int MonitorID { get; set; }
            public string MonitorNom { get; set; }
            public bool MonitorEstado { get; set; }
            public int Agrupacion { get; set; }
            public bool Alarma { get; set; }
        }

        public class MonitorEstadoDetalle
        {
            public int MonitorID { get; set; }
            public string MonitorNom { get; set; }
            public string MonitorDescripcion { get; set; }
            public bool MonitorEstado { get; set; }
            public bool Alarma { get; set; }
            public DateTime FechaHistEst { get; set; }
            public string Procedimiento { get; set; }
            public int KeyMonitorProce {get;set;}            

        }
    }
}