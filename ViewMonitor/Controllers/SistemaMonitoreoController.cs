using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ViewMonitor.Data;
using ViewMonitor.Metodos.SistemaMonitoreo;

namespace ViewMonitor.Controllers
{
    public class SistemaMonitoreoController : Controller
    {
        ApplicationDbContext _context;
        public SistemaMonitoreoController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> MonitoreoVisual()
        {
            var _model = await new SistemaMonitoreoGet(_context).GetDatosMonitoreo();

            return View(_model);
        }

        public async Task<IActionResult> MantenedorMonitores()
        {
            var _model = await new SistemaMonitoreoGet(_context).GetMantenedorMonitores();

            return View(_model);
        }
    }
}
