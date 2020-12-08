using Cliente_Api_Anime.Azure;
using Cliente_Api_Anime.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cliente_Api_Anime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        //localhost:8080/api/usuario/all

        //GET: api/<UsuarioController>/all
        [HttpGet("all")]
        public JsonResult ObtenerUsuarios()
        {
            var usuariosRecibidos = UsuarioAzure.ObtenerUsuarios();
            return new JsonResult(usuariosRecibidos);
        }

        //GET: api/usuario/{1}-{nombre}
        [HttpGet("{id_usuario}")]
        public JsonResult ObtenerUsuario(string id_usuario)
        {
            var conversionExitosa = int.TryParse(id_usuario, out int idConvertido);
            Usuario usuarioRecibido;


            if (conversionExitosa)
            {
                usuarioRecibido =  UsuarioAzure.ObtenerUsuarioPorId(idConvertido);
            }
            else
            {
                usuarioRecibido = UsuarioAzure.ObtenerUsuarioPorNombre(id_usuario);
            }

            if (usuarioRecibido is null)
            {
                return new JsonResult($"Intente nuevamente con un parametro distino a {id_usuario}");
            }
            else
            {
                return new JsonResult(usuarioRecibido);
            }

            //POST: api/usuario
            [HttpPost]
            public void AgregarUsuario([FromBody] Usuario usuario)
            {
                UsuarioAzure.AgregarUsuario(usuario);
            }

           
        }




        //// GET: api/<UsuarioController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<UsuarioController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<UsuarioController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<UsuarioController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<UsuarioController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
