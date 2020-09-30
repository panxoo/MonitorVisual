using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
