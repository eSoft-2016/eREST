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
using System.Windows.Navigation;
using System.Windows.Shapes;

using eREST.ModalWindows.MaintenanceWindows;
using eREST.BO;
using eREST.BL.Persona;
using eREST.BL.Empleado;
namespace eREST.Pages
{
    /// <summary>
    /// Interaction logic for Empleados.xaml
    /// </summary>
    public partial class Empleados : Page
    {
        #region Global Variables

        bool registrar = true;
        eREST_PERSONAS Persona = new eREST_PERSONAS();
        eREST_spEmpleadosResult Empleado = new eREST_spEmpleadosResult();
        MantenimientoPersona ManPe = new MantenimientoPersona();
        MantenimientoEmpleado ManEm = new MantenimientoEmpleado();
        int error = 0;
        #endregion
        public Empleados()
        {
            InitializeComponent();
            ListarEmpleados();
        }

        private void ListarEmpleados()
        {
            try
            {
                btnEdit.IsEnabled = false;
                MantenimientoEmpleado ManEmp = new MantenimientoEmpleado();
                dgEmpleados.Items.Clear();
                List<eREST_spEmpleadosResult> Lista = new List<eREST_spEmpleadosResult>();
                Lista = ManEmp.ListarEmpleados();
                if (Lista != null)
                {
                    foreach (eREST_spEmpleadosResult sec in Lista)
                    {
                        dgEmpleados.Items.Add(sec);
                    }
                    
                }
                else
                {
                    //ewMessage nwError = new ewMessage(ErrorMessages.errorGeneralList, UserMessages.titlePermissions);
                    //nwError.ShowDialog();
                    //if (nwError.DialogResult == true)
                    //    nwError.Close();
                }
            }
            catch
            {//(Exception ex)
                //ewMessage nwError = new ewMessage(ErrorMessages.errorGeneralList + ex.Message.ToString(), UserMessages.titlePermissions);
                //nwError.ShowDialog();
                //if (nwError.DialogResult == true)
                //    nwError.Close();
            }
        }
        private void MostrarVentanaMat(eREST_spEmpleadosResult pEmpleado)
        {
            wmEmpleado ventis = new wmEmpleado(pEmpleado);
            ventis.Closed += new EventHandler(ventis_Closed); ;
            ventis.ShowDialog();
        }
        #region Eventos privados
        void ventis_Closed(object sender, EventArgs e)
        {
            if (((wmEmpleado)sender).DialogResult == true)
            {
                ListarEmpleados();
            }
        }

        private void btnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            ListarEmpleados();
            dgEmpleados.SelectedIndex = -1;
        }


        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            MostrarVentanaMat(null);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            MostrarVentanaMat(Empleado);
        }
        private void dgEmpleados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                Empleado = ((DataGrid)sender).SelectedItem as eREST_spEmpleadosResult;
                Persona = ManPe.ConsultarPersona(Empleado.PER_PK_PERSONA);
                if (Empleado != null)
                {
                    btnEdit.IsEnabled = true;
                }
                else
                {
                    btnEdit.IsEnabled = false;
                }
            }
            catch
            {//(Exception ex)
                //ewMessage nwError = new ewMessage(ErrorMessages.titlePermissions + ex.Message.ToString(), UserMessages.titlePermissions);
                //    nwError.ShowDialog();
                //    if (nwError.DialogResult == true)
                //        nwError.Close();
            }
        } 
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MantenimientoEmpleado blMant = new MantenimientoEmpleado();
            blMant.EliminarEmpleado(Empleado.PER_PK_PERSONA);
            ListarEmpleados();
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        { 
            wmDireccion ventis = new wmDireccion(Persona);
            ventis.ShowDialog();
        }

        

        #endregion

        
    }
}
