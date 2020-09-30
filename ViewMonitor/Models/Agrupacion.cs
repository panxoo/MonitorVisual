using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViewMonitor.Models
{
    public class Agrupacion
    {
        public int AgrupacionID { get; set; }
        [StringLength(1000)]
        public string Nombre { get; set; }
        public List<Monitor> Monitors { get; set; }
    }
}
