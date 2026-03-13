using Hospital_Gestion_2_CD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public class Prioridad
{
    public int IdPrioridad { get; set; }
    public string Nombre { get; set; }
    public int Nivel { get; set; }
}

public class Paciente
{
    public int IdPaciente { get; set; }
    public string Dni { get; set; }
    public string Nombre { get; set; }
    public string Telefono { get; set; }
}

public class Turno
{
    public int IdTurno { get; set; }
    public int IdPaciente { get; set; }
    public int IdPrioridad { get; set; }
    public string NroTurno { get; set; }
    public string Motivo { get; set; }
    public DateTime FechaIngreso { get; set; }
    public DateTime? FechaAtencion { get; set; }
    public string Estado { get; set; }
    public string PacienteNombre { get; set; }
    public string PrioridadNombre { get; set; }
    public int MinutosEspera { get; set; }
}

public class Consulta
{
    public int IdConsulta { get; set; }
    public int IdTurno { get; set; }
    public string Medico { get; set; }
    public string Diagnostico { get; set; }
    public DateTime HoraInicio { get; set; }
    public DateTime? HoraFin { get; set; }
}

public static class Validar
{
    public static void SoloTexto(string valor, string campo)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException($"{campo} no puede estar vacío.");

        foreach (char c in valor)
        {
            if (!char.IsLetter(c) && c != ' ')
                throw new ArgumentException($"{campo} solo debe contener letras.");
        }
    }

    public static void SoloNumeros(string valor, string campo)
    {
        if (string.IsNullOrWhiteSpace(valor) || !valor.All(char.IsDigit))
            throw new ArgumentException($"{campo} solo debe contener números y no puede estar vacío.");
    }

    public static void IdPositivo(int valor, string campo)
    {
        if (valor <= 0)
            throw new ArgumentException($"{campo} debe ser un número positivo.");
    }

    public static void NoVacio(string valor, string campo)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException($"{campo} no puede estar vacío.");
    }
}

public class PrioridadDAL
{
    public static List<Prioridad> ObtenerTodas()
    {
        var lista = new List<Prioridad>();
        try
        {
            var bd = new CD_Conexion();
            SqlConnection con = bd.AbrirConexion();
            SqlDataReader dr = new SqlCommand("SELECT * FROM Prioridad ORDER BY nivel", con).ExecuteReader();
            while (dr.Read())
                lista.Add(new Prioridad
                {
                    IdPrioridad = Convert.ToInt32(dr["id_prioridad"]),
                    Nombre = dr["nombre"].ToString(),
                    Nivel = Convert.ToInt32(dr["nivel"])
                });
            dr.Close();
            bd.CerrarConexion(con);
        }
        catch (Exception ex) { throw new Exception("Error al obtener prioridades: " + ex.Message); }
        return lista;
    }
}

public class PacienteDAL
{
    public static void Insertar(Paciente p)
    {
        try
        {
            Validar.SoloNumeros(p.Dni, "DNI");
            Validar.SoloTexto(p.Nombre, "Nombre");
            Validar.SoloNumeros(p.Telefono, "Teléfono");

            var bd = new CD_Conexion();
            SqlConnection con = bd.AbrirConexion();
            SqlCommand cmd = new SqlCommand("INSERT INTO Paciente (dni, nombre, telefono) VALUES (@dni, @nombre, @telefono)", con);
            cmd.Parameters.AddWithValue("@dni", p.Dni);
            cmd.Parameters.AddWithValue("@nombre", p.Nombre);
            cmd.Parameters.AddWithValue("@telefono", p.Telefono);
            cmd.ExecuteNonQuery();
            bd.CerrarConexion(con);
        }
        catch (ArgumentException) { throw; }
        catch (Exception ex) { throw new Exception("Error al insertar paciente: " + ex.Message); }
    }

    public static Paciente BuscarPorDni(string dni)
    {
        try
        {
            Validar.SoloNumeros(dni, "DNI");
            var bd = new CD_Conexion();
            SqlConnection con = bd.AbrirConexion();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Paciente WHERE dni = @dni", con);
            cmd.Parameters.AddWithValue("@dni", dni);
            SqlDataReader dr = cmd.ExecuteReader();
            Paciente paciente = dr.Read() ? new Paciente
            {
                IdPaciente = Convert.ToInt32(dr["id_paciente"]),
                Dni = dr["dni"].ToString(),
                Nombre = dr["nombre"].ToString(),
                Telefono = dr["telefono"].ToString()
            } : null;
            dr.Close();
            bd.CerrarConexion(con);
            return paciente;
        }
        catch (ArgumentException) { throw; }
        catch (Exception ex) { throw new Exception("Error al buscar paciente: " + ex.Message); }
    }

