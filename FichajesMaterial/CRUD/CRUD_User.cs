using FichajesMaterial.modelo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FichajesMaterial.CRUD
{
    class CRUD_User
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["FichajesMaterial.Properties.Settings.fichajesConnectionString"].ConnectionString;
        static ModelDatosDataContext datos = new ModelDatosDataContext(connectionString);

        public static void insertUser(users u1)
        {
            datos.users.InsertOnSubmit(u1);
            datos.SubmitChanges();
        }
        public static List<users> listarUsers()
        {
            List<users> users = datos.users.ToList();
            return users;
        }
        public static void insertFichaje(fichajes f, int idUser)
        {
            users u = datos.users.FirstOrDefault(es => es.Id_user == idUser);
            if (u == null)
            {
                MessageBox.Show("El id del usuario introducido no existe");
            }
            else
            {
                //Introducimos el fichaje, comprobando antes que el id de usuario existe
                datos.fichajes.InsertOnSubmit(f);
                datos.SubmitChanges();
                MessageBox.Show("Fichaje introducido con existo");
            }
            /*
            datos.fichajes.InsertOnSubmit(f);
            datos.SubmitChanges();*/
        }
        public static List<fichajes> listarFichajes()
        {
            List<fichajes> fichajes = datos.fichajes.ToList();
            return fichajes;
        }

        public static Boolean buscarUser(int idUser)
        {
            users u = datos.users.FirstOrDefault(es => es.Id_user == idUser);
            if(u == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /*Lo que este metodo va a hacer es:
         * Buscamos si el id del usuario ya esta en la tabla fichajes
         * Si no esta, introducimos uno nuevo
         * Si esta pero la fecha de hoy es distinta a la de la tabla, introducimos fichaje nuevo
         * Si no esta, debemos hacer un uptade de la hora de salida
         */
        public static void buscarFichaje(fichajes f1, int idUser)
        {
            
            var fechaHoy = DateTime.Today;
            var fechaBuscada = fechaHoy.Date;
            fichajes f = datos.fichajes.FirstOrDefault(es => es.Id_usuario == idUser && es.fecha.Date == fechaHoy);
            

            if (f == null)
            {

                insertFichaje(f1, idUser);
                //Usuario no encontrado, se procede a realizar el fichaje
            }
            else
            {
                //Comparamos las fechas

                var fechaEncontrada = f.fecha;
                int comparamos = DateTime.Compare(fechaEncontrada, fechaBuscada);
                //Cuando comparamos sea == 0 significa que las fechas coinciden, cualquier otro
                //Valor es que no coinciden
                if (comparamos != 0)
                {

                    insertFichaje(f1, idUser);
                    //Hay un fichaje de ese usuario pero es de otro dia, procedemos a fichar

                }
                else
                {
                    MessageBox.Show("Seteamos hora salida");
                    DateTime today = DateTime.Now;
                    var horaEntrada = today.ToLongTimeString();
                    TimeSpan hora = TimeSpan.Parse(horaEntrada);
                    MessageBox.Show(hora.ToString());
                    //Ahora debemos hacer un update de la fecha de salida
                    f.hora_salida = hora;

                    datos.SubmitChanges();
                }
            }
        }
        }
}
