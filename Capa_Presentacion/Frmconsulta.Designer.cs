using Guna.UI2.WinForms;

namespace Capa_Presentacion
{
    partial class FrmConsulta
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTop = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblTurno = new System.Windows.Forms.Label();
            this.lblDoctor = new System.Windows.Forms.Label();
            this.lblDiagnostico = new System.Windows.Forms.Label();
            this.lblHoraFin = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cmbTurno = new System.Windows.Forms.ComboBox();
            this.cmbDoctor = new System.Windows.Forms.ComboBox();
            this.txtDiagnostico = new Guna.UI2.WinForms.Guna2TextBox();
            this.chkHoraFin = new System.Windows.Forms.CheckBox();
            this.dtpHoraFin = new System.Windows.Forms.DateTimePicker();
            this.btnGuardar = new Guna.UI2.WinForms.Guna2Button();
            this.btnLimpiar = new Guna.UI2.WinForms.Guna2Button();
            this.btnTurnos = new Guna.UI2.WinForms.Guna2Button();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panelTop.Controls.Add(this.lblTitulo);
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(580, 60);
            this.panelTop.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.lblTitulo.Location = new System.Drawing.Point(10, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(420, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "  REGISTRO DE CONSULTA";
            // 
            // lblTurno
            // 
            this.lblTurno.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.lblTurno.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTurno.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblTurno.Location = new System.Drawing.Point(20, 78);
            this.lblTurno.Name = "lblTurno";
            this.lblTurno.Size = new System.Drawing.Size(100, 20);
            this.lblTurno.TabIndex = 1;
            this.lblTurno.Text = "Turno *";
            this.lblTurno.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDoctor
            // 
            this.lblDoctor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.lblDoctor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDoctor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblDoctor.Location = new System.Drawing.Point(20, 118);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new System.Drawing.Size(100, 20);
            this.lblDoctor.TabIndex = 3;
            this.lblDoctor.Text = "Doctor *";
            this.lblDoctor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiagnostico
            // 
            this.lblDiagnostico.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.lblDiagnostico.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDiagnostico.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblDiagnostico.Location = new System.Drawing.Point(20, 162);
            this.lblDiagnostico.Name = "lblDiagnostico";
            this.lblDiagnostico.Size = new System.Drawing.Size(100, 20);
            this.lblDiagnostico.TabIndex = 5;
            this.lblDiagnostico.Text = "Diagnóstico *";
            this.lblDiagnostico.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHoraFin
            // 
            this.lblHoraFin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.lblHoraFin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHoraFin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblHoraFin.Location = new System.Drawing.Point(30, 244);
            this.lblHoraFin.Name = "lblHoraFin";
            this.lblHoraFin.Size = new System.Drawing.Size(100, 20);
            this.lblHoraFin.TabIndex = 7;
            this.lblHoraFin.Text = "Hora fin";
            this.lblHoraFin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEstado
            // 
            this.lblEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblEstado.ForeColor = System.Drawing.Color.LightGreen;
            this.lblEstado.Location = new System.Drawing.Point(20, 266);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(540, 20);
            this.lblEstado.TabIndex = 10;
            // 
            // cmbTurno
            // 
            this.cmbTurno.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.cmbTurno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTurno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTurno.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbTurno.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.cmbTurno.Location = new System.Drawing.Point(130, 76);
            this.cmbTurno.Name = "cmbTurno";
            this.cmbTurno.Size = new System.Drawing.Size(420, 23);
            this.cmbTurno.TabIndex = 2;
            this.cmbTurno.SelectedIndexChanged += new System.EventHandler(this.cmbTurno_SelectedIndexChanged);
            // 
            // cmbDoctor
            // 
            this.cmbDoctor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.cmbDoctor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDoctor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDoctor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbDoctor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.cmbDoctor.Location = new System.Drawing.Point(130, 116);
            this.cmbDoctor.Name = "cmbDoctor";
            this.cmbDoctor.Size = new System.Drawing.Size(260, 23);
            this.cmbDoctor.TabIndex = 4;
            this.cmbDoctor.SelectedIndexChanged += new System.EventHandler(this.cmbDoctor_SelectedIndexChanged);
            // 
            // txtDiagnostico
            // 
            this.txtDiagnostico.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.txtDiagnostico.BorderRadius = 6;
            this.txtDiagnostico.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDiagnostico.DefaultText = "";
            this.txtDiagnostico.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.txtDiagnostico.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDiagnostico.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtDiagnostico.Location = new System.Drawing.Point(130, 156);
            this.txtDiagnostico.MaxLength = 200;
            this.txtDiagnostico.Multiline = true;
            this.txtDiagnostico.Name = "txtDiagnostico";
            this.txtDiagnostico.PlaceholderText = "";
            this.txtDiagnostico.SelectedText = "";
            this.txtDiagnostico.Size = new System.Drawing.Size(420, 60);
            this.txtDiagnostico.TabIndex = 6;
            // 
            // chkHoraFin
            // 
            this.chkHoraFin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.chkHoraFin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkHoraFin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.chkHoraFin.Location = new System.Drawing.Point(140, 244);
            this.chkHoraFin.Name = "chkHoraFin";
            this.chkHoraFin.Size = new System.Drawing.Size(180, 20);
            this.chkHoraFin.TabIndex = 8;
            this.chkHoraFin.Text = "Registrar hora de fin";
            this.chkHoraFin.UseVisualStyleBackColor = false;
            this.chkHoraFin.CheckedChanged += new System.EventHandler(this.chkHoraFin_CheckedChanged);
            // 
            // dtpHoraFin
            // 
            this.dtpHoraFin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.dtpHoraFin.Enabled = false;
            this.dtpHoraFin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpHoraFin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.dtpHoraFin.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHoraFin.Location = new System.Drawing.Point(460, 240);
            this.dtpHoraFin.Name = "dtpHoraFin";
            this.dtpHoraFin.ShowUpDown = true;
            this.dtpHoraFin.Size = new System.Drawing.Size(120, 23);
            this.dtpHoraFin.TabIndex = 9;
            this.dtpHoraFin.ValueChanged += new System.EventHandler(this.dtpHoraFin_ValueChanged);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BorderRadius = 8;
            this.btnGuardar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(150, 337);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(160, 36);
            this.btnGuardar.TabIndex = 11;
            this.btnGuardar.Text = "Registrar Consulta";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnLimpiar.BorderRadius = 8;
            this.btnLimpiar.BorderThickness = 1;
            this.btnLimpiar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLimpiar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnLimpiar.Location = new System.Drawing.Point(516, 337);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(120, 36);
            this.btnLimpiar.TabIndex = 12;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnTurnos
            // 
            this.btnTurnos.BorderRadius = 8;
            this.btnTurnos.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(140)))), ((int)(((byte)(80)))));
            this.btnTurnos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTurnos.ForeColor = System.Drawing.Color.White;
            this.btnTurnos.Location = new System.Drawing.Point(763, 337);
            this.btnTurnos.Name = "btnTurnos";
            this.btnTurnos.Size = new System.Drawing.Size(130, 36);
            this.btnTurnos.TabIndex = 13;
            this.btnTurnos.Text = "← Ir a Turnos";
            this.btnTurnos.Click += new System.EventHandler(this.btnTurnos_Click);
            // 
            // FrmConsulta
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(1117, 552);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.lblTurno);
            this.Controls.Add(this.cmbTurno);
            this.Controls.Add(this.lblDoctor);
            this.Controls.Add(this.cmbDoctor);
            this.Controls.Add(this.lblDiagnostico);
            this.Controls.Add(this.txtDiagnostico);
            this.Controls.Add(this.lblHoraFin);
            this.Controls.Add(this.chkHoraFin);
            this.Controls.Add(this.dtpHoraFin);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnTurnos);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmConsulta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta — Hospital Gestión 2";
            this.Load += new System.EventHandler(this.FrmConsulta_Load);
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblTurno;
        private System.Windows.Forms.Label lblDoctor;
        private System.Windows.Forms.Label lblDiagnostico;
        private System.Windows.Forms.Label lblHoraFin;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.ComboBox cmbTurno;
        private System.Windows.Forms.ComboBox cmbDoctor;
        private System.Windows.Forms.CheckBox chkHoraFin;
        private System.Windows.Forms.DateTimePicker dtpHoraFin;
        private Guna2TextBox txtDiagnostico;
        private Guna2Button btnGuardar;
        private Guna2Button btnLimpiar;
        private Guna2Button btnTurnos;
        private Guna2Panel panelTop;
    }
}