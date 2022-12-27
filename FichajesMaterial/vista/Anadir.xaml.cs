using FichajesMaterial.CRUD;
using FichajesMaterial.modelo;
using FichajesMaterial.validaciones;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Lógica de interacción para Anadir.xaml
    /// </summary>
    public partial class Anadir : Window
    {
        ModelDatosDataContext datos;
        public Anadir()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["FichajesMaterial.Properties.Settings.fichajesConnectionString"].ConnectionString;
            datos = new ModelDatosDataContext(connectionString);
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void goFichar(object sender, RoutedEventArgs e)
        {
            Fichar f1 = new Fichar();
            f1.Show();
            this.Close();
        }

        private void goSettings(object sender, RoutedEventArgs e)
        {
            Ajustes a = new Ajustes();
            a.Show();
            this.Close();
        }

        private void goInfo(object sender, RoutedEventArgs e)
        {
            Info info = new Info();
            info.Show();
            this.Close();
        }

        private void goAddUser(object sender, RoutedEventArgs e)
        {
            Anadir anadir = new Anadir();
            anadir.Show();
            this.Close();
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAnadir_Click(object sender, RoutedEventArgs e)
        {
            users u1 = new users();
            //Comprobamos si los campos estan vacios antes de insertar
            if (string.IsNullOrEmpty(txtNombre.Text) ||
                string.IsNullOrEmpty(txtApellidos.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(txtTelefono.Text))
            {
                MessageBox.Show("Rellena todos los campos para insertar");

            }
            else if (!Validar.validateEmail(txtEmail.Text))
            {
                MessageBox.Show("El formato del email no es correcto");
            }
            else if (!Validar.validarTelefono(txtTelefono.Text))
            {
                MessageBox.Show("Introduce un numero de telefono correcto");
            }
            else
            {
                /**
                * Si los campos no estan vacios hacemos la inserción y borramos despues
                * SIempre comprobamos el formato del email y del telefono
                * El id no se mete porque es autoincrement
                */
                try
                {
                    u1.nombre = txtNombre.Text;
                    u1.apellidos = txtApellidos.Text;
                    u1.email = txtEmail.Text;
                    u1.telefono = txtTelefono.Text;
                    CRUD_User.insertUser(u1);
                    MessageBox.Show("Usuario insertado con exito ->id: " + u1.Id_user + " name : " + u1.nombre);
                }
                catch (Exception)
                {
                    MessageBox.Show("Algo ha salido mal");
                }

                txtNombre.Text = "";
                txtApellidos.Text = "";
                txtEmail.Text = "";
                txtTelefono.Text = "";
            }
        }
    }
}
