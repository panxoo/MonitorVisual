using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ViewMonitor.Data;
using ViewMonitor.Metodos.SistemaMonitoreo;
using ViewMonitor.Models.Sistema;
using ViewMonitor.Models.SistemaMonitoreo;

namespace ViewMonitor.Controllers
{
    public class SistemaMonitoreoController : Controller
    {
        private ApplicationDbContext _context;
        private IHostingEnvironment _hostingEnvironment;

        public SistemaMonitoreoController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(SistemaMonitoreoController.MonitoreoVisual), "SistemaMonitoreo");
        }

        public async Task<IActionResult> MonitoreoVisual()
        {
            var _model = await new SistemaMonitoreoGet(_context).GetDatosMonitoreo();

            return View(_model);
        }

        public async Task<IActionResult> MonitoreoVisualDetalle(string id)
        {
            var _model = await new SistemaMonitoreoGet(_context).GetDatosMonitorDetalle(id);

            return PartialView("Shared/_MonitoreVisualDetalle", _model);
        }

        public async Task<IActionResult> MonitoreoVisualCall()
        {
            var _model = await new SistemaMonitoreoGet(_context).GetDatosMonitoreo();

            return PartialView("Shared/_MonitoreoVisualMonitor", _model);
        }

        public async Task<IActionResult> MonitoreoVisualExecJob(string id)
        {
            await new SistemaMonitoreoPost(_context).PostMonitoreoVisualExecJob(id);

            var _model = await new SistemaMonitoreoGet(_context).GetDatosMonitoreo();

            return PartialView("Shared/_MonitoreoVisualMonitor", _model);
        }

        public async Task<IActionResult> MantenedorMonitores()
        {
            var _model = await new SistemaMonitoreoGet(_context).GetMantenedorMonitores();

            return View(_model);
        }

        [HttpPost]
        public async Task<IActionResult> MantenedorMonitoresEdit(MonitorVisualInputPost _model)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new RetornoActionView { mnsj = "Falta llenar los campos obligatorios" });
            }
            RetornoAccion rt = await new SistemaMonitoreoPost(_context).PostMantenedorMonitoresEdit(_model);

            switch (rt.Code)
            {
                case 0:
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return PartialView("Shared/_MantenedorMonitoresMonitores", rt.Parametro);

                case 1:
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new RetornoActionView { mnsj = rt.Mensaje });

                default:
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new RetornoActionView
                    {
                        redirectToUrl = Url.Action(nameof(SistemaMonitoreoController.MantenedorMonitores), "SistemaMonitoreo"),
                        redir = true,
                        mnsj = string.IsNullOrEmpty(rt.Mensaje) ? "Error en el registro, volver abrir pantalla para registro." : rt.Mensaje
                    });
            }
        }

        public async Task<IActionResult> MantenedorParametros()
        {
            var _model = await new SistemaMonitoreoGet(_context).GetMantenedorParametros();

            return View(_model);
        }

        [HttpPost]
        public async Task<IActionResult> MantenedorJobEdit(MantenedorJobInputPost _model)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new RetornoActionView { mnsj = "Falta llenar los campos obligatorios" });
            }
            RetornoAccion rt = await new SistemaMonitoreoPost(_context).PostMantenedorJobEdit(_model);

            switch (rt.Code)
            {
                case 0:
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return PartialView("Shared/_MantenedorParametro_Job", rt.Parametro);

                case 1:
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new RetornoActionView { mnsj = rt.Mensaje });

                default:
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new RetornoActionView
                    {
                        redirectToUrl = Url.Action(nameof(SistemaMonitoreoController.MantenedorParametros), "SistemaMonitoreo"),
                        redir = true,
                        mnsj = string.IsNullOrEmpty(rt.Mensaje) ? "Error en el registro, volver abrir pantalla para registro." : rt.Mensaje
                    });
            }
        }

        [HttpPost]
        public async Task<IActionResult> MantenedorAgrupacionEdit(MantenedorAgrupacionInputPost _model)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new RetornoActionView { mnsj = "Falta llenar los campos obligatorios" });
            }
            RetornoAccion rt = await new SistemaMonitoreoPost(_context).PostMantenedorAgrupacionEdit(_model);

            switch (rt.Code)
            {
                case 0:
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return PartialView("Shared/_MantenedorParametro_Agrupacion", rt.Parametro);

                case 1:
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new RetornoActionView { mnsj = rt.Mensaje });

                default:
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new RetornoActionView
                    {
                        redirectToUrl = Url.Action(nameof(SistemaMonitoreoController.MantenedorParametros), "SistemaMonitoreo"),
                        redir = true,
                        mnsj = string.IsNullOrEmpty(rt.Mensaje) ? "Error en el registro, volver abrir pantalla para registro." : rt.Mensaje
                    });
            }
        }

        [HttpPost]
        public async Task<IActionResult> MantenedorAgrupacionDel(int id)
        {
            RetornoAccion rt = await new SistemaMonitoreoPost(_context).PostMantenedorAgrupacionDel(id);

            switch (rt.Code)
            {
                case 0:
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return PartialView("Shared/_MantenedorParametro_Agrupacion", rt.Parametro);

                case 1:
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new RetornoActionView { mnsj = rt.Mensaje });

                default:
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new RetornoActionView
                    {
                        redirectToUrl = Url.Action(nameof(SistemaMonitoreoController.MantenedorParametros), "SistemaMonitoreo"),
                        redir = true,
                        mnsj = string.IsNullOrEmpty(rt.Mensaje) ? "Error en el registro, volver abrir pantalla para registro." : rt.Mensaje
                    });
            }
        }

        [HttpPost]
        public async Task<IActionResult> MantenedorJobDel(int id)
        {
            RetornoAccion rt = await new SistemaMonitoreoPost(_context).PostMantenedorJobsDel(id);

            switch (rt.Code)
            {
                case 0:
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return PartialView("Shared/_MantenedorParametro_Job", rt.Parametro);

                case 1:
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new RetornoActionView { mnsj = rt.Mensaje });

                default:
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new RetornoActionView
                    {
                        redirectToUrl = Url.Action(nameof(SistemaMonitoreoController.MantenedorParametros), "SistemaMonitoreo"),
                        redir = true,
                        mnsj = string.IsNullOrEmpty(rt.Mensaje) ? "Error en el registro, volver abrir pantalla para registro." : rt.Mensaje
                    });
            }
        }

        public async Task<IActionResult> ReporteHistoricoMonitor()
        {
            var _model = await new SistemaMonitoreoGet(_context).GetReporteHistoricoMonitor();

            return View(_model);
        }

        public async Task<IActionResult> ReporteHistoricoMonitorFilter(DateTime fechaIni, DateTime fechaFin, int id)
        {
            var _model = await new SistemaMonitoreoGet(_context).GetReporteHistoricoMonitorDt(fechaIni, fechaFin, id);

            Response.StatusCode = (int)HttpStatusCode.OK;

            return PartialView("Shared/_ReporteHistoricoMonitorDatos", _model);
        }

        [HttpPost]
        public async Task<IActionResult> ExportHistMonit(DateTime FechaIni, DateTime FechaFin, int MonitorId)
        {
            var _model = await new SistemaMonitoreoGet(_context).GetReporteHistoricoMonitorDt(FechaIni, FechaFin, MonitorId);

            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = @"Reporte Historico Monitor.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                var _dr = new GenereacionReporte(_context).GeneracionExcelHistoricoEstado(_model);
                _dr.Write(fs);
            }

            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            Response.StatusCode = (int)HttpStatusCode.OK;

            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
        }

        [HttpPost]
        public async Task<IActionResult> ReporteAddFalsoPositivo(ReporteHistoricoFPPost _model)
        {
            if (_model.Ids.Count == 0)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new RetornoActionView { mnsj = "Se debe seleccionar por lo menois un caso" });
            }

            RetornoAccion rt = await new SistemaMonitoreoPost(_context).PostReporteHistoricoFP(_model);

            switch (rt.Code)
            {
                case 0:
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return Json(new RetornoActionView());

                case 1:
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new RetornoActionView { mnsj = rt.Mensaje });

                default:
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new RetornoActionView
                    {
                        redirectToUrl = Url.Action(nameof(SistemaMonitoreoController.MantenedorParametros), "SistemaMonitoreo"),
                        redir = true,
                        mnsj = string.IsNullOrEmpty(rt.Mensaje) ? "Error en el registro, volver abrir pantalla para registro." : rt.Mensaje
                    });
            }
        }
    }
}