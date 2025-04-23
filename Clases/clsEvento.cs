using Examen3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Examen3.Clases
{
	public class clsEvento
	{
        private DBExamen3Entities dbExamen3 = new DBExamen3Entities();
        public Evento evento { get; set; }

        public string Insertar()
        {
            try
            {
                dbExamen3.Eventos.Add(evento);
                dbExamen3.SaveChanges();
                return "El Evento se agregó correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar el Evento " + ex.Message;
            }
        }

        public Evento Consultar(int Id_eve)
        {
            Evento eve = dbExamen3.Eventos.FirstOrDefault(v => v.idEventos == Id_eve);
            return eve;

        }
        public Evento ConsultarXtipo(string tipoEv)
        {
            Evento eve = dbExamen3.Eventos.FirstOrDefault(v => v.TipoEvento == tipoEv);
            return eve;

        }
        public Evento ConsultarXNombre(string nombre)
        {
            Evento eve = dbExamen3.Eventos.FirstOrDefault(v => v.NombreEvento == nombre);
            return eve;

        }
        public Evento ConsultarXFecha(DateTime fecha)
        {
            Evento eve = dbExamen3.Eventos.FirstOrDefault(v => v.FechaEvento == fecha);
            return eve;
        }
        public Evento ConsultarXTNF(string tipo, string nombre, DateTime fecha)
        {
            Evento eve = dbExamen3.Eventos.FirstOrDefault(v => v.NombreEvento == nombre&&v.NombreEvento==nombre&&v.FechaEvento==fecha);
            return eve;

        }

        public List<Evento> ConsultarTodas()
        {
            return dbExamen3.Eventos.OrderBy(v => v.idEventos).ToList();

        }

        public string Actualizar()
        {
            try
            {
                Evento eve = Consultar(evento.idEventos);
                if (eve != null)
                {
                    dbExamen3.Eventos.AddOrUpdate(evento);
                    dbExamen3.SaveChanges();
                    return "El Evento se actualizó correctamente ";
                }
                else
                {
                    return "El Evento no existe ";
                }
            }
            catch (Exception ex)
            {
                return "Error al actualizar el Evento " + ex.Message;
            }
        }
        public string Eliminar()
        {
            try
            {
                Evento eve = Consultar(evento.idEventos);
                if (eve == null)
                {
                    return "El Evento no existe ";
                }
                else
                {

                    dbExamen3.Eventos.Remove(eve);
                    dbExamen3.SaveChanges();
                    return "El Evento se eliminó correctamente ";
                }
            }
            catch (Exception ex)
            {

                return "Error al eliminar El Evento " + ex.Message;
            }

        }
    }
}