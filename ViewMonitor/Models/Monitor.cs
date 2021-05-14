using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewMonitor.Models
{
    public class Monitor
    {
        public int MonitorID { get; set; }

        [StringLength(500)]
        [Display(Name = "Nombre Monitor")]
        [Required(ErrorMessage = "Se debe ingresar Nombre del Monitor")]
        public string Nombre { get; set; }

        [StringLength(1000)]
        [Required(ErrorMessage = "Se debe ingresar el procedimiento del monitor")]
        public string Procedimiento { get; set; }

        [StringLength(5000)]
        public string Descripcion { get; set; }

        [Display(Name = "Monitor Activo")]
        public bool Activo { get; set; }

        [Display(Name = "Alerta Monitor")]
        public bool Alerta { get; set; }

        [Display(Name = "Job Ejecución")]
        [Required(ErrorMessage = "Seleccionar el job donde se ejecuta el monitor")]
        public int Job_MonitorID { get; set; }

        public Job_Monitor Job_Monitor { get; set; }

        [Display(Name = "Agrupación")]
        public int AgrupacionID { get; set; }

        public Agrupacion Agrupacion { get; set; }

        public List<Monitor_Estado_Hist> Monitor_Estado_Hists { get; set; }

        public Monitor_Estado Monitor_Estado { get; set; }
    }
}