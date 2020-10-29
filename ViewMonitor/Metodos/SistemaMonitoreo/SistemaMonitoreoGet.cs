using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewMonitor.Data;
using ViewMonitor.Models;
using ViewMonitor.Models.SistemaMonitoreo;

namespace ViewMonitor.Metodos.SistemaMonitoreo
{
    public class SistemaMonitoreoGet
    {
        public SistemaMonitoreoGet(ApplicationDbContext context)
        {
            _context = context;
        }

        private ApplicationDbContext _context;

        public async Task<MonitorVisualInput> GetDatosMonitoreo()
        {
            MonitorVisualInput _model = new MonitorVisualInput();

            _model.MonitoresVisual = await _context.Monitors.Where(w => w.Activo).Select(s => new MonitorVisualInput.MonitoresEstado
            {
                MonitorID = s.MonitorID,
                MonitorNom = s.Nombre,
                MonitorEstado = s.Monitor_Estado.Estado,
                Agrupacion = s.AgrupacionID,
                Alarma = s.Alerta
            }).OrderBy(o => o.MonitorNom).ToListAsync();

            _model.Agrupaciones = await _context.Agrupacions.Where(w => w.Activo).ToListAsync();

            return _model;
        }

         public async Task<MonitorVisualInput.MonitorEstadoDetalle> GetDatosMonitorDetalle(string id)
        {
            MonitorVisualInput.MonitorEstadoDetalle _model = new MonitorVisualInput.MonitorEstadoDetalle();
         

            _model = await _context.Monitors.Where(w => w.MonitorID.Equals(Convert.ToInt32(id))).Select(s => new MonitorVisualInput.MonitorEstadoDetalle
            {
                MonitorID = s.MonitorID,
                MonitorNom = s.Nombre,
                MonitorEstado = s.Monitor_Estado.Estado,
                 MonitorDescripcion = s.Descripcion,
                Alarma = s.Alerta,
                FechaHistEst = s.Monitor_Estado.Fecha,
                Procedimiento = s.Procedimiento
            }).FirstOrDefaultAsync();
          
            return _model;
        }

        public async Task<MantenedorMonitoreInput> GetMantenedorMonitores()
        {
            MantenedorMonitoreInput _model = new MantenedorMonitoreInput();

            _model.MonitoresDt = await  _context.Monitors.Select(s => new MantenedorMonitoreInput.MonitoresDatos
            {
                MonitorID = s.MonitorID,
                Nombre = s.Nombre,
                Activo = s.Activo,
                AgrupacionID = s.AgrupacionID,
                AgrupacionName = s.Agrupacion.Nombre,
                Alerta = s.Alerta,
                JobName = s.Job_Monitor.Nombre,
                Job_MonitorID = s.Job_MonitorID,
                Procedimiento = s.Procedimiento,
                Descripcion = s.Descripcion
            }).OrderByDescending(o =>  o.Activo).ThenBy(t => t.Nombre).ToListAsync();

            _model.Agrupacions = await _context.Agrupacions.Where(w => w.Activo).Select(s => new SelectListItem { Value = s.AgrupacionID.ToString(), Text = s.Nombre }).ToListAsync();
            _model.Job_Monitors = await _context.Job_Monitors.Where(w => w.Activo).Select(s => new SelectListItem { Value = s.Job_MonitorID.ToString(), Text = s.Nombre }).ToListAsync();

            return _model;
        }


        public async Task<MantenedorParametrosInput> GetMantenedorParametros()
        {
            MantenedorParametrosInput _model = new MantenedorParametrosInput();

            _model.AgrupacionDt = await GetMantenedorAgrupacion();
            _model.Job_MonitorDt = await GetMantenedorJobs();

            return _model;
        }

        public async Task<List<Agrupacion>> GetMantenedorAgrupacion()
        {
            return await _context.Agrupacions.OrderByDescending(o => o.Activo).ThenBy(o => o.Nombre).ToListAsync();
        }

        public async Task<List<Job_Monitor>> GetMantenedorJobs()
        {
            return await _context.Job_Monitors.OrderByDescending(o => o.Activo).ThenBy(o => o.Nombre).ToListAsync();
        }



    }
}
