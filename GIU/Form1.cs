using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics ;
using System.Collections ;
using System.Runtime;


namespace AppProyecto
{

    public partial class Form1 : Form
    {
        Empleado empleado = new Empleado();
        EmpleadoList list = new EmpleadoList();
        int c = 0;
        string indice;

     
        Controles control = new Controles();
        
        Button[] botones = new Button [5];
        string guardar = "";
       
        public Form1()
        {
            
            InitializeComponent();
            CenterToScreen();
            MaximizeBox = false ;
            MinimizeBox = false;
            cargarbotones();
            control.Botones(new Boolean[] { true, false, false, false, true }, botones);
            control.Groupbox (grpDatos, false);
            tooltip();
          
            cboPuesto.SelectedIndex = 0;
            control.limpiarcajas(grpDatos );
            control.limpiarcajas(grpDatos );

        }
        private void tooltip()
        {
            tollMensaje.SetToolTip(btnSalir  , "Salir Del Formulario");
            tollMensaje.SetToolTip(btnNuevo, "Ingresar un Nuevo Empleado");
            tollMensaje.SetToolTip(btnEliminar , "Eliminar Empleado");
            tollMensaje.SetToolTip(btnModificar, "Modificar Empleado");
            tollMensaje.SetToolTip(btnGuardar, "Guardar Empleado");
            tollMensaje.ToolTipTitle = "Mantenimiento de Empleado";
            tollMensaje.ToolTipIcon = ToolTipIcon.Info;
            tollMensaje.BackColor = Color.Tomato;
        }

        private void cargarbotones()
        {
            botones[0] = btnNuevo;
            botones[1] = btnGuardar;
            botones[2] = btnEliminar;
            botones[3] = btnModificar;
            botones[4] = btnSalir ;
        }
    private void btnNuevo_Click(object sender, EventArgs e)
        {
            guardar = "N";
            control.limpiarcajas(grpDatos);
            control.Botones(new Boolean[] { true, true, false, false, true }, botones);
            control.Groupbox (grpDatos, true );
        }

        private void TablaEmpleado_Click(object sender, EventArgs e)
        {
            
            control.Botones(new Boolean[] { true, false, true, true, true }, botones);
            control.Groupbox(grpDatos, false);
        }
        
