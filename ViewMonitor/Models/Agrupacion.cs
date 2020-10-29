using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewMonitor.Models
{
    public class Agrupacion
    {
        public int AgrupacionID { get; set; }

        [StringLength(1000)]
        [Required(ErrorMessage = "Se requiere ingresar el nombre del Grupo")]
        public string Nombre { get; set; }
        public bool Activo {get;set;}
        public List<Monitor> Monitors { get; set; }
    }
}
