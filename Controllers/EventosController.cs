using Examen3.Clases;
using Examen3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Examen3.Controllers
{
    [RoutePrefix("api/Eventos")]
    public class EventosController : ApiController
    {
        [HttpGet]
        [Route("ConsultarTodos")]
        public List<Evento> ConsultarTodas()
        {
            clsEvento clsEvento = new clsEvento();
            return clsEvento.ConsultarTodas();
        }

        [HttpGet]
        [Route("ConsultarXtipo")]
        public Evento Consultar(string tipo)
        {
            clsEvento clsEvento = new clsEvento();
            return clsEvento.ConsultarXtipo(tipo);
        }

        [HttpGet]
        [Route("ConsultarXNombre")]
        public Evento ConsultarXNombre(string nombre)
        {
            clsEvento clsEvento = new clsEvento();
            return clsEvento.ConsultarXNombre(nombre);
        }

        [HttpGet]
        [Route("ConsultarXFecha")]
        public Evento ConsultarXFecha(DateTime fecha)
        {
            clsEvento clsEvento = new clsEvento();
            return clsEvento.ConsultarXFecha(fecha);
        }

        [HttpPost]
        [Route("ConsultarXTNF")]
        public Evento ConsultarXTNF([FromBody] FiltroEvento filtro)
        {
            var db = new DBExamen3Entities();
            return db.Eventos.FirstOrDefault(v =>
                v.TipoEvento == filtro.Tipo &&
                v.NombreEvento == filtro.Nombre &&
                v.FechaEvento == filtro.Fecha);
        }

        [HttpPost]
        [Route("Insertar")]
        [Authorize]
        public string Insertar([FromBody] Evento evento)
        {
            using (var db = new DBExamen3Entities())
            {
                if (!db.Administradors.Any(a => a.idAdministrador == evento.idAdministrador))
                {
                    return "Error: The specified idAdministrador does not exist.";
                }
            }
            clsEvento clsEvento = new clsEvento();
            clsEvento.evento = evento;
            return clsEvento.Insertar();
        }

        [HttpPut]
        [Route("Actualizar")]
        [Authorize]
        public string Actualizar([FromBody] Evento evento)
        {
            clsEvento clsEvento = new clsEvento();
            clsEvento.evento = evento;
            return clsEvento.Actualizar();
        }

        [HttpDelete]
        [Route("Eliminar")]
        [Authorize]
        public string Eliminar([FromBody] Evento evento)
        {
            clsEvento clsEvento = new clsEvento();
            clsEvento.evento = evento;
            return clsEvento.Eliminar();
        }

    }
}

public class FiltroEvento
{
    public string Tipo { get; set; }
    public string Nombre { get; set; }
    public DateTime Fecha { get; set; }
}