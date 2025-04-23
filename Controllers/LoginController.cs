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
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("Ingresar")]
        [AllowAnonymous]
        public IQueryable<LoginRespuesta> Ingresar( [FromBody] Login login)
        {
            clsLogin _Login = new clsLogin();
            _Login.login = login;
            return _Login.Ingresar();

        }
    }
}