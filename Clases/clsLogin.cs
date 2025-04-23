using Examen3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examen3.Clases
{
    public class clsLogin
    {
        public clsLogin()
        {
            loginRespuesta = new LoginRespuesta();
        }
        public DBExamen3Entities dbExamen3 = new DBExamen3Entities();
        public Login login { get; set; }
        public LoginRespuesta loginRespuesta { get; set; }
        public bool ValidarUsuario()
        {
            try
            {
               // clsCypher cifrar = new clsCypher();
                Administrador administrador = dbExamen3.Administradors.FirstOrDefault(u => u.Usuario == login.Usuario);
                if (administrador == null)
                {
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "Administrador no existe";
                    return false;
                }
               // byte[] arrBytesSalt = Convert.FromBase64String(administrador.Salt);
               // string ClaveCifrada = cifrar.HashPassword(login.Clave, arrBytesSalt);
                //login.Clave = ClaveCifrada;
                return true;
            }
            catch (Exception ex)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = ex.Message;
                return false;
            }
        }
        private bool ValidarClave()
        {
            try
            {
                Administrador administrador = dbExamen3.Administradors.FirstOrDefault(u => u.Usuario == login.Usuario && u.Clave == login.Clave);
                if (administrador == null)
                {
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "La clave no coincide";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = ex.Message;
                return false;
            }
        }
        public IQueryable<LoginRespuesta> Ingresar()
        {
            if (ValidarUsuario() && ValidarClave())
            {
                string token = TokenGenerator.GenerateTokenJwt(login.Usuario);
                return from A in dbExamen3.Set<Administrador>()
                       where A.Usuario == login.Usuario &&
                               A.Clave == login.Clave
                       select new LoginRespuesta
                       {
                           Usuario = A.Usuario,
                           Autenticado = true,
                           Nombre = A.NombreCompleto,
                           Token = token,
                           Mensaje = ""
                       };
            }
            else
            {
                List<LoginRespuesta> List = new List<LoginRespuesta>();
                List.Add(loginRespuesta);
                return List.AsQueryable();
            }
        }

    }
}