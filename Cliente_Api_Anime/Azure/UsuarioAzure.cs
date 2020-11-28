using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cliente_Api_Anime.Models;

namespace Cliente_Api_Anime.Azure
{
    public class UsuarioAzure
    {
        static string connectionString = @"Server=ALEVIERA;Database=Anime;Trusted_Connection=True;";

        public static List<UsuarioAzure> usu;
        



        //OBTENER USUARIOS
        public static List<UsuarioAzure> ObtenerUsuarios()
        {
            var dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "select * from usuario";

                connection.Open();
                var DataAdapter = new SqlDataAdapter(sqlCommand);

                DataAdapter.Fill(dataTable);

                usuarios = new List<UsuarioAzure>();

                //COn este for hacemos que ingrese un id la cantidad de veces segun las filas que encuentre
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Usuario usuario = new Usuario();
                    usuario.id_usuario = int.Parse(dataTable.Rows[i]["id_usuario"].ToString());
                    usuario.nombre_usuario = dataTable.Rows[i]["nombre_usuario"].ToString();
                    usuario.pass = dataTable.Rows[i]["pass"].ToString();
                    usuario.tipo_usuario = dataTable.Rows[i]["tipo_usuario"].ToString();

                    usu.Add(usuario);//error aqui :c
                    
                }



            }

            return usu;
        }
    }
}
