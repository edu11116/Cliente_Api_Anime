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

        public static Usuario ObtenerUsuarioPorNombre(string nombre_usuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = $"select * from Usuario where nombre_usuario = '{nombre_usuario}'";

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

        //DESDE AQUI AGREGAR A ANIMESAZURE

        public static int AgregarUsuario(Usuario usuario)
        {
            int filasAfectadas = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Insert into Usuario (id_usuario, nombre_usuario, pass, tipo_usuario) values (@id_usuario, @nombre_usuario, @pass, @tipo_usuario )";
                sqlCommand.Parameters.AddWithValue("@id_usuario", usuario.id_usuario);
                sqlCommand.Parameters.AddWithValue("@nombre_usuario", usuario.nombre_usuario);
                sqlCommand.Parameters.AddWithValue("@pass", usuario.pass);
                sqlCommand.Parameters.AddWithValue("@tipo_usuario", usuario.tipo_usuario);

                try
                {
                    connection.Open();
                    filasAfectadas = sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


            }
            return filasAfectadas;
        }

        public static int AgregarUsuario(int id_usuario, string nombre_usuario, string pass, string tipo_usuario)
        {
            int resultado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Insert into Usuario (id_usuario, nombre_usuario, pass, tipo_usuario) values (@id_usuario, @nombre_usuario, @pass, @tipo_usuario)";
                sqlCommand.Parameters.AddWithValue("@id_usuario", id_usuario);
                sqlCommand.Parameters.AddWithValue("@nombre_usuario", nombre_usuario);
                sqlCommand.Parameters.AddWithValue("@pass", pass);
                sqlCommand.Parameters.AddWithValue("@tipo_usuario", tipo_usuario);
                try
                {
                    connection.Open();
                    resultado = sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return resultado;
        }

        public static int EliminarUsuarioPorNombre(string nombre_usuario)
        {
            int resultado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Delete from Usuario where nombre_usuario = @nombre_usuario";
                sqlCommand.Parameters.AddWithValue("@nombre_usuario", nombre_usuario);

                try
                {
                    connection.Open();
                    resultado = sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return resultado;
            }
        }

        public static int ActualizarUsuarioPorId(Usuario usuario)
        {
            int resultado = 0;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "Update Usuario SET  nombre_usuario = @nombre_usuario, pass = @pass, tipo_usuario = @tipo_usuario where id_usuario = @id_usuario";

                sqlCommand.Parameters.AddWithValue("@nombre_usuario", usuario.nombre_usuario);
                sqlCommand.Parameters.AddWithValue("@pass", usuario.pass);
                sqlCommand.Parameters.AddWithValue("@tipo_usuario", usuario.tipo_usuario);

                try
                {
                    sqlConnection.Open();
                    resultado = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


            return resultado;
        }

        //HASTA AQUIIIIIIIIIIII EDUARDOOOOO XDD


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

        private static DataTable retornoDeUsuariosSQL(SqlConnection connection)
        {
            SqlCommand sqlCommand = new SqlCommand(null, connection);
            sqlCommand.CommandText = "select * from Usuario";
            connection.Open();

            return LlenarDataTable(sqlCommand);
        }

    }
}
