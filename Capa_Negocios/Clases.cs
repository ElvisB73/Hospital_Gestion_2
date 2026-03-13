using Hospital_Gestion_2_CD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Hospital_Gestion_2_CN
{
    public abstract class EntidadBase
    {
        public DateTime FechaCreacion { get; private set; }

        public EntidadBase()
        {
            FechaCreacion = DateTime.Now;
        }

        public abstract bool Validar();

        public virtual string ObtenerDescripcion()
        {
            return $"Registro creado: {FechaCreacion:dd/MM/yyyy HH:mm}";
        }

        public string ObtenerFechaFormateada()
        {
            return FechaCreacion.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }

    public class PacienteNegocio : EntidadBase
    {
        public int IdPaciente { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }

        public PacienteNegocio(string dni, string nombre, string telefono)
        {
            Dni = dni;
            Nombre = nombre;
            Telefono = telefono;
        }

        public PacienteNegocio() { }

        public override bool Validar()
        {
            if (string.IsNullOrWhiteSpace(Dni)) return false;
            if (string.IsNullOrWhiteSpace(Nombre)) return false;
            if (string.IsNullOrWhiteSpace(Telefono)) return false;
            if (Dni.Length < 3 || Dni.Length > 15) return false;
            return true;
        }

        public override string ObtenerDescripcion()
        {
            return $"Paciente: {Nombre} | DNI: {Dni} | Tel: {Telefono}";
        }

        public string ObtenerResumen()
        {
            return $"{Nombre} (DNI: {Dni})";
        }

        public bool Registrar()
        {
            if (!Validar()) return false;

            var existente = PacienteDAL.BuscarPorDni(Dni);
            if (existente != null)
            {
                IdPaciente = existente.IdPaciente;
                return true;
            }

            PacienteDAL.Insertar(new Paciente
            {
                Dni = this.Dni,
                Nombre = this.Nombre,
                Telefono = this.Telefono
            });

            var nuevo = PacienteDAL.BuscarPorDni(Dni);
            if (nuevo != null) IdPaciente = nuevo.IdPaciente;
            return true;
        }

        public static bool Eliminar(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni)) return false;
            PacienteDAL.Eliminar(dni);
            return true;
        }

        public static PacienteNegocio BuscarPorDni(string dni)
        {
            var p = PacienteDAL.BuscarPorDni(dni);
            if (p == null) return null;
            return new PacienteNegocio
            {
                IdPaciente = p.IdPaciente,
                Dni = p.Dni,
                Nombre = p.Nombre,
                Telefono = p.Telefono
            };
        }

        public static List<PacienteNegocio> ObtenerTodos()
        {
            var lista = new List<PacienteNegocio>();
            foreach (var p in PacienteDAL.ObtenerTodos())
                lista.Add(new PacienteNegocio
                {
                    IdPaciente = p.IdPaciente,
                    Dni = p.Dni,
                    Nombre = p.Nombre,
                    Telefono = p.Telefono
                });
            return lista;
        }
    }

    public class TurnoNegocio : EntidadBase
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

        public TurnoNegocio(int idPaciente, int idPrioridad, string motivo)
        {
            IdPaciente = idPaciente;
            IdPrioridad = idPrioridad;
            Motivo = motivo;
            Estado = "En espera";
        }

        public TurnoNegocio() { }

        public override bool Validar()
        {
            if (IdPaciente <= 0) return false;
            if (IdPrioridad <= 0) return false;
            if (string.IsNullOrWhiteSpace(Motivo)) return false;
            return true;
        }

        public override string ObtenerDescripcion()
        {
            return $"Turno {NroTurno} | {PacienteNombre} | {PrioridadNombre} | {Estado}";
        }

        public bool Registrar()
        {
            if (!Validar()) return false;
            NroTurno = GenerarNroTurno();
            TurnoDAL.Insertar(new Turno
            {
                IdPaciente = this.IdPaciente,
                IdPrioridad = this.IdPrioridad,
                NroTurno = this.NroTurno,
                Motivo = this.Motivo
            });
            return true;
        }

        public static string GenerarNroTurno()
        {
            try
            {
                var bd = new CD_Conexion();
                var con = bd.AbrirConexion();
                SqlCommand cmd = new SqlCommand(@"
                    SELECT COUNT(*) FROM Turno
                    WHERE CAST(fecha_ingreso AS DATE) = CAST(GETDATE() AS DATE)", con);
                int cantidad = Convert.ToInt32(cmd.ExecuteScalar());
                bd.CerrarConexion(con);
                return $"T{cantidad + 1:D3}";
            }
            catch
            {
                return $"T{DateTime.Now:HHmmss}";
            }
        }

        public static List<TurnoNegocio> ObtenerCola()
        {
            var lista = new List<TurnoNegocio>();
            foreach (var t in TurnoDAL.ObtenerCola())
                lista.Add(new TurnoNegocio
                {
                    IdTurno = t.IdTurno,
                    NroTurno = t.NroTurno,
                    PrioridadNombre = t.PrioridadNombre,
                    PacienteNombre = t.PacienteNombre,
                    Motivo = t.Motivo,
                    FechaIngreso = t.FechaIngreso,
                    MinutosEspera = t.MinutosEspera
                });
            return lista;
        }

        public static void CambiarEstado(int idTurno, string estado)
        {
            TurnoDAL.ActualizarEstado(idTurno, estado);
        }

        public static DataTable ObtenerReporte()
        {
            return TurnoDAL.ObtenerReporte();
        }
    }

    public class ConsultaNegocio : EntidadBase
    {
        public int IdTurno { get; set; }
        public string Medico { get; set; }
        public string Diagnostico { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime? HoraFin { get; set; }

        public ConsultaNegocio(int idTurno, string medico, string diagnostico)
        {
            IdTurno = idTurno;
            Medico = medico;
            Diagnostico = diagnostico;
            HoraInicio = DateTime.Now;
        }

        public ConsultaNegocio() { }

        public override bool Validar()
        {
            if (IdTurno <= 0) return false;
            if (string.IsNullOrWhiteSpace(Medico)) return false;
            if (string.IsNullOrWhiteSpace(Diagnostico)) return false;
            return true;
        }

        public override string ObtenerDescripcion()
        {
            return $"Consulta | Dr/a: {Medico} | {Diagnostico}";
        }

        public int CalcularDuracion()
        {
            if (HoraFin == null) return 0;
            return (int)(HoraFin.Value - HoraInicio).TotalMinutes;
        }

        public bool Registrar()
        {
            if (!Validar()) return false;
            ConsultaDAL.Insertar(new Consulta
            {
                IdTurno = this.IdTurno,
                Medico = this.Medico,
                Diagnostico = this.Diagnostico,
                HoraInicio = DateTime.Now,
                HoraFin = this.HoraFin
            });
            TurnoDAL.ActualizarEstado(IdTurno, "Atendido");
            return true;
        }
    }

    public class PrioridadNegocio : EntidadBase
    {
        public int IdPrioridad { get; set; }
        public string Nombre { get; set; }
        public int Nivel { get; set; }

        public PrioridadNegocio() { }

        public override bool Validar()
        {
            return !string.IsNullOrWhiteSpace(Nombre) && Nivel > 0;
        }

        public override string ObtenerDescripcion()
        {
            return $"Nivel {Nivel}: {Nombre}";
        }

        public static List<PrioridadNegocio> ObtenerTodas()
        {
            var lista = new List<PrioridadNegocio>();
            foreach (var p in PrioridadDAL.ObtenerTodas())
                lista.Add(new PrioridadNegocio
                {
                    IdPrioridad = p.IdPrioridad,
                    Nombre = p.Nombre,
                    Nivel = p.Nivel
                });
            return lista;
        }
    }
}