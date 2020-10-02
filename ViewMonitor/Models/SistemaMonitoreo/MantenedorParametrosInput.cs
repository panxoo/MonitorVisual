using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewMonitor.Models.SistemaMonitoreo
{
    public class MantenedorParametrosInput
    {
        public List<Agrupacion> AgrupacionDt { get; set; }
        public List<Job_Monitor> Job_MonitorDt { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar el nombre del Grupo")]
        [Display(Name = "Nombre Agrupación")]
        public string AgrupacionInput { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar el nombre del Job")]
        [Display(Name = "Nombre Job")]
        public string Job_MonitorInput { get; set; }
    }
}
