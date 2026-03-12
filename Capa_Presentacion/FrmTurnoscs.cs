using Capa_Presentacion;
using Hospital_Gestion_2_CN;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Hospital_Gestion_2_CN
{
    public partial class FrmTurno : Form
    {
        private int _idPaciente = 0;

        public FrmTurno()
        {
            InitializeComponent();
        }

        private void FrmTurno_Load(object sender, EventArgs e)
        {
            CargarPrioridades();
            RefrescarCola();
        }

        private void CargarPrioridades()
        {
            cmbPrioridad.Items.Clear();
            foreach (var p in PrioridadNegocio.ObtenerTodas())
                cmbPrioridad.Items.Add(new ItemCombo(p.IdPrioridad, $"{p.Nivel} — {p.Nombre}"));
            cmbPrioridad.SelectedIndex = -1;
        }

        private void RefrescarCola()
        {
            var dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Turno");
            dt.Columns.Add("Prioridad");
            dt.Columns.Add("Paciente");
            dt.Columns.Add("Motivo");
            dt.Columns.Add("Ingreso");
            dt.Columns.Add("Min. espera");

            foreach (var t in TurnoNegocio.ObtenerCola())
                dt.Rows.Add(t.IdTurno, t.NroTurno, t.PrioridadNombre,
                    t.PacienteNombre, t.Motivo,
                    t.FechaIngreso.ToString("HH:mm"), t.MinutosEspera);

            dgvCola.DataSource = dt;
            if (dgvCola.Columns["ID"] != null)
                dgvCola.Columns["ID"].Visible = false;
        }

        private void Limpiar()
        {
            txtDni.Text = "";
            txtMotivo.Text = "";
            cmbPrioridad.SelectedIndex = -1;
            lblPaciente.Text = "";
            _idPaciente = 0;
        }

        private int ObtenerIdPrioridad()
        {
            if (cmbPrioridad.SelectedItem is ItemCombo item) return item.Id;
            return 0;
        }

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDni.Text))
            {
                lblPaciente.ForeColor = Color.Tomato;
                lblPaciente.Text = " Ingresá el DNI.";
                return;
            }
            var p = PacienteNegocio.BuscarPorDni(txtDni.Text.Trim());
            if (p == null)
            {
                lblPaciente.ForeColor = Color.Tomato;
                lblPaciente.Text = "✖  No encontrado. Registralo primero.";
                _idPaciente = 0;
                return;
            }
            _idPaciente = p.IdPaciente;
            lblPaciente.ForeColor = Color.LightGreen;
            lblPaciente.Text = $"✔  {p.ObtenerResumen()}";
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (_idPaciente == 0)
            { MessageBox.Show("Buscá un paciente primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (ObtenerIdPrioridad() == 0)
            { MessageBox.Show("Seleccioná una prioridad.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (string.IsNullOrWhiteSpace(txtMotivo.Text))
            { MessageBox.Show("Ingresá el motivo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var turno = new TurnoNegocio(_idPaciente, ObtenerIdPrioridad(), txtMotivo.Text.Trim());
            if (turno.Registrar())
            {
                MessageBox.Show($"Turno registrado: {turno.NroTurno}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                RefrescarCola();
            }
        }

        private void btnAtender_Click(object sender, EventArgs e)
        {
            if (dgvCola.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvCola.CurrentRow.Cells["ID"].Value);
            string nro = dgvCola.CurrentRow.Cells["Turno"].Value?.ToString();
            if (MessageBox.Show($"¿Atender turno {nro}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            { TurnoNegocio.CambiarEstado(id, "Atendido"); RefrescarCola(); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (dgvCola.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvCola.CurrentRow.Cells["ID"].Value);
            string nro = dgvCola.CurrentRow.Cells["Turno"].Value?.ToString();
            if (MessageBox.Show($"¿Cancelar turno {nro}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            { TurnoNegocio.CambiarEstado(id, "Cancelado"); RefrescarCola(); }
        }

        private void btnRefrescar_Click(object sender, EventArgs e) => RefrescarCola();
       // private void btnReporte_Click(object sender, EventArgs e) => new FrmReporte().Show();
        private void btnPacientes_Click(object sender, EventArgs e)
        {
            new FrmPaciente().Show();
            this.Hide();
        }

        private void lblEstado_Click(object sender, EventArgs e)
        {

        }

        private void lblPrioridad_Click(object sender, EventArgs e)
        {

        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            new FrmReporte().Show();
        }
    }

    public class ItemCombo
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public ItemCombo(int id, string texto) { Id = id; Texto = texto; }
        public override string ToString() => Texto;
    }
}