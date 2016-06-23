using eREST.BL.Empleado;
using eREST.BL.Persona;
using eREST.BO;
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

namespace eREST.ModalWindows.MaintenanceWindows
{
    /// <summary>
    /// Lógica de interacción para Empleado.xaml
    /// </summary>
    public partial class wmEmpleado : Window
    {
        #region Global Variables

        bool registrar = true;
        eREST_PERSONAS Persona = new eREST_PERSONAS();
        eREST_EMPLEADOS Empleado = new eREST_EMPLEADOS();
        eREST_TIPOEMPLEADOS TTEmpleado = new eREST_TIPOEMPLEADOS();
        MantenimientoPersona blaMant = new MantenimientoPersona();
        MantenimientoEmpleado blaEmp = new MantenimientoEmpleado();
        int error = 0;
        #endregion
        public wmEmpleado( eREST_spEmpleadosResult Pe)
        {

            InitializeComponent();
            Persona = null;
            TTEmpleado = null;
            Empleado = null;
            if(Pe!=null)
            {
                Persona = blaMant.ConsultarPersona(Pe.PER_PK_PERSONA);
                Empleado = blaEmp.ConsultarEmpleado(Pe.PER_PK_PERSONA);
                TTEmpleado = blaEmp.ListarTipoEmpleado(Empleado.EMP_FK_TIPOEMPLEADO);
            }
            cbEstadoCivil.Items.Add("Casado");
            cbEstadoCivil.Items.Add("Soltero");
            cbEstadoCivil.Items.Add("Viudo");
            cbEstadoCivil.Items.Add("Divorciado");
            cbEstadoCivil.Items.Add("Unión libre"); 
            cbGenero.Items.Add("Masculino");
            cbGenero.Items.Add("Femenino");
            List<eREST_TIPOEMPLEADOS> TTP = blaEmp.ListarTipoEmpleados(); 
            foreach(eREST_TIPOEMPLEADOS ttps in TTP){
                ListBoxItem ite = new ListBoxItem();
                ite.Content = ttps.TEM_NOMBRE;
                ite.DataContext = ttps;
                cbPuesto.Items.Add(ite);
            }

            Inicializar();
        }

        #region Private Functions
        private void Inicializar()
        {
            try
            {
                if (Persona == null)
                {
                    gridPersona.DataContext = new eREST_PERSONAS();
                    registrar = true;
                }
                else
                {
                    gridPersona.DataContext = Persona;
                    registrar = false;
                    if (Persona.PER_ESTADOCIVIL == 'C') cbEstadoCivil.SelectedItem = "Casado";
                    else if (Persona.PER_ESTADOCIVIL == 'S') cbEstadoCivil.SelectedItem = "Soltero";
                    else if (Persona.PER_ESTADOCIVIL == 'D') cbEstadoCivil.SelectedItem = "Divorciado";
                    else if (Persona.PER_ESTADOCIVIL == 'V') cbEstadoCivil.SelectedItem = "Viudo";
                    else if (Persona.PER_ESTADOCIVIL == 'L') cbEstadoCivil.SelectedItem = "Unión libre";
                    else Persona.PER_ESTADOCIVIL = null;

                    if (Persona.PER_GENERO == 'M')cbGenero.SelectedItem = "Masculino" ;
                    else if (Persona.PER_GENERO == 'F')cbGenero.SelectedItem = "Femenino" ;
                    else Persona.PER_GENERO = null;

                    cbPuesto.Text= TTEmpleado.TEM_NOMBRE;

                    if (Persona.PER_HDELINCUENCIA == 'S') chHistorialDelictivo.IsChecked = true;
                    else chHistorialDelictivo.IsChecked = false;

                    if (Persona.PER_NACIONALIDAD == 'C') chNacional.IsChecked = true;
                    else chNacional.IsChecked = false;

                }
            }
            catch
            {//(Exception ex)
                //ewMessage nwError = new ewMessage(ex.Message.ToString(), UserMessages.titlePermissions);
                //nwError.ShowDialog();
                //if (nwError.DialogResult == true)
                //    nwError.Close();
            }
        }

        #endregion

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                eREST_PERSONAS pPersona = new eREST_PERSONAS();
                eREST_EMPLEADOS pEmpleado = new eREST_EMPLEADOS();
                eREST_TIPOEMPLEADOS pTTEmpleado = new eREST_TIPOEMPLEADOS();
                pPersona = gridPersona.DataContext as eREST_PERSONAS;
                pTTEmpleado = ((ListBoxItem)cbPuesto.SelectedItem).DataContext as eREST_TIPOEMPLEADOS;
                pPersona.PER_ESTADO = 'A';

                if ((string)cbEstadoCivil.SelectedItem == "Casado") pPersona.PER_ESTADOCIVIL = 'C';
                else if ((string)cbEstadoCivil.SelectedItem == "Soltero") pPersona.PER_ESTADOCIVIL = 'S';
                else if ((string)cbEstadoCivil.SelectedItem == "Divorciado") pPersona.PER_ESTADOCIVIL = 'D';
                else if ((string)cbEstadoCivil.SelectedItem == "Viudo") pPersona.PER_ESTADOCIVIL = 'V';
                else if ((string)cbEstadoCivil.SelectedItem == "Unión libre") pPersona.PER_ESTADOCIVIL = 'L';
                else pPersona.PER_ESTADOCIVIL = null;

                if ((string)cbGenero.SelectedItem == "Masculino") pPersona.PER_GENERO = 'M';
                else if ((string)cbGenero.SelectedItem == "Femenino") pPersona.PER_GENERO = 'F';
                else pPersona.PER_GENERO = null;



                if (chHistorialDelictivo.IsChecked == true) pPersona.PER_HDELINCUENCIA = 'S';
                else pPersona.PER_HDELINCUENCIA = 'N';

                if (chNacional.IsChecked == true) pPersona.PER_NACIONALIDAD = 'C';
                else pPersona.PER_NACIONALIDAD = 'I'; 
                if (registrar)
                {
                    eREST_PERSONAS rePersona = new eREST_PERSONAS();
                    rePersona = blaMant.RegistrarPersona(pPersona);
                    Empleado.EMP_FECHAREGISTRO = DateTime.Now;
                    Empleado.EMP_FK_PERSONA = rePersona.PER_PK_PERSONA;
                    Empleado.EMP_FK_TIPOEMPLEADO = pTTEmpleado.TEM_PK_TIPOEMPLEADO;
                    blaEmp.RegistrarEmpleado(pEmpleado);
                    MessageBox.Show("Empleado registrado satisfactoriamente.", "eREST", MessageBoxButton.OK);
                }
                else
                {
                    pPersona.PER_PK_PERSONA = Persona.PER_PK_PERSONA;
                    blaMant.EditarPersona(pPersona);
                    Empleado.EMP_FK_PERSONA = Persona.PER_PK_PERSONA;
                    Empleado.EMP_FK_TIPOEMPLEADO = pTTEmpleado.TEM_PK_TIPOEMPLEADO;
                    blaEmp.EditarEmpleado(Empleado);
                    MessageBox.Show("Empleado Editado satisfactoriamente.", "eREST", MessageBoxButton.OK);
                }

                this.DialogResult = true;
            }
            catch
            {
                MessageBox.Show("No se ha podido registrar el empleado.", "eREST", MessageBoxButton.OK);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

    }
}
