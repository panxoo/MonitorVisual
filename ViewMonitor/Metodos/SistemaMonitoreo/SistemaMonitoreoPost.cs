using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using ViewMonitor.Data;
using ViewMonitor.Models;
using ViewMonitor.Models.Sistema;
using ViewMonitor.Models.SistemaMonitoreo;
using System.Linq;


namespace ViewMonitor.Metodos.SistemaMonitoreo
{
    public class SistemaMonitoreoPost
    {

        ApplicationDbContext _context; 
        public SistemaMonitoreoPost(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RetornoAccion> PostMantenedorMonitoresEdit(MonitorVisualInputPost _model)
        {

            RetornoAccion retornoAccion = new RetornoAccion { Code = 0 };

            if (_model.Accion == "4d6f64")
            {
                if (await _context.Monitors.AnyAsync(a => !a.MonitorID.Equals(_model.MonitorID) && a.Nombre.Equals(_model.Nombre.Trim())))
                {
                    retornoAccion = new RetornoAccion { Code = 1, Mensaje = "El nombre del monitor ya se encuentra registrado, ingresar uno nuevo." };
                }
            }
            else
            {
                if (await _context.Monitors.AnyAsync(a => a.Nombre.Equals(_model.Nombre.Trim())))
                {
                    retornoAccion = new RetornoAccion { Code = 1, Mensaje = "El nombre del monitor ya se encuentra registrado, ingresar uno nuevo." };
                }
            }

            if (!await _context.Agrupacions.AnyAsync(a => a.AgrupacionID.Equals(_model.AgrupacionID)) || !await _context.Job_Monitors.AnyAsync(a => a.Job_MonitorID.Equals(_model.Job_MonitorID)))
            {
                retornoAccion = new RetornoAccion { Code = 2, Mensaje = "Error de pagna volver a ingresar." };
            }


            if (retornoAccion.Code == 0)
            {
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




                    };

                    await _context.AddAsync(_dt);
                    await _context.SaveChangesAsync();
                }

                retornoAccion.Parametro = await new SistemaMonitoreoGet(_context).GetMantenedorMonitores();
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
            string job = await _context.Monitors.Where(w => w.MonitorID.Equals(Convert.ToInt32(id))).Select(s => s.Procedimiento).FirstOrDefaultAsync();

          var ss =  await _context.Database.ExecuteSqlCommandAsync("exec " + job);
        }
    }
}
