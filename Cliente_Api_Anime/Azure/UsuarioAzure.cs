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

        private static List<Usuario> usu;

        //OBTENER USUARIOS
        public static List<Usuario> ObtenerUsuarios()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = "select * from Usuario";
                var comando = ConsultaSqlUsuario(connection, consultaSql);
                var dataTableUsuarios = LlenarDataTable((SqlCommand)comando);//la conversion explicita no se si esta correcta((SqlCommand)comando)
                return LlenadoUsuario(dataTableUsuarios);
            }
            
        }

        public static Usuario ObtenerUsuarioPorId(int id_usuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = $"select * from Usuario where id_usuario = {id_usuario}";

                var comando = ConsultaSqlUsuario(connection, consultaSql);

                var dataTable = LlenarDataTable((SqlCommand)comando);//la conversion explicita no se si esta correcta((SqlCommand)comando)

                return CreacionUsuario(dataTable);
            }
        }


        private static object ConsultaSqlUsuario(SqlConnection connection, string consulta)
        {
            SqlCommand sqlCommand = new SqlCommand(null, connection);
            sqlCommand.CommandText = consulta;
            connection.Open();
            return sqlCommand;
        }

        private static Usuario CreacionUsuario(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                Usuario usuario = new Usuario();
                usuario.id_usuario = int.Parse(dataTable.Rows[0]["id_usuario"].ToString());
                usuario.nombre_usuario = dataTable.Rows[0]["nombre_usuario"].ToString();
                usuario.pass = dataTable.Rows[0]["pass"].ToString();
                usuario.tipo_usuario = dataTable.Rows[0]["tipo_usuario"].ToString();
                return usuario;
            }
            else
            {
                return null;
            }
        }


        public static DataTable LlenarDataTable(SqlCommand comando)
        {
           
            var dataTable = new DataTable();
            var dataAdapter = new SqlDataAdapter(comando);
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        private static List<Usuario> LlenadoUsuario(DataTable dataTable)
        {
            usu = new List<Usuario>();

            //COn este for hacemos que ingrese un id la cantidad de veces segun las filas que encuentre
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Usuario usuario = new Usuario();
                usuario.id_usuario = int.Parse(dataTable.Rows[i]["id_usuario"].ToString());
                usuario.nombre_usuario = dataTable.Rows[i]["nombre_usuario"].ToString();
                usuario.pass = dataTable.Rows[i]["pass"].ToString();
                usuario.tipo_usuario = dataTable.Rows[i]["tipo_usuario"].ToString();

                usu.Add(usuario);
            }


            return usu;
        }


    }
}
