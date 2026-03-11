using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Hospital_Gestion_2_CD
{

    public class CD_Conexion
    {
        private readonly string cadenaConexion =
            "Server=.; Database=Hospital_Turnos_Traige; Integrated Security=True;";

        public SqlConnection AbrirConexion()
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            if (conexion.State == ConnectionState.Closed)
                conexion.Open();
            return conexion;
        }

        public void CerrarConexion(SqlConnection conexion)
        {
            if (conexion.State == ConnectionState.Open)
                conexion.Close();
        }
    }

}