        private void btnGuardar_Click(object sender, EventArgs e)
        {

              int i ;
              if (rdbFemenino.Checked)
              {
                  i = 0;}
              else {
                  i = 1;}


              if (MessageBox.Show("Desea Guardar los Datos", "Mantenimiento de Empleado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
              {
                  ErrorCaja.Clear();
                  if (control.ValidaEntrada(ErrorCaja, grpDatos) )
                  {
                      string validar = txtNombre.Text + ";" + txtTelefono.Text + ";" + txtCedula.Text;


                      if (guardar == "N" && list.datosrepetidos(ErrorCaja, grpDatos,validar))
                      {
                          empleado = new Empleado(txtNombre.Text, txtCedula.Text, txtTelefono.Text, Convert.ToInt32(numEdad.Value), cboPuesto.Text, i, Convert.ToDouble(txtSueldo.Text), chkEstado.Checked);
                          MessageBox.Show("Registro Grabado Correctamente", "Mantenimiento de Empleado", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                          list.guardar(empleado);
                          cargarcomponentes();
                         
                      }
                      else
                      {
                          if (guardar == "M" && list.datosrepetidosmodificar(ErrorCaja, grpDatos,validar, indice))
                          {
                              empleado = new Empleado(txtNombre.Text, txtCedula.Text, txtTelefono.Text, Convert.ToInt32(numEdad.Value), cboPuesto.Text, i, Convert.ToDouble(txtSueldo.Text), chkEstado.Checked);
                              list.modificar(indice ,empleado );
                              MessageBox.Show("Registro Modificado Correctamente", "Mantenimiento de Empleado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                              cargarcomponentes();

                          }
                          else
                          {
                              
                          MessageBox.Show("Datos Repetidos", "Mantenimiento de Empleado ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                          }
                      }
                     
                     
                  }
                  else
                  {
                      MessageBox.Show("Datos Incorrectos", "Mantenimiento de Empleado ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                      control.Botones(new Boolean[] {false, true, false , false, true} , botones);
                  }

              }
              else
              {
                  MessageBox.Show("Operacion Cancelada", "Mantenimiento de Empleado", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                  control.Groupbox(grpDatos, false );
                  control.Botones(new Boolean[] { true, false, false, false, true }, botones);
                  control.limpiarcajas(grpDatos);
                  ErrorCaja.Clear();

              }
                    
                

            
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int i = TablaEmpleado.CurrentRow.Index;
            string empleado = Convert.ToString(TablaEmpleado.Rows[i].Cells[1].Value);
            string nota = Convert.ToString(TablaEmpleado.Rows[i].Cells[0].Value);


            if (MessageBox.Show("Esta Seguro De Eliminar Al Empleado :" + "\n" + nota , "Mantenimiento de Empleado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                list.eliminar(empleado );
                MessageBox.Show("Registro Eliminado Correctamente", "Mantenimiento de Empleado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                control.limpiarcajas(grpDatos);
                list.cargarTabla(TablaEmpleado);
               

            }
            else
            {
                control.limpiarcajas(grpDatos);
                MessageBox.Show("Operacion Cancelada", "Mantenimiento de Empleado", MessageBoxButtons.OK, MessageBoxIcon.Hand);

            }
            control.Botones(new Boolean[] { true, false, false, false, true }, botones);
           
        }

        private void TablaEmpleado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ErrorCaja.Clear();
            guardar = "";
            control.Groupbox(grpDatos, false);
            control.limpiarcajas(grpDatos);

            try
            {
                int i = TablaEmpleado.CurrentRow.Index;

                if (Convert.ToString(TablaEmpleado.Rows[i].Cells[0].Value) != "")
                {

                    clik(i);
                   
                    control.Botones(new Boolean[] { true, false, true, true, true }, botones);
                }
            }
            catch 
            {}  
          
        }


        private void btnModificar_Click(object sender, EventArgs e)
        {
            int i = TablaEmpleado.CurrentRow.Index;
            indice = Convert.ToString(TablaEmpleado.Rows[i].Cells[1].Value);
            guardar = "M";
            control.Groupbox(grpDatos, true);
            control.Botones(new Boolean[] { false, true, true, true, true }, botones);

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            control.limpiarcajas(grpDatos);
            control.Groupbox(grpDatos, false );
            if (rdBuscaNombre.Checked)
            {
                list.buscar (TablaEmpleado, 0,txtBuscar.Text);
            }
            if (rdBuscaCedula.Checked)
            {
                list.buscar(TablaEmpleado, 1, txtBuscar.Text);
            }
            if (rdBuscaTelefono.Checked)
            {
                list.buscar(TablaEmpleado, 2, txtBuscar.Text);
            }
            if (rdBuscaPuesto.Checked)
            {
                list.buscar(TablaEmpleado, 3, txtBuscar.Text);
            }



        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
          
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
               
                e.Handled = true;
                return;
            }

           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit (e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {

                e.Handled = true;
                return;
            }

        }

        private void txtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {

                e.Handled = true;
                return;
            }
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetterOrDigit (e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {

                e.Handled = true;
                return;
            }
        }

        private void txtSueldo_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!txtSueldo.Text.Contains(","))
            {
                c = 0;
            }

            if (e.KeyChar == 44 && !txtSueldo.Text.Contains(","))
            {
                e.Handled = false;
            }
            else
            {
                if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)Keys.Back && !txtSueldo.Text.Contains(","))
                { e.Handled = true; }
                else
                {
                    if (txtSueldo.Text.Contains(","))
                    {
                        if (char.IsNumber(e.KeyChar) && e.KeyChar != (char)Keys.Back && c < 2)
                        {
                            c++;
                        }
                        else
                        {
                            if (e.KeyChar == (char)Keys.Back)
                            {
                                c--;
                            }
                            else { e.Handled = true; }
                        }
                    }
                }
            }
        }

        private void clik(int i )
        {

            txtNombre.Text = Convert.ToString(TablaEmpleado.Rows[i].Cells[0].Value);
            txtTelefono.Text = Convert.ToString(TablaEmpleado.Rows[i].Cells[2].Value);
            cboPuesto.Text = Convert.ToString(TablaEmpleado.Rows[i].Cells[4].Value);
            txtSueldo.Text = Convert.ToString(TablaEmpleado.Rows[i].Cells[6].Value);
            txtCedula.Text = Convert.ToString(TablaEmpleado.Rows[i].Cells[1].Value);
            numEdad.Value = Convert.ToInt32(TablaEmpleado.Rows[i].Cells[3].Value);
            chkEstado.Checked  = Convert.ToBoolean(TablaEmpleado.Rows[i].Cells[7].Value);
            if (Convert.ToString(TablaEmpleado.Rows[i].Cells[5].Value)=="Femenino")
            {
                rdbFemenino.Checked = true;
            }
            else
            {
                rdbMasculino.Checked = true;
            }
            
        }

        private void cargarcomponentes()
        {

            control.limpiarcajas(grpDatos);
            list.cargarTabla(TablaEmpleado);
            control.Groupbox(grpDatos, false);
            control.Botones(new Boolean[] { true, false , false, false, true }, botones);

        }
       
       
       
        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            txtNombre.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtNombre.Text );
            txtNombre.SelectionStart = txtNombre.Text.Length;
        }

        private void grpDatos_Enter(object sender, EventArgs e)
        {

        }

   
     
    }
}
