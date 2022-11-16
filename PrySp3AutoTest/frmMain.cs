using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrySp3AutoTest
{
    public partial class frmMain : Form
    {
        
        public struct TURNO
        {
            public int NumeroTurno;
            public string Dominio;
            public int AnioFabricacion;
            public string Titular;
        };

        
        const int MAX = 50;
        public TURNO[] turnos;
        public int Cantidad = 0;
        public frmMain()
        {
            InitializeComponent();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmAutoTest_Load(object sender, EventArgs e)
        {
            
            turnos = new TURNO[MAX];
            Cantidad = 0;
            inicializarInterfaz();
        }

        private void inicializarInterfaz()
        {
            limpiarControles(); 
            txtCantidadTurnos.Clear();
            txtAnioMasAntiguo.Clear();
            txtDominio6Caracteres.Clear();
        }

        private void limpiarControles()
        {
            
            txtNroTurno.Clear();
            txtDominio.Clear();
            numAnioFabricacion.Value = 2022;
            txtTitular.Clear();
        }

        
        private void txtNroTurno_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!(char.IsDigit(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true; 
            }

        }


        
        private void txtDominio_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (Char.IsLower (e.KeyChar)) 
            {
                e.KeyChar = Char.ToUpper(e.KeyChar); 
            }
            
            if (!Char.IsLetterOrDigit (e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; 
            }
        }

        private bool validarDatos()
        {
            bool resultado = false; 
            if (txtNroTurno.Text != "" && txtDominio.Text != "" && txtTitular.Text != "")
            {
                if (txtDominio.Text.Length >= 6) 
                {
                    
                    if (!buscarTurno(int.Parse(txtNroTurno.Text)))
                    {
                        resultado = true; 
                    }
                    else
                    {
                        MessageBox.Show("El Número de Turno ingresado ya existe",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("El Dominio debe tener 6 o 7 caracteres",
                        "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }                
            }
            else
            {
                MessageBox.Show("Debe completar los datos faltantes",
                    "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return resultado;
        }

        
        private bool buscarTurno(int numero)
        {
            bool existe = false; 
            int pos = 0; 
            
            while(pos < Cantidad)
            {
                
                if (numero == turnos[pos].NumeroTurno)
                {
                    existe = true; 
                    break; 
                }
                pos++; 
            }
            return existe; 
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            
            if (validarDatos())
            {
                
                turnos[Cantidad].NumeroTurno = int.Parse(txtNroTurno.Text);
                turnos[Cantidad].Dominio = txtDominio.Text;
                turnos[Cantidad].AnioFabricacion = int.Parse(numAnioFabricacion.Value.ToString());
                turnos[Cantidad].Titular = txtTitular.Text;
                
                Cantidad++;
                
                if (Cantidad == MAX)
                {
                    MessageBox.Show("Se completó la capacidad de carga de datos",
                        "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    btnRegistrar.Enabled = false; 
                }
                limpiarControles(); 
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {

            
            txtCantidadTurnos.Text = Cantidad.ToString();
            int menor = int.MaxValue; 
            int pos;
            
            for(pos=0; pos<Cantidad; pos++)
            {
                
                if (turnos[pos].AnioFabricacion < menor)
                {
                    menor = turnos[pos].AnioFabricacion; 
                }
            }
            
            txtAnioMasAntiguo.Text = menor.ToString();
            int contador = 0;            
            for(pos = 0; pos<Cantidad; pos++)
            {
                
                if (turnos[pos].Dominio.Length == 6)
                {
                    contador++; 
                }
            }
            txtDominio6Caracteres.Text = contador.ToString();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
