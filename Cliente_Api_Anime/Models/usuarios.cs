using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cliente_Api_Anime.Models
{
    public class usuarios
    {
        public int id_usuario { get; set; }
        public string nombre_usuario { get; set; } 
        public string pass { get; set; }
        public string tipo_usuario { get; set; }
    }
}
