using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ViewMonitor.Data;
using ViewMonitor.Metodos.SistemaMonitoreo;
using ViewMonitor.Models;
using ViewMonitor.Models.Sistema;
using ViewMonitor.Models.SistemaMonitoreo;

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

        public async Task<IActionResult> MonitoreoVisualDetalle(string id)
        {
            var _model = await new SistemaMonitoreoGet(_context).GetDatosMonitorDetalle(id);

            return PartialView("Shared/_MonitoreVisualDetalle",_model);
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
    }
}
