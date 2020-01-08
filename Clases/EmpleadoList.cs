using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Dynamic;

namespace AppProyecto
{
    class EmpleadoList
    {

        private List<   Empleado> empleado= new List<Empleado>();

        public void guardar(Empleado empe)
        {
            empleado.Add(new Empleado (empe.nombre,empe.cedula,empe.telefono,empe.edad,empe.puesto,empe.sexo,empe.sueldo,empe.seguro ) );
          
        }


        public void cargarTabla(DataGridView tabla) 
        {
            tabla.Rows.Clear();
            for (int i = 0; i < empleado.Count; i++)
            {   
                tabla.Rows.Add(empleado[i].nombre,empleado[i].cedula ,empleado[i].telefono,empleado[i].edad ,empleado[i].puesto , (empleado[i].sexo == 0 ? "Femenino" : "Masculino"), empleado [i].sueldo ,empleado[i].seguro );
            }

        }

        public void eliminar(string cedula)

        {
            for (int i = 0; i < empleado.Count; i++)
            {
                
                if (empleado[i].cedula.ToUpper() == cedula.ToUpper())
                {
                     empleado.RemoveAt(i);
                }
            }
           

        }

        public void modificar (string cedula , Empleado empe)
        {
            for (int i = 0; i < empleado.Count; i++)
            {

                if (empleado[i].cedula.ToUpper() == cedula.ToUpper())
                {

                    empleado[i] = new Empleado(empe.nombre, empe.cedula, empe.telefono, empe.edad, empe.puesto, empe.sexo, empe.sueldo, empe.seguro);
                break;
                }
            }

        }

        


        public void buscar(DataGridView tabla ,int indice,string informacion)  
        {

            tabla.Rows.Clear();
            for (int i = 0; i < empleado.Count; i++)
            {
                string cadena = empleado[i].nombre + ";" + empleado[i].cedula + ";" + empleado[i].telefono+";"+empleado[i].puesto ;
                string[] datos = cadena.Split(new char[] { ';' });
                if((datos [indice].ToUpper()).Contains(informacion.ToUpper()) )
                {
                    tabla.Rows.Add(empleado[i].nombre, empleado[i].cedula, empleado[i].telefono, empleado[i].edad, empleado[i].puesto, (empleado[i].sexo == 0 ? "Femenino" : "Masculino"), empleado[i].sueldo, empleado[i].seguro);
                }
             
            }
        }

    
          public Boolean  datosrepetidos(ErrorProvider err, GroupBox grp, string buscado)
        {
            string[] vector = buscado.Split(new char[] { ';' });
            Boolean salida = true;


            for (int i = 0; i < empleado.Count; i++)
            {
                string datos = empleado[i].nombre + ";" + empleado[i].cedula + ";" + empleado[i].telefono;
                string[] emp = datos.Split(new char[] { ';' });

                if (emp[0].ToUpper() == vector[0].ToUpper())
                {
                    foreach (Control c in grp.Controls)
                    {
                        if (c is TextBox && c.Text.ToUpper() == emp[0].ToUpper())
                        {
                            err.SetError(c, "Datos repetidos");
                           
                        }
                    }
                    salida = false;
                }
                if (emp[2].ToUpper() == vector[1].ToUpper())
                {
                    foreach (Control c in grp.Controls)
                    {
                        if (c is TextBox && c.Text.ToUpper() == emp[2].ToUpper())
                        {
                            err.SetError(c, "Datos repetidos");

                        }
                    }
                    salida = false;
                }
                if (emp[1].ToUpper() == vector[2].ToUpper())
                {
                    foreach (Control c in grp.Controls)
                    {
                        if (c is TextBox && c.Text.ToUpper() == emp[1].ToUpper())
                        {
                            err.SetError(c, "Datos repetidos");

                        }
                    }
                    salida = false;
                }

            }

            return salida ;
        }

         

          public Boolean datosrepetidosmodificar(ErrorProvider err, GroupBox grp,string buscado,string cedula)
          {
              string[] vector = buscado.Split(new char[] { ';' });
              Boolean salida = true;


              for (int i = 0; i < empleado.Count; i++)
              {
                 

                 if (empleado[i].cedula.ToUpper() != cedula.ToUpper() )
                 {
                     if (empleado[i].nombre.ToUpper() == vector[0].ToUpper())
                     {
                         foreach (Control c in grp.Controls)
                         {
                             if (c is TextBox && c.Text.ToUpper() == vector[0].ToUpper())
                             {
                                 err.SetError(c, "Datos repetidos");

                             }
                         }
                         salida = false;
                     }
                     if (empleado[i].telefono.ToUpper() == vector[1].ToUpper())
                     {
                         foreach (Control c in grp.Controls)
                         {
                             if (c is TextBox && c.Text.ToUpper() == vector[1].ToUpper())
                             {
                                 err.SetError(c, "Datos repetidos");

                             }
                         }
                         salida = false;
                     }
                     if (empleado[i].cedula .ToUpper() == vector[2].ToUpper())
                     {
                         foreach (Control c in grp.Controls)
                         {
                             if (c is TextBox && c.Text.ToUpper() == vector[2].ToUpper())
                             {
                                 err.SetError(c, "Datos repetidos");

                             }
                         }
                         salida = false;
                     }
                 }
              }

              return salida;
          }

    }

}
