using Hospital_Gestion_2_CN;
using Hospital_Manejo_Turnos.CapaPresentacion;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void btnPacientes_Click(object sender, EventArgs e)
        {
            new FrmPaciente().Show();
            this.Hide();
        }

        private void btnTurnos_Click(object sender, EventArgs e)
        {
            new FrmTurno().Show();
            this.Hide();
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            new FrmConsulta().Show();
            this.Hide();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            new FrmReporte().Show();
            this.Hide();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblVersion_Click(object sender, EventArgs e)
        {

        }
    }
}
