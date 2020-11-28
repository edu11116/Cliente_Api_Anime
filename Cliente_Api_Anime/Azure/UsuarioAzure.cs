﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cliente_Api_Anime.Azure
{
    public class UsuarioAzure
    {
        static string connectionString = @"Server=ALEVIERA;Database=Anime;Trusted_Connection=True;";

        public static List<UsuarioAzure> usuario;
        



        //OBTENER USUARIOS
        public static List<UsuarioAzure> ObtenerUsuarios()
        {
            var dataTable = new dataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "select * from usuario";

                connection.Open();
                var DataAdapter = new SqlDataAdapter(sqlCommand);

                DataAdapter.Fill(dataTable);

                

            

               
                

            }

            return usuario;
        }
    }
}