    public static void Eliminar(string dni)
    {
        try
        {
            Validar.SoloNumeros(dni, "DNI");
            var bd = new CD_Conexion();
            SqlConnection con = bd.AbrirConexion();
            SqlCommand cmdId = new SqlCommand("SELECT id_paciente FROM Paciente WHERE dni = @dni", con);
            cmdId.Parameters.AddWithValue("@dni", dni);
            object resultado = cmdId.ExecuteScalar();
            if (resultado == null) { bd.CerrarConexion(con); return; }

            int id = Convert.ToInt32(resultado);
            new SqlCommand($"DELETE FROM Consulta WHERE id_turno IN (SELECT id_turno FROM Turno WHERE id_paciente = {id})", con).ExecuteNonQuery();
            new SqlCommand($"DELETE FROM Turno WHERE id_paciente = {id}", con).ExecuteNonQuery();
            new SqlCommand($"DELETE FROM Paciente WHERE dni = '{dni}'", con).ExecuteNonQuery();
            bd.CerrarConexion(con);
        }
        catch (ArgumentException) { throw; }
        catch (Exception ex) { throw new Exception("Error al eliminar paciente: " + ex.Message); }
    }

    public static List<Paciente> ObtenerTodos()
    {
        var lista = new List<Paciente>();
        try
        {
            var bd = new CD_Conexion();
            SqlConnection con = bd.AbrirConexion();
            SqlDataReader dr = new SqlCommand("SELECT * FROM Paciente ORDER BY nombre", con).ExecuteReader();
            while (dr.Read())
                lista.Add(new Paciente
                {
                    IdPaciente = Convert.ToInt32(dr["id_paciente"]),
                    Dni = dr["dni"].ToString(),
                    Nombre = dr["nombre"].ToString(),
                    Telefono = dr["telefono"].ToString()
                });
            dr.Close();
            bd.CerrarConexion(con);
        }
        catch (Exception ex) { throw new Exception("Error al obtener pacientes: " + ex.Message); }
        return lista;
    }
}

public class TurnoDAL
{
    public static void Insertar(Turno t)
    {
        try
        {
            Validar.IdPositivo(t.IdPaciente, "ID Paciente");
            Validar.IdPositivo(t.IdPrioridad, "ID Prioridad");
            Validar.NoVacio(t.Motivo, "Motivo");
            // ← NroTurno NO se valida, lo genera el sistema solo

            var bd = new CD_Conexion();
            var con = bd.AbrirConexion();
            var cmd = new SqlCommand(
                "INSERT INTO Turno (id_paciente, id_prioridad, nro_turno, motivo) VALUES (@idPac, @idPrio, @nro, @motivo)", con);
            cmd.Parameters.AddWithValue("@idPac", t.IdPaciente);
            cmd.Parameters.AddWithValue("@idPrio", t.IdPrioridad);
            cmd.Parameters.AddWithValue("@nro", t.NroTurno);
            cmd.Parameters.AddWithValue("@motivo", t.Motivo);
            cmd.ExecuteNonQuery();
            bd.CerrarConexion(con);
        }
        catch (ArgumentException) { throw; }
        catch (Exception ex) { throw new Exception("Error al insertar turno: " + ex.Message); }
    }

