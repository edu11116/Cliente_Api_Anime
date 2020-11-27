}}using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cliente_Api_Anime.Models
{
    public class Animes
    {
        public int id_anime { get; set; }
        public string nombre_anime { get; set; }
        public string categoria { get; set; }
        public string genero { get; set; }
        public string duracion { get; set; } 
        public int capitulo { get; set; }
        public string calidad { get; set; }
    }
}
