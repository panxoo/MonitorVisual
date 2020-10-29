using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewMonitor.Models
{
    public class Job_Monitor
    {
        public int Job_MonitorID { get; set; }

        [StringLength(1000)]
        [Required(ErrorMessage = "Se requiere ingresar el nombre del Job")]

        public string Nombre { get; set; }
        public bool Activo {get;set;}
        public List<Monitor> Monitors { get; set; }
    }
}