    public static List<Turno> ObtenerCola()
    {
        var lista = new List<Turno>();
        try
        {
            var bd = new CD_Conexion();
            SqlConnection con = bd.AbrirConexion();
            string sql = @"SELECT T.id_turno, T.nro_turno, PR.nombre AS prioridad, P.nombre AS paciente, 
                           T.motivo, T.fecha_ingreso, DATEDIFF(MINUTE, T.fecha_ingreso, GETDATE()) AS minutos_espera
                           FROM Turno T
                           JOIN Prioridad PR ON T.id_prioridad = PR.id_prioridad
                           JOIN Paciente P ON T.id_paciente = P.id_paciente
                           WHERE T.estado = 'En espera'
                           ORDER BY PR.nivel ASC, T.fecha_ingreso ASC";
            SqlDataReader dr = new SqlCommand(sql, con).ExecuteReader();
            while (dr.Read())
                lista.Add(new Turno
                {
                    IdTurno = Convert.ToInt32(dr["id_turno"]),
                    NroTurno = dr["nro_turno"].ToString(),
                    PrioridadNombre = dr["prioridad"].ToString(),
                    PacienteNombre = dr["paciente"].ToString(),
                    Motivo = dr["motivo"].ToString(),
                    FechaIngreso = Convert.ToDateTime(dr["fecha_ingreso"]),
                    MinutosEspera = Convert.ToInt32(dr["minutos_espera"])
                });
            dr.Close();
            bd.CerrarConexion(con);
        }
        catch (Exception ex) { throw new Exception("Error al obtener cola: " + ex.Message); }
        return lista;
    }

    public static void ActualizarEstado(int idTurno, string estado)
    {
        try
        {
            Validar.IdPositivo(idTurno, "ID Turno");
            Validar.SoloTexto(estado, "Estado");

            var bd = new CD_Conexion();
            SqlConnection con = bd.AbrirConexion();
            SqlCommand cmd = new SqlCommand(@"UPDATE Turno SET estado = @estado,
                fecha_atencion = CASE WHEN @estado = 'Atendido' THEN GETDATE() ELSE fecha_atencion END
                WHERE id_turno = @id", con);
            cmd.Parameters.AddWithValue("@estado", estado);
            cmd.Parameters.AddWithValue("@id", idTurno);
            cmd.ExecuteNonQuery();
            bd.CerrarConexion(con);
        }
        catch (ArgumentException) { throw; }
        catch (Exception ex) { throw new Exception("Error al actualizar estado: " + ex.Message); }
    }

    public static DataTable ObtenerReporte()
    {
        try
        {
            var bd = new CD_Conexion();
            SqlConnection con = bd.AbrirConexion();
            string sql = @"SELECT PR.nivel, PR.nombre AS prioridad, COUNT(T.id_turno) AS total_atendidos,
                           ROUND(AVG(CAST(DATEDIFF(MINUTE, T.fecha_ingreso, T.fecha_atencion) AS FLOAT)), 1) AS espera_promedio_min
                           FROM Turno T
                           JOIN Prioridad PR ON T.id_prioridad = PR.id_prioridad
                           WHERE T.estado = 'Atendido'
                           GROUP BY PR.id_prioridad, PR.nivel, PR.nombre
                           ORDER BY PR.nivel";
            DataTable dt = new DataTable();
            new SqlDataAdapter(sql, con).Fill(dt);
            bd.CerrarConexion(con);
            return dt;
        }
        catch (Exception ex) { throw new Exception("Error al obtener reporte: " + ex.Message); }
    }
}

public class ConsultaDAL
{
    public static void Insertar(Consulta c)
    {
        try
        {
            Validar.IdPositivo(c.IdTurno, "ID Turno");
            Validar.SoloTexto(c.Medico, "Médico");
            Validar.NoVacio(c.Diagnostico, "Diagnóstico");

            var bd = new CD_Conexion();
            SqlConnection con = bd.AbrirConexion();
            SqlCommand cmd = new SqlCommand(@"INSERT INTO Consulta (id_turno, medico, diagnostico, hora_inicio, hora_fin)
                VALUES (@idTurno, @medico, @diagnostico, @horaInicio, @horaFin)", con);
            cmd.Parameters.AddWithValue("@idTurno", c.IdTurno);
            cmd.Parameters.AddWithValue("@medico", c.Medico);
            cmd.Parameters.AddWithValue("@diagnostico", c.Diagnostico);
            cmd.Parameters.AddWithValue("@horaInicio", c.HoraInicio);
            cmd.Parameters.AddWithValue("@horaFin", (object)c.HoraFin ?? DBNull.Value);
            cmd.ExecuteNonQuery();
            bd.CerrarConexion(con);
        }
        catch (ArgumentException) { throw; }
        catch (Exception ex) { throw new Exception("Error al insertar consulta: " + ex.Message); }
    }
}