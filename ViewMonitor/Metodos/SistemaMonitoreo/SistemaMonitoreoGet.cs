using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
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
            }).ToListAsync();

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
                FechaHistEst = s.Monitor_Estado_Ultimo.FechaEstado,
                Procedimiento = s.Procedimiento,
                UltimoErrorPeriodo = string.IsNullOrEmpty(s.Monitor_Estado_Ultimo.PeriodoError) ? "No ha presentado Errores." : s.Monitor_Estado_Ultimo.PeriodoError,
                UltimaRevision = s.Monitor_Estado.Fecha
            }).FirstOrDefaultAsync();

            return _model;
        }

        public async Task<MantenedorMonitoreInput> GetMantenedorMonitores()
        {
            MantenedorMonitoreInput _model = new MantenedorMonitoreInput();

            _model.MonitoresDt = await _context.Monitors.Select(s => new MantenedorMonitoreInput.MonitoresDatos
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
                Descripcion = s.Descripcion,
                KeyMonitorProce = s.KeyMonitorProce
            }).OrderByDescending(o => o.Activo).ThenBy(t => t.Nombre).ToListAsync();

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

        public async Task<ReporteHistoricoInput> GetReporteHistoricoMonitor()
        {
            ReporteHistoricoInput _model = new ReporteHistoricoInput();

            _model.FechaIni = DateTime.Today.AddMonths(-3);
            _model.FechaFin = DateTime.Today;
            _model.Monitores = await _context.Monitors.Where(w => w.Activo).Select(s => new SelectListItem { Value = s.MonitorID.ToString(), Text = s.Nombre }).OrderBy(o => o.Text).ToListAsync();

            _model.ReporteHistorials = new List<ViewHistEstadoMonitor>();

            return _model;
        }

        public async Task<List<ViewHistEstadoMonitor>> GetReporteHistoricoMonitorDt(DateTime fechaIni, DateTime fechaFin, int monit)
        {
            List<ViewHistEstadoMonitor> _model = new List<ViewHistEstadoMonitor>();

                if (monit == -1)

                    _model = await _context.ViewHistEstadoMonitors.Where(w => w.FechaError.Date >= fechaIni.Date && w.FechaError.Date <= fechaFin.Date)
                                                                .OrderByDescending(o => o.FechaError).ThenBy(o => o.Nombre).ToListAsync();
                else

                    _model = await _context.ViewHistEstadoMonitors.Where(w => w.FechaError.Date >= fechaIni.Date && w.FechaError.Date <= fechaFin.Date && w.MonitorID.Equals(monit))
                                                                .OrderByDescending(o => o.FechaError).ThenBy(o => o.Nombre).ToListAsync();
          
            return _model;
        }
    }
}