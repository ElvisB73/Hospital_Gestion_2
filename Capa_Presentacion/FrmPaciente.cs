using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Hospital_Gestion_2_CN;

namespace Hospital_Gestion_2_CN
{
    public partial class FrmPaciente : Form
    {
        public FrmPaciente()
        {
            InitializeComponent();
        }

        private void FrmPaciente_Load(object sender, EventArgs e)
        {
            CargarPacientes();
        }

        

        private void CargarPacientes()
        {
            var lista = PacienteNegocio.ObtenerTodos();
            var dt = new DataTable();
            dt.Columns.Add("DNI");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Teléfono");

            foreach (var p in lista)
                dt.Rows.Add(p.Dni, p.Nombre, p.Telefono);

            dgvPacientes.DataSource = dt;
        }

        private void Limpiar()
        {
            txtDni.Text = "";
            txtNombre.Text = "";
            txtTelefono.Text = "";
            lblEstado.Text = "";
            txtDni.Focus();
        }

        

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

     

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var paciente = new PacienteNegocio(
                txtDni.Text.Trim(),
                txtNombre.Text.Trim(),
                txtTelefono.Text.Trim()
            );

            if (!paciente.Validar())
            {
                lblEstado.ForeColor = Color.Tomato;
                lblEstado.Text = "⚠  Completá todos los campos. DNI entre 3 y 15 dígitos.";
                return;
            }

            if (paciente.Registrar())
            {
                lblEstado.ForeColor = Color.LightGreen;
                lblEstado.Text = $"✔  {paciente.ObtenerResumen()} registrado correctamente.";
                Limpiar();
                CargarPacientes();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDni.Text))
            {
                lblEstado.ForeColor = Color.Tomato;
                lblEstado.Text = "⚠  Ingresá un DNI para buscar.";
                return;
            }

            var p = PacienteNegocio.BuscarPorDni(txtDni.Text.Trim());
            if (p == null)
            {
                lblEstado.ForeColor = Color.Tomato;
                lblEstado.Text = "✖  Paciente no encontrado.";
                return;
            }

            txtNombre.Text = p.Nombre;
            txtTelefono.Text = p.Telefono;
            lblEstado.ForeColor = Color.LightGreen;
            lblEstado.Text = $"✔  {p.ObtenerDescripcion()}";
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDni.Text))
            {
                lblEstado.ForeColor = Color.Tomato;
                lblEstado.Text = "⚠  Buscá el paciente antes de eliminar.";
                return;
            }

            var conf = MessageBox.Show(
                $"¿Eliminar paciente con DNI {txtDni.Text}?\nSe eliminarán también sus turnos.",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (conf == DialogResult.Yes)
            {
                PacienteNegocio.Eliminar(txtDni.Text.Trim());
                lblEstado.ForeColor = Color.LightGreen;
                lblEstado.Text = "✔  Paciente eliminado correctamente.";
                Limpiar();
                CargarPacientes();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

     

        private void dgvPacientes_DoubleClick(object sender, EventArgs e)
        {
            if (dgvPacientes.CurrentRow == null) return;
            txtDni.Text = dgvPacientes.CurrentRow.Cells["DNI"].Value?.ToString();
            txtNombre.Text = dgvPacientes.CurrentRow.Cells["Nombre"].Value?.ToString();
            txtTelefono.Text = dgvPacientes.CurrentRow.Cells["Teléfono"].Value?.ToString();
        }

        private void FrmPaciente_Load_1(object sender, EventArgs e)
        {

        }

        private void lblEstado_Click(object sender, EventArgs e)
        {

        }

        private void btnIrTurnos_Click(object sender, EventArgs e)
        {
            new FrmTurno().Show();
        }
    }
}