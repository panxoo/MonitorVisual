using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewMonitor.Data;
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

            _model.MonitoresVisual = await _context.Monitors.Select(s => new MonitorVisualInput.MonitoresEstado
            {
                MonitorID = s.MonitorID,
                MonitorNom = s.Nombre,
                JobNom = s.Job_Monitor.Nombre,
                MonitorEstado = s.Monitor_Estado.Estado,
                Agrupacion = s.AgrupacionID,
                Alarma = s.Alerta
            }).ToListAsync();

            _model.Agrupaciones = await _context.Agrupacions.ToListAsync();

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
               Descripcion = s.Descripcion
            }).ToListAsync();

            _model.Agrupacions = await _context.Agrupacions.Select(s => new SelectListItem { Value = s.AgrupacionID.ToString(), Text = s.Nombre }).ToListAsync();
            _model.Job_Monitors = await _context.Job_Monitors.Select(s => new SelectListItem { Value = s.Job_MonitorID.ToString(), Text = s.Nombre }).ToListAsync();

            return _model;
        }


    }
}
