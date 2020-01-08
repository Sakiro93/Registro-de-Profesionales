using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;
using System.Runtime;
namespace AppProyecto
{
    class Controles
    {

        public void Botones(Boolean[] Opciones, Button[] Botones)
        {
            for (int i = 0; i < Botones.Length; i++)
            {
                Botones[i].Enabled = Opciones[i];
            }
        }



        public void Groupbox(GroupBox Grupo, Boolean opcion )
        {
            Grupo.Enabled = opcion;
            
        }

        public Boolean  ValidaEntrada(ErrorProvider err, GroupBox grp) 
        {
        Boolean er = true;
       
            foreach ( Control   c in grp.Controls )
            {
                if (c is TextBox )
                {
                    if (c.Text != "")
                    {
                        if (c.Name.Equals("txtCedula") || c.Name.Equals("txtTelefono"))
                        {
                            if (c.Text.Length != 10)
                            {
                                err.SetError(c, "Los datos deben tener 10 digitos");
                                er = false;
                            }
                            else
                            {
                                if (c.Name.Equals("txtCedula")&&  ! validarcedula(c.Text))
                                {
                                    err.SetError(c, "La cédula es incorrecta");
                                    er = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        err.SetError(c, "Ingrese Datos");
                        er = false;
                    }
                }
                else
                {
                    if (c is ComboBox && c.Text == "")
                    {
                        err.SetError(c, "Ingrese Datos");
                        er = false;
                    }

                }
            }
        return er;
        }

        public void limpiarcajas( GroupBox grp)
        {
            foreach (Control c in grp.Controls)
            {
                if (c is TextBox )
                {
                    c.Text = "";
                }
               
            }
        }

        public Boolean validarcedula(string cedula)
        {
            Boolean cedulaCorrecta=false;
            char[] vector = cedula.ToArray();
            int sumatotal = 0;
 
                for (int i = 0; i < vector.Length - 1; i++)
                {
                    int numero = Convert.ToInt32(vector[i].ToString());
                    if ((i + 1) % 2 == 1)
                    {
                        numero = Convert.ToInt32(vector[i].ToString()) * 2;
                        if (numero > 9)
                        {
                            numero = numero - 9;
                        }
                    }
                    sumatotal += numero;
                }
                sumatotal = 10 - (sumatotal % 10);
                if (sumatotal > 9)
                {
                    sumatotal = 0;
                }
                if (Convert.ToString(vector[9]+"") == Convert.ToString(sumatotal+""))
                {
                    cedulaCorrecta = true;
                }
            return cedulaCorrecta;
        }





    }
}
