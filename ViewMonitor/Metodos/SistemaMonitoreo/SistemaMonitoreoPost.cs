using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using ViewMonitor.Data;
using ViewMonitor.Models;
using ViewMonitor.Models.Sistema;
using ViewMonitor.Models.SistemaMonitoreo;
using ViewMonitor.Metodos.Sistema;

namespace ViewMonitor.Metodos.SistemaMonitoreo
{
    public class SistemaMonitoreoPost
    {
        private ApplicationDbContext _context;

        public SistemaMonitoreoPost(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RetornoAccion> PostMantenedorMonitoresEdit(MonitorVisualInputPost _model)
        {
            RetornoAccion retornoAccion = new RetornoAccion { Code = 0 };

            if (_model.Accion == "4d6f64")
            {
                if (await _context.Monitors.AnyAsync(a => !a.MonitorID.Equals(_model.MonitorID) && (a.Nombre.Equals(_model.Nombre.Trim()) || a.KeyMonitorProce.Equals(_model.KeyMonitorProce))))
                {
                    retornoAccion = new RetornoAccion { Code = 1, Mensaje = "El nombre del monitor o la Key de monitor ya se encuentra registrado, ingresar uno nuevo." };
                }
            }
            else
            {
                if (await _context.Monitors.AnyAsync(a => a.Nombre.Equals(_model.Nombre.Trim()) || a.KeyMonitorProce.Equals(_model.KeyMonitorProce)))
                {
                    retornoAccion = new RetornoAccion { Code = 1, Mensaje = "El nombre del monitor o la Key de monitor ya se encuentra registrado, ingresar uno nuevo." };
                }
            }

            if (!await _context.Agrupacions.AnyAsync(a => a.AgrupacionID.Equals(_model.AgrupacionID)) || !await _context.Job_Monitors.AnyAsync(a => a.Job_MonitorID.Equals(_model.Job_MonitorID)))
            {
                retornoAccion = new RetornoAccion { Code = 2, Mensaje = "Error de pagina volver a ingresar." };
            }

            if (retornoAccion.Code == 0)
            {
                try{

               
                    if (_model.Accion == "4d6f64")
                    {
                        Monitor _dt = await _context.Monitors.FirstOrDefaultAsync(f => f.MonitorID.Equals(_model.MonitorID));

                        _dt.Nombre = _model.Nombre.Trim();
                        _dt.Descripcion = string.IsNullOrEmpty(_model.Descripcion) ? "" : _model.Descripcion.Trim();
                        _dt.Activo = _model.Activo;
                        _dt.AgrupacionID = _model.AgrupacionID;
                        _dt.Alerta = _model.Alerta;
                        _dt.Job_MonitorID = _model.Job_MonitorID;
                        _dt.Procedimiento = _model.Procedimiento.Trim();
                        _dt.KeyMonitorProce = _model.KeyMonitorProce;

                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        Monitor _dt = new Monitor
                        {
                            Nombre = _model.Nombre.Trim(),
                            Descripcion = string.IsNullOrEmpty(_model.Descripcion) ? "" : _model.Descripcion.Trim(),
                            Activo = _model.Activo,
                            AgrupacionID = _model.AgrupacionID,
                            Alerta = _model.Alerta,
                            Job_MonitorID = _model.Job_MonitorID,
                            Procedimiento = _model.Procedimiento.Trim(),
                            Monitor_Estado = new Monitor_Estado
                            {
                                Estado = true,
                                Fecha = DateTime.Now
                            },
                            KeyMonitorProce = _model.KeyMonitorProce,
                            Monitor_Estado_Hists = new System.Collections.Generic.List<Monitor_Estado_Hist>(),
                            Monitor_Estado_Ultimo = new Monitor_Estado_Ultimo
                            {
                                FechaEstado = DateTime.Now                                
                            }
                        };

                        _dt.Monitor_Estado_Hists.Add(new Monitor_Estado_Hist{ estado = true, FalsoPositivo = false, Fecha= DateTime.Now});

                        await _context.AddAsync(_dt);
                        await _context.SaveChangesAsync();
                    }

                    retornoAccion.Parametro = await new SistemaMonitoreoGet(_context).GetMantenedorMonitores();
                }
                catch(Exception ex){
                    retornoAccion = new RetornoAccion { Code = 1, Mensaje = ex.Message };
                }
            }

            return retornoAccion;
        }

        public async Task<RetornoAccion> PostMantenedorJobEdit(MantenedorJobInputPost _model)
        {
            RetornoAccion retornoAccion = new RetornoAccion { Code = 0 };

            if (_model.Accion == "4d6f64")
            {
                if (await _context.Job_Monitors.AnyAsync(a => !a.Job_MonitorID.Equals(_model.Job_MonitorID) && a.Nombre.Equals(_model.Nombre.Trim())))
                {
                    retornoAccion = new RetornoAccion { Code = 1, Mensaje = "El job ya se encuentra creada, ingresar un job distinto." };
                }
            }
            else
            {
                if (await _context.Job_Monitors.AnyAsync(a => a.Nombre.Equals(_model.Nombre.Trim())))
                {
                    retornoAccion = new RetornoAccion { Code = 1, Mensaje = "El job ya se encuentra creada, ingresar un job distinto." };
                }
            }

            if (retornoAccion.Code == 0)
            {
                if (_model.Accion == "4d6f64")
                {
                    Job_Monitor _dt = await _context.Job_Monitors.FirstOrDefaultAsync(f => f.Job_MonitorID.Equals(_model.Job_MonitorID));
                    _dt.Nombre = _model.Nombre.Trim();

                    await _context.SaveChangesAsync();
                }
                else
                {
                    Job_Monitor _dt = new Job_Monitor
                    {
                        Nombre = _model.Nombre
                    };

                    await _context.AddAsync(_dt);
                    await _context.SaveChangesAsync();
                }
                retornoAccion.Parametro = await new SistemaMonitoreoGet(_context).GetMantenedorJobs();
            }

            return retornoAccion;
        }

        public async Task<RetornoAccion> PostMantenedorAgrupacionEdit(MantenedorAgrupacionInputPost _model)
        {
            RetornoAccion retornoAccion = new RetornoAccion { Code = 0 };

            if (_model.Accion == "4d6f64")
            {
                if (await _context.Agrupacions.AnyAsync(a => !a.AgrupacionID.Equals(_model.AgrupacionID) && a.Nombre.Equals(_model.Nombre.Trim())))
                {
                    retornoAccion = new RetornoAccion { Code = 1, Mensaje = "El Grupo ya se encuentra creada, ingresar un job distinto." };
                }
            }
            else
            {
                if (await _context.Agrupacions.AnyAsync(a => a.Nombre.Equals(_model.Nombre.Trim())))
                {
                    retornoAccion = new RetornoAccion { Code = 1, Mensaje = "El Grupo ya se encuentra creada, ingresar un job distinto." };
                }
            }

            if (retornoAccion.Code == 0)
            {
                if (_model.Accion == "4d6f64")
                {
                    Agrupacion _dt = await _context.Agrupacions.FirstOrDefaultAsync(f => f.AgrupacionID.Equals(_model.AgrupacionID));
                    _dt.Nombre = _model.Nombre.Trim();

                    await _context.SaveChangesAsync();
                }
                else
                {
                    Agrupacion _dt = new Agrupacion
                    {
                        Nombre = _model.Nombre
                    };

                    await _context.AddAsync(_dt);
                    await _context.SaveChangesAsync();
                }
                retornoAccion.Parametro = await new SistemaMonitoreoGet(_context).GetMantenedorAgrupacion();
            }

            return retornoAccion;
        }

        public async Task PostMonitoreoVisualExecJob(string id)
        {
            try
            {
                string job = await _context.Monitors.Where(w => w.MonitorID.Equals(Convert.ToInt32(id))).Select(s => s.Procedimiento).FirstOrDefaultAsync();

                await _context.Database.ExecuteSqlCommandAsync("exec " + job);
            }
            catch (Exception ex)
            {
                VariablesLog.logger.Info("Error Ejecutar procedimiento :  " + ex.Message);
            }
        }

        public async Task<RetornoAccion> PostMantenedorAgrupacionDel(int id)
        {
            RetornoAccion retornoAccion = new RetornoAccion { Code = 0 };
            Agrupacion _dtAgrp = await _context.Agrupacions.FirstOrDefaultAsync(f => f.AgrupacionID.Equals(id));

            if (_dtAgrp.Activo)
            {
                if (await _context.Monitors.AnyAsync(a => a.AgrupacionID.Equals(id) && a.Activo))
                {
                    retornoAccion = new RetornoAccion { Code = 1, Mensaje = "No se puede desactivar, hay monitores activos asignados al grupo" };
                }
            }

            if (retornoAccion.Code.Equals(0))
            {
                _dtAgrp.Activo = !_dtAgrp.Activo;
                await _context.SaveChangesAsync();
                retornoAccion.Parametro = await new SistemaMonitoreoGet(_context).GetMantenedorAgrupacion();
            }
            return retornoAccion;
        }

        public async Task<RetornoAccion> PostMantenedorJobsDel(int id)
        {
            RetornoAccion retornoAccion = new RetornoAccion { Code = 0 };
            Job_Monitor _dtJob = await _context.Job_Monitors.FirstOrDefaultAsync(f => f.Job_MonitorID.Equals(id));

            if (_dtJob.Activo)
            {
                if (await _context.Monitors.AnyAsync(a => a.Job_MonitorID.Equals(id) && a.Activo))
                {
                    retornoAccion = new RetornoAccion { Code = 1, Mensaje = "No se puede desactivar, hay monitores activos asignados al JOB" };
                }
            }

            if (retornoAccion.Code.Equals(0))
            {
                _dtJob.Activo = !_dtJob.Activo;
                await _context.SaveChangesAsync();
                retornoAccion.Parametro = await new SistemaMonitoreoGet(_context).GetMantenedorJobs();
            }
            return retornoAccion;
        }

        public async Task<RetornoAccion> PostReporteHistoricoFP(ReporteHistoricoFPPost _model)
        {
            RetornoAccion retornoAccion = new RetornoAccion { Code = 0 };

            foreach (int id in _model.Ids)
            {
                Monitor_Estado_Hist mh = await _context.Monitor_Estado_Hists.FirstOrDefaultAsync(f => f.Monitor_Estado_HistID.Equals(id));
                mh.FalsoPositivo = _model.FalsoPositivo;
                mh.Nota = _model.Observacion;
                await _context.SaveChangesAsync();
            }

            return retornoAccion;
        }
    }
}