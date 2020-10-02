using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ViewMonitor.Models.SistemaMonitoreo
{
    public class MantenedorMonitoreInput
    {
        public List<MonitoresDatos> MonitoresDt { get; set; }
        public List<SelectListItem> Job_Monitors { get; set; }
        public List<SelectListItem> Agrupacions { get; set; }

        public Monitor MonitorInput { get; set; }


        public class MonitoresDatos : Monitor
        {
            public string JobName { get; set; }
            public string AgrupacionName { get; set; }
        }

    }
}
