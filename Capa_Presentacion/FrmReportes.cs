using Capa_Presentacion;
using Hospital_Gestion_2_CN;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Hospital_Manejo_Turnos.CapaPresentacion
{
    public partial class FrmReporte : Form
    {
        public FrmReporte()
        {
            InitializeComponent();
        }

        private void FrmReporte_Load(object sender, EventArgs e)
        {
            CargarReporte();
        }

        private void CargarReporte()
        {
            lblFecha.Text = $"Generado: {DateTime.Now:dd/MM/yyyy  HH:mm:ss}";

            // ── Grilla resumen por prioridad ──
            DataTable dt = TurnoNegocio.ObtenerReporte();

            if (dt.Columns.Contains("nivel")) dt.Columns["nivel"].ColumnName = "Nivel";
            if (dt.Columns.Contains("prioridad")) dt.Columns["prioridad"].ColumnName = "Prioridad";
            if (dt.Columns.Contains("total_atendidos")) dt.Columns["total_atendidos"].ColumnName = "Atendidos";
            if (dt.Columns.Contains("espera_promedio_min")) dt.Columns["espera_promedio_min"].ColumnName = "Espera Prom.(min)";

            dgvReporte.DataSource = dt;

            foreach (DataGridViewRow row in dgvReporte.Rows)
            {
                if (row.Cells["Nivel"].Value == null) continue;
                int nivel = Convert.ToInt32(row.Cells["Nivel"].Value);
                Color color;
                if (nivel == 1) color = Color.FromArgb(120, 30, 30);
                else if (nivel == 2) color = Color.FromArgb(120, 80, 20);
                else if (nivel == 3) color = Color.FromArgb(90, 90, 20);
                else color = Color.FromArgb(25, 80, 40);
                row.DefaultCellStyle.BackColor = color;
            }

            int total = 0;
            foreach (DataRow row in dt.Rows)
                total += Convert.ToInt32(row["Atendidos"]);

            lblResumen.Text = dt.Rows.Count > 0
                ? $"Total pacientes atendidos: {total}"
                : "Sin datos atendidos todavía.";

            // ── Grilla historial (atendidos + cancelados) ──
            CargarHistorial();
        }

        private void CargarHistorial()
        {
            try
            {
                DataTable dt = TurnoNegocio.ObtenerHistorial();

                if (dt.Columns.Contains("nro_turno")) dt.Columns["nro_turno"].ColumnName = "Turno";
                if (dt.Columns.Contains("nombre")) dt.Columns["nombre"].ColumnName = "Paciente";
                if (dt.Columns.Contains("prioridad")) dt.Columns["prioridad"].ColumnName = "Prioridad";
                if (dt.Columns.Contains("motivo")) dt.Columns["motivo"].ColumnName = "Motivo";
                if (dt.Columns.Contains("fecha_ingreso")) dt.Columns["fecha_ingreso"].ColumnName = "Ingreso";
                if (dt.Columns.Contains("fecha_atencion")) dt.Columns["fecha_atencion"].ColumnName = "Fin";
                if (dt.Columns.Contains("estado")) dt.Columns["estado"].ColumnName = "Estado";

                dgvHistorial.DataSource = dt;

                // Colorear filas por estado
                foreach (DataGridViewRow row in dgvHistorial.Rows)
                {
                    if (row.Cells["Estado"].Value == null) continue;
                    string estado = row.Cells["Estado"].Value.ToString();
                    row.DefaultCellStyle.BackColor = estado == "Atendido"
                        ? Color.FromArgb(25, 70, 40)
                        : Color.FromArgb(90, 25, 25);
                }

                int atendidos = 0;
                int cancelados = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Estado"].ToString() == "Atendido") atendidos++;
                    else cancelados++;
                }

                lblHistorial.Text = $"Historial — Atendidos: {atendidos}  |  Cancelados: {cancelados}";
            }
            catch (Exception ex)
            {
                lblHistorial.Text = "Error al cargar historial: " + ex.Message;
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e) => CargarReporte();
        private void btnCerrar_Click(object sender, EventArgs e) => this.Close();
        private void btnTurnos_Click(object sender, EventArgs e)
        {
            new FrmTurno().Show();
            this.Hide();
        }

        private void btnvolver_Click(object sender, EventArgs e)
        {

        }

        private void dgvHistorial_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnvolver_Click_1(object sender, EventArgs e)
        {
            new FrmPrincipal().Show();
            this.Close();
        }
    }
}