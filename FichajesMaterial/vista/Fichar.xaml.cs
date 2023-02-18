using FichajesMaterial.CRUD;
using FichajesMaterial.modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FichajesMaterial.vista
{
    /// <summary>
    /// Lógica de interacción para Fichar.xaml
    /// </summary>
    public partial class Fichar : Window
    {
        public Fichar()
        {
            InitializeComponent();
        }

        private void TwitterButton_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Fichar fichar = new Fichar();
            fichar.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Ajustes a = new Ajustes();
            a.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Info info = new Info();
            info.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Anadir anadir = new Anadir();
            anadir.Show();
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnFichar_Click(object sender, RoutedEventArgs e)
        {
            //Si el usuario aparece en el archivo, es que esta de vacaciones, no se podra fichar con el 
            string path = "C:\\DAM\\INTERFACES\\FichajesMaterial\\FichajesMaterial\\FichajesMaterial\\settings\\ajustes.txt";
            if (File.ReadAllText(path).Contains(txtID.Text))
            {
                MessageBox.Show("Este usuario se encuentra de vacaciones");
            }
            else
            {
                DateTime today = DateTime.Now;

                var fechaHoy = today.ToShortDateString();
                DateTime fecha = DateTime.Parse(fechaHoy);

                var horaEntrada = today.ToLongTimeString();
                TimeSpan hora = TimeSpan.Parse(horaEntrada);

                //Si encuentra el id en la tabla users, va a generar un nuevo fichaje con ese id
                /**
                 * Ya hemos parseado el time y el date para poder introducirlo
                 * El formato sera de la siguiente manera
                 * Cuando introducimos un nuevo fichaje, la hora de salida sera 00:00:00
                 * Pero cuando encontramos el id del usuario en la tabla y la fecha coincide
                 * Lo que haremos sera hacer un update de esa row de la hora de salida
                 */

                //Introducimos un nuevo fichaje
                fichajes f = new fichajes();
                f.fecha = fecha;
                f.hora_entrada = hora;
                f.Id_usuario = Int32.Parse(txtID.Text);

                CRUD_User.buscarFichaje(f, Int32.Parse(txtID.Text));
            }
               
        }

        
    }
}
