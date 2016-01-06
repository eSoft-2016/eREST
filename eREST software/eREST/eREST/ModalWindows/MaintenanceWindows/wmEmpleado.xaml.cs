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

using eREST.BL.Empleado;
using eREST.BL.Persona;
using eREST.BO;

namespace eREST.ModalWindows.MaintenanceWindows
{
    /// <summary>
    /// Interaction logic for wmEmpleado.xaml
    /// </summary>
    public partial class wmEmpleado : Window
    {
        public wmEmpleado()
        {
            InitializeComponent();
            cbEstadoCivil.Items.Add("Casado");
            cbEstadoCivil.Items.Add("Soltero");
            cbEstadoCivil.Items.Add("Viudo");
            cbEstadoCivil.Items.Add("Divorciado");
            cbEstadoCivil.Items.Add("Unión libre");

            cbGenero.Items.Add("Masculino");
            cbGenero.Items.Add("Femenino");

            btnSave.IsEnabled = true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                eREST_PERSONAS nuevaPersona = new eREST_PERSONAS();

                nuevaPersona.PER_PNOMBRE = PNombreTextBox.Text;
                nuevaPersona.PER_SNOMBRE = SNombreTextBox.Text;
                nuevaPersona.PER_PAPELLIDO = PApellidoTextBox.Text;
                nuevaPersona.PER_SAPELLIDO = SApellidoTextBox.Text;

                nuevaPersona.PER_ESTADO = 'A';

                if ((string)cbEstadoCivil.SelectedItem == "Casado") nuevaPersona.PER_ESTADOCIVIL = 'C';
                else if ((string)cbEstadoCivil.SelectedItem == "Soltero") nuevaPersona.PER_ESTADOCIVIL = 'S';
                else if ((string)cbEstadoCivil.SelectedItem == "Divorciado") nuevaPersona.PER_ESTADOCIVIL = 'D';
                else if ((string)cbEstadoCivil.SelectedItem == "Viudo") nuevaPersona.PER_ESTADOCIVIL = 'V';
                else if ((string)cbEstadoCivil.SelectedItem == "Unión libre") nuevaPersona.PER_ESTADOCIVIL = 'L';
                else nuevaPersona.PER_ESTADOCIVIL = null;

                if ((string)cbGenero.SelectedItem == "Masculino") nuevaPersona.PER_GENERO = 'M';
                else if ((string)cbGenero.SelectedItem == "Femenino") nuevaPersona.PER_GENERO = 'F';
                else nuevaPersona.PER_GENERO = null;



                if (chHistorialDelictivo.IsChecked == true) nuevaPersona.PER_HDELINCUENCIA = 'S';
                else nuevaPersona.PER_HDELINCUENCIA = 'N';

                if (chNacional.IsChecked == true) nuevaPersona.PER_NACIONALIDAD = 'C';
                else nuevaPersona.PER_NACIONALIDAD = 'I';

                MantenimientoEmpleado nuevoEmpleado = new MantenimientoEmpleado();
                nuevoEmpleado.RegistrarEmpleado(nuevaPersona);

                MessageBox.Show("Empleado registrado satisfactoriamente.", "eREST", MessageBoxButton.OK);
            }
            catch
            {
                MessageBox.Show("No se ha podido registrar el empleado.", "eREST", MessageBoxButton.OK);
            }
        }
    }
}
