using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ViewMonitor.Models.SistemaMonitoreo
{
    public class ReporteHistoricoInput
    {
        [Display(Name="Fecha Inicial")]
        public DateTime FechaIni { get; set; }
       
       [Display(Name="Fecha Final")]
        public DateTime FechaFin { get; set; }
        public List<SelectListItem> Monitores {get;set;}

       [Display(Name="Filtrar Monitor")]
        public int MonitorId {get;set;}
        public List<ViewHistEstadoMonitor> ReporteHistorials { get; set; }

    }
}