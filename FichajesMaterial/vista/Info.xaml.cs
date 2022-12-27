using FichajesMaterial.CRUD;
using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para Info.xaml
    /// </summary>
    public partial class Info : Window
    {
        public Info()
        {
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

        private void tablaFichajes_Loaded(object sender, RoutedEventArgs e)
        {
            tablaFichajes.DataContext = CRUD_User.listarFichajes();
        }

        private void tabla_Loaded(object sender, RoutedEventArgs e)
        {
            tabla.DataContext = CRUD_User.listarUsers();
        }
    }
}
