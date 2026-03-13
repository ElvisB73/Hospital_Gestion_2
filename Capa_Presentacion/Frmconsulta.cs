using System;
using System.Drawing;
using System.Windows.Forms;
using Hospital_Gestion_2_CN;

namespace Capa_Presentacion
{
    public partial class FrmConsulta : Form
    {
        private int _idTurno = 0;
        private string _medico = "";

        public FrmConsulta()
        {
            InitializeComponent();
        }

        private void FrmConsulta_Load(object sender, EventArgs e)
        {
            CargarTurnos();
            CargarDoctores();
        }

        private void CargarTurnos()
        {
            cmbTurno.Items.Clear();
            foreach (var t in TurnoNegocio.ObtenerCola())
                cmbTurno.Items.Add(new ItemCombo(t.IdTurno,
                    $"{t.NroTurno} — {t.PacienteNombre} ({t.PrioridadNombre})"));
            cmbTurno.SelectedIndex = -1;
        }

        private void CargarDoctores()
        {
            cmbDoctor.Items.Clear();
            foreach (var d in DoctorNegocio.ObtenerTodos())
                cmbDoctor.Items.Add(new ItemCombo(d.IdDoctor, d.NombreCompleto));
            cmbDoctor.SelectedIndex = -1;
        }

        private void Limpiar()
        {
            cmbTurno.SelectedIndex = -1;
            cmbDoctor.SelectedIndex = -1;
            txtDiagnostico.Text = "";
            chkHoraFin.Checked = false;
            dtpHoraFin.Enabled = false;
            lblEstado.Text = "";
            _idTurno = 0;
            _medico = "";
        }

        private void cmbTurno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTurno.SelectedItem is ItemCombo item)
                _idTurno = item.Id;
        }

        private void cmbDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDoctor.SelectedItem is ItemCombo item)
                _medico = item.Texto;
        }

        private void chkHoraFin_CheckedChanged(object sender, EventArgs e)
        {
            dtpHoraFin.Enabled = chkHoraFin.Checked;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_idTurno <= 0)
                    throw new ArgumentException("Seleccioná un turno de la lista.");
                if (string.IsNullOrWhiteSpace(_medico))
                    throw new ArgumentException("Seleccioná un doctor de la lista.");

                var consulta = new ConsultaNegocio(_idTurno, _medico, txtDiagnostico.Text.Trim());

                if (chkHoraFin.Checked)
                    consulta.HoraFin = dtpHoraFin.Value;

                if (consulta.Registrar())
                {
                    lblEstado.ForeColor = Color.LightGreen;
                    lblEstado.Text = "✔ Consulta registrada. Turno marcado como Atendido.";
                    Limpiar();
                    CargarTurnos();
                }
            }
            catch (ArgumentException ex)
            {
                lblEstado.ForeColor = Color.Tomato;
                lblEstado.Text = "⚠ " + ex.Message;
            }
            catch (Exception ex)
            {
                lblEstado.ForeColor = Color.Tomato;
                lblEstado.Text = "✖ " + ex.Message;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e) => Limpiar();

        private void btnTurnos_Click(object sender, EventArgs e)
        {
            new FrmTurno().Show();
            this.Hide();
        }

        private void dtpHoraFin_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}