using System;
using System.Collections.Generic;

namespace ViewMonitor.Models.SistemaMonitoreo
{
    public class ReporteHistoricoFPPost
    {
        public List<int> Ids {  get;  set;  } 
        public Boolean FalsoPositivo {  get;  set;  }
        public string Observacion { get;    set;    }
    }
}