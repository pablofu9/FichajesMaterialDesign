using FichajesMaterial.CRUD;
using FichajesMaterial.modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;
using Path = System.IO.Path;

namespace FichajesMaterial.vista
{
    /// <summary>
    /// Lógica de interacción para Ajustes.xaml
    /// </summary>
    public partial class Ajustes : Window
    {
        public Ajustes()
        {
            InitializeComponent();
            _reportViewer.Load += ReportViewer_load;
        }
        private bool _isReportViewerLoader;
        String archivoSettings = @"C:\DAM\INTERFACES\FichajesMaterial\FichajesMaterial\FichajesMaterial\settings\ajustes.txt";

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
        private void ReportViewer_load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoader)
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource = new Microsoft.Reporting.WinForms.ReportDataSource();
                fichajesDataSet fichajesDataSet = new fichajesDataSet();
                fichajesDataSet.BeginInit();
                reportDataSource.Name = "FichajesDataSet"; //Esta cadena debe ser el nombre de la clase
                reportDataSource.Value = fichajesDataSet.fichajes; //Nombre de la tabla que hayamos metido

                this._reportViewer.LocalReport.DataSources.Add(reportDataSource);
                this._reportViewer.LocalReport.ReportPath = "C:\\DAM\\INTERFACES\\FichajesMaterial\\FichajesMaterial\\FichajesMaterial\\reports\\Report1.rdlc";
                fichajesDataSet.EndInit();
                //Si no pongo la ruta absoluta no me funciona
                fichajesDataSetTableAdapters.fichajesTableAdapter fichajesTableAdapter = new fichajesDataSetTableAdapters.fichajesTableAdapter();
                fichajesTableAdapter.ClearBeforeFill = true;
                fichajesTableAdapter.Fill(fichajesDataSet.fichajes);
                _reportViewer.RefreshReport();
                _isReportViewerLoader = true;
            }

        }
        private List<users> verUsers()
        {
            List<users> users = new List<users>();
            users = CRUD_User.listarUsers();
            return users;
        }

        //Lo que vamos a hacer es conceder dias libres, si el usuario esta de vacaciones no se podra fichar ese usuario
        //Hasta que se cancelen las vacaciones mediante el textfield de abajo
        private void btnDias_Click(object sender, RoutedEventArgs e)
        {

            string path="C:\\DAM\\INTERFACES\\FichajesMaterial\\FichajesMaterial\\FichajesMaterial\\settings\\ajustes.txt";
            int value;
            Boolean userExiste=false;
            String texto = txtID.Text;
            Boolean coincide = false;
            List<users> lista = verUsers();
            //Comprobamos quel si el usuario esta en el archivo ya o no
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if(line == texto)
                    {
                        userExiste = true;
                    }
                }
            }
            
              
            //Si el textbox es numerico, se escribira
            if(int.TryParse(txtID.Text, out value)){
                foreach (users u in lista)
                {
                    if (u.Id_user.ToString().Equals(texto))
                    {
                        
                        coincide = true;
                        break;
                        //Entonces se podra insertar
                    }
                    
                    
                }
                if (coincide==true)
                {
                    if (userExiste)
                    {
                        
                        String[] lineas = File.ReadAllLines(path);
                        List<string> updatedLines = new List<string>();
                        for (int i = 0; i < lineas.Length; i++)
                        {
                            if (!lineas[i].Contains(texto)) // si la línea no contiene el texto a eliminar, agrega la línea actual a la lista actualizada
                            {
                                updatedLines.Add(lineas[i]);
                            }
                        }
                        File.WriteAllLines(path, updatedLines);
                        //Usuario ya esta, no se va a poder insertar
                        MessageBox.Show("El usuario "+texto+" deja de estar de vacaciones");
                    }
                    else
                    {
                        using (StreamWriter archivo = new StreamWriter(@"C:\DAM\INTERFACES\FichajesMaterial\FichajesMaterial\FichajesMaterial\settings\ajustes.txt", true))
                        {

                            archivo.WriteLine(
                                txtID.Text);

                        }
                        MessageBox.Show("El usuario " + texto + " ahora se encuentra de vacaciones");
                    }
                    //Introducimos el usuario que va a estar de vacaciones
                    
                    //Se puede introducir
                }
                else
                {
                    MessageBox.Show(texto);
                    //no se puede introducir
                    MessageBox.Show("El usuario no existe");
                }
                
            }
            else
            {
                MessageBox.Show("El id debe ser numerico");
            }
           
        }
    }
}
