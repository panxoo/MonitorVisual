using System.Runtime.CompilerServices;
using System;
namespace ViewMonitor.Models
{
    public class ViewHistEstadoMonitor
    {
        public int Monitor_Estado_HistID {get;set;}
        public int ProcesoId {get;set;}
        public int MonitorID {get;set;}
        public string Nombre {get;set;}
        public DateTime FechaError {get;set;}
        public bool Solucion {get;set;}
        public DateTime FechaSolucion {get;set;}
        public bool FalsoPositivo {get;set;}
        public string Nota {get;set;}
    }
}