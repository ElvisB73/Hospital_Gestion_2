using Hospital_Gestion_2_CN;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Hospital_Gestion_2_CN
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
                ? $"Total pacientes atendidos hoy: {total}"
                : "Sin datos atendidos todavía.";
        }

        private void btnRefrescar_Click(object sender, EventArgs e) => CargarReporte();
        private void btnCerrar_Click(object sender, EventArgs e) => this.Close();
        private void btnTurnos_Click(object sender, EventArgs e)
        {
            new FrmTurno().Show();
            this.Hide();
        }

        private void lblResumen_Click(object sender, EventArgs e)
        {

        }
    }
}