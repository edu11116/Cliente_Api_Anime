using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cliente_Api_Anime.Models;
using System.Data;
using System.Data.SqlClient;

namespace Cliente_Api_Anime.Azure
{
    public class AnimeAzure
    {
        static string connectionString = @"Server=ALEVIERA;Database=Anime;Trusted_Connection=True;";

        private static List<Animes> ani;

        //OBTENER ANIMER

        public static List<Animes> obtenerAnimes()
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = "select * from Animes";
                var comando = ConsultaSqlAnimes(connection, consultaSql);
                var dataTableAnimes = LlenarDataTable((SqlCommand)comando);//la conversion explicita no se si esta correcta((SqlCommand)comando)
                return LlenadoAnimes(dataTableAnimes);
            }
        }

        public static Animes ObtenerAnimePorId(int id_anime)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = $"select * from Animes where id_anime = {id_anime}";

                var comando = ConsultaSqlAnimes(connection, consultaSql);

                var dataTable = LlenarDataTable((SqlCommand)comando);//la conversion explicita no se si esta correcta((SqlCommand)comando)

                return CreacionAnimes (dataTable);
            }
        }

        public static Animes ObtenerAnimePorNombre(int nombre_anime)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = $"select * from Animes where nombre_anime = {nombre_anime}";

                var comando = ConsultaSqlAnimes(connection, consultaSql);

                var dataTable = LlenarDataTable((SqlCommand)comando);//la conversion explicita no se si esta correcta((SqlCommand)comando)

                return CreacionAnimes(dataTable);
            }
        }

        private static Animes CreacionAnimes(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                Animes animes = new Animes();
                Animes anime = new Animes();
                anime.capitulo = int.Parse(dataTable.Rows[0]["capitulo"].ToString());
                anime.id_anime = int.Parse(dataTable.Rows[0]["id_anime"].ToString());
                anime.nombre_anime = dataTable.Rows[0]["nombre_anime"].ToString();
                anime.genero = dataTable.Rows[0]["genero"].ToString();
                anime.duracion = dataTable.Rows[0]["duracion"].ToString();
                anime.categoria = dataTable.Rows[0]["categoria"].ToString();
                anime.calidad = dataTable.Rows[0]["calidad"].ToString();
            }
            else
            {
                return null;
            }
        }

        private static object ConsultaSqlAnimes(SqlConnection connection, string consulta)
        {
            SqlCommand sqlCommand = new SqlCommand(null, connection);
            sqlCommand.CommandText = consulta;
            connection.Open();
            return sqlCommand;
        }

        public static DataTable LlenarDataTable(SqlCommand comando)
        {

            var dataTable = new DataTable();
            var dataAdapter = new SqlDataAdapter(comando);
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        private static DataTable retornoDeAnimesSQL(SqlConnection connection)
        {
            SqlCommand sqlCommand = new SqlCommand(null, connection);
            sqlCommand.CommandText = "select * from Animes";
            connection.Open();

            return LlenarDataTable(sqlCommand);
        }

        private static List<Animes> LlenadoAnimes(DataTable dataTable)
        {
            ani = new List<Animes>();

            //COn este for hacemos que ingrese un id la cantidad de veces segun las filas que encuentre
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Animes anime = new Animes();
                anime.capitulo = int.Parse(dataTable.Rows[i]["capitulo"].ToString());
                anime.id_anime = int.Parse(dataTable.Rows[i]["id_anime"].ToString());
                anime.nombre_anime = dataTable.Rows[i]["nombre_anime"].ToString();
                anime.genero = dataTable.Rows[i]["genero"].ToString();
                anime.duracion = dataTable.Rows[i]["duracion"].ToString();
                anime.categoria = dataTable.Rows[i]["categoria"].ToString();
                anime.calidad = dataTable.Rows[i]["calidad"].ToString();

                ani.Add(anime);
            }
            return ani;
        }


    }
}
