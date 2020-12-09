using Cliente_Api_Anime.Azure;
using Cliente_Api_Anime.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cliente_Api_Anime.Controllers
{
   
        [Route("api/[controller]")]
        [ApiController]
        public class AnimesController : ControllerBase
        {


            //GET: api/<AnimesController>/all
            [HttpGet("all")]
            public JsonResult ObtenerAnimes()
            {
                var AnimesRecibidos = AnimeAzure.obtenerAnimes();
                return new JsonResult(AnimesRecibidos);
            }

            //GET: api/animes/{1}-{nombre}
            [HttpGet("{id_anime}")]
            public JsonResult ObtenerAnimes(string id_anime)
            {
                var conversionExitosa = int.TryParse(id_anime, out int idConvertido);
                Animes AnimesRecibido;


                if (conversionExitosa)
                {
                    AnimesRecibido = AnimeAzure.ObtenerAnimePorId(idConvertido);
                }
                else
                {
                    AnimesRecibido = AnimeAzure.ObtenerAnimePorNombre(id_anime);
                }
                if (AnimesRecibido is null)
                {
                    return new JsonResult($"Intente nuevamente con un parametro distino a {id_anime}");
                }
                else
                {
                    return new JsonResult(AnimesRecibido);
                }


            }

            [HttpPost]
            public void AgregarAnimes([FromBody] Animes animes)
            {
                AnimeAzure.AgregarAnimes(animes);
            }

        }
    